using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Algorithm.Patternization;
using System.Threading.Tasks;

namespace LASI.Algorithm.ComparativeHeuristics
{

    public static class VerbalSimilarityProvider
    {
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
        public static SimilarityResult IsSimilarTo(this IVerbal first, IVerbal second) {
            return first.Match().Yield<bool>()
                    .When(first.Text.ToUpper() == second.Text.ToUpper()).Then(true)
                    .Case<Verb>(v1 =>
                        second.Match().Yield<bool>()
                          .Case<Verb>(v2 => v1.IsSynonymFor(v2))
                          .Case<VerbPhrase>(vp2 => v1.IsSimilarTo(vp2))
                        .Result())
                    .Case<VerbPhrase>(vp1 =>
                        second.Match().Yield<bool>()
                          .Case<VerbPhrase>(vp2 => vp1.IsSimilarTo(vp2))
                          .Case<Verb>(v2 => vp1.IsSimilarTo(v2))
                        .Result())
                    .Result();
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
        public static SimilarityResult IsSimilarTo(this Verb first, Verb second) {
            return new SimilarityResult(first.IsSynonymFor(second));
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
        public static SimilarityResult IsSimilarTo(this VerbPhrase first, Verb second) {
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
        public static SimilarityResult IsSimilarTo(this Verb first, VerbPhrase second) {
            return new SimilarityResult(
                second.Words.TakeWhile(w => !(w is ToLinker))
                .OfVerb()
                .Any(v => v.IsSynonymFor(first)));//This is kind of rough.
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
        public static SimilarityResult IsSimilarTo(this VerbPhrase first, VerbPhrase second) {

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
                    return SimilarityResult.Dissimilar;
                }
            }
            return new SimilarityResult(result);
        }

    }
}
