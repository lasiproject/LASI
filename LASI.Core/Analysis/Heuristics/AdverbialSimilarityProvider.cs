using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    public static partial class Lexicon
    {
        /// <summary>
        /// Determines if two IAdverbial instances are similar.
        /// </summary>
        /// <param name="first">The first IAdverbial</param>
        /// <param name="second">The second IAdverbial</param>
        /// <returns><c>true</c> if the given IAdverbial instances are similar; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this IAdverbial first, IAdverbial second) => Similarity.FromBoolean(
                Equals(first, second) ||
                first.Text.EqualsIgnoreCase(second.Text) ||
                first.Match()
                    .Case((Adverb a1) =>
                        second.Match()
                            .Case((Adverb a2) => a1.IsSynonymFor(a2))
                            .Case((AdverbPhrase ap2) => ap2.IsSimilarTo(a1))
                            .Result())
                    .Case((AdverbPhrase ap1) =>
                        second.Match()
                            .Case((AdverbPhrase ap2) => ap1.IsSimilarTo(ap2))
                            .Case((Adverb a2) => ap1.IsSimilarTo(a2))
                            .Result())
                    .Result());

        /// <summary>
        /// Determines if the two provided Adverb instances are similar.
        /// </summary>
        /// <param name="first">The first Adverb.</param>
        /// <param name="second">The second Adverb.</param>
        /// <returns><c>true</c> if the first Adverb is similar to the second; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this Adverb first, Adverb second) => Similarity.FromBoolean(first.IsSynonymFor(second));

        /// <summary>
        /// Determines if the provided AdverbPhrase is similar to the provided Adverb.
        /// </summary>
        /// <param name="first">The AdverbPhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns>
        /// <c>true</c> if the provided AdverbPhrase is similar to the provided Adverb; otherwise, <c>false</c>.
        /// </returns>
        public static Similarity IsSimilarTo(this AdverbPhrase first, Adverb second) => second.IsSimilarTo(first);

        /// <summary>
        /// Determines if the provided Adverb is similar to the provided AdverbPhrase.
        /// </summary>
        /// <param name="first">The Adverb.</param>
        /// <param name="second">The AdverbPhrase.</param>
        /// <returns>
        /// <c>true</c> if the provided Adverb is similar to the provided AdverbPhrase; otherwise, <c>false</c>.
        /// </returns>
        public static Similarity IsSimilarTo(this Adverb first, AdverbPhrase second) => Similarity.FromBoolean(second.Words.OfAdverb().Any(a => a.IsSynonymFor(first)));

        /// <summary>
        /// Determines if two AdverbPhrases are similar.
        /// </summary>
        /// <param name="first">The first AdverbPhrase</param>
        /// <param name="second">The second AdverbPhrase</param>
        /// <returns><c>true</c> if the given AdverbPhrases are similar; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this AdverbPhrase first, AdverbPhrase second)
        {
            var percentMatched =
                first.Words.OfAdverb()
                .Zip(second.Words.OfAdverb(),
                    (a, b) => a.IsSynonymFor(b))
                .PercentTrue();
            return Similarity.FromBoolean(first == second || percentMatched / 100 > SimilarityThreshold);
        }
    }
}