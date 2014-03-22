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

namespace LASI.Core.Heuristics.Morphemization
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

            var hyphIndex = verbForm.IndexOf('-');

            var root = hyphIndex > -1 ? verbForm.Substring(0, hyphIndex) : verbForm;
            var afterHyphen = hyphIndex > -1 ? verbForm.Substring(hyphIndex) : string.Empty;

            IEnumerable<string> results;
            if (!exceptionData.TryGetValue(root, out results)) {
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
            var hyphIndex = verbForm.IndexOf('-');
            var afterHyphen = hyphIndex > -1 ? verbForm.Substring(hyphIndex) : string.Empty;
            var results = new List<string>();
            for (var i = ENDINGS.Count - 1; i >= 0; --i) {
                if (verbForm.EndsWith(SUFFICIES[i], StringComparison.OrdinalIgnoreCase)) {
                    checked {
                        try {
                            var possibleRoot = verbForm.Substring(0, verbForm.Length - (SUFFICIES[i].Length + afterHyphen.Length));

                            if (string.IsNullOrEmpty(ENDINGS[i]) || (possibleRoot).EndsWith(ENDINGS[i])) {
                                results.Add(possibleRoot);
                                return results.Select(r => r + afterHyphen);
                            }
                        }
                        catch (StackOverflowException) { }
                    }
                }
            }
            return results.Select(r => r + afterHyphen).DefaultIfEmpty(verbForm);
        }

        private static IEnumerable<string> CheckSpecialForms(string verbForm) {
            return from verbExceptKVs in exceptionData
                   where verbExceptKVs.Value.Contains(verbForm)
                   from v in verbExceptKVs.Value
                   select v;
        }


        #region Exception File Processing




        private static void LoadExceptionFile() {
            using (var reader = new StreamReader(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc")) {
                var exceptions = from line in reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                                 select line.SplitRemoveEmpty(' ').Select(r => r.Replace('_', '-'));

                exceptionData = new ConcurrentDictionary<string, IEnumerable<string>>(
                    from items in exceptions
                    from i in items
                    select new
                    {
                        i,
                        items
                    } into kvp
                    group kvp by kvp.i into g
                    select new KeyValuePair<string, IEnumerable<string>>(g.Key, g.SelectMany(e => e.items)));

            }
        }
        private readonly static IList<string> ENDINGS = new[] { "", "y", "e", "", " e", "", "e", "" }.ToList();
        private readonly static IList<string> SUFFICIES = new[] { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" }.ToList();
        private static readonly IDictionary<string, IEnumerable<string>> sufficesByEnding = new Dictionary<string, IEnumerable<string>> {
            { "", new []{ "s",  "es",  "ed", "ing" } },
            { "e", new []{ "es", "ed", "ing"} },    
            { "y", new []{ "ies" } },
        };

        private static ConcurrentDictionary<string, IEnumerable<string>> exceptionData;

        static VerbMorpher() { LoadExceptionFile(); }

        #endregion
    }

}
