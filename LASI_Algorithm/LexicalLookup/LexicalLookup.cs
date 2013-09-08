using LASI.Utilities;
using LASI.Algorithm.Patternization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Lookup.Morphemization;

namespace LASI.Algorithm.Lookup
{
    /// <summary>
    /// Provides Comprehensive static facilities for Synoynm Identification, Word and Phrase Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static class LexicalLookup
    {
        #region Public Methods

        #region Synonym Lookup Methods

        /// <summary>
        /// Returns the synonyms for the provided Noun.
        /// </summary>
        /// <param name="noun">The Noun to lookup.</param>
        /// <returns>The synonyms for the provided Noun.</returns>
        public static IEnumerable<string> GetSynonyms(this Noun noun) {
            return InternalLookup(noun);
        }
        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">The Verb to lookup.</param>
        /// <returns>The synonyms for the provided Verb.</returns>
        public static IEnumerable<string> GetSynonyms(this Verb verb) {
            return InternalLookup(verb);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">The Adjective to lookup.</param>
        /// <returns>The synonyms for the provided Adjective.</returns>
        public static IEnumerable<string> GetSynonyms(this Adjective adjective) {
            return InternalLookup(adjective);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">The Adverb to lookup.</param>
        /// <returns>The synonyms for the provided Adverb.</returns>
        public static IEnumerable<string> GetSynonyms(this Adverb adverb) {
            return InternalLookup(adverb);
        }

        /// <summary>
        /// Determines if two Noun instances are synonymous.
        /// </summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun</param>
        /// <returns>True if the Noun instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Noun first, Noun second) {
            return InternalLookup(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Verb instances are synonymous.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb</param>
        /// <returns>True if the Verb instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Verb first, Verb second) {
            if (first == null || second == null)
                return false;
            return InternalLookup(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Adjective instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adjective.</param>
        /// <param name="other">The second Adjective</param>
        /// <returns>True if the Adjective instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Adjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }

        #region Private Adjective Specific Lookups
        static bool IsSynonymFor(this Adjective word, SuperlativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdjective word, SuperlativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdjective word, ComparativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this Adjective word, ComparativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdjective word, SuperlativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdjective word, ComparativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        #endregion

        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adverb.</param>
        /// <param name="other">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Adverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }


        #region Private Adverb Specific Lookups
        static bool IsSynonymFor(this Adverb word, SuperlativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdverb word, SuperlativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdverb word, ComparativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this Adverb word, ComparativeAdverb other) {
            var otherRoot = AdverbConjugator.FindRoot(other);
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdverb word, SuperlativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdverb word, ComparativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        #endregion


        #endregion

        #region Similarity Comparison Methods

        /// <summary>
        /// Determines if two IEntity instances are similar.
        /// </summary>
        /// <param name="first">The first IEntity</param>
        /// <param name="second">The second IEntity</param>
        /// <returns>True if the given IEntity instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(e1, e2) ) { ... }</code>
        /// <code>if ( e1.IsSimilarTo(e2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IEntity first, IEntity second) {
            return first.Match().Yield<SimResult>()
                    .When(string.Equals(first.Text, second.Text, StringComparison.OrdinalIgnoreCase))
                        .Then(SimResult.Similar)
                    .Case<Noun>(n1 =>
                        second.Match().Yield<SimResult>()
                          .Case<Noun>(n2 => new SimResult(n1.IsSynonymFor(n2)))
                          .Case<NounPhrase>(np2 => n1.IsSimilarTo(np2))
                        .Result())
                    .Case<NounPhrase>(np1 =>
                        second.Match().Yield<SimResult>()
                          .Case<NounPhrase>(np2 => np1.IsSimilarTo(np2))
                          .Case<Noun>(n2 => np1.IsSimilarTo(n2))
                        .Result())
                    .Result();
        }
        /// <summary>
        /// Determines if two IVerbal instances are similar.
        /// </summary>
        /// <param name="first">The first IVerbal</param>
        /// <param name="second">The second IVerbal</param>
        /// <returns>True if the given IVerbal instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, v2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(v2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IVerbal first, IVerbal second) {
            return first.Match().Yield<SimResult>()
                    .When(string.Equals(first.Text, second.Text, StringComparison.OrdinalIgnoreCase))
                        .Then(SimResult.Similar)
                    .Case<Verb>(v1 =>
                        second.Match().Yield<SimResult>()
                          .Case<Verb>(v2 => new SimResult(v1.IsSynonymFor(v2)))
                          .Case<VerbPhrase>(vp2 => v1.IsSimilarTo(vp2))
                        .Result())
                    .Case<VerbPhrase>(vp1 =>
                        second.Match().Yield<SimResult>()
                          .Case<VerbPhrase>(vp2 => vp1.IsSimilarTo(vp2))
                          .Case<Verb>(v2 => vp1.IsSimilarTo(v2))
                        .Result())
                    .Result();
        }
        /// <summary>
        /// Determines if two IDescriptor instances are similar.
        /// </summary>
        /// <param name="first">The first IDescriptor</param>
        /// <param name="second">The second IDescriptor</param>
        /// <returns>True if the given IDescriptor instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(d1, d2) ) { ... }</code>
        /// <code>if ( d1.IsSimilarTo(d2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IDescriptor first, IDescriptor second) {
            return first.Match().Yield<SimResult>()
                    .When(string.Equals(first.Text, second.Text, StringComparison.OrdinalIgnoreCase))
                        .Then(SimResult.Similar)
                    .Case<Adjective>(j1 =>
                        second.Match().Yield<SimResult>()
                            .Case<Adjective>(j2 => new SimResult(j1.IsSynonymFor(j2)))
                            .Case<AdjectivePhrase>(jp2 => jp2.IsSimilarTo(j1))
                        .Result())
                    .Case<AdjectivePhrase>(jp1 =>
                        second.Match().Yield<SimResult>()
                          .Case<AdjectivePhrase>(jp2 => jp1.IsSimilarTo(jp2))
                          .Case<Adjective>(j2 => jp1.IsSimilarTo(j2))
                        .Result())
                    .Result();
        }
        /// <summary>
        /// Determines if two IAdverbial instances are similar.
        /// </summary>
        /// <param name="first">The first IAdverbial</param>
        /// <param name="second">The second IAdverbial</param>
        /// <returns>True if the given IAdverbial instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(d1, d2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IAdverbial first, IAdverbial second) {
            return first.Match().Yield<SimResult>()
                    .When(string.Equals(first.Text, second.Text, StringComparison.OrdinalIgnoreCase))
                        .Then(SimResult.Similar)
                    .Case<Adverb>(a1 =>
                        second.Match().Yield<SimResult>()
                            .Case<Adverb>(a2 => new SimResult(a1.IsSynonymFor(a2)))
                            .Case<AdverbPhrase>(ap2 => ap2.IsSimilarTo(a1))
                        .Result())
                    .Case<AdverbPhrase>(ap1 => second.Match().Yield<SimResult>()
                        .Case<AdverbPhrase>(ap2 => ap1.IsSimilarTo(ap2))
                        .Case<Adverb>(a2 => ap1.IsSimilarTo(a2))
                    .Result())
                .Result();
        }

        /// <summary>
        /// Determines if the provided Noun is similar to the provided NounPhrase.
        /// </summary>
        /// <param name="first">The Noun.</param>
        /// <param name="second">The NounPhrase.</param>
        /// <returns>True if the provided Noun is similar to the provided NounPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(n1, np2) ) { ... }</code>
        /// <code>if ( n1.IsSimilarTo(np2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Noun first, NounPhrase second) {
            var phraseNouns = second.Words.GetNouns();
            return new SimResult(phraseNouns.Count() == 1 && phraseNouns.First().IsSynonymFor(first));
        }
        /// <summary>
        /// Determines if the provided NounPhrase is similar to the provided Noun.
        /// </summary>
        /// <param name="first">The NounPhrase.</param>
        /// <param name="second">The Noun.</param>
        /// <returns>True if the provided NounPhrase is similar to the provided Noun, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(np1, n2) ) { ... }</code>
        /// <code>if ( np1.IsSimilarTo(n2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this NounPhrase first, Noun second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the two provided Noun instances are similar.
        /// </summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun.</param>
        /// <returns>True if the first Noun is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(n1, n2) ) { ... }</code>
        /// <code>if ( n1.IsSimilarTo(n2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Noun first, Noun second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the two provided Verb instances are similar.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb.</param>
        /// <returns>True if the first Verb is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, v2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(v2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Verb first, Verb second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided VerbPhrase is similar to the provided Verb.
        /// </summary>
        /// <param name="first">The VerbPhrase.</param>
        /// <param name="second">The Verb.</param>
        /// <returns>True if the provided VerbPhrase is similar to the provided Verb, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, v2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(v2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this VerbPhrase first, Verb second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Verb is similar to the provided VerbPhrase.
        /// </summary>
        /// <param name="first">The Verb.</param>
        /// <param name="second">The VerbPhrase.</param>
        /// <returns>True if the provided Verb is similar to the provided VerbPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, vp2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Verb first, VerbPhrase second) {
            return new SimResult(second.Words.TakeWhile(w => !(w is ToLinker)).GetVerbs().Any(v => v.IsSynonymFor(first)));//This is kind of rough.
        }

        /// <summary>
        /// Determines if two NounPhrases are similar.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>True if the given NounPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(np1, np2) ) { ... }</code>
        /// <code>if ( np1.IsSimilarTo(np2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this NounPhrase first, NounPhrase second) {
            var ratio = GetSimilarityRatio(first, second);
            return new SimResult(ratio > SIMILARITY_THRESHOLD, ratio);
        }
        /// <summary>
        /// Determines if two VerbPhrases are similar.
        /// </summary>
        /// <param name="first">The first VerbPhrase</param>
        /// <param name="second">The second VerbPhrase</param>
        /// <returns>True if the given VerbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this VerbPhrase first, VerbPhrase second) {

            //Look into refining this
            List<Verb> leftHandVerbs = first.Words.GetVerbs().ToList();
            List<Verb> rightHandVerbs = second.Words.GetVerbs().ToList();

            bool result = leftHandVerbs.Count == rightHandVerbs.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandVerbs.Count; ++i) {
                        result &= leftHandVerbs[i].IsSynonymFor(rightHandVerbs[i]);
                    }
                }
                catch (NullReferenceException) {
                    return SimResult.Dissimilar;
                }
            }

            return new SimResult(result);
        }
        /// <summary>
        /// Determines if the two provided Adjective instances are similar.
        /// </summary>
        /// <param name="first">The first Adjective.</param>
        /// <param name="second">The second Adjective.</param>
        /// <returns>True if the first Adjective is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, a2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adjective first, Adjective second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided AdjectivePhrase is similar to the provided Adjective.
        /// </summary>
        /// <param name="first">The AdjectivePhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns>True if the provided AdjectivePhrase is similar to the provided Adjective, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, a2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdjectivePhrase first, Adjective second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Adjective is similar to the provided AdjectivePhrase.
        /// </summary>
        /// <param name="first">The Adjective.</param>
        /// <param name="second">The AdjectivePhrase.</param>
        /// <returns>True if the provided Adjective is similar to the provided AdjectivePhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adjective first, AdjectivePhrase second) {
            return new SimResult(second.Words.GetAdjectives().Any(adj => adj.IsSynonymFor(first)));
        }


        /// <summary>
        /// Determines if two AdjectivePhrase are similar.
        /// </summary>
        /// <param name="first">The first AdjectivePhrase</param>
        /// <param name="second">The second AdjectivePhrase</param>
        /// <returns>True if the given AdjectivePhrase are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdjectivePhrase first, AdjectivePhrase second) {

            //Look into refining this
            List<Adjective> leftHandAdjectives = first.Words.GetAdjectives().ToList();
            List<Adjective> rightHandAdjectives = second.Words.GetAdjectives().ToList();

            bool result = leftHandAdjectives.Count == rightHandAdjectives.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandAdjectives.Count; ++i) {
                        result &= leftHandAdjectives[i].IsSynonymFor(rightHandAdjectives[i]);
                    }
                }
                catch (NullReferenceException) {
                    return SimResult.Dissimilar;
                }
            }
            return new SimResult(result);
        }
        /// <summary>
        /// Determines if the two provided Adverb instances are similar.
        /// </summary>
        /// <param name="first">The first Adverb.</param>
        /// <param name="second">The second Adverb.</param>
        /// <returns>True if the first Adverb is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, a2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adverb first, Adverb second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided AdverbPhrase is similar to the provided Adverb.
        /// </summary>
        /// <param name="first">The AdverbPhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns>True if the provided AdverbPhrase is similar to the provided Adverb, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, a2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdverbPhrase first, Adverb second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Adverb is similar to the provided AdverbPhrase.
        /// </summary>
        /// <param name="first">The Adverb.</param>
        /// <param name="second">The AdverbPhrase.</param>
        /// <returns>True if the provided Adverb is similar to the provided AdverbPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adverb first, AdverbPhrase second) {
            return new SimResult(second.Words.GetAdverbs().Any(adj => adj.IsSynonymFor(first)));
            // Must refine this to check for negators and modals which will potentially invert the meaning.
        }

        /// <summary>
        /// Determines if two AdverbPhrases are similar.
        /// </summary>
        /// <param name="first">The first AdverbPhrase</param>
        /// <param name="second">The second AdverbPhrase</param>
        /// <returns>True if the given AdverbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdverbPhrase first, AdverbPhrase second) {

            //Look into refining this
            List<Adverb> leftHandAdjectives = first.Words.GetAdverbs().ToList();
            List<Adverb> rightHandAdjectives = second.Words.GetAdverbs().ToList();

            bool result = leftHandAdjectives.Count == rightHandAdjectives.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandAdjectives.Count; ++i) {
                        result &= leftHandAdjectives[i].IsSynonymFor(rightHandAdjectives[i]);
                    }
                }
                catch (NullReferenceException) {
                    return SimResult.Dissimilar;
                }
            }
            return new SimResult(result);
        }
        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        private static double GetSimilarityRatio(NounPhrase first, NounPhrase second) {
            NounPhrase outer = null;
            NounPhrase inner = null;
            double similarCount = 0.0d;

            if (first.Words.Count() >= second.Words.Count()) {
                outer = first;
                inner = second;
            } else {
                outer = second;
                inner = first;
            }

            if ((outer.Words.GetNouns().Count() != 0) && (inner.Words.GetNouns().Count() != 0)) {
                foreach (var o in outer.Words.GetNouns()) {
                    foreach (var i in inner.Words.GetNouns()) {
                        if (i.IsSimilarTo(o))
                            similarCount += 0.7;
                    }
                    var scaleFactor = inner.Words.GetNouns().Count() * outer.Words.GetNouns().Count();
                    return (similarCount / scaleFactor == 0 ? 1 : scaleFactor);
                }

            }
            return 0d;
        }

        #endregion

        #region Name Gender Lookup Methods

        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the entity.
        /// </summary>
        /// <param name="entity">The entity whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the entity.</returns>
        public static Gender GetGender(this IEntity entity) {
            return entity.Match().Yield<Gender>()
                    .Case<IGendered>(p => p.Gender)
                    .Case<IPronoun>(p => p.GetPronounGender())
                    .Case<NounPhrase>(n => n.GetGender())
                    .Case<GenericNoun>(n => Gender.Neutral)
                    .When(e => e.BoundPronouns.Any())
                    .Then<IEntity>(e => (from pro in e.BoundPronouns
                                         let gen = pro.Match().Yield<Gender>().Case<IGendered>(p => p.Gender).Result()
                                         group gen by gen into byGen
                                         orderby byGen.Count() descending
                                         select byGen.Key).FirstOrDefault())
                    .Result();
        }
        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the Pronoun based on its referrent if known, or else its PronounKind.
        /// </summary>
        /// <param name="pronoun">The Pronoun whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the Pronoun.</returns>
        private static Gender GetPronounGender(this IPronoun pronoun) {
            return pronoun != null ?
                pronoun.Match().Yield<Gender>()
                    .Case<IGendered>(p => p.Gender)
                    .Case<PronounPhrase>(p => GetPhraseGender(p))
                .Result() :
                pronoun.Match().Yield<Gender>()
                .When<IPronoun>(p => p.RefersTo != null)
                .Then<IPronoun>(p => {
                    return (from referent in p.RefersTo
                            let gen =
                               referent.Match().Yield<Gender>()
                                   .Case<NounPhrase>(r => r.GetGender())
                                   .Case<Pronoun>(r => r.GetPronounGender())
                                   .Case<ProperSingularNoun>(r => r.Gender)
                                   .Case<GenericNoun>(n => Gender.Neutral)
                               .Result()
                            group gen by gen into byGen
                            where byGen.Count() == pronoun.RefersTo.Count()
                            select byGen.Key).FirstOrDefault();
                }).Result();
        }

        /// <summary>
        /// Returns a NameGender value indiciating the likely prevailing gender within the NounPhrase.
        /// </summary>
        /// <param name="name">The NounPhrase whose prevailing gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely prevailing gender of the NounPhrase.</returns>
        static Gender GetGender(this NounPhrase name) {
            return GetNounPhraseGender(name);
        }

        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Name, false otherwise.</returns>
        public static bool IsFullName(this NounPhrase name) {
            return GetNounPhraseGender(name).IsMaleOrFemale() && name.Words.GetProperNouns().Any(n => n.IsLastName());

        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Female Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Female Name, false otherwise.</returns>
        public static bool IsFullFemale(this NounPhrase name) {
            return GetNounPhraseGender(name).IsFemale();
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Male Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Male Name, false otherwise.</returns>
        public static bool IsFullMale(this NounPhrase name) {
            return GetNounPhraseGender(name).IsMale();
        }


        private static Gender GetNounPhraseGender(NounPhrase name) {
            var propers = name.Words.GetProperNouns();
            var first = propers.GetSingular().FirstOrDefault(n => n.Gender.IsMaleOrFemale());
            var last = propers.LastOrDefault(n => n != first && n.IsLastName());
            return first != null && (last != null || propers.All(n => n.GetGender() == first.Gender)) ?
                first.Gender : name.Words.GetNouns().All(n => n.GetGender().IsNeutral()) ? Gender.Neutral : Gender.Undetermined;
        }
        private static Gender GetPhraseGender(PronounPhrase name) {
            if (name.Words.All(w => w is Determiner))
                return Gender.Neutral;
            var genderedWords = name.Words.OfType<IGendered>().Select(w => w.Gender);
            return name.Words.GetProperNouns().Any(n => !(n is IGendered)) ?
                GetNounPhraseGender(name)
                :
                genderedWords.All(p => p.IsFemale()) ? Gender.Female :
                genderedWords.All(p => p.IsMale()) ? Gender.Male :
                genderedWords.All(p => p.IsNeutral()) ? Gender.Neutral :
                Gender.Undetermined;
        }

        #endregion

        #region First Name Lookup Methods
        /// <summary>
        /// Determines wether the provided ProperNoun is a FirstName.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the provided ProperNoun is a FirstName, false otherwise.</returns>
        public static bool IsFirstName(this ProperNoun proper) {
            return IsFirstName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a last name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the english language, false otherwise.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return IsLastName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a female first name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the english language, false otherwise.</returns>
        public static bool IsFemaleFirstName(this ProperNoun proper) {
            return IsFemaleFirstName(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a male first name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the english language, false otherwise.</returns>
        public static bool IsMaleFirstName(this ProperNoun proper) {
            return IsMaleFirstName(proper.Text);
        }
        /// <summary>
        /// Determines if provided text is in the set of Female or Male first names.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if the provided text is in the set of Female or Male first names, false otherwise.</returns>
        private static bool IsFirstName(string text) {
            return femaleNames.Count > maleNames.Count ?
                maleNames.Contains(text) || femaleNames.Contains(text) :
                femaleNames.Contains(text) || maleNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common lastname in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common lastname in the english language, false otherwise.</returns>
        private static bool IsLastName(string text) {
            return lastNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common female name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common female name in the english language, false otherwise.</returns>
        private static bool IsFemaleFirstName(string text) {
            return femaleNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common male name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common male name in the english language, false otherwise.</returns>
        private static bool IsMaleFirstName(string text) {
            return maleNames.Contains(text);
        }

        #endregion

        #region Lookup Loading Methods
        /// <summary>
        /// Returns a sequence of Tasks containing all of the yet unstarted LexicalLookup loading operations.
        /// Await each Task to start its corresponding loading operation.
        /// </summary>
        /// <returns>a sequence of Tasks containing all of the yet unstarted LexicalLookup loading operations.</returns>
        public static IEnumerable<Task<string>> GetLoadingTasks() {
            return new[] {
                LoadingTaskBuilder.NounThesaurusLoadTask,
                LoadingTaskBuilder.VerbThesaurusLoadTask, 
                LoadingTaskBuilder.AdjectiveThesaurusLoadTask, 
                LoadingTaskBuilder.AdverbThesaurusLoadTask, 
                LoadingTaskBuilder.NameDataLoadTask }
            .Where(t => t != null);
        }
        #endregion

        #endregion

        #region Private Methods

        #region Internal Syonym Lookup Methods

        private static ISet<string> InternalLookup(Noun noun) {
            return cachedNounData.GetOrAdd(noun.Text, key => nounLookup[key]);
        }
        private static ISet<string> InternalLookup(Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => verbLookup[key]);
        }
        private static ISet<string> InternalLookup(Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => adverbLookup[key]);
        }
        private static ISet<string> InternalLookup(Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => adjectiveLookup[key]);
        }

        #endregion

        private static async Task LoadNameDataAsync() {
            await Task.Factory.ContinueWhenAll(
                new[] {  
                    Task.Run(async () => lastNames = await GetLinesAsync(lastNamesFilePath)),
                    Task.Run(async () => femaleNames = await GetLinesAsync(femaleNamesFilePath)),
                    Task.Run(async () => maleNames = await GetLinesAsync(maleNamesFilePath)) 
                },
                results => {
                    genderAmbiguousNames =
                        new HashSet<string>(maleNames.Intersect(femaleNames).Concat(femaleNames.Intersect(maleNames)), StringComparer.OrdinalIgnoreCase);

                    var stratified =
                        from m in maleNames.Select((s, i) => new { Rank = (double)i / maleNames.Count, Name = s })
                        join f in femaleNames.Select((s, i) => new { Rank = (double)i / femaleNames.Count, Name = s })
                        on m.Name equals f.Name
                        group f.Name by f.Rank / m.Rank > 1 ? 'M' : m.Rank / f.Rank > 1 ? 'F' : 'U';

                    maleNames.ExceptWith(from s in stratified where s.Key == 'F' from n in s select n);
                    femaleNames.ExceptWith(from s in stratified where s.Key == 'M' from n in s select n);
                }
            );
        }

        private static async Task<HashSet<string>> GetLinesAsync(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                return new HashSet<string>(
                    (await reader.ReadToEndAsync()).Split(
                        new[] { '\r', '\n' },
                        StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()),
                    StringComparer.OrdinalIgnoreCase);
            }
        }

        #endregion

        #region Public Properties

        #region Name Collection Accessors

        /// <summary>
        /// Gets a sequence of all known Last Names.
        /// </summary>
        public static IReadOnlyCollection<string> LastNames {
            get {
                return lastNames.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a sequence of all known Female Names.
        /// </summary>
        public static IReadOnlyCollection<string> FemaleNames {
            get {
                return femaleNames.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a sequence of all known Male Names.
        /// </summary>
        public static IReadOnlyCollection<string> MaleNames {
            get {
                return maleNames.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a sequence of all known Names which are just as likely to be Female or Male.
        /// </summary>
        public static IReadOnlyCollection<string> GenderAmbiguousNames {
            get {
                return genderAmbiguousNames.ToList().AsReadOnly();
            }
        }

        #endregion

        #endregion

        #region Private Properties




        #endregion

        #region Private Fields
        // WordNet Data File Paths
        private static readonly string nounWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        private static readonly string adverbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        private static readonly string adjectiveWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        // Internal Thesauri
        private static NounLookup nounLookup = new NounLookup(nounWNFilePath);
        private static VerbLookup verbLookup = new VerbLookup(verbWNFilePath);
        private static AdjectiveLookup adjectiveLookup = new AdjectiveLookup(adjectiveWNFilePath);
        private static AdverbLookup adverbLookup = new AdverbLookup(adverbWNFilePath);
        // Synonym Lookup Caches
        private static ConcurrentDictionary<string, ISet<string>> cachedNounData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        private static ConcurrentDictionary<string, ISet<string>> cachedVerbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        private static ConcurrentDictionary<string, ISet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        private static ConcurrentDictionary<string, ISet<string>> cachedAdverbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        // Name Data File Paths
        private static readonly string lastNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
        private static readonly string femaleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
        private static readonly string maleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";
        // Name Data Sets
        private static ISet<string> lastNames;
        private static ISet<string> maleNames;
        private static ISet<string> femaleNames;
        private static ISet<string> genderAmbiguousNames;
        //Loading states for specific data items
        private static LoadingState nounLoadingState = LoadingState.NotStarted;
        private static LoadingState verbLoadingState = LoadingState.NotStarted;
        private static LoadingState adjectiveLoadingState = LoadingState.NotStarted;
        private static LoadingState adverbLoadingState = LoadingState.NotStarted;
        private static LoadingState nameDataLoadingState = LoadingState.NotStarted;

        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ration of a similarity comparison is >= the threshold, 
        /// then the similarity comparison will return true.
        /// </summary>
        public const double SIMILARITY_THRESHOLD = 0.6;
        #endregion



        #region Utility Types


        /// <summary>
        /// Encapsulates multiple pieces of information gathered during a similarity comparison into a light weight type.
        /// The structure cannot be created from outside of the LexicalLookup class and is used to convey internal results.
        /// No special syntax is or code changes are required to manipulate this type. It will implicitely convert to bool
        /// So all code with forms such as: 
        /// <code>if ( a.IsSimilarTo(b) ) { ... }</code>
        /// need not and should not be changed. 
        /// However, If the numeric ratio used to determine similarity is needed and applicable, the type will implcitely convert
        /// to a double. This removes the need for public code such as: 
        /// <code>if ( LexicalLookup.GetSimiliarityRatio(a, b) > 0.7 ) { ... }</code>
        /// Instead one may simple write the same logic as: 
        /// <code>if ( a.IsSimilarTo(b) > 0.7 ) { ... }</code>
        /// </summary>
        public struct SimResult : IEquatable<SimResult>, IComparable<SimResult>
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the SimilarityResult structure from the provided values.
            /// </summary>
            /// <param name="similar">Indicates the result the true of false result of an IsSimilarTo test.</param>
            /// <param name="similarityRatio">Represents the similarity ratio between the tested elements, if applicable.</param>
            internal SimResult(bool similar, double similarityRatio)
                : this() {
                booleanResult = similar;
                rationalResult = similarityRatio;
            }
            /// <summary>
            /// intializes a new instance of the SimilarityResult structure from the provided bool value.
            /// </summary>
            /// <param name="similar">Indicates the result the true of false result of an IsSimilarTo test.</param>
            /// <remarks>Use this constructor when the ratio itself is not specified or not provided.
            /// In such cases, the RatioResult property will be automatically set to 1 or 0 based on the truthfullness of the provided similar argument.
            /// </remarks>
            internal SimResult(bool similar) : this(similar, similar ? 1 : 0) { }

            #endregion

            #region Methods
            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            ///  </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>true if the current object is equal to the other parameter, false otherwise.</returns>
            public bool Equals(SimResult other) {
                return this == other;
            }
            /// <summary>
            /// Returns a value that indicates whether the specified object is equal to the current SimResult.
            /// </summary>
            /// <param name="obj">The object to compare with.</param> 
            /// <returns>True if the specified object is equal to the current SimResult, false otherwise.</returns> 
            public override bool Equals(object obj) {
                return obj != null && obj is SimResult && this == (SimResult)obj;
            }
            /// <summary>
            /// Compares the current object with another object of the same type.
            /// </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>
            /// A value that indicates the relative order of the objects being compared.
            /// The return value has the following meanings: Value Meaning Less than zero
            /// This object is less than the other parameter.Zero This object is equal to
            /// other. Greater than zero This object is greater than other.
            /// </returns>
            public int CompareTo(SimResult other) {
                return this.rationalResult.CompareTo(other.rationalResult);
            }
            /// <summary>
            /// Returns the hash code for this instance.
            /// </summary>
            /// <returns>A 32-bit signed integer hash code.</returns>
            public override int GetHashCode() {
                return rationalResult.GetHashCode() ^ booleanResult.GetHashCode();
            }
            #endregion

            #region Fields

            private bool booleanResult;
            private double rationalResult;

            #endregion

            #region Operators

            #region Implcit Conversion Operators
            // These allow the type to implcitely convert to the desired result type for the condition. 
            // Thus, refactoring the IsSimilarTo implementations preserves and enhances existing code
            // without the need to rewrite any conditionals or call expensive methods multiple times to get numeric vs boolean results

            /// <summary>
            /// Converts the SimResult into its boolean representation. The resulting boolean has the same value as the conversion target's booleanResult Property.
            /// </summary>
            /// <param name="sr">The SimResult to convert.</param>
            /// <returns>A boolean with the same value as the conversion target's booleanResult Property.</returns>
            public static implicit operator bool(SimResult sr) { return sr.booleanResult; }
            /// <summary>
            /// Converts the SimResult into its double representation. The resulting boolean has the same value as the conversion target's RatioResult Property.
            /// </summary>
            /// <param name="sr">The SimResult to convert.</param>
            /// <returns>A double with the same value as the conversion target's RatioResult Property.</returns>
            public static implicit operator double(SimResult sr) { return sr.rationalResult; }

            #endregion

            #region Comparison Operators
            /// <summary>
            /// Returns a value that indicates whether the SimResult on the left is equal to the SimResult on the right.
            /// Although it seems unlikely that two instances of SimResult will be compared directly for equality. 
            /// The == and != operators or defined to ensure type coersion does not result from the implicit conversions which make the class convenient.
            /// Equality is defined strictly such that both RatioResult properties must match exactly in addition to both booleanResult properties.
            /// Keep this in mind if, for some reason, it is ever necessary to write code such as: 
            /// <code>if ( a.IsSimilarTo(b) == b.IsSimilarTo(a) ) { ... } </code>
            /// as the lexical lookup class itself currently makes no guarantees about reflexive equality over phrase-wise comparisons.
            /// </summary>
            /// <param name="left">The SimRult on the left hand side.</param>
            /// <param name="right">The SimRult on the right hand side.</param>
            /// <returns>True if the SimResult on the left is equal to the SimResult on the right.</returns>
            public static bool operator ==(SimResult left, SimResult right) {
                return left.rationalResult == right.rationalResult && left.booleanResult == right.booleanResult;
            }
            /// <summary>
            /// Returns a value that indicates whether the SimResult on the left is not equal to the SimResult on the right.
            /// Although it seems unlikely that two instances of SimResult will be compared directly for equality. 
            /// The == and != operators or defined to ensure type coersion does not result from the implicit conversions which make the class convenient.
            /// Equality is defined strictly such that both RatioResult properties must match exactly in addition to both booleanResult properties.
            /// Keep this in mind if, for some reason, it is ever necessary to write code such as:
            /// <code>if ( a.IsSimilarTo(b) == b.IsSimilarTo(a) ) { ... }</code> 
            /// as the lexical lookup class itself currently makes no guarantees about reflexive equality over phrase-wise comparisons.
            /// </summary>
            /// <param name="left">The SimRult on the left hand side.</param>
            /// <param name="right">The SimRult on the right hand side.</param>
            /// <returns>False if the SimResult on the left is equal to the SimResult on the right.</returns>
            public static bool operator !=(SimResult left, SimResult right) {
                return !(left == right);
            }
            #endregion

            #endregion

            #region Static Properties
            internal static readonly SimResult Similar = new SimResult(true, 1);
            internal static readonly SimResult Dissimilar = new SimResult(false, 0);
            #endregion

        }

        /// <summary>
        /// Exposes properties which construct the Tasks which correspond to the various loading operations of the LexicalLookup class.
        /// </summary>
        private static class LoadingTaskBuilder
        {
            internal static Task<string> AdjectiveThesaurusLoadTask {
                get {
                    var result = adjectiveLoadingState == LoadingState.NotStarted ?
                        Task.Run(async () => {
                            await adjectiveLookup.LoadAsync();
                            adjectiveLoadingState = LoadingState.Finished;
                            return "Adjective Thesaurus Loaded";
                        }) :
                        null;
                    adjectiveLoadingState = LoadingState.InProgress;
                    return result;
                }
            }
            internal static Task<string> AdverbThesaurusLoadTask {
                get {
                    var result = adverbLoadingState == LoadingState.NotStarted ?
                        Task.Run(async () => {
                            await adverbLookup.LoadAsync();
                            adverbLoadingState = LoadingState.Finished;
                            return "Adverb Thesaurus Loaded";
                        }) :
                        null;
                    adverbLoadingState = LoadingState.InProgress;
                    return result;
                }
            }
            internal static Task<string> VerbThesaurusLoadTask {
                get {
                    var result = verbLoadingState == LoadingState.NotStarted ?
                        Task.Run(async () => {
                            await verbLookup.LoadAsync();
                            verbLoadingState = LoadingState.Finished;
                            return "Verb Thesaurus Loaded";
                        }) :
                        null;
                    verbLoadingState = LoadingState.InProgress;
                    return result;
                }
            }
            internal static Task<string> NounThesaurusLoadTask {
                get {
                    var result = nounLoadingState == LoadingState.NotStarted ?
                        Task.Run(async () => {
                            await nounLookup.LoadAsync();
                            nounLoadingState = LoadingState.Finished;
                            return "Noun Thesaurus Loaded";
                        }) :
                        null;
                    nounLoadingState = LoadingState.InProgress;
                    return result;
                }
            }
            internal static Task<string> NameDataLoadTask {
                get {
                    var result = nameDataLoadingState == LoadingState.NotStarted ?
                        Task.Run(async () => {
                            await LoadNameDataAsync();
                            nameDataLoadingState = LoadingState.Finished;
                            return "Loaded Name Data";
                        }) :
                        null;
                    nameDataLoadingState = LoadingState.InProgress;
                    return result;
                }
            }
        }
        /// <summary>
        /// Represents the various states of a loading operation.
        /// </summary>
        private enum LoadingState
        {
            NotStarted,
            InProgress,
            Finished
        }
        #endregion
    }
}