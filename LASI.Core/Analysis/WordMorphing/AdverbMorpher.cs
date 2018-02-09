using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Heuristics.Heuristics.WordMorphing
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
        public string FindRoot(string adverb) => CheckSpecialForms(adverb).FirstOrDefault() ?? ComputeBaseForm(adverb) ?? adverb;

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

        public string GetAdjectivalForm(Adverb adverb) => GetAdjectivalForm(adverb.Text);

        public string GetAdjectivalForm(string adverbText)
        {
            var root = FindRoot(adverbText);
            for (var i = suffices.Length - 1; i <= 0; --i)
            {
                if (root.EndsWith(endings[i]))
                {
                    return root.Substring(0, root.Length - endings[i].Length) + suffices[i];
                }
            }
            return adverbText;
        }

        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverbText">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(string adverbText) => ComputeForms(adverbText);

        /// <summary>
        /// Gets all forms of the Adverb root.
        /// </summary>
        /// <param name="adverb">The root of a Adverb as a string.</param>
        /// <returns>All forms of the Adverb root.</returns>
        public IEnumerable<string> GetLexicalForms(Adverb adverb) => GetLexicalForms(adverb.Text);

        static IEnumerable<string> CheckSpecialForms(string search) =>
                                   from nounExceptKVs in ExceptionMapping
                                   where nounExceptKVs.Value.Contains(search)
                                   select nounExceptKVs.Key;

        string ComputeBaseForm(string adverbText)
        {
            for (var i = 0; i < suffices.Length; ++i)
            {
                if (adverbText.EndsWith(endings[i]))
                {
                    return adverbText.Substring(0, adverbText.Length - endings[i].Length);
                }
            }
            return adverbText;
        }

        IEnumerable<string> ComputeForms(string containingRoot)
        {
            var hyphenIndex = containingRoot.IndexOf('-');
            var root = FindRoot(hyphenIndex > -1 ? containingRoot.Substring(0, hyphenIndex) : containingRoot);
            var hyphenatedAppendage = hyphenIndex > -1 ? root.Substring(hyphenIndex) : "";
            IEnumerable<string> exceptionalForms;
            if (!ExceptionMapping.TryGetValue(root, out exceptionalForms))
            {
                foreach (var (suffix, ending) in suffices.Zip(endings))
                {
                    if (root.EndsWith(suffix, ordinalIgnoreCase) || string.IsNullOrEmpty(ending))
                    {
                        if (root.EndsWith("y", ordinalIgnoreCase))
                        {
                            root = root.Substring(0, root.Length - 1) + 'i';
                        }
                        yield return root + ending + hyphenatedAppendage;
                    }
                }
                yield break;
            }
        }

        const StringComparison ordinalIgnoreCase = StringComparison.OrdinalIgnoreCase;

        #region Exception File Processing

        static readonly string[] endings = { "ly" };

        static readonly string[] suffices = { "" };

        static readonly WordNetExceptionDataManager Helper = new WordNetExceptionDataManager("adv.exc");
        static readonly IVariantDictionary<string, IEnumerable<string>> ExceptionMapping = Helper.ExcMapping.ToVariantDictionary(x => x.Key, x => x.Value);
        #endregion Exception File Processing
    }
}
