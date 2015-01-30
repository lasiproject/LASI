using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Heuristics
{
    public static partial class Lexicon
    {
        /// <summary>
        /// Determines if two IDescriptor instances are similar.
        /// </summary>
        /// <param name="first">The first IDescriptor</param>
        /// <param name="second">The second IDescriptor</param>
        /// <returns> <c>true</c> if the given IDescriptor instances are similar; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this IDescriptor first, IDescriptor second) {
            return first.Match()
                    .When(first.Text.EqualsIgnoreCase(second.Text))
                    .Then(() => Similarity.Similar)
                    .Case((Adjective a1) => second.Match()
                           .Case((Adjective a2) => Similarity.FromBoolean(a1.IsSynonymFor(a2)))
                           .Case((AdjectivePhrase ap2) => ap2.IsSimilarTo(a1))
                       .Result())
                    .Case((AdjectivePhrase ap1) => second.Match()
                            .Case((AdjectivePhrase ap2) => ap1.IsSimilarTo(ap2))
                            .Case((Adjective a2) => ap1.IsSimilarTo(a2))
                        .Result())
                    .Result();
        }
        /// <summary>
        /// Determines if the two provided Adjective instances are similar.
        /// </summary>
        /// <param name="first">The first Adjective.</param>
        /// <param name="second">The second Adjective.</param>
        /// <returns> <c>true</c> if the first Adjective is similar to the second; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this Adjective first, Adjective second) {
            return Similarity.FromBoolean(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided AdjectivePhrase is similar to the provided Adjective.
        /// </summary>
        /// <param name="first">The AdjectivePhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns> <c>true</c> if the provided AdjectivePhrase is similar to the provided Adjective; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this AdjectivePhrase first, Adjective second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Adjective is similar to the provided AdjectivePhrase.
        /// </summary>
        /// <param name="first">The Adjective.</param>
        /// <param name="second">The AdjectivePhrase.</param>
        /// <returns> <c>true</c> if the provided Adjective is similar to the provided AdjectivePhrase; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this Adjective first, AdjectivePhrase second) {
            return Similarity.FromBoolean(second.Words.OfAdjective().Any(adj => adj.IsSynonymFor(first)));
        }
        /// <summary>
        /// Determines if two AdjectivePhrase are similar.
        /// </summary>
        /// <param name="first">The first AdjectivePhrase</param>
        /// <param name="second">The second AdjectivePhrase</param>
        /// <returns> <c>true</c> if the given AdjectivePhrase are similar; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this AdjectivePhrase first, AdjectivePhrase second) {
            var result = first.Words
                .OfAdjective()
                .Zip(second.Words.OfAdjective(), (a, b) => a.IsSynonymFor(b))
                .Aggregate(new { T = 0, F = 0 }, (a, c) => new { T = a.T + (c ? 1 : 0), F = a.F + (c ? 0 : 1) });
            return Similarity.FromBoolean(first == second || ((float)(result.T / result.F + result.T) > SIMILARITY_THRESHOLD));
        }
    }
}
