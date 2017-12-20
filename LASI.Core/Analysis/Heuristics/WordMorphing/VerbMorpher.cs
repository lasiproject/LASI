using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    /// <summary>Performs both verb root extraction and verb conjugation generation.</summary>
    public static class VerbMorpher
    {

        /// <summary>Gets all forms of the verb root.</summary>
        /// <param name="verb">The root of a verb as a string.</param>
        /// <returns>All forms of the verb root.</returns>
        public static IEnumerable<string> GetConjugations(string verb)
        {
            var hyphenIndex = verb.IndexOf('-');

            var beforeHyphen = hyphenIndex > -1 ? verb.Substring(0, hyphenIndex) : verb;
            var afterHyphen = hyphenIndex > -1 ? verb.Substring(hyphenIndex) : string.Empty;
            var root = FindRoots(beforeHyphen).FirstOrDefault() ?? beforeHyphen;
            var results = GetWithSpecialForms(root);
            if (!results.Any())
            {
                results = from ending in SufficesByEnding.Keys
                          where ending.Length == 0 || beforeHyphen.EndsWithInsensitive(ending)
                          from suffix in SufficesByEnding[ending]
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
        public static IEnumerable<string> FindRoots(string verb)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var result = GetRootIfSpecialForm(verb) ?? ComputeRoot(verb);
#pragma warning restore CS0618 // Type or member is obsolete
            yield return result ?? verb;
        }

        [Obsolete("Figure out wtf I was on.", error: false)]
        static string ComputeRoot(string verb)
        {
            var hyphenIndex = verb.IndexOf('-');
            var afterHyphen = hyphenIndex > -1 ? verb.Substring(hyphenIndex) : string.Empty;
            var results = new List<string>();
            for (var i = Endings.Length - 1; i >= 0; --i)
            {
                if (verb.EndsWithInsensitive(Sufficies[i]))
                {
                    checked
                    {
                        try
                        {
                            var possibleRoot = verb.Substring(0, verb.Length - Sufficies[i].Length);

                            if (Endings[i].IsNullOrWhiteSpace() || possibleRoot.EndsWithInsensitive(Endings[i]))
                            {
                                return possibleRoot + afterHyphen;
                            }
                        }
                        catch (StackOverflowException e)
                        {
                            Logger.Log(e);
                        }
                    }
                }
            }
            return verb;
        }

        static string GetRootIfSpecialForm(string verb) => ExceptionMapping.FirstOrDefault(kv => kv.Value.Contains(verb) || kv.Key == verb).Key;

        static IEnumerable<string> GetWithSpecialForms(string verb)
        {
            foreach (var kv in ExceptionMapping)
            {
                if (kv.Value.Contains(verb))
                {
                    yield return kv.Key;
                }
            }
        }

        #region Exception File Processing

        static VerbMorpher() => ExceptionMapping = ExcManager.ExcMapping;

        static readonly string[] Endings = { "", "y", "e", "", " e", "", "e", "" };

        static readonly string[] Sufficies = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };

        static readonly IDictionary<string, IEnumerable<string>> SufficesByEnding = new Dictionary<string, IEnumerable<string>>
        {
            [""] = new[] { "s", "es", "ed", "ing" },
            ["e"] = new[] { "es", "ed", "ing" },
            ["y"] = new[] { "ies" },
        };

        static readonly WordNetExceptionDataManager ExcManager = new WordNetExceptionDataManager("verb.exc");

        static IReadOnlyDictionary<string, IEnumerable<string>> ExceptionMapping;

        #endregion Exception File Processing
    }
}
