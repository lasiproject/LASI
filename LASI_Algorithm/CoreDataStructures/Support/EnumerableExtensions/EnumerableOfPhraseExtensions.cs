using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of Phrase instances.
    /// </summary>
    /// <see cref="Phrase"/>
    public static class EnumerableOfPhraseExtensions
    {
        /// <summary>
        /// Returns all Phrases in the document which are in between the provided Phrases. The range of the resulting sequence is neither upper nor lower inclusive.
        /// </summary>
        /// <param name="after">The exclusive lower bound of the range to aggregate.</param>
        /// <param name="before">The exclusive upper bound of the range to aggregate.</param>
        /// <returns>All Phrases in the document which are in between the provided Phrases.
        /// The range of the resulting sequence is neither upper nor lower incluse. 
        /// If the Phrases are improperly ordered, are in different documents or if the given phrases are adjacent, an empty sequence will be returned.</returns>
        public static IEnumerable<Phrase> Between(this Phrase after, Phrase before) {
            return after.Document.Phrases.Contains(before) ?
                after.Document.Phrases.SkipWhile(p => p != after).Skip(1).TakeWhile(p => p != before) :
                Enumerable.Empty<Phrase>();
        }
        /// <summary>
        /// Returns all Phrases in the document which are in between the provided starting Phrase and the first phrase which matches the provided selector function.
        /// The range of the resulting sequence is neither upper nor lower inclusive.
        /// </summary>
        /// <param name="after">The exclusive lower bound of the range to aggregate.</param>
        /// <param name="endSelector">The function which is used to test each subsequent phrase returning true when a match verbalSelector is found.</param>
        /// <returns>All Phrases in the document which are in between the provided start Phrase and the first Phrase for which the provided predicate function returns true.
        /// The range of the resulting sequence is neither upper nor lower incluse.
        /// If no matching phrase exists after the starting phrase or the given start Phrase is immediately followed by a Phrase matching the end selector function,
        /// an empty sequence will be returned.an empty sequence is returned is returned. 
        /// </returns>
        public static IEnumerable<Phrase> Between(this Phrase after, Func<Phrase, bool> endSelector) {
            return after.Document.Phrases.Any(endSelector) ?
               after.Document.Phrases.SkipWhile(p => p != after).Skip(1).TakeWhile(p => !endSelector(p)) :
               Enumerable.Empty<Phrase>();
        }


        #region Sequential Implementations

        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the given Phrase in the source sequence.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="after">The exclusive lower bound of the desired subset of Phrases.</param>
        /// <returns>A new sequence containing all of the Phrases which follow the given Phrase in the source sequence.</returns>
        public static IEnumerable<Phrase> PhrasesFollowing(this IEnumerable<Phrase> phrases, Phrase after) {
            return phrases.SkipWhile(r => r != after).Skip(1);
        }
        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the first Phrase in the source sequence which matches the provided selector function.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="startSelector">The function which will select be used to select the exclusive lower bound of the new sequence.</param>
        /// <returns>A new sequence containing all of the Phrases which follow the first Phrase in the source sequence which matches the provided selector function..</returns>
        public static IEnumerable<Phrase> PhrasesFollowing(this IEnumerable<Phrase> phrases, Func<Phrase, bool> startSelector) {
            return phrases.SkipWhile(r => !startSelector(r)).Skip(1);
        }
        /// <summary>
        /// Returns all NounPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All NounPhrases in the sequence</returns>
        public static IEnumerable<NounPhrase> OfNounPhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<NounPhrase>();
        }
        /// <summary>
        /// Returns all VerbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter.</param>
        /// <returns>All VerbPhrases in the sequence</returns>
        public static IEnumerable<VerbPhrase> OfVerbPhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<VerbPhrase>();
        }
        /// <summary>
        /// Returns all AdverbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdverbPhrases in the sequence</returns>
        public static IEnumerable<AdverbPhrase> OfAdverbPhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdverbPhrase>();
        }
        /// <summary>
        /// Returns all ConjunctionPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All ConjunctionPhrases in the sequence</returns>
        public static IEnumerable<ConjunctionPhrase> OfConjunctionPhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<ConjunctionPhrase>();
        }
        /// <summary>
        /// Returns all PrepositionalPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PrepositionalPhrases in the sequence</returns>
        public static IEnumerable<PrepositionalPhrase> OfPrepositionalPhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<PrepositionalPhrase>();
        }
        /// <summary>
        /// Returns all PronounPhrase in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PronounPhrase in the sequence</returns>
        public static IEnumerable<PronounPhrase> OfPronounPhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<PronounPhrase>();
        }
        /// <summary>
        /// Returns all AdjectivePhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdjectivePhrases in the sequence</returns>
        public static IEnumerable<AdjectivePhrase> OfAdjectivePhrase(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdjectivePhrase>();
        }

        #endregion

        #region Parallel Implementations
        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the given Phrase in the source sequence.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="after">The exclusive lower bound of the desired subset of Phrases.</param>
        /// <returns>A new sequence containing all of the Phrases which follow the given Phrase in the source sequence.</returns>
        public static ParallelQuery<Phrase> PhrasesFollowing(this ParallelQuery<Phrase> phrases, Phrase after) {
            return phrases.SkipWhile(r => r != after).Skip(1);
        }
        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the first Phrase in the source sequence which matches the provided selector function.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="startSelector">The function which will select be used to select the exclusive lower bound of the new sequence.</param>
        /// <returns>A new sequence containing all of the Phrases which follow the first Phrase in the source sequence which matches the provided selector function..</returns>
        public static ParallelQuery<Phrase> PhrasesFollowing(this ParallelQuery<Phrase> phrases, Func<Phrase, bool> startSelector) {
            return phrases.SkipWhile(r => !startSelector(r)).Skip(1);
        }
        /// <summary>
        /// Returns all NounPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All NounPhrases in the sequence</returns>
        public static ParallelQuery<NounPhrase> OfNounPhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<NounPhrase>();
        }
        /// <summary>
        /// Returns all VerbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter.</param>
        /// <returns>All VerbPhrases in the sequence</returns>
        public static ParallelQuery<VerbPhrase> OfVerbPhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<VerbPhrase>();
        }
        /// <summary>
        /// Returns all AdverbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdverbPhrases in the sequence</returns>
        public static ParallelQuery<AdverbPhrase> OfAdverbPhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<AdverbPhrase>();
        }
        /// <summary>
        /// Returns all ConjunctionPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All ConjunctionPhrases in the sequence</returns>
        public static ParallelQuery<ConjunctionPhrase> OfConjunctionPhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<ConjunctionPhrase>();
        }
        /// <summary>
        /// Returns all PrepositionalPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PrepositionalPhrases in the sequence</returns>
        public static ParallelQuery<PrepositionalPhrase> OfPrepositionalPhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<PrepositionalPhrase>();
        }
        /// <summary>
        /// Returns all PronounPhrase in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PronounPhrase in the sequence</returns>
        public static ParallelQuery<PronounPhrase> OfPronounPhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<PronounPhrase>();
        }
        /// <summary>
        /// Returns all AdjectivePhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdjectivePhrases in the sequence</returns>
        public static ParallelQuery<AdjectivePhrase> OfAdjectivePhrase(this ParallelQuery<Phrase> phrases) {
            return phrases.OfType<AdjectivePhrase>();
        }
        #endregion


    }
}