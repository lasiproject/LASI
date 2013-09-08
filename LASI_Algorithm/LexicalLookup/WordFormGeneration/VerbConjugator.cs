using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Lookup.Morphemization
{
    /// <summary>
    /// Performs both verb root extraction and verb conjugation generation.
    /// </summary>
    public static class VerbConjugator
    {
        /// <summary>
        /// Gets all forms of the verb root.
        /// </summary>
        /// <param name="containingRoot">The root of a verb as a string.</param>
        /// <returns>All forms of the verb root.</returns>
        public static IEnumerable<string> GetConjugations(string containingRoot) {

            var hyphIndex = containingRoot.IndexOf('-');

            var root = hyphIndex > -1 ? containingRoot.Substring(0, hyphIndex) : containingRoot;
            var afterHyphen = hyphIndex > -1 ? containingRoot.Substring(hyphIndex) : string.Empty;

            IEnumerable<string> results;
            if (!exceptionData.TryGetValue(root, out results)) {
                results = from ending in sufficesByEnding.Keys
                          where ending == "" || root.EndsWith(ending, StringComparison.OrdinalIgnoreCase)
                          from suffix in sufficesByEnding[ending]
                          select root.Substring(0, root.Length - ending.Length) + suffix + afterHyphen;
            }
            return results.Append(containingRoot).Distinct();
        }
        /// <summary>
        /// Returns the root of the given verb string. If no root can be found, the verb string itself is returned.
        /// </summary>
        /// <param name="search">The verb string to find the root of.</param>
        /// <returns>The root of the given verb string. If no root can be found, the verb string itself is returned.</returns>
        public static IEnumerable<string> FindRoots(string search) {
            var result = CheckSpecialForms(search);
            return result.Any() ? result.Distinct() : BuildLexicalForms(search).DefaultIfEmpty(search);
        }

        private static IEnumerable<string> BuildLexicalForms(string root) {
            var hyphIndex = root.IndexOf('-');
            var afterHyphen = hyphIndex > -1 ? root.Substring(hyphIndex) : string.Empty;
            var results = new List<string>();
            for (var i = ENDINGS.Length - 1; i >= 0; --i) {
                if (root.EndsWith(SUFFICIES[i], StringComparison.OrdinalIgnoreCase)) {
                    var possibleRoot = root.Substring(0, root.Length - SUFFICIES[i].Length);
                    if (string.IsNullOrEmpty(ENDINGS[i]) || (possibleRoot).EndsWith(ENDINGS[i])) {
                        results.Add(possibleRoot);
                        return results.Select(r => r + afterHyphen);
                    }
                }
            }
            return results.Select(r => r + afterHyphen).DefaultIfEmpty(root);
        }

        private static IEnumerable<string> CheckSpecialForms(string checkFor) {
            return from verbExceptKVs in exceptionData
                   where verbExceptKVs.Value.Contains(checkFor)
                   from v in verbExceptKVs.Value
                   select v;
        }


        #region Exception File Processing


        private static string exceptionFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";


        private readonly static string[] ENDINGS = { "", "y", "e", "", " e", "", "e", "" };
        private readonly static string[] SUFFICIES = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };
        private static readonly IDictionary<string, IEnumerable<string>> sufficesByEnding = new Dictionary<string, IEnumerable<string>> {
            { "", new []{ "s",  "es",  "ed", "ing" } },
            { "e", new []{ "es", "ed", "ing"} },    
            { "y", new []{ "ies" } },
        };

        private static void LoadExceptionFile(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                var exceptions = from line in reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 select line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Replace('_', '-'));

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
        private static ConcurrentDictionary<string, IEnumerable<string>> exceptionData;

        static VerbConjugator() { LoadExceptionFile(exceptionFilePath); }

        #endregion
    }

}
