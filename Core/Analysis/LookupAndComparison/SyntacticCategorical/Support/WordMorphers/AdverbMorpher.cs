using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Performs both Adverb root extraction and Adverb form generation.
    /// </summary>
    public class AdverbMorpher : IWordMorpher<Adverb>
    {
        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverbText">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(string adverbText) {
            return TryComputeConjugations(adverbText);
        }
        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverb">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(Adverb adverb) {
            return GetLexicalForms(adverb.Text);
        }
        private IEnumerable<string> TryComputeConjugations(string containingRoot) {
            var hyphenIndex = containingRoot.IndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? containingRoot.Substring(0, hyphenIndex) : containingRoot);
            List<string> results;
            if (!exceptionData.TryGetValue(root, out results)) {
                results = new List<string>();
                for (var i = 0;
                i < SUFFICIES.Length;
                i++) {
                    if (root.EndsWith(ENDINGS[i]) || string.IsNullOrEmpty(ENDINGS[i])) {
                        results.Add(root.Substring(0, root.Length - ENDINGS[i].Length) + SUFFICIES[i]);
                        break;
                    }
                }

            }
            results.Add(root);
            return results;
        }


        /// <summary>
        /// Returns the root of the given adverb string. If the adverb cannot be reduced to a root, the string itself is returned.
        /// </summary>
        /// <param name="adverbText">The adverb string to find the root of.</param>
        /// <returns>The root of the given adverb string. If the adverb cannot be reduced to a root, the string itself is returned.</returns>
        public string FindRoot(string adverbText) {
            return CheckSpecialForms(adverbText).FirstOrDefault() ?? ComputeBaseForm(adverbText).FirstOrDefault() ?? adverbText;

        }
        /// <summary>
        /// Returns the root of the given Adverb. If the adverb cannot be reduced to a root, the adverb's textual representation is returned.
        /// </summary>
        /// <param name="adverb">The Adverb string to find the root of.</param>
        /// <returns>The root of the given adverb string. If no root can be found, the adverb's oirignal text is returned.</returns>
        public string FindRoot(Adverb adverb) {
            return FindRoot(adverb.Text);

        }

        private IEnumerable<string> ComputeBaseForm(string adverbText) {
            var result = new List<string>();
            for (var i = 0;
            i < SUFFICIES.Length;
            i++) {
                if (adverbText.EndsWith(SUFFICIES[i])) {
                    result.Add(adverbText.Substring(0, adverbText.Length - SUFFICIES[i].Length) + ENDINGS[i]);
                    break;
                }
            }
            return result;
        }


        private IEnumerable<string> CheckSpecialForms(string search) {
            return from nounExceptKVs in exceptionData
                   where nounExceptKVs.Value.Contains(search)
                   select nounExceptKVs.Key;
        }




        #region Exception File Processing
        private static void LoadExceptionFile() {
            using (var reader = new StreamReader(exceptionsFilePath)) {
                while (!reader.EndOfStream) {
                    var keyVal = ProcessLine(reader.ReadLine());
                    exceptionData[keyVal.Key] = keyVal.Value;
                }
            }
        }

        static AdverbMorpher() {
            LoadExceptionFile();
        }

        private static KeyValuePair<string, List<string>> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.SplitRemoveEmpty(' ');
            return KeyValuePair.Create(kvstr.Last(), kvstr.Take(kvstr.Count() - 1).ToList());
        }
        private static readonly ConcurrentDictionary<string, List<string>> exceptionData = new ConcurrentDictionary<string, List<string>>(Interop.Concurrency.Max, 2055);

        private static readonly string[] ENDINGS = new[] { "", "s", "x", "z", "ch", "sh", "man", "y", };
        private static readonly string[] SUFFICIES = new[] { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };

        static readonly string resourcesDirectory = ConfigurationManager.AppSettings["ResourcesDirectory"];
        static readonly string wordnetDataDirectory = resourcesDirectory + ConfigurationManager.AppSettings["WordnetFileDirectory"];
        private static string exceptionsFilePath = wordnetDataDirectory + "adv.exc";
        #endregion



    }
}
