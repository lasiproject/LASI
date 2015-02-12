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
        public static IEnumerable<string> GetConjugations(string verbForm)
        {

            var hyphenIndex = verbForm.IndexOf('-');

            var beforeHyphen = hyphenIndex > -1 ? verbForm.Substring(0, hyphenIndex) : verbForm;
            var afterHyphen = hyphenIndex > -1 ? verbForm.Substring(hyphenIndex) : string.Empty;
            var root = FindRoots(beforeHyphen).FirstOrDefault() ?? beforeHyphen;
            IEnumerable<string> results = GetWithSpecialForms(root);
            if (results.None())
            {
                results = from ending in sufficesByEnding.Keys
                          where ending.Length == 0 || beforeHyphen.EndsWith(ending, StringComparison.OrdinalIgnoreCase)
                          from suffix in sufficesByEnding[ending]
                          select beforeHyphen.Substring(0, beforeHyphen.Length - ending.Length) + suffix + afterHyphen;
            }
            return results.Append(beforeHyphen).Distinct();
        }

        /// <summary>
        /// Returns the root of the given verb string. If no root can be found, the verb string itself is returned.
        /// </summary>
        /// <param name="verb">The verb string to find the root of.</param>
        /// <returns>The root of the given verb string. If no root can be found, the verb string itself is returned.</returns>
        public static IEnumerable<string> FindRoots(string verb)
        {
            var result = GetRootIfSpecialForm(verb) ?? ComputeRoot(verb);
            yield return result ?? verb;
        }

        private static string ComputeRoot(string verb)
        {
            var hyphenIndex = verb.IndexOf('-');
            var afterHyphen = hyphenIndex > -1 ? verb.Substring(hyphenIndex) : string.Empty;
            var results = new List<string>();
            for (var i = endings.Length - 1; i >= 0; --i)
            {
                if (verb.EndsWith(sufficies[i], StringComparison.OrdinalIgnoreCase))
                {
                    checked
                    {
                        try
                        {
                            var possibleRoot = verb.Substring(0, verb.Length - sufficies[i].Length);

                            if (endings[i].IsNullOrWhiteSpace() || possibleRoot.EndsWith(endings[i]))
                            {
                                return possibleRoot + afterHyphen;
                            }
                        }
                        catch (StackOverflowException e) { Output.WriteLine(e); }
                    }
                }
            }
            return verb;
        }

        private static string GetRootIfSpecialForm(string verb)
        {
            return exceptionMapping.FirstOrDefault(kv => kv.Value.Contains(verb) || kv.Key == verb).Key;
        }

        private static IEnumerable<string> GetWithSpecialForms(string verb)
        {
            foreach (var kv in exceptionMapping)
            {
                if (kv.Value.Contains(verb))
                {
                    yield return kv.Key;
                }
            }
        }

        #region Exception File Processing 


        private static readonly string[] endings = { "", "y", "e", "", " e", "", "e", "" };

        private static readonly string[] sufficies = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };

        private static readonly IDictionary<string, IEnumerable<string>> sufficesByEnding = new Dictionary<string, IEnumerable<string>>
        {
            [""] = new[] { "s", "es", "ed", "ing" },
            ["e"] = new[] { "es", "ed", "ing" },
            ["y"] = new[] { "ies" },
        };

        private static ConcurrentDictionary<string, IEnumerable<string>> exceptionMapping;

        private static void LoadExceptionFile()
        {
            using (var reader = new StreamReader(ExceptionsFilePath))
            {
                var verbExceptionFileLines = from line in reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                                             select line.SplitRemoveEmpty(' ').Select(r => r.Replace('_', '-'));
                exceptionMapping = new ConcurrentDictionary<string, IEnumerable<string>>(
                    from exceptionSet in verbExceptionFileLines
                    from exception in exceptionSet
                    group exceptionSet by exception into g
                    select KeyValuePair.Create(g.Key, g.SelectMany(exceptionSet => exceptionSet)));
            }
        }

        static VerbMorpher()
        {
            LoadExceptionFile();
        }
        private static LASI.Utilities.IConfig Config => Lexicon.InjectedConfiguration;

        static string ResourcesDirectory =>
            Config != null ?
            Config["ResourcesDirectory"] :
            ConfigurationManager.AppSettings["ResourcesDirectory"];
        static string WordnetDataDirectory =>
            ResourcesDirectory + (Config != null ? Config["WordnetFileDirectory"] :
            ConfigurationManager.AppSettings["WordnetFileDirectory"]);

        static string ExceptionsFilePath => WordnetDataDirectory + "verb.exc";
        #endregion
    }

}
