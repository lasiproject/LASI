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
        /// Returns all Phrases in the document which are in between the provided Phrases. The range of the resulting sequence is neither upper nor lower inclusive.
        /// </summary>
        /// <param name="start">The exclusive lower bound of the range to aggregate.</param>
        /// <param name="end">The exclusive upper bound of the range to aggregate.</param>
        /// <returns>All Phrases in the document which are in between the provided Phrases.
        /// The range of the resulting sequence is neither upper nor lower incluse or null if the Phrases are improperly ordered or are in different documents. 
        /// If the given phrases are adjacent, an empty sequence will be returned.</returns>
        public static IEnumerable<Phrase> Between(this Phrase start, Phrase end) {
            var phrasesInDocument = start.Document.Phrases.ToList();//Converted to list for fast indexing in the following calls.
            var startIndex = phrasesInDocument.IndexOf(start);
            var endIndex = phrasesInDocument.Skip(startIndex).ToList().IndexOf(end);
            try {
                return phrasesInDocument.GetRange(startIndex + 1, startIndex - (endIndex - 1));
            }
            catch (ArgumentOutOfRangeException) {
                return null;
            }
        }
        /// <summary>
        /// Returns all Phrases in the document which are in between the provided starting Phrase and the first phrase which matches the provided selector function.
        /// The range of the resulting sequence is neither upper nor lower inclusive.
        /// </summary>
        /// <param name="start">The exclusive lower bound of the range to aggregate.</param>
        /// <param name="end">The function which is used to test each subsequent phrase returning true when a match verbalSelector is found.</param>
        /// <returns>All Phrases in the document which are in between the provided start Phrase and the first Phrase for which the provided predicate function returns true.
        /// The range of the resulting sequence is neither upper nor lower incluse. If no matching phrase exists after the starting phrase, null is returned. 
        /// If the given phrases are adjacent, an empty sequence will be returned.</returns>
        public static IEnumerable<Phrase> Between(this Phrase start, Func<Phrase, bool> endSelector) {
            var phrasesInDocument = start.Document.Phrases.ToList();//Converted to list for fast indexing in the following calls.
            var startIndex = phrasesInDocument.IndexOf(start);
            var endIndex = phrasesInDocument.Skip(startIndex).TakeWhile(phrase => !endSelector(phrase)).Count();
            try {
                return phrasesInDocument.GetRange(startIndex + 1, startIndex - endIndex);
            }
            catch (ArgumentOutOfRangeException) {
                return null;
            }
        }

        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the given Phrase in the source sequence.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="phrase">The exclusive lower bound of the desired subset of Phrases.</param>
        /// <returns>A new sequence containing all of the Phrases which follow the given Phrase in the source sequence.</returns>
        public static IEnumerable<Phrase> GetPhrasesAfter(this IEnumerable<Phrase> phrases, Phrase phrase) {
            return phrases.SkipWhile(r => r != phrase).Skip(1);
        }
        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the first Phrase in the source sequence which matches the provided selector function.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="phrase">The function which will select be used to select the exclusive lower bound of the new sequence.</param>
        /// <returns>A new sequence containing all of the Phrases which follow the first Phrase in the source sequence which matches the provided selector function..</returns>
        public static IEnumerable<Phrase> GetPhrasesAfter(this IEnumerable<Phrase> phrases, Func<Phrase, bool> startSelector) {
            return phrases.SkipWhile(r => !startSelector(r)).Skip(1);
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