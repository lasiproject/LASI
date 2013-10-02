using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Algorithm.LexicalLookup.Morphemization
{
    /// <summary>
    /// Performs both noun root extraction and noun form generation.
    /// </summary>
    public static class NounMorpher
    {

        static NounMorpher() {
            LoadExceptionFile(exceptionFilePath);

        }

        /// <summary>
        /// Gets all forms of the noun root.
        /// </summary>
        /// <param name="nounText">The root of a noun as a string.</param>
        /// <returns>All forms of the noun root.</returns>
        public static IEnumerable<string> GetLexicalForms(string nounText) {
            return TryComputeConjugations(nounText);

        }

        private static IEnumerable<string> TryComputeConjugations(string containingRoot) {
            var hyphenIndex = containingRoot.IndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? containingRoot.Substring(0, hyphenIndex) : containingRoot);
            List<string> results;
            if (!exceptionData.TryGetValue(root, out results)) {
                results = new List<string>();
                for (var i = 0; i < SUFFICIES.Length; i++) {
                    if (root.EndsWith(ENDINGS[i]) || ENDINGS[i] == "") {
                        results.Add(root.Substring(0, root.Length - ENDINGS[i].Length) + SUFFICIES[i]);
                        break;
                    }
                }

            }
            results.Add(root);
            return results;
        }


        /// <summary>
        /// Returns the root of the given noun string. If no root can be found, the noun string itself is returned.
        /// </summary>
        /// <param name="nounText">The noun string to find the root of.</param>
        /// <returns>The root of the given noun string. If no root can be found, the noun string itself is returned.</returns>
        public static string FindRoot(string nounText) {
            return CheckSpecialForms(nounText).FirstOrDefault() ?? ComputeBaseForm(nounText).FirstOrDefault() ?? nounText;

        }
        /// <summary>
        /// Returns the root of the given Noun. If no root can be found, the Noun's orignal text is returned.
        /// </summary>
        /// <param name="noun">The Noun to find the root of.</param>
        /// <returns>The root of the given Noun. If no root can be found, the Noun's orignal text is returned.</returns>
        public static string FindRoot(Noun noun) { return FindRoot(noun.Text); }

        private static IEnumerable<string> ComputeBaseForm(string NounText) {
            var result = new List<string>();
            for (var i = 0; i < SUFFICIES.Length; i++) {
                if (NounText.EndsWith(SUFFICIES[i])) {
                    result.Add(NounText.Substring(0, NounText.Length - SUFFICIES[i].Length) + ENDINGS[i]);
                    break;
                }
            }
            return result;
        }


        private static IEnumerable<string> CheckSpecialForms(string search) {
            return from nounExceptKVs in exceptionData
                   where nounExceptKVs.Value.Contains(search)
                   select nounExceptKVs.Key;
        }




        #region Exception File Processing
        private static string exceptionFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc";
        private static void LoadExceptionFile(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                while (!reader.EndOfStream) {
                    var keyVal = ProcessLine(reader.ReadLine());
                    exceptionData[keyVal.Key] = keyVal.Value;
                }
            }
        }

        private static KeyValuePair<string, List<string>> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new KeyValuePair<string, List<string>>(kvstr.Last(), kvstr.Take(kvstr.Count() - 1).ToList());
        }
        private static readonly ConcurrentDictionary<string, List<string>> exceptionData = new ConcurrentDictionary<string, List<string>>(Concurrency.Max, 2055);

        private static readonly string[] ENDINGS = new[] { "", "s", "x", "z", "ch", "sh", "man", "y", };
        private static readonly string[] SUFFICIES = new[] { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };
        #endregion

    }
}
