using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of Phrase instances.
    /// </summary>
    /// <seealso cref="Phrase"/>
    public static partial class LexicalEnumerable
    {
        /// <summary>
        /// Returns all Phrases in the document which are in between the provided Phrases. The range
        /// of the resulting sequence is neither upper nor lower inclusive.
        /// </summary>
        /// <param name="after">The exclusive lower bound of the range to aggregate.</param>
        /// <param name="before">The exclusive upper bound of the range to aggregate.</param>
        /// <returns>
        /// All Phrases in the document which are in between the provided Phrases. The range of the
        /// resulting sequence is neither upper nor lower inclusive. If the Phrases are improperly
        /// ordered, are in different documents or if the given phrases are adjacent, an empty
        /// sequence will be returned.
        /// </returns>
        public static IEnumerable<Phrase> Between(this Phrase after, Phrase before) => after.Between(phrase => phrase == before);

        /// <summary>
        /// Returns all Phrases in the document which are in between the provided starting Phrase
        /// and the first phrase which matches the provided selector function. The range of the
        /// resulting sequence is neither upper nor lower inclusive.
        /// </summary>
        /// <param name="after">The exclusive lower bound of the range to aggregate.</param>
        /// <param name="endSelector">
        /// The function which is used to test each subsequent phrase returning true when a match
        /// verbalSelector is found.
        /// </param>
        /// <returns>
        /// All Phrases in the document which are in between the provided start Phrase and the first
        /// Phrase for which the provided predicate function returns true. The range of the
        /// resulting sequence is neither upper nor lower inclusive. If no matching phrase exists
        /// after the starting phrase or the given start Phrase is immediately followed by a Phrase
        /// matching the end selector function, an empty sequence will be returned.an empty sequence
        /// is returned is returned.
        /// </returns>
        public static IEnumerable<Phrase> Between(this Phrase after, Func<Phrase, bool> endSelector) => after.Document.Phrases.Any(endSelector) ?
               after.Document.Phrases.SkipWhile(p => p != after).Skip(1).TakeWhile(p => !endSelector(p)) :
               Empty<Phrase>();

        #region Sequential Implementations

        /// <summary>
        /// Returns all AdjectivePhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdjectivePhrases in the sequence</returns>
        public static IEnumerable<AdjectivePhrase> OfAdjectivePhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<AdjectivePhrase>();

        /// <summary>
        /// Returns all AdverbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdverbPhrases in the sequence</returns>
        public static IEnumerable<AdverbPhrase> OfAdverbPhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<AdverbPhrase>();

        /// <summary>
        /// Returns all ConjunctionPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All ConjunctionPhrases in the sequence</returns>
        public static IEnumerable<ConjunctionPhrase> OfConjunctionPhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<ConjunctionPhrase>();

        /// <summary>
        /// Returns all NounPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All NounPhrases in the sequence</returns>
        public static IEnumerable<NounPhrase> OfNounPhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<NounPhrase>();

        /// <summary>
        /// Returns all PrepositionalPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PrepositionalPhrases in the sequence</returns>
        public static IEnumerable<PrepositionalPhrase> OfPrepositionalPhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<PrepositionalPhrase>();

        /// <summary>
        /// Returns all PronounPhrase in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PronounPhrase in the sequence</returns>
        public static IEnumerable<PronounPhrase> OfPronounPhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<PronounPhrase>();

        /// <summary>
        /// Returns all VerbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter.</param>
        /// <returns>All VerbPhrases in the sequence</returns>
        public static IEnumerable<VerbPhrase> OfVerbPhrase(this IEnumerable<Phrase> phrases) => phrases.OfType<VerbPhrase>();

        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the given Phrase in
        /// the source sequence.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="after">The exclusive lower bound of the desired subset of Phrases.</param>
        /// <returns>
        /// A new sequence containing all of the Phrases which follow the given Phrase in the source sequence.
        /// </returns>
        public static IEnumerable<Phrase> PhrasesAfter(this IEnumerable<Phrase> phrases, Phrase after) => phrases.SkipWhile(r => r != after).Skip(1);

        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the first Phrase in
        /// the source sequence which matches the provided selector function.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="startSelector">
        /// The function which will select be used to select the exclusive lower bound of the new sequence.
        /// </param>
        /// <returns>
        /// A new sequence containing all of the Phrases which follow the first Phrase in the source
        /// sequence which matches the provided selector function..
        /// </returns>
        public static IEnumerable<Phrase> PhrasesAfter(this IEnumerable<Phrase> phrases, Func<Phrase, bool> startSelector) => phrases.SkipWhile(r => !startSelector(r)).Skip(1);

        #endregion Sequential Implementations

        #region Parallel Implementations

        /// <summary>
        /// Returns all AdjectivePhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdjectivePhrases in the sequence</returns>
        public static ParallelQuery<AdjectivePhrase> OfAdjectivePhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<AdjectivePhrase>();

        /// <summary>
        /// Returns all AdverbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All AdverbPhrases in the sequence</returns>
        public static ParallelQuery<AdverbPhrase> OfAdverbPhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<AdverbPhrase>();

        /// <summary>
        /// Returns all ConjunctionPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All ConjunctionPhrases in the sequence</returns>
        public static ParallelQuery<ConjunctionPhrase> OfConjunctionPhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<ConjunctionPhrase>();

        /// <summary>
        /// Returns all NounPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All NounPhrases in the sequence</returns>
        public static ParallelQuery<NounPhrase> OfNounPhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<NounPhrase>();

        /// <summary>
        /// Returns all PrepositionalPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PrepositionalPhrases in the sequence</returns>
        public static ParallelQuery<PrepositionalPhrase> OfPrepositionalPhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<PrepositionalPhrase>();

        /// <summary>
        /// Returns all PronounPhrase in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter</param>
        /// <returns>All PronounPhrase in the sequence</returns>
        public static ParallelQuery<PronounPhrase> OfPronounPhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<PronounPhrase>();

        /// <summary>
        /// Returns all VerbPhrases in the sequence.
        /// </summary>
        /// <param name="phrases">The sequence of componentPhrases to filter.</param>
        /// <returns>All VerbPhrases in the sequence</returns>
        public static ParallelQuery<VerbPhrase> OfVerbPhrase(this ParallelQuery<Phrase> phrases) => phrases.OfType<VerbPhrase>();

        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the given Phrase in
        /// the source sequence.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="after">The exclusive lower bound of the desired subset of Phrases.</param>
        /// <returns>
        /// A new sequence containing all of the Phrases which follow the given Phrase in the source sequence.
        /// </returns>
        public static ParallelQuery<Phrase> PhrasesAfter(this ParallelQuery<Phrase> phrases, Phrase after) => phrases.SkipWhile(r => r != after).Skip(1);

        /// <summary>
        /// Returns a new sequence containing all of the Phrases which follow the first Phrase in
        /// the source sequence which matches the provided selector function.
        /// </summary>
        /// <param name="phrases">The source sequence of Phrases.</param>
        /// <param name="startSelector">
        /// The function which will select be used to select the exclusive lower bound of the new sequence.
        /// </param>
        /// <returns>
        /// A new sequence containing all of the Phrases which follow the first Phrase in the source
        /// sequence which matches the provided selector function..
        /// </returns>
        public static ParallelQuery<Phrase> PhrasesAfter(this ParallelQuery<Phrase> phrases, Func<Phrase, bool> startSelector) => phrases.SkipWhile(r => !startSelector(r)).Skip(1);

        #endregion Parallel Implementations
    }
}