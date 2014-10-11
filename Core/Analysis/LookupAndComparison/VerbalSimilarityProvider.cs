using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Heuristics
{
    public static partial class Lookup
    {
        /// <summary>e
        /// Determines if two IVerbal instances are similar.
        /// </summary>
        /// <param name="first">The first IVerbal</param>
        /// <param name="second">The second IVerbal</param>
        /// <returns>True if the given IVerbal instances are similar; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if (Lookup.IsSimilarTo(v1, v2)) { ... }</code>
        /// <code>if (v1.IsSimilarTo(v2)) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimilarityResult IsSimilarTo(this IVerbal first, IVerbal second) {
            return
                first.Match().Yield<SimilarityResult>()
                    .When(first.Text.EqualsIgnoreCase(second.Text))
                    .Then(SimilarityResult.Similar)
                    .With((Verb v1) =>
                        second.Match().Yield<SimilarityResult>()
                          .With((Verb v2) => v1.IsSimilarTo(v2))
                          .With((VerbPhrase vp2) => v1.IsSimilarTo(vp2))
                        .Result())
                    .With((VerbPhrase vp1) =>
                        second.Match().Yield<SimilarityResult>()
                          .With((VerbPhrase vp2) => vp1.IsSimilarTo(vp2))
                          .With((Verb v2) => vp1.IsSimilarTo(v2))
                    .Result())
                .Result();
        }
        /// <summary>
        /// Determines if the two provided Verb instances are similar.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb.</param>
        /// <returns>True if the first Verb is similar to the second; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(v1, v2)) { ... }</code>
        /// <code>if (v1.IsSimilarTo(v2)) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimilarityResult IsSimilarTo(this Verb first, Verb second) {
            return new SimilarityResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided VerbPhrase is similar to the provided Verb.
        /// </summary>
        /// <param name="first">The VerbPhrase.</param>
        /// <param name="second">The Verb.</param>
        /// <returns>True if the provided VerbPhrase is similar to the provided Verb; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(vp1, v2)) { ... }</code>
        /// <code>if (vp1.IsSimilarTo(v2)) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimilarityResult IsSimilarTo(this VerbPhrase first, Verb second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Verb is similar to the provided VerbPhrase.
        /// </summary>
        /// <param name="first">The Verb.</param>
        /// <param name="second">The VerbPhrase.</param>
        /// <returns>True if the provided Verb is similar to the provided VerbPhrase; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(v1, vp2)) { ... }</code>
        /// <code>if (v1.IsSimilarTo(vp2)) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimilarityResult IsSimilarTo(this Verb first, VerbPhrase second) {
            //This is rough and needs to be enhanced.
            return new SimilarityResult(second.Words
                .TakeWhile(w => !(w is ToLinker)) // Collect all words in the phrase cutting short when and if an infinitive precedant is found.
                .OfVerb().Any(v => v.IsSynonymFor(first))); // If an infinitive is found, it will be the local direct object of the verb phrase.
        }
        /// <summary>
        /// Determines if two VerbPhrases are similar.
        /// </summary>
        /// <param name="first">The first VerbPhrase</param>
        /// <param name="second">The second VerbPhrase</param>
        /// <returns>True if the given VerbPhrases are similar; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(vp1, vp2)) { ... }</code>
        /// <code>if (vp1.IsSimilarTo(vp2)) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimilarityResult IsSimilarTo(this VerbPhrase first, VerbPhrase second) {
            //Look into refining this
            var leftHandVerbs = first.Words.OfVerb().ToList();
            var rightHandVerbs = second.Words.OfVerb().ToList();

            try {
                // TODO: make this fuzzier.
                return new SimilarityResult(leftHandVerbs.Count == rightHandVerbs.Count &&
                    leftHandVerbs.Zip(rightHandVerbs, (x, y) => x.IsSynonymFor(y))
                        .All(synonymous => synonymous)
                    );
            }
            catch (NullReferenceException x) {
                Output.WriteLine(x.Message);
                return SimilarityResult.Dissimilar;
            }
        }
    }
}
