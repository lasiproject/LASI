using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    /// <summary>
    /// Performs both Adverb root extraction and Adverb form generation.
    /// </summary>
    public class AdverbMorpher : IWordMorpher<Adverb>
    {
        /// <summary>
        /// Returns the root of the given adverb string. If the adverb cannot be reduced to a root,
        /// the string itself is returned.
        /// </summary>
        /// <param name="adverb">The adverb string to find the root of.</param>
        /// <returns>
        /// The root of the given adverb string. If the adverb cannot be reduced to a root, the
        /// string itself is returned.
        /// </returns>
        public string FindRoot(string adverb) => CheckSpecialForms(adverb).FirstOrDefault() ?? ComputeBaseForm(adverb).FirstOrDefault() ?? adverb;

        /// <summary>
        /// Returns the root of the given Adverb. If the adverb cannot be reduced to a root, the
        /// adverb's textual representation is returned.
        /// </summary>
        /// <param name="adverb">The Adverb string to find the root of.</param>
        /// <returns>
        /// The root of the given adverb string. If no root can be found, the adverb's original text
        /// is returned.
        /// </returns>
        public string FindRoot(Adverb adverb) => FindRoot(adverb.Text);

        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverbText">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(string adverbText) => TryComputeConjugations(adverbText);

        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverb">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(Adverb adverb) => GetLexicalForms(adverb.Text);

        private IEnumerable<string> CheckSpecialForms(string search) => from nounExceptKVs in ExceptionMapping
                                                                        where nounExceptKVs.Value.Contains(search)
                                                                        select nounExceptKVs.Key;

        private IEnumerable<string> ComputeBaseForm(string adverbText)
        {
            for (var i = sufficies.Length - 1; i >= 0; --i)
            {
                if (adverbText.EndsWith(sufficies[i]))
                {
                    yield return adverbText.Substring(0, adverbText.Length - sufficies[i].Length) + endings[i];
                    break;
                }
            }
        }

        private IEnumerable<string> TryComputeConjugations(string containingRoot)
        {
            var hyphenIndex = containingRoot.IndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? containingRoot.Substring(0, hyphenIndex) : containingRoot);
            List<string> results;
            if (!ExceptionMapping.TryGetValue(root, out results))
            {
                for (var i = 0; i < sufficies.Length; ++i)
                {
                    if (root.EndsWith(endings[i]) || string.IsNullOrEmpty(endings[i]))
                    {
                        yield return root.Substring(0, root.Length - endings[i].Length) + sufficies[i];
                    }
                }
            }
            foreach (var result in results.EmptyIfNull())
            {
                yield return result;
            }
            yield return root;
        }

        #region Exception File Processing

        static AdverbMorpher()
        {
            ExceptionMapping = Helper.ExcMapping;
        }

        private static IList<ExceptionEntry> ProcessLine(string exceptionLine)
        {
            var lineEntries = exceptionLine.SplitRemoveEmpty(' ').ToList();
            return from exc in lineEntries
                   select new ExceptionEntry(exc, lineEntries);
        }

        private static readonly string[] endings = { "", "ly", "est" };
        private static readonly IReadOnlyDictionary<string, List<string>> ExceptionMapping;
        private static readonly ExcDataManager Helper = new ExcDataManager("adv.exc");
        private static readonly string[] sufficies = { "", "", "" };

        #endregion Exception File Processing
    }
}