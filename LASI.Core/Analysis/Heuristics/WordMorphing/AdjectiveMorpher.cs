using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    public class AdjectiveMorpher : IWordMorpher<Adjective>
    {
        /// <summary>
        /// Returns the base form of the specified <see cref="Adjective" />. If the word is already
        /// in its base form, the text content of the adjective will simply be returned.
        /// </summary>
        /// <param name="adjective">
        /// An <see cref="Adjective" /> from whose text a root is to be extracted.
        /// </param>
        /// <returns>
        /// The base form of the given adjective. If the word is already in its base form, the text
        /// content of the adjective will simply be returned.
        /// </returns>
        public string FindRoot(Adjective adjective) => FindRoot(adjective.Text);

        /// <summary>
        /// Returns the base form of the specified adjective. If the word is already in its base
        /// form, the text content of the adjective will simply be returned.
        /// </summary>
        /// <param name="adjective">A string whose text represents the lexical form of an adjective.</param>
        /// <returns>
        /// The base form of the given type of word. If the word is already in its base form, the
        /// text content of the adjective will simply be returned.
        /// </returns>
        public string FindRoot(string adjective)
        {
            bool exceptional;
            return FindRootImplementation(adjective, out exceptional);
        }

        private string FindRootImplementation(string adjective, out bool exceptional)
        {
            var hyphenPosition = adjective.IndexOf('-');
            if (hyphenPosition > 0)
            {
                var hyphenatedAppendage = adjective.Substring(hyphenPosition);
                var root = FindRootImplementation(adjective.Substring(0, hyphenPosition), out exceptional);
                return root + hyphenatedAppendage;
            }
            var exceptionalMapping = CheckExceptionMapping(adjective);
            exceptional = exceptionalMapping != null;
            if (exceptional)
            {
                return exceptionalMapping;
            }
            for (var i = 3; i >= 0; --i)
            {
                var suffixAndEnding = SuffixEndingPairs[i];
                if (adjective.EndsWith(suffixAndEnding.Suffix))
                {
                    return suffixAndEnding.RemoveEnding(adjective);
                }
            }
            return adjective;
        }

        private static string CheckExceptionMapping(string adjective)
        {
            if (ExceptionMapping.ContainsKey(adjective))
            {
                return adjective;
            }
            var exceptionBaseForms = from mapping in ExceptionMapping
                                     where mapping.Value.Contains(adjective, StringComparer.OrdinalIgnoreCase)
                                     select mapping.Key;
            var exceptionResult = exceptionBaseForms.FirstOrDefault();
            return exceptionResult;
        }

        /// <summary>
        /// Computes and returns the list of all conjugated forms of the specified <see cref="Adjective" />.
        /// </summary>
        /// <param name="adjective">
        /// The <see cref="Adjective" /> whose lexical forms are to be generated and produced.
        /// </param>
        /// <returns>
        /// The collection of all conjugated forms of the specified adjective, including the
        /// originally the adjective itself.
        /// </returns>
        public IEnumerable<string> GetLexicalForms(Adjective adjective) => GetLexicalForms(adjective.Text);

        /// <summary>
        /// Computes and returns the list of all conjugated forms of the adjective specified by the
        /// specified adjective.
        /// </summary>
        /// <param name="adjective">The string representation of an adjective.</param>
        /// <returns>
        /// The collection of all conjugated forms of the specified adjective, including the
        /// originally the adjective itself.
        /// </returns>
        public IEnumerable<string> GetLexicalForms(string adjective)
        {
            var hyphenIndex = adjective.IndexOf('-');
            var hyphenatedAppendage = string.Empty;
            if (hyphenIndex > 0)
            {
                hyphenatedAppendage = adjective.Substring(hyphenIndex);
            }
            bool exceptional;
            var rootWithHyphenatedAppendage = FindRootImplementation(adjective, out exceptional);
            var rootHyphenIndex = rootWithHyphenatedAppendage.IndexOf('-');
            var root = rootWithHyphenatedAppendage.Substring(0, rootHyphenIndex > 0 ? rootHyphenIndex : rootWithHyphenatedAppendage.Length);
            yield return root + hyphenatedAppendage;

            if (exceptional)
            {
                foreach (var exc in ExceptionMapping[root])
                {
                    yield return exc + hyphenatedAppendage;
                }
                yield break;
            }
            else
            {
                foreach (var form in from suffixAndEnding in SuffixEndingPairs/*.Skip(root[root.Length - 1].EqualsIgnoreCase('e') ? 2 : 0).Take(2)*/
                                     where suffixAndEnding.EndingLength == 0 || root.EndsWith(suffixAndEnding.Ending)
                                     select suffixAndEnding.RemoveEndingAndApplySuffix(root))
                {
                    yield return form + hyphenatedAppendage;
                }
            }
        }

        private static readonly SuffixEndingPair[] SuffixEndingPairs =
        {
            new SuffixEndingPair { Ending = "e", Suffix = "er" },
            new SuffixEndingPair { Ending = "e", Suffix = "est" },
            new SuffixEndingPair { Ending = "", Suffix = "er" },
            new SuffixEndingPair { Ending = "", Suffix = "est" },
        };

        private static readonly WordNetExceptionDataManager Helper = new WordNetExceptionDataManager("adj.exc");
        private static readonly IReadOnlyDictionary<string, List<string>> ExceptionMapping = Helper.ExcMapping;

        private struct SuffixEndingPair
        {
            public string RemoveEnding(string word) => word.Substring(0, word.Length - SuffixLength);

            public string RemoveEndingAndApplySuffix(string word) => $"{word.Substring(0, word.Length - EndingLength)}{Suffix}";

            public string Suffix
            {
                get => suffix;
                set
                {
                    suffix = value;
                    suffixLength = suffix.Length;
                }
            }

            public string Ending
            {
                get => ending;
                set
                {
                    endingLength = value.Length;
                    ending = value;
                }
            }

            public int EndingLength => endingLength;

            public int SuffixLength => suffixLength;

            private string ending;
            private string suffix;
            private int endingLength;
            private int suffixLength;
        }
    }
}