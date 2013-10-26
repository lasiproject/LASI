using System;
using LASI.Core.Patternization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.ComparativeHeuristics.Morphemization;

namespace LASI.Core.ComparativeHeuristics
{
    public static class DescriptorSimilarityProvider
    {
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
        public static SimilarityResult IsSimilarTo(this IDescriptor first, IDescriptor second) {
            return new SimilarityResult(
                first.Match().Yield<bool>()
                    .When(first.Text.ToUpper() == second.Text.ToUpper())
                        .Then(true)
                    .Case<Adjective>(j1 =>
                        second.Match().Yield<bool>()
                            .Case<Adjective>(j2 => j1.IsSynonymFor(j2))
                            .Case<AdjectivePhrase>(jp2 => jp2.IsSimilarTo(j1))
                        .Result())
                    .Case<AdjectivePhrase>(jp1 =>
                        second.Match().Yield<bool>()
                          .Case<AdjectivePhrase>(jp2 => jp1.IsSimilarTo(jp2))
                          .Case<Adjective>(j2 => jp1.IsSimilarTo(j2))
                        .Result())
                    .Result());
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
        public static SimilarityResult IsSimilarTo(this Adjective first, Adjective second) {
            return new SimilarityResult(first.IsSynonymFor(second));
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
        public static SimilarityResult IsSimilarTo(this AdjectivePhrase first, Adjective second) {
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
        public static SimilarityResult IsSimilarTo(this Adjective first, AdjectivePhrase second) {
            return new SimilarityResult(second.Words.OfAdjective().Any(adj => adj.IsSynonymFor(first)));
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
        public static SimilarityResult IsSimilarTo(this AdjectivePhrase first, AdjectivePhrase second) {
            var synResults =
                first.Words.OfAdjective()
                .Zip(second.Words.OfAdjective(), (a, b) => a.IsSynonymFor(b))
                .Aggregate(new { Trues = 0f, Falses = 0f }, (a, c) => new { Trues = a.Trues + (c ? 1 : 0), Falses = a.Falses + (c ? 0 : 1) });
            return new SimilarityResult(first == second || synResults.Trues / (synResults.Falses + synResults.Trues) > Lookup.SIMILARITY_THRESHOLD);
        }
    }
}
