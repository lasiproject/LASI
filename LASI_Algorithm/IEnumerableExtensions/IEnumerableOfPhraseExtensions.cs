using LASI.Algorithm.LexicalStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfPhraseExtensions
    {
        public static IEnumerable<Phrase> Between(this Phrase phrase, Phrase other) {
            return phrase.ParentSentence.GetPhrasesAfter(phrase).TakeWhile(r => !r.Equals(other)).ToList();
        }
        public static IEnumerable<Phrase> GetPhrasesAfter(this IEnumerable<Phrase> phrases, Phrase phrase) {
            return phrases.SkipWhile(r => r != phrase).Skip(1);
        }
        /// <summary>
        /// Returns all NounPhrases in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter</param>
        /// <returns>All NounPhrases in the sequence</returns>
        public static IEnumerable<NounPhrase> GetNounPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<NounPhrase>();
        }

        /// <summary>
        /// Returns all VerbPhrases in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter.</param>
        /// <returns>All VerbPhrases in the sequence</returns>
        public static IEnumerable<VerbPhrase> GetVerbPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<VerbPhrase>();
        }
        /// <summary>
        /// Returns all AdverbPhrases in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdverbPhrases in the sequence</returns>
        public static IEnumerable<AdverbPhrase> GetAdverbPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdverbPhrase>();
        }
        /// <summary>
        /// Returns all ConjunctionPhrases in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter</param>
        /// <returns>All ConjunctionPhrases in the sequence</returns>
        public static IEnumerable<ConjunctionPhrase> GetConjunctionPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<ConjunctionPhrase>();
        }
        /// <summary>
        /// Returns all PrepositionalPhrases in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PrepositionalPhrases in the sequence</returns>
        public static IEnumerable<PrepositionalPhrase> GetPrepositionalPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<PrepositionalPhrase>();
        }
        /// <summary>
        /// Returns all PronounPhrase in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PronounPhrase in the sequence</returns>
        public static IEnumerable<PronounPhrase> GetPronounPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<PronounPhrase>();
        }
        /// <summary>
        /// Returns all AdjectivePhrases in the sequence.
        /// </summary>
        /// <param name="componentPhrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdjectivePhrases in the sequence</returns>
        public static IEnumerable<AdjectivePhrase> GetAdjectivePhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdjectivePhrase>();
        }
    }
}