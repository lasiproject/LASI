﻿using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    using Validate = LASI.Utilities.Validation.Validate;

    /// <summary>
    /// Defines extension methods for sequences of Word instances.
    /// </summary>
    /// <seealso cref="Word"/>
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementations

        /// <summary>
        /// Returns all words in the Word collection which come after the given word.
        /// </summary>
        /// <param name="words">A sequence of word objects</param>
        /// <param name="startAfter">The delimiting word</param>
        /// <returns>All words in the Word collection which come after the given word.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when words or startAfter is null.
        /// </exception>
        public static IEnumerable<Word> WordsAfter(this IEnumerable<Word> words, Word startAfter)
        {
            Validate.NotNull(words, nameof(words), startAfter, nameof(startAfter));
            return words.SkipWhile(w => w != startAfter).Skip(1);
        }

        #region Syntactic Type Filtering

        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adjectives in the collection.</returns>
        public static IEnumerable<Adjective> OfAdjective(this IEnumerable<Word> words) => words.OfType<Adjective>();

        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adverbs in the collection.</returns>
        public static IEnumerable<Adverb> OfAdverb(this IEnumerable<Word> words) => words.OfType<Adverb>();

        /// <summary>
        /// Returns all Determiners in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Determiners in the collection.</returns>
        public static IEnumerable<Determiner> OfDeterminer(this IEnumerable<Word> words) => words.OfType<Determiner>();

        /// <summary>
        /// Returns all GenericNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All GenericNouns in the collection.</returns>
        public static IEnumerable<CommonNoun> OfCommonNoun(this IEnumerable<Word> words) => words.OfType<CommonNoun>();

        /// <summary>
        /// Returns all ModalAuxilaries in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ModalAuxilarys in the collection.</returns>
        public static IEnumerable<ModalAuxilary> OfModal(this IEnumerable<Word> words) => words.OfType<ModalAuxilary>();

        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Nouns in the collection.</returns>
        public static IEnumerable<Noun> OfNoun(this IEnumerable<Word> words) => words.OfType<Noun>();

        /// <summary>
        /// Returns all GenericPluralNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericNouns in the GenericNoun sequence.</returns>
        public static IEnumerable<CommonPluralNoun> OfPlural(this IEnumerable<CommonNoun> nouns) => nouns.OfType<CommonPluralNoun>();

        /// <summary>
        /// Returns all ProperPluralNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperPluralNouns in the ProperNoun sequence.</returns>
        public static IEnumerable<ProperPluralNoun> OfPlural(this IEnumerable<ProperNoun> nouns) => nouns.OfType<ProperPluralNoun>();

        /// <summary>
        /// Returns all of the standard Pronouns in the collection. RelativePronouns are not
        /// included in the result.
        /// </summary>
        /// <param name="words">The collection of Words to filter.</param>
        /// <returns>All Pronouns in the collection.</returns>
        public static IEnumerable<Pronoun> OfPronoun(this IEnumerable<Word> words) => words.OfType<Pronoun>();

        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ProperNouns in the collection.</returns>
        public static IEnumerable<ProperNoun> OfProperNoun(this IEnumerable<Word> words) => words.OfType<ProperNoun>();

        /// <summary>
        /// Returns all Punctuators in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Punctuators in the collection.</returns>
        public static IEnumerable<Punctuator> OfPunctuator(this IEnumerable<Word> words) => words.OfType<Punctuator>();

        /// <summary>
        /// Returns all Quantifiers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Quantifiers in the collection.</returns>
        public static IEnumerable<Quantifier> OfQuantifier(this IEnumerable<Word> words) => words.OfType<Quantifier>();

        /// <summary>
        /// Returns all of the RelativePronouns in the collection. Standard Pronouns are not
        /// included in the result.
        /// </summary>
        /// <param name="words">The collection of Words to filter.</param>
        /// <returns>All RelativePronouns in the collection.</returns>
        public static IEnumerable<RelativePronoun> OfRelativePronoun(this IEnumerable<Word> words) => words.OfType<RelativePronoun>();

        /// <summary>
        /// Returns all GenericSingularNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericSingularNouns in the GenericNoun sequence.</returns>
        public static IEnumerable<CommonSingularNoun> OfSingular(this IEnumerable<CommonNoun> nouns) => nouns.OfType<CommonSingularNoun>();

        /// <summary>
        /// Returns all ProperSingularNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperSingularNouns in the ProperNoun sequence.</returns>
        public static IEnumerable<ProperSingularNoun> OfSingular(this IEnumerable<ProperNoun> nouns) => nouns.OfType<ProperSingularNoun>();

        /// <summary>
        /// Returns all ToLinkers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ToLinkers in the collection.</returns>
        public static IEnumerable<ToLinker> OfToLinker(this IEnumerable<Word> words) => words.OfType<ToLinker>();

        /// <summary>
        /// Returns all Verbs in the collection.
        /// </summary>
        /// <param name="words">The collection of Words to filter.</param>
        /// <returns>All Verbs in the collection.</returns>
        public static IEnumerable<Verb> OfVerb(this IEnumerable<Word> words) => words.OfType<Verb>();

        #endregion Syntactic Type Filtering

        #endregion Sequential Implementations

        #region Parallel Implementations

        #region Syntactic Type Filtering

        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adjectives in the collection.</returns>
        public static ParallelQuery<Adjective> OfAdjective(this ParallelQuery<Word> words) => words.OfType<Adjective>();

        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adverbs in the collection.</returns>
        public static ParallelQuery<Adverb> OfAdverb(this ParallelQuery<Word> words) => words.OfType<Adverb>();

        /// <summary>
        /// Returns all Determiners in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Determiners in the collection.</returns>
        public static ParallelQuery<Determiner> OfDeterminer(this ParallelQuery<Word> words) => words.OfType<Determiner>();

        /// <summary>
        /// Returns all GenericNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All GenericNouns in the collection.</returns>
        public static ParallelQuery<CommonNoun> OfCommonNoun(this ParallelQuery<Word> words) => words.OfType<CommonNoun>();

        /// <summary>
        /// Returns all ModalAuxilaries in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ModalAuxilarys in the collection.</returns>
        public static ParallelQuery<ModalAuxilary> OfModal(this ParallelQuery<Word> words) => words.OfType<ModalAuxilary>();

        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Nouns in the collection.</returns>
        public static ParallelQuery<Noun> OfNoun(this ParallelQuery<Word> words) => words.OfType<Noun>();

        /// <summary>
        /// Returns all GenericPluralNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericNouns in the GenericNoun sequence.</returns>
        public static ParallelQuery<CommonPluralNoun> OfPlural(this ParallelQuery<CommonNoun> nouns) => nouns.OfType<CommonPluralNoun>();

        /// <summary>
        /// Returns all ProperPluralNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperPluralNouns in the ProperNoun sequence.</returns>
        public static ParallelQuery<ProperPluralNoun> OfPlural(this ParallelQuery<ProperNoun> nouns) => nouns.OfType<ProperPluralNoun>();

        /// <summary>
        /// Returns all of the standard Pronouns in the collection. RelativePronouns are not
        /// included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Pronouns in the collection.</returns>
        public static ParallelQuery<Pronoun> OfPronoun(this ParallelQuery<Word> words) => words.OfType<Pronoun>();

        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ProperNouns in the collection.</returns>
        public static ParallelQuery<ProperNoun> OfProperNoun(this ParallelQuery<Word> words) => words.OfType<ProperNoun>();

        /// <summary>
        /// Returns all Punctuators in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Punctuators in the collection.</returns>
        public static ParallelQuery<Punctuator> OfPunctuator(this ParallelQuery<Word> words) => words.OfType<Punctuator>();

        /// <summary>
        /// Returns all Quantifiers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Quantifiers in the collection.</returns>
        public static ParallelQuery<Quantifier> OfQuantifier(this ParallelQuery<Word> words) => words.OfType<Quantifier>();

        /// <summary>
        /// Returns all of the RelativePronouns in the collection. Standard Pronouns are not
        /// included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All RelativePronouns in the collection.</returns>
        public static ParallelQuery<RelativePronoun> OfRelativePronoun(this ParallelQuery<Word> words) => words.OfType<RelativePronoun>();

        /// <summary>
        /// Returns all GenericSingularNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericSingularNouns in the GenericNoun sequence.</returns>
        public static ParallelQuery<CommonSingularNoun> OfSingular(this ParallelQuery<CommonNoun> nouns) => nouns.OfType<CommonSingularNoun>();

        /// <summary>
        /// Returns all ProperSingularNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperSingularNouns in the ProperNoun sequence.</returns>
        public static ParallelQuery<ProperSingularNoun> OfSingular(this ParallelQuery<ProperNoun> nouns) => nouns.OfType<ProperSingularNoun>();

        /// <summary>
        /// Returns all ToLinkers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ToLinkers in the collection.</returns>
        public static ParallelQuery<ToLinker> OfToLinker(this ParallelQuery<Word> words) => words.OfType<ToLinker>();

        /// <summary>
        /// Returns all Verbs in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Verbs in the collection.</returns>
        public static ParallelQuery<Verb> OfVerb(this ParallelQuery<Word> words) => words.OfType<Verb>();

        #endregion Syntactic Type Filtering

        #endregion Parallel Implementations
    }
}