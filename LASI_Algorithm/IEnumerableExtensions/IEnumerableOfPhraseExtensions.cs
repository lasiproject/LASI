using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfPhraseExtensions
    {
        /// <summary>
        /// Returns all NounPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of phrases to filter</param>
        /// <returns>All NounPhrases in the sequence</returns>
        public static IEnumerable<NounPhrase> GetNounPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<NounPhrase>();
        }
        ///// <summary>
        ///// Returns all VerbPhrases in the sequence.
        ///// </summary>
        ///// <param name="phrases">The sequence of phrases to filter</param>
        ///// <returns>All VerbPhrases in the sequence</returns>
        //public static IEnumerable<VerbPhrase> GetVerbPhrases(this IEnumerable<Phrase> phrases) {
        //    return phrases.OfType<VerbPhrase>();
        //}
        /// <summary>
        /// Returns all TransitiveVerbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of phrases to filter</param>
        /// <returns>All TransitiveVerbPhrases in the sequence</returns>
        public static IEnumerable<TransitiveVerbPhrase> GetVerbPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<TransitiveVerbPhrase>();
        }
        /// <summary>
        /// Returns all AdverbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of phrases to filter</param>
        /// <returns>All AdverbPhrases in the sequence</returns>
        public static IEnumerable<AdverbPhrase> GetAdverbPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdverbPhrase>();
        }
        /// <summary>
        /// Returns all ConjunctionPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of phrases to filter</param>
        /// <returns>All ConjunctionPhrases in the sequence</returns>
        public static IEnumerable<ConjunctionPhrase> GetConjunctionPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<ConjunctionPhrase>();
        }
        /// <summary>
        /// Returns all AdjectivePhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of phrases to filter</param>
        /// <returns>All AdjectivePhrases in the sequence</returns>
        public static IEnumerable<AdjectivePhrase> GetAdjectivePhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdjectivePhrase>();
        }
    }
}