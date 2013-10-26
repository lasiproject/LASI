using LASI.Core.ComparativeHeuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Core
{

    /// <summary>
    /// Defines extension methods for sequences of Word instances.
    /// </summary>
    /// <see cref="Word"/>
    public static class EnumerableOfWordExtensions
    {
        #region Sequential Implementations

        /// <summary>
        /// Returns all words in the Word collection which come after the given word.
        /// </summary>
        /// <param name="words">A sequence of word objects</param>
        /// <param name="startAfter">The delimiting word</param>
        /// <returns>All words in the Word collection which come after the given word.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when words or startAfter is null.</exception>
        public static IEnumerable<Word> WordsFollowing(this IEnumerable<Word> words, Word startAfter) {
            if (words == null || startAfter == null) { throw new ArgumentNullException("startAfter", "The provided Word to take after was null."); }
            return words.SkipWhile(w => w != startAfter).Skip(1);
        }

        #region Syntactic Type Filtering
        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adverbs in the collection.</returns>
        public static IEnumerable<Adverb> OfAdverb(this IEnumerable<Word> words) {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adjectives in the collection.</returns>
        public static IEnumerable<Adjective> OfAdjective(this IEnumerable<Word> words) {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Nouns in the collection.</returns>
        public static IEnumerable<Noun> OfNoun(this IEnumerable<Word> words) {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ProperNouns in the collection.</returns>
        public static IEnumerable<ProperNoun> OfProperNoun(this IEnumerable<Word> words) {
            return words.OfType<ProperNoun>();
        }
        /// <summary>
        /// Returns all GenericNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All GenericNouns in the collection.</returns>
        public static IEnumerable<CommonNoun> OfGenericNoun(this IEnumerable<Word> words) {
            return words.OfType<CommonNoun>();
        }
        /// <summary>
        /// Returns all GenericSingularNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericSingularNouns in the GenericNoun sequence.</returns>
        public static IEnumerable<CommonSingularNoun> Singulars(this IEnumerable<CommonNoun> nouns) {
            return nouns.OfType<CommonSingularNoun>();
        }
        /// <summary>
        /// Returns all GenericPluralNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericNouns in the GenericNoun sequence.</returns>
        public static IEnumerable<CommonPluralNoun> Plurals(this IEnumerable<CommonNoun> nouns) {
            return nouns.OfType<CommonPluralNoun>();
        }/// <summary>
        /// Returns all ProperSingularNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperSingularNouns in the ProperNoun sequence.</returns>
        public static IEnumerable<ProperSingularNoun> Singulars(this IEnumerable<ProperNoun> nouns) {
            return nouns.OfType<ProperSingularNoun>();
        }
        /// <summary>
        /// Returns all ProperPluralNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperPluralNouns in the ProperNoun sequence.</returns>
        public static IEnumerable<ProperPluralNoun> Plurals(this IEnumerable<ProperNoun> nouns) {
            return nouns.OfType<ProperPluralNoun>();
        }
        /// <summary>
        /// Returns all of the standard Pronouns in the collection.
        /// RelativePronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Pronouns in the collection.</returns>
        public static IEnumerable<Pronoun> OfPronoun(this IEnumerable<Word> words) {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all of the RelativePronouns in the collection.
        /// Standard Pronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All RelativePronouns in the collection.</returns>
        public static IEnumerable<RelativePronoun> OfRelativePronoun(this IEnumerable<Word> words) {
            return words.OfType<RelativePronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Verbs in the collection.</returns>
        public static IEnumerable<Verb> OfVerb(this IEnumerable<Word> words) {
            return words.OfType<Verb>();
        }
        /// <summary>
        /// Returns all Verbs in the collection with the given verb tense.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <param name="tense">The tense to match against</param>
        /// <returns>All Verbs in the collection.</returns>
        public static IEnumerable<Verb> OfVerb(this IEnumerable<Word> words, VerbForm tense) {
            return words.OfType<Verb>().Where(v => v.Tense == tense);
        }
        /// <summary>
        /// Returns all ToLinkers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ToLinkers in the collection.</returns>
        public static IEnumerable<ToLinker> OfToLinker(this IEnumerable<Word> words) {
            return words.OfType<ToLinker>();
        }
        /// <summary>
        /// Returns all ModalAuxilaries in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ModalAuxilarys in the collection.</returns>
        public static IEnumerable<ModalAuxilary> OfModal(this IEnumerable<Word> words) {
            return words.OfType<ModalAuxilary>();
        }
        /// <summary>
        /// Returns all Determiners in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Determiners in the collection.</returns>
        public static IEnumerable<Determiner> OfDeterminer(this IEnumerable<Word> words) {
            return words.OfType<Determiner>();
        }
        /// <summary>
        /// Returns all Quantifiers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Quantifiers in the collection.</returns>
        public static IEnumerable<Quantifier> OfQuantifier(this IEnumerable<Word> words) {
            return words.OfType<Quantifier>();
        }
        #endregion

        #endregion

        #region Parallel Implementations


        #region Syntactic Type Filtering
        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adverbs in the collection.</returns>
        public static ParallelQuery<Adverb> OfAdverb(this ParallelQuery<Word> words) {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adjectives in the collection.</returns>
        public static ParallelQuery<Adjective> OfAdjective(this ParallelQuery<Word> words) {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Nouns in the collection.</returns>
        public static ParallelQuery<Noun> OfNoun(this ParallelQuery<Word> words) {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ProperNouns in the collection.</returns>
        public static ParallelQuery<ProperNoun> OfProperNoun(this ParallelQuery<Word> words) {
            return words.OfType<ProperNoun>();
        }
        /// <summary>
        /// Returns all GenericNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All GenericNouns in the collection.</returns>
        public static ParallelQuery<CommonNoun> OfGenericNoun(this ParallelQuery<Word> words) {
            return words.OfType<CommonNoun>();
        }
        /// <summary>
        /// Returns all GenericSingularNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericSingularNouns in the GenericNoun sequence.</returns>
        public static ParallelQuery<CommonSingularNoun> Singulars(this ParallelQuery<CommonNoun> nouns) {
            return nouns.OfType<CommonSingularNoun>();
        }
        /// <summary>
        /// Returns all GenericPluralNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericNouns in the GenericNoun sequence.</returns>
        public static ParallelQuery<CommonPluralNoun> Plurals(this ParallelQuery<CommonNoun> nouns) {
            return nouns.OfType<CommonPluralNoun>();
        }/// <summary>
        /// Returns all ProperSingularNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperSingularNouns in the ProperNoun sequence.</returns>
        public static ParallelQuery<ProperSingularNoun> Singulars(this ParallelQuery<ProperNoun> nouns) {
            return nouns.OfType<ProperSingularNoun>();
        }
        /// <summary>
        /// Returns all ProperPluralNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperPluralNouns in the ProperNoun sequence.</returns>
        public static ParallelQuery<ProperPluralNoun> Plurals(this ParallelQuery<ProperNoun> nouns) {
            return nouns.OfType<ProperPluralNoun>();
        }
        /// <summary>
        /// Returns all of the standard Pronouns in the collection.
        /// RelativePronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Pronouns in the collection.</returns>
        public static ParallelQuery<Pronoun> OfPronoun(this ParallelQuery<Word> words) {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all of the RelativePronouns in the collection.
        /// Standard Pronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All RelativePronouns in the collection.</returns>
        public static ParallelQuery<RelativePronoun> OfRelativePronoun(this ParallelQuery<Word> words) {
            return words.OfType<RelativePronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Verbs in the collection.</returns>
        public static ParallelQuery<Verb> OfVerb(this ParallelQuery<Word> words) {
            return words.OfType<Verb>();
        }
        /// <summary>
        /// Returns all Verbs in the collection with the given verb tense.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <param name="tense">The tense to match against</param>
        /// <returns>All Verbs in the collection.</returns>
        public static ParallelQuery<Verb> OfVerb(this ParallelQuery<Word> words, VerbForm tense) {
            return words.OfType<Verb>().Where(v => v.Tense == tense);
        }
        /// <summary>
        /// Returns all ToLinkers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ToLinkers in the collection.</returns>
        public static ParallelQuery<ToLinker> OfToLinker(this ParallelQuery<Word> words) {
            return words.OfType<ToLinker>();
        }
        /// <summary>
        /// Returns all ModalAuxilaries in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ModalAuxilarys in the collection.</returns>
        public static ParallelQuery<ModalAuxilary> OfModal(this ParallelQuery<Word> words) {
            return words.OfType<ModalAuxilary>();
        }
        /// <summary>
        /// Returns all Determiners in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Determiners in the collection.</returns>
        public static ParallelQuery<Determiner> OfDeterminer(this ParallelQuery<Word> words) {
            return words.OfType<Determiner>();
        }
        /// <summary>
        /// Returns all Quantifiers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Quantifiers in the collection.</returns>
        public static ParallelQuery<Quantifier> OfQuantifier(this ParallelQuery<Word> words) {
            return words.OfType<Quantifier>();
        }
        #endregion

        #endregion
    }

}