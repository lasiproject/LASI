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
using LASI.Core.Analysis.LookupAndComparison.Syntactic.Support;

namespace LASI.Core.Heuristics.Morphemization
{
    /// <summary>
    /// Performs both noun root extraction and noun form generation.
    /// </summary>
    public class NounMorpher : IWordMorpher<Noun>
    {

        static NounMorpher() {
            LoadExceptionFile();

        }

        /// <summary>
        /// Gets all forms of the noun root.
        /// </summary>
        /// <param name="nounForm">The root of a noun as a string.</param>
        /// <returns>All forms of the noun root.</returns>
        public IEnumerable<string> GetLexicalForms(string nounForm) {
            return TryComputeConjugations(nounForm);

        }
        public IEnumerable<string> GetLexicalForms(Noun search) {
            return GetLexicalForms(search.Text);
        }

        private IEnumerable<string> TryComputeConjugations(string nounForm) {
            var hyphenIndex = nounForm.LastIndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? nounForm.Substring(0, hyphenIndex) : nounForm);
            List<string> results;
            if (!exceptionData.TryGetValue(root, out results)) {
                results = new List<string>();
                for (var i = 0; i < SUFFICIES.Length; i++) {
                    if (root.EndsWith(ENDINGS[i]) || ENDINGS[i].Length == 0) {
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
        /// <param name="nounForm">The noun string to find the root of.</param>
        /// <returns>The root of the given noun string. If no root can be found, the noun string itself is returned.</returns>
        public string FindRoot(string nounForm) {
            return CheckSpecialForms(nounForm).FirstOrDefault() ?? ComputeBaseForm(nounForm).FirstOrDefault() ?? nounForm;

        }
        /// <summary>
        /// Returns the root of the given Noun. If no root can be found, the Noun's orignal text is returned.
        /// </summary>
        /// <param name="noun">The Noun to find the root of.</param>
        /// <returns>The root of the given Noun. If no root can be found, the Noun's orignal text is returned.</returns>
        public string FindRoot(Noun noun) { return FindRoot(noun.Text); }

        private IEnumerable<string> ComputeBaseForm(string nounForm) {
            var result = new List<string>();
            for (var i = 0; i < SUFFICIES.Length; i++) {
                if (nounForm.EndsWith(SUFFICIES[i])) {
                    result.Add(nounForm.Substring(0, nounForm.Length - SUFFICIES[i].Length) + ENDINGS[i]);
                    break;
                }
            }
            return result;
        }


        private IEnumerable<string> CheckSpecialForms(string nounForm) {
            return from nounExceptKVs in exceptionData
                   where nounExceptKVs.Value.Contains(nounForm)
                   select nounExceptKVs.Key;
        }




        #region Exception File Processing
        private static void LoadExceptionFile() {
            using (var reader = new StreamReader(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc")) {
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
