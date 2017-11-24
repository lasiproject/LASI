using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using static System.StringComparison;

namespace LASI.Core.Analysis.Heuristics.WordMorphing {
    /// <summary>Performs both verb root extraction and verb conjugation generation.</summary>
    public static class VerbMorpher {
        /// <summary>Gets all forms of the verb root.</summary>
        /// <param name="verb">The root of a verb as a string.</param>
        /// <returns>All forms of the verb root.</returns>
        public static IEnumerable<string> GetConjugations(string verb) {
            var hyphenIndex = verb.IndexOf('-');

            var beforeHyphen = hyphenIndex > -1 ? verb.Substring(0, hyphenIndex) : verb;
            var afterHyphen = hyphenIndex > -1 ? verb.Substring(hyphenIndex) : string.Empty;
            var root = FindRoots(beforeHyphen).FirstOrDefault() ?? beforeHyphen;
            var results = GetWithSpecialForms(root);
            if (!results.Any()) {
                results = from ending in sufficesByEnding.Keys
                          where ending.Length == 0 || beforeHyphen.EndsWith(ending, OrdinalIgnoreCase)
                          from suffix in sufficesByEnding[ending]
                          select beforeHyphen.Substring(0, beforeHyphen.Length - ending.Length) + suffix + afterHyphen;
            }
            return results.Append(beforeHyphen).Distinct();
        }

        /// <summary>
        /// Returns the root of the given verb string. If no root can be found, the verb string
        /// itself is returned.
        /// </summary>
        /// <param name="verb">The verb string to find the root of.</param>
        /// <returns>
        /// The root of the given verb string. If no root can be found, the verb string itself is returned.
        /// </returns>
        public static IEnumerable<string> FindRoots(string verb) {
            var result = GetRootIfSpecialForm(verb) ?? ComputeRoot(verb);
            yield return result ?? verb;
        }

        private static string ComputeRoot(string verb) {
            var hyphenIndex = verb.IndexOf('-');
            var afterHyphen = hyphenIndex > -1 ? verb.Substring(hyphenIndex) : string.Empty;
            var results = new List<string>();
            for (var i = endings.Length - 1; i >= 0; --i) {
                if (verb.EndsWith(sufficies[i], OrdinalIgnoreCase)) {
                    checked {
                        try {
                            var possibleRoot = verb.Substring(0, verb.Length - sufficies[i].Length);

                            if (endings[i].IsNullOrWhiteSpace() || possibleRoot.EndsWith(endings[i], CurrentCulture)) {
                                return possibleRoot + afterHyphen;
                            }
                        }
                        catch (StackOverflowException e) { Logger.Log(e); }
                    }
                }
            }
            return verb;
        }

        private static string GetRootIfSpecialForm(string verb) => exceptionMapping.FirstOrDefault(kv => kv.Value.Contains(verb) || kv.Key == verb).Key;

        private static IEnumerable<string> GetWithSpecialForms(string verb) {
            foreach (var kv in exceptionMapping) {
                if (kv.Value.Contains(verb)) {
                    yield return kv.Key;
                }
            }
        }

        #region Exception File Processing

        static VerbMorpher() {
            exceptionMapping = ExcManager.ExcMapping;
        }

        private static readonly string[] endings = { "", "y", "e", "", " e", "", "e", "" };

        private static readonly string[] sufficies = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };

        private static readonly IDictionary<string, IEnumerable<string>> sufficesByEnding = new Dictionary<string, IEnumerable<string>> {
            [""] = new[] { "s", "es", "ed", "ing" },
            ["e"] = new[] { "es", "ed", "ing" },
            ["y"] = new[] { "ies" },
        };

        private static readonly WordNetExceptionDataManager ExcManager = new WordNetExceptionDataManager("verb.exc");
        private static IReadOnlyDictionary<string, List<string>> exceptionMapping;

        #endregion Exception File Processing
    }
}
