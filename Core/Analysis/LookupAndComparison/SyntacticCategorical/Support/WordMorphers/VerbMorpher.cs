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

namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Performs both verb root extraction and verb conjugation generation.
    /// </summary>
    public static class VerbMorpher
    {
        /// <summary>
        /// Gets all forms of the verb root.
        /// </summary>
        /// <param name="verbForm">The root of a verb as a string.</param>
        /// <returns>All forms of the verb root.</returns>
        public static IEnumerable<string> GetConjugations(string verbForm) {

            var hyphenIndex = verbForm.IndexOf('-');

            var root = hyphenIndex > -1 ? verbForm.Substring(0, hyphenIndex) : verbForm;
            var afterHyphen = hyphenIndex > -1 ? verbForm.Substring(hyphenIndex) : string.Empty;

            IEnumerable<string> results;
            if (!exceptionMapping.TryGetValue(root, out results)) {
                results = from ending in sufficesByEnding.Keys
                          where ending.Length == 0 || root.EndsWith(ending, StringComparison.OrdinalIgnoreCase)
                          from suffix in sufficesByEnding[ending]
                          select root.Substring(0, root.Length - ending.Length) + suffix + afterHyphen;
            }
            return results.Append(verbForm).Distinct();
        }
        /// <summary>
        /// Returns the root of the given verb string. If no root can be found, the verb string itself is returned.
        /// </summary>
        /// <param name="verbForm">The verb string to find the root of.</param>
        /// <returns>The root of the given verb string. If no root can be found, the verb string itself is returned.</returns>
        public static IEnumerable<string> FindRoots(string verbForm) {
            var result = CheckSpecialForms(verbForm);
            return result.Any() ? result.Distinct() : BuildLexicalForms(verbForm).DefaultIfEmpty(verbForm);
        }

        private static IEnumerable<string> BuildLexicalForms(string verbForm) {
            var hyphenIndex = verbForm.IndexOf('-');
            var afterHyphen = hyphenIndex > -1 ? verbForm.Substring(hyphenIndex) : string.Empty;
            var results = new List<string>();
            for (var i = ENDINGS.Length - 1; i >= 0; --i) {
                if (verbForm.EndsWith(SUFFICIES[i], StringComparison.OrdinalIgnoreCase)) {
                    checked
                    {
                        try {
                            var possibleRoot = verbForm.Substring(0, verbForm.Length - (SUFFICIES[i].Length));

                            if (string.IsNullOrEmpty(ENDINGS[i]) || (possibleRoot).EndsWith(ENDINGS[i])) {
                                results.Add(possibleRoot);
                                return results.Select(result => result + afterHyphen);
                            }
                        }
                        catch (StackOverflowException) { }
                    }
                }
            }
            return results.Select(r => r + afterHyphen).DefaultIfEmpty(verbForm);
        }

        private static IEnumerable<string> CheckSpecialForms(string verbForm) {
            return from e in exceptionMapping
                   where e.Value.Contains(verbForm)
                   from exception in e.Value
                   select exception;
        }


        #region Exception File Processing 


        private static readonly string[] ENDINGS = { "", "y", "e", "", " e", "", "e", "" };

        private static readonly string[] SUFFICIES = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };

        private static readonly IDictionary<string, IEnumerable<string>> sufficesByEnding = new Dictionary<string, IEnumerable<string>> {
            { "", new[] { "s",  "es",  "ed", "ing" } },
            { "e", new[] { "es", "ed", "ing"} },
            { "y", new[] { "ies" } },
        };

        private static ConcurrentDictionary<string, IEnumerable<string>> exceptionMapping;

        private static void LoadExceptionFile() {
            using (var reader = new StreamReader(exceptionsFilePath)) {
                var verbExceptionFileLines = from line in reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                                             select line.SplitRemoveEmpty(' ').Select(r => r.Replace('_', '-'));

                exceptionMapping = new ConcurrentDictionary<string, IEnumerable<string>>(
                    from exceptionSet in verbExceptionFileLines
                    from exception in exceptionSet
                    group exceptionSet by exception into g
                    select KeyValuePair.Create(g.Key, g.SelectMany(exceptionSet => exceptionSet)));
            }
        }

        static VerbMorpher() {
            LoadExceptionFile();
        }
        static readonly string resourcesDirectory = ConfigurationManager.AppSettings["ResourcesDirectory"];
        static readonly string wordnetDataDirectory = resourcesDirectory + ConfigurationManager.AppSettings["WordnetFileDirectory"];
        private static readonly string exceptionsFilePath = wordnetDataDirectory + "verb.exc";
        #endregion
    }

}
