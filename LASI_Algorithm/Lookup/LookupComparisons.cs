using System;
using LASI.Algorithm.Patternization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.LexicalLookup.Morphemization;

namespace LASI.Algorithm.LexicalLookup
{
    public static partial class Lookup
    {
        #region Synonym LexicalLookup Methods

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
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
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
            var otherRoot = AdverbMorpher.FindRoot(other);
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
        /// <code>if ( Lookup.IsSimilarTo(e1, e2) ) { ... }</code>
        /// <code>if ( e1.IsSimilarTo(e2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IEntity first, IEntity second) {
            return first.Match().Yield<SimResult>()
                    .When(string.Equals(first.Text, second.Text, StringComparison.OrdinalIgnoreCase))
                        .Then(SimResult.Similar)
                    .Case<AggregateEntity>(ae1 =>
                        second.Match().Yield<SimResult>()
                          .Case<AggregateEntity>(ae2 => ae1.IsSimilarTo(ae2))
                          .Case<IEntity>(e2 => new SimResult(ae1.Any(entity => entity.IsSimilarTo(e2))))
                        .Result())
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
        /// Determines if two IAggregateEntity instances are similar.
        /// </summary>
        /// <param name="first">The first IAggregateEntity</param>
        /// <param name="second">The second IAggregateEntity</param>
        /// <returns>True if the given IAggregateEntity instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( Lookup.IsSimilarTo(e1, e2) ) { ... }</code>
        /// <code>if ( e1.IsSimilarTo(e2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IAggregateEntity first, IAggregateEntity second) {
            var simResults = from e1 in first
                             from e2 in second
                             select e1.IsSimilarTo(e2) into result
                             group result by (bool)result into byResult
                             let Count = (double)byResult.Count()
                             orderby Count descending
                             select new { byResult.Key, Count };
            return new SimResult(simResults.First().Key, simResults.Skip(1).Aggregate(simResults.First().Count, (ratioSoFar, current) => ratioSoFar /= current.Count));
        }
        /// <summary>
        /// Determines if two IVerbal instances are similar.
        /// </summary>
        /// <param name="first">The first IVerbal</param>
        /// <param name="second">The second IVerbal</param>
        /// <returns>True if the given IVerbal instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( Lookup.IsSimilarTo(v1, v2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(d1, d2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(d1, d2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(n1, np2) ) { ... }</code>
        /// <code>if ( n1.IsSimilarTo(np2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Noun first, NounPhrase second) {
            var phraseNouns = second.Words.OfNoun();
            return new SimResult(phraseNouns.Count() == 1 && phraseNouns.First().IsSynonymFor(first));
        }
        /// <summary>
        /// Determines if the provided NounPhrase is similar to the provided Noun.
        /// </summary>
        /// <param name="first">The NounPhrase.</param>
        /// <param name="second">The Noun.</param>
        /// <returns>True if the provided NounPhrase is similar to the provided Noun, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(np1, n2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(n1, n2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(v1, v2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(vp1, v2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(v1, vp2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Verb first, VerbPhrase second) {
            return new SimResult(second.Words.TakeWhile(w => !(w is ToLinker)).OfVerb().Any(v => v.IsSynonymFor(first)));//This is kind of rough.
        }

        /// <summary>
        /// Determines if two NounPhrases are similar.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>True if the given NounPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(np1, np2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this VerbPhrase first, VerbPhrase second) {

            //Look into refining this
            List<Verb> leftHandVerbs = first.Words.OfVerb().ToList();
            List<Verb> rightHandVerbs = second.Words.OfVerb().ToList();

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
        /// <code>if ( Lookup.IsSimilarTo(a1, a2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(ap1, a2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adjective first, AdjectivePhrase second) {
            return new SimResult(second.Words.OfAdjective().Any(adj => adj.IsSynonymFor(first)));
        }


        /// <summary>
        /// Determines if two AdjectivePhrase are similar.
        /// </summary>
        /// <param name="first">The first AdjectivePhrase</param>
        /// <param name="second">The second AdjectivePhrase</param>
        /// <returns>True if the given AdjectivePhrase are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdjectivePhrase first, AdjectivePhrase second) {

            //Look into refining this
            List<Adjective> leftHandAdjectives = first.Words.OfAdjective().ToList();
            List<Adjective> rightHandAdjectives = second.Words.OfAdjective().ToList();

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
        /// <code>if ( Lookup.IsSimilarTo(a1, a2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(ap1, a2) ) { ... }</code>
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
        /// <code>if ( Lookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adverb first, AdverbPhrase second) {
            return new SimResult(second.Words.OfAdverb().Any(adj => adj.IsSynonymFor(first)));
            // Must refine this to check for negators and modals which will potentially invert the meaning.
        }

        /// <summary>
        /// Determines if two AdverbPhrases are similar.
        /// </summary>
        /// <param name="first">The first AdverbPhrase</param>
        /// <param name="second">The second AdverbPhrase</param>
        /// <returns>True if the given AdverbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdverbPhrase first, AdverbPhrase second) {

            //Look into refining this
            List<Adverb> leftHandAdjectives = first.Words.OfAdverb().ToList();
            List<Adverb> rightHandAdjectives = second.Words.OfAdverb().ToList();

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

            if ((outer.Words.OfNoun().Count() != 0) && (inner.Words.OfNoun().Count() != 0)) {
                foreach (var o in outer.Words.OfNoun()) {
                    foreach (var i in inner.Words.OfNoun()) {
                        if (i.IsSimilarTo(o))
                            similarCount += 0.7;
                    }
                    var scaleFactor = inner.Words.OfNoun().Count() * outer.Words.OfNoun().Count();
                    return (similarCount / scaleFactor == 0 ? 1 : scaleFactor);
                }

            }
            return 0d;
        }

        #endregion


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

    }
}
