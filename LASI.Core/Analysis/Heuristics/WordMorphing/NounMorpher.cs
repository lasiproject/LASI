using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using static System.StringComparison;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    /// <summary>Performs both noun root extraction and noun form generation.</summary>
    public class NounMorpher : IWordMorpher<Noun>
    {
        static NounMorpher()
        {
            ExceptionMapping = Helper.ExcMapping;
        }

        /// <summary>Gets all forms of the noun root.</summary>
        /// <param name="nounText">The root of a noun as a string.</param>
        /// <returns>All forms of the noun root.</returns>
        public IEnumerable<string> GetLexicalForms(string nounText) => ComputeForms(nounText);

        /// <summary>Gets all forms of the noun.</summary>
        /// <param name="noun">The of a noun.</param>
        /// <returns>All forms of the noun.</returns>
        public IEnumerable<string> GetLexicalForms(Noun noun) => GetLexicalForms(noun.Text);

        /// <summary>
        /// Returns the root of the given noun string. If no root can be found, the noun string
        /// itself is returned.
        /// </summary>
        /// <param name="nounText">The noun string to find the root of.</param>
        /// <returns>
        /// The root of the given noun string. If no root can be found, the noun string itself is returned.
        /// </returns>
        public string FindRoot(string nounText) => CheckSpecialForms(nounText).FirstOrDefault() ?? ComputeBaseForm(nounText).FirstOrDefault() ?? nounText;

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
            var exceptions = ExceptionMapping.GetValueOrDefault(noun);
            if (exceptions != null)
            {
                foreach (var exception in exceptions) { yield return exception; }
                yield break;
            }

            var hyphenIndex = noun.LastIndexOf('-');

            var root = FindRoot(hyphenIndex > -1 ? noun.Substring(hyphenIndex + 1) : noun);
            var hyphenatadAppendage = hyphenIndex > -1 ? noun.Substring(0, hyphenIndex + 1) : string.Empty;
            IEnumerable<string> results;
            if (!ExceptionMapping.TryGetValue(root, out results))
            {
                foreach (var form in synthesize())
                {
                    yield return form;
                }
            }
            else
            {
                foreach (var form in from result in results select hyphenatadAppendage + result)
                    yield return form;
            }
            yield return hyphenatadAppendage + root;

            IEnumerable<string> synthesize()
            {
                for (var i = 0; i < sufficies.Length; i++)
                {
                    if (root.EndsWith(endings[i]) || endings[i].Length == 0)
                    {
                        yield return hyphenatadAppendage + root.Substring(0, root.Length - endings[i].Length) + sufficies[i];
                        yield break;
                    }
                }
            }
        }

        private IEnumerable<string> ComputeBaseForm(string noun)
        {
            var result = new List<string>();
            for (var i = 0; i < sufficies.Length; ++i)
            {
                if (noun.EndsWith(sufficies[i], CurrentCulture))
                {
                    result.Add(noun.Substring(0, noun.Length - sufficies[i].Length) + endings[i]);
                    break;
                }
            }
            return result;
        }

        private IEnumerable<string> CheckSpecialForms(string noun)
        {
            var hyphenIndex = noun.LastIndexOf('-');
            var match = hyphenIndex > -1 ? noun.Substring(hyphenIndex + 1) : noun;
            return from exception in ExceptionMapping
                   let keyHyphenated = exception.Key.Contains('-')
                   where exception.Key == match || exception.Value.Contains(match) || (keyHyphenated && exception.Key == noun)
                   select keyHyphenated ? exception.Key : hyphenIndex > -1 ? noun.Substring(0, hyphenIndex) + exception.Key : exception.Key;
        }

        private static readonly WordNetExceptionDataManager Helper = new WordNetExceptionDataManager("noun.exc");

        private static readonly IReadOnlyDictionary<string, IEnumerable<string>> ExceptionMapping;

        private static readonly string[] endings = { "", "s", "x", "z", "ch", "sh", "man", "y", };
        private static readonly string[] sufficies = { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };

    }
}
