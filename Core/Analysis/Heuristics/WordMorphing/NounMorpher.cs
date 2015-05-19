using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using LASI.Core.Heuristics;
using LASI.Core.Configuration;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    /// <summary>Performs both noun root extraction and noun form generation.</summary>
    public class NounMorpher : IWordMorpher<Noun>
    {
        static NounMorpher()
        {
            ExceptionMapping = Helper.ExcMapping;
            //exceptionMapping = File.ReadAllLines(ExceptionsFilePath)
            //    .Select(ProcessLine)
            //    .GroupBy(entry => entry.Key, entry => entry.Value)
            //    .DistinctBy(group => group.Key)
            //    .ToDictionary(group => group.Key, group => group.SelectMany(values => values).ToList());
        }

        /// <summary>Gets all forms of the noun root.</summary>
        /// <param name="noun">The root of a noun as a string.</param>
        /// <returns>All forms of the noun root.</returns>
        public IEnumerable<string> GetLexicalForms(string noun) => ComputeForms(noun);

        /// <summary>Gets all forms of the noun.</summary>
        /// <param name="noun">The of a noun.</param>
        /// <returns>All forms of the noun.</returns>
        public IEnumerable<string> GetLexicalForms(Noun noun) => GetLexicalForms(noun.Text);

        /// <summary>
        /// Returns the root of the given noun string. If no root can be found, the noun string
        /// itself is returned.
        /// </summary>
        /// <param name="noun">The noun string to find the root of.</param>
        /// <returns>
        /// The root of the given noun string. If no root can be found, the noun string itself is returned.
        /// </returns>
        public string FindRoot(string noun) => CheckSpecialForms(noun).FirstOrDefault() ?? ComputeBaseForm(noun).FirstOrDefault() ?? noun;

        /// <summary>
        /// Returns the root of the given Noun. If no root can be found, the Noun's original text is returned.
        /// </summary>
        /// <param name="noun">The Noun to find the root of.</param>
        /// <returns>
        /// The root of the given Noun. If no root can be found, the Noun's original text is returned.
        /// </returns>
        public string FindRoot(Noun noun) => FindRoot(noun.Text);

        private IEnumerable<string> ComputeForms(string noun)
        {
            var hyphenIndex = noun.LastIndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? noun.Substring(0, hyphenIndex) : noun);
            var afterHyphen = noun.Substring(hyphenIndex + 1);
            List<string> results;
            if (!ExceptionMapping.TryGetValue(root, out results))
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

        private IEnumerable<string> ComputeBaseForm(string noun)
        {
            var result = new List<string>();
            for (var i = 0; i < sufficies.Length; ++i)
            {
                if (noun.EndsWith(sufficies[i]))
                {
                    result.Add(noun.Substring(0, noun.Length - sufficies[i].Length) + endings[i]);
                    break;
                }
            }
            return result;
        }

        private IEnumerable<string> CheckSpecialForms(string noun) => from nounExceptKVs in ExceptionMapping
                                                                      where nounExceptKVs.Value.Contains(noun)
                                                                      select nounExceptKVs.Key;

        private static readonly ExcDataManager Helper = new ExcDataManager("noun.exc");

        #region Exception File Processing

        private static ExceptionEntry ProcessLine(string exceptionLine)
        {
            var exceptionData = exceptionLine.SplitRemoveEmpty('\r', '\n').Select(s => s.Trim().Replace('_', '-')).ToList();
            return new ExceptionEntry(
                key: exceptionData[exceptionData.Count - 1],
                value: exceptionData
            );
        }


        private static readonly IReadOnlyDictionary<string, List<string>> ExceptionMapping;

        private static readonly string[] endings = { "", "s", "x", "z", "ch", "sh", "man", "y", };
        private static readonly string[] sufficies = { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };

        #endregion Exception File Processing
    }
}