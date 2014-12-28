using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Core.PatternMatching;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics
{
    public static partial class Lookup
    {
        /// <summary>
        /// Determines if two IAdverbial instances are similar.
        /// </summary>
        /// <param name="first">The first IAdverbial</param>
        /// <param name="second">The second IAdverbial</param>
        /// <returns> <c>true</c> if the given IAdverbial instances are similar; otherwise, <c>false</c>.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( Lookup.IsSimilarTo(d1, d2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static Similarity IsSimilarTo(this IAdverbial first, IAdverbial second) {
            return Similarity.FromBoolean(
                first.Text.EqualsIgnoreCase(second.Text) ||
                first.Match().Yield<bool>()
                    .Case((Adverb a1) =>
                        second.Match().Yield<bool>()
                            .Case((Adverb a2) => a1.IsSynonymFor(a2))
                            .Case((AdverbPhrase ap2) => ap2.IsSimilarTo(a1))
                            .Result())
                    .Case((AdverbPhrase ap1) =>
                        second.Match().Yield<bool>()
                            .Case((AdverbPhrase ap2) => ap1.IsSimilarTo(ap2))
                            .Case((Adverb a2) => ap1.IsSimilarTo(a2))
                            .Result())
                    .Result());
        }
        /// <summary>
        /// Determines if the two provided Adverb instances are similar.
        /// </summary>
        /// <param name="first">The first Adverb.</param>
        /// <param name="second">The second Adverb.</param>
        /// <returns> <c>true</c> if the first Adverb is similar to the second; otherwise, <c>false</c>.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(a1, a2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static Similarity IsSimilarTo(this Adverb first, Adverb second) {
            return new Similarity(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided AdverbPhrase is similar to the provided Adverb.
        /// </summary>
        /// <param name="first">The AdverbPhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns> <c>true</c> if the provided AdverbPhrase is similar to the provided Adverb; otherwise, <c>false</c>.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(ap1, a2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static Similarity IsSimilarTo(this AdverbPhrase first, Adverb second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Adverb is similar to the provided AdverbPhrase.
        /// </summary>
        /// <param name="first">The Adverb.</param>
        /// <param name="second">The AdverbPhrase.</param>
        /// <returns> <c>true</c> if the provided Adverb is similar to the provided AdverbPhrase; otherwise, <c>false</c>.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static Similarity IsSimilarTo(this Adverb first, AdverbPhrase second) {
            return new Similarity(second.Words.OfAdverb().Any(a => a.IsSynonymFor(first)));
            // Must refine this to check for negators and modals which will potentially invert the meaning.
        }
        /// <summary>
        /// Determines if two AdverbPhrases are similar.
        /// </summary>
        /// <param name="first">The first AdverbPhrase</param>
        /// <param name="second">The second AdverbPhrase</param>
        /// <returns> <c>true</c> if the given AdverbPhrases are similar; otherwise, <c>false</c>.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static Similarity IsSimilarTo(this AdverbPhrase first, AdverbPhrase second) {
            var percentMatched =
                first.Words.OfAdverb()
                .Zip(second.Words.OfAdverb(),
                    (a, b) => a.IsSynonymFor(b))
                .PercentOf();
            return new Similarity(first == second || percentMatched > SIMILARITY_THRESHOLD);
        }
    }
}