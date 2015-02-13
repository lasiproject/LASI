using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using LASI.Core.Reporting;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.Linq.List;

namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Performs both noun root extraction and noun form generation.
    /// </summary>
    public class NounMorpher : IWordMorpher<Noun>
    {

        static NounMorpher()
        {
            exceptionData = File.ReadAllLines(ExceptionsFilePath)
                .Select(ProcessLine)
                .ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Gets all forms of the noun root.
        /// </summary>
        /// <param name="nounForm">The root of a noun as a string.</param>
        /// <returns>All forms of the noun root.</returns>
        public IEnumerable<string> GetLexicalForms(string nounForm)
        {
            return TryComputeConjugations(nounForm);
        }
        /// <summary>
        /// Gets all forms of the noun.
        /// </summary>
        /// <param name="noun">The of a noun.</param>
        /// <returns>All forms of the noun.</returns>
        public IEnumerable<string> GetLexicalForms(Noun noun)
        {
            return GetLexicalForms(noun.Text);
        }

        private IEnumerable<string> TryComputeConjugations(string nounForm)
        {
            var hyphenIndex = nounForm.LastIndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? nounForm.Substring(0, hyphenIndex) : nounForm);
            List<string> results;
            if (!exceptionData.TryGetValue(root, out results))
            {
                results = new List<string>();
                for (var i = 0; i < sufficies.Length; i++)
                {
                    if (root.EndsWith(endings[i]) || endings[i].Length == 0)
                    {
                        results.Add(root.Substring(0, root.Length - endings[i].Length) + sufficies[i]);
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
        public string FindRoot(string nounForm)
        {
            return CheckSpecialForms(nounForm).FirstOrDefault() ?? ComputeBaseForm(nounForm).FirstOrDefault() ?? nounForm;

        }
        /// <summary>
        /// Returns the root of the given Noun. If no root can be found, the Noun's orignal text is returned.
        /// </summary>
        /// <param name="noun">The Noun to find the root of.</param>
        /// <returns>The root of the given Noun. If no root can be found, the Noun's orignal text is returned.</returns>
        public string FindRoot(Noun noun) { return FindRoot(noun.Text); }

        private IEnumerable<string> ComputeBaseForm(string nounForm)
        {
            var result = new List<string>();
            for (var i = 0; i < sufficies.Length; ++i)
            {
                if (nounForm.EndsWith(sufficies[i]))
                {
                    result.Add(nounForm.Substring(0, nounForm.Length - sufficies[i].Length) + endings[i]);
                    break;
                }
            }
            return result;
        }


        private IEnumerable<string> CheckSpecialForms(string nounForm)
        {
            return from nounExceptKVs in exceptionData
                   where nounExceptKVs.Value.Contains(nounForm)
                   select nounExceptKVs.Key;
        }




        #region Exception File Processing

        private static ExceptionEntry ProcessLine(string exceptionLine)
        {
            var exceptionData = exceptionLine.SplitRemoveEmpty(' ').ToList();
            return new ExceptionEntry(
                key: exceptionData.Last(),
                value: exceptionData
            );
        }
        private static readonly IReadOnlyDictionary<string, List<string>> exceptionData = new Dictionary<string, List<string>>();

        private static readonly string[] endings = { "", "s", "x", "z", "ch", "sh", "man", "y", };
        private static readonly string[] sufficies = { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };

        static string ResourcesDirectory =>
             Lexicon.InjectedConfiguration != null ?
             Lexicon.InjectedConfiguration["ResourcesDirectory"] :
             ConfigurationManager.AppSettings["ResourcesDirectory"];
        static string WordnetDataDirectory =>
            ResourcesDirectory + (Lexicon.InjectedConfiguration != null ?
                Lexicon.InjectedConfiguration["WordnetFileDirectory"] :
                ConfigurationManager.AppSettings["WordnetFileDirectory"]);

        static string ExceptionsFilePath => WordnetDataDirectory + "noun.exc";
        #endregion



    }
}
