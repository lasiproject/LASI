using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.Linq.List;

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
        public IEnumerable<string> GetLexicalForms(string adverbText)
        {
            return TryComputeConjugations(adverbText);
        }
        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverb">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(Adverb adverb)
        {
            return GetLexicalForms(adverb.Text);
        }
        private IEnumerable<string> TryComputeConjugations(string containingRoot)
        {
            var hyphenIndex = containingRoot.IndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? containingRoot.Substring(0, hyphenIndex) : containingRoot);
            List<string> results;
            if (!exceptionData.TryGetValue(root, out results))
            {
                results = new List<string>();
                for (var i = 0; i < sufficies.Length; ++i)
                {
                    if (root.EndsWith(endings[i]) || string.IsNullOrEmpty(endings[i]))
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
        /// Returns the root of the given adverb string. If the adverb cannot be reduced to a root, the string itself is returned.
        /// </summary>
        /// <param name="adverbText">The adverb string to find the root of.</param>
        /// <returns>The root of the given adverb string. If the adverb cannot be reduced to a root, the string itself is returned.</returns>
        public string FindRoot(string adverbText)
        {
            return CheckSpecialForms(adverbText).FirstOrDefault() ?? ComputeBaseForm(adverbText).FirstOrDefault() ?? adverbText;

        }
        /// <summary>
        /// Returns the root of the given Adverb. If the adverb cannot be reduced to a root, the adverb's textual representation is returned.
        /// </summary>
        /// <param name="adverb">The Adverb string to find the root of.</param>
        /// <returns>The root of the given adverb string. If no root can be found, the adverb's original text is returned.</returns>
        public string FindRoot(Adverb adverb)
        {
            return FindRoot(adverb.Text);

        }

        private IEnumerable<string> ComputeBaseForm(string adverbText)
        {
            var result = new List<string>();
            for (var i = 0;
            i < sufficies.Length;
            i++)
            {
                if (adverbText.EndsWith(sufficies[i]))
                {
                    result.Add(adverbText.Substring(0, adverbText.Length - sufficies[i].Length) + endings[i]);
                    break;
                }
            }
            return result;
        }


        private IEnumerable<string> CheckSpecialForms(string search)
        {
            return from nounExceptKVs in exceptionData
                   where nounExceptKVs.Value.Contains(search)
                   select nounExceptKVs.Key;
        }




        #region Exception File Processing
        private static void LoadExceptionFile()
        {
            using (var reader = new StreamReader(ExceptionsFilePath))
            {
                while (!reader.EndOfStream)
                {
                    ProcessLine(reader.ReadLine()).ForEach(entry => exceptionData[entry.Key] = entry.Value);
                }
            }
        }

        static AdverbMorpher()
        {
            LoadExceptionFile();
        }

        private static List<ExceptionEntry> ProcessLine(string exceptionLine)
        {
            var lineEntries = exceptionLine.SplitRemoveEmpty(' ').ToList();
            return from exc in lineEntries
                   select new ExceptionEntry(exc, lineEntries);
        }
        private static readonly ConcurrentDictionary<string, List<string>> exceptionData = new ConcurrentDictionary<string, List<string>>(Reporting.Concurrency.Max, 2055);

        private static readonly string[] endings = { "", "s", "x", "z", "ch", "sh", "man", "y", };
        private static readonly string[] sufficies = { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };
        private static IConfig Config => Lexicon.InjectedConfiguration;

        static string ResourcesDirectory =>
                   Lexicon.InjectedConfiguration != null ?
                   Lexicon.InjectedConfiguration["ResourcesDirectory"] :
                   ConfigurationManager.AppSettings["ResourcesDirectory"];
        static string WordnetDataDirectory =>
            ResourcesDirectory + (Lexicon.InjectedConfiguration != null ?
                Lexicon.InjectedConfiguration["WordnetFileDirectory"] :
                ConfigurationManager.AppSettings["WordnetFileDirectory"]);

        static string ExceptionsFilePath => WordnetDataDirectory + "adv.exc";
        #endregion



    }
}
