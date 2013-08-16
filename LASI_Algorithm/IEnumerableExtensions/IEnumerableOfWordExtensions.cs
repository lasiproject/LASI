using LASI.Algorithm.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{

    /// <summary>
    /// Defines extension methods for sequences of Word instances.
    /// </summary>
    /// <see cref="Word"/>
    public static class IEnumerableOfWordExtensions
    {
        #region Sequential Implementations
        ///// <summary>
        ///// Retrives all words in the words collection which compare equal to a given word.
        ///// </summary>
        ///// <typeparam name="T">Word or any Type derriving from it.</typeparam>
        ///// <param name="toMatch">The word to match</param>
        ///// <param name="words">A sequence of word instances</param>
        ///// <returns>A WordList containing all words which match the argument</returns>
        ///// <see cref="Word"/>
        //public static IEnumerable<T> FindAllMatches<T>(this IEnumerable<T> words, T toMatch) where T : Word {
        //    if (toMatch == null) { throw new ArgumentNullException("toMatch", "The provided Word to match against was null."); }
        //    return from word in words
        //           where word.Text == toMatch.Text && word.GetType() == toMatch.GetType()
        //           select word;
        //}
        ///// <summary>
        ///// Finds all Words in the sequence which are equivalent to the Word to match, based on the logic of the provided comparison function.
        ///// </summary> 
        ///// <typeparam name="T">Word or any Type derriving from it.</typeparam>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">A Word to match against.</param>
        ///// <param name="comparison">The function to use to compare Words.</param>
        ///// <returns>All nouns in the which compare equal to the Word to match in the provided comparison function.</returns>
        //public static IEnumerable<T> FindAllMatches<T>(this IEnumerable<T> words, T toMatch, Func<T, T, bool> comparison) where T : Word {
        //    if (toMatch == null) { throw new ArgumentNullException("toMatch", "The provided Word to match against was null."); }
        //    if (comparison == null) { throw new ArgumentNullException("comparison", "The provided comparison function was null."); }
        //    return from W in words
        //           where comparison(toMatch, W)
        //           select W;
        //}
        /// <summary>
        /// Returns all words in the Word collection which come after the given word.
        /// </summary>
        /// <param name="words">A sequence of word objects</param>
        /// <param name="startAfter">The delimiting word</param>
        /// <returns>All words in the Word collection which come after the given word.</returns>
        public static IEnumerable<Word> GetWordsAfter(this IEnumerable<Word> words, Word startAfter) {
            if (startAfter == null) { throw new ArgumentNullException("startFrom", "The provided Word to take after was null."); }
            return words.SkipWhile(w => w != startAfter).Skip(1);
        }

        #region Syntactic Type Filtering
        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adverbs in the collection.</returns>
        public static IEnumerable<Adverb> GetAdverbs(this IEnumerable<Word> words) {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adjectives in the collection.</returns>
        public static IEnumerable<Adjective> GetAdjectives(this IEnumerable<Word> words) {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Nouns in the collection.</returns>
        public static IEnumerable<Noun> GetNouns(this IEnumerable<Word> words) {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ProperNouns in the collection.</returns>
        public static IEnumerable<ProperNoun> GetProperNouns(this IEnumerable<Word> words) {
            return words.OfType<ProperNoun>();
        }
        /// <summary>
        /// Returns all GenericNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All GenericNouns in the collection.</returns>
        public static IEnumerable<GenericNoun> GetGenericNouns(this IEnumerable<Word> words) {
            return words.OfType<GenericNoun>();
        }
        /// <summary>
        /// Returns all GenericSingularNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericSingularNouns in the GenericNoun sequence.</returns>
        public static IEnumerable<GenericSingularNoun> GetSingular(this IEnumerable<GenericNoun> nouns) {
            return nouns.OfType<GenericSingularNoun>();
        }
        /// <summary>
        /// Returns all GenericPluralNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericNouns in the GenericNoun sequence.</returns>
        public static IEnumerable<GenericPluralNoun> GetPlural(this IEnumerable<GenericNoun> nouns) {
            return nouns.OfType<GenericPluralNoun>();
        }/// <summary>
        /// Returns all ProperSingularNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperSingularNouns in the ProperNoun sequence.</returns>
        public static IEnumerable<ProperSingularNoun> GetSingular(this IEnumerable<ProperNoun> nouns) {
            return nouns.OfType<ProperSingularNoun>();
        }
        /// <summary>
        /// Returns all ProperPluralNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperPluralNouns in the ProperNoun sequence.</returns>
        public static IEnumerable<ProperPluralNoun> GetPlural(this IEnumerable<ProperNoun> nouns) {
            return nouns.OfType<ProperPluralNoun>();
        }
        /// <summary>
        /// Returns all of the standard Pronouns in the collection.
        /// RelativePronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Pronouns in the collection.</returns>
        public static IEnumerable<Pronoun> GetPronouns(this IEnumerable<Word> words) {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all of the RelativePronouns in the collection.
        /// Standard Pronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All RelativePronouns in the collection.</returns>
        public static IEnumerable<RelativePronoun> GetRelativePronouns(this IEnumerable<Word> words) {
            return words.OfType<RelativePronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Verbs in the collection.</returns>
        public static IEnumerable<Verb> GetVerbs(this IEnumerable<Word> words) {
            return words.OfType<Verb>();
        }
        /// <summary>
        /// Returns all Verbs in the collection with the given verb tense.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <param name="tense">The tense to match against</param>
        /// <returns>All Verbs in the collection.</returns>
        public static IEnumerable<Verb> GetVerbs(this IEnumerable<Word> words, VerbTense tense) {
            return words.OfType<Verb>().Where(v => v.Tense == tense);
        }
        /// <summary>
        /// Returns all ToLinkers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ToLinkers in the collection.</returns>
        public static IEnumerable<ToLinker> GetToLinkers(this IEnumerable<Word> words) {
            return words.OfType<ToLinker>();
        }
        /// <summary>
        /// Returns all ModalAuxilaries in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ModalAuxilarys in the collection.</returns>
        public static IEnumerable<ModalAuxilary> GetModals(this IEnumerable<Word> words) {
            return words.OfType<ModalAuxilary>();
        }
        /// <summary>
        /// Returns all Determiners in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Determiners in the collection.</returns>
        public static IEnumerable<Determiner> GetDeterminers(this IEnumerable<Word> words) {
            return words.OfType<Determiner>();
        }
        /// <summary>
        /// Returns all Quantifiers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Quantifiers in the collection.</returns>
        public static IEnumerable<Quantifier> GetQuantifiers(this IEnumerable<Word> words) {
            return words.OfType<Quantifier>();
        }
        #endregion

        #endregion

        #region Parallel Implementations

        /// <summary>
        /// Returns all words in the Word collection which come after the given word.
        /// </summary>
        /// <param name="words">A sequence of word objects</param>
        /// <param name="startAfter">The delimiting word</param>
        /// <returns>All words in the Word collection which come after the given word.</returns>
        public static ParallelQuery<Word> GetWordsAfter(this ParallelQuery<Word> words, Word startAfter) {
            if (startAfter == null) { throw new ArgumentNullException("startFrom", "The provided Word to take after was null."); }
            return words.SkipWhile(w => w != startAfter).Skip(1);
        }

        #region Syntactic Type Filtering
        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adverbs in the collection.</returns>
        public static ParallelQuery<Adverb> GetAdverbs(this ParallelQuery<Word> words) {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Adjectives in the collection.</returns>
        public static ParallelQuery<Adjective> GetAdjectives(this ParallelQuery<Word> words) {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Nouns in the collection.</returns>
        public static ParallelQuery<Noun> GetNouns(this ParallelQuery<Word> words) {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ProperNouns in the collection.</returns>
        public static ParallelQuery<ProperNoun> GetProperNouns(this ParallelQuery<Word> words) {
            return words.OfType<ProperNoun>();
        }
        /// <summary>
        /// Returns all GenericNouns in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All GenericNouns in the collection.</returns>
        public static ParallelQuery<GenericNoun> GetGenericNouns(this ParallelQuery<Word> words) {
            return words.OfType<GenericNoun>();
        }
        /// <summary>
        /// Returns all GenericSingularNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericSingularNouns in the GenericNoun sequence.</returns>
        public static ParallelQuery<GenericSingularNoun> GetSingular(this ParallelQuery<GenericNoun> nouns) {
            return nouns.OfType<GenericSingularNoun>();
        }
        /// <summary>
        /// Returns all GenericPluralNouns in the GenericNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of GenericNouns to filter.</param>
        /// <returns>All GenericNouns in the GenericNoun sequence.</returns>
        public static ParallelQuery<GenericPluralNoun> GetPlural(this ParallelQuery<GenericNoun> nouns) {
            return nouns.OfType<GenericPluralNoun>();
        }/// <summary>
        /// Returns all ProperSingularNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperSingularNouns in the ProperNoun sequence.</returns>
        public static ParallelQuery<ProperSingularNoun> GetSingular(this ParallelQuery<ProperNoun> nouns) {
            return nouns.OfType<ProperSingularNoun>();
        }
        /// <summary>
        /// Returns all ProperPluralNouns in the ProperNoun sequence.
        /// </summary>
        /// <param name="nouns">The sequence of ProperNouns to filter.</param>
        /// <returns>All ProperPluralNouns in the ProperNoun sequence.</returns>
        public static ParallelQuery<ProperPluralNoun> GetPlural(this ParallelQuery<ProperNoun> nouns) {
            return nouns.OfType<ProperPluralNoun>();
        }
        /// <summary>
        /// Returns all of the standard Pronouns in the collection.
        /// RelativePronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Pronouns in the collection.</returns>
        public static ParallelQuery<Pronoun> GetPronouns(this ParallelQuery<Word> words) {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all of the RelativePronouns in the collection.
        /// Standard Pronouns are not included in the result.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All RelativePronouns in the collection.</returns>
        public static ParallelQuery<RelativePronoun> GetRelativePronouns(this ParallelQuery<Word> words) {
            return words.OfType<RelativePronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Verbs in the collection.</returns>
        public static ParallelQuery<Verb> GetVerbs(this ParallelQuery<Word> words) {
            return words.OfType<Verb>();
        }
        /// <summary>
        /// Returns all Verbs in the collection with the given verb tense.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <param name="tense">The tense to match against</param>
        /// <returns>All Verbs in the collection.</returns>
        public static ParallelQuery<Verb> GetVerbs(this ParallelQuery<Word> words, VerbTense tense) {
            return words.OfType<Verb>().Where(v => v.Tense == tense);
        }
        /// <summary>
        /// Returns all ToLinkers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ToLinkers in the collection.</returns>
        public static ParallelQuery<ToLinker> GetToLinkers(this ParallelQuery<Word> words) {
            return words.OfType<ToLinker>();
        }
        /// <summary>
        /// Returns all ModalAuxilaries in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All ModalAuxilarys in the collection.</returns>
        public static ParallelQuery<ModalAuxilary> GetModals(this ParallelQuery<Word> words) {
            return words.OfType<ModalAuxilary>();
        }
        /// <summary>
        /// Returns all Determiners in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Determiners in the collection.</returns>
        public static ParallelQuery<Determiner> GetDeterminers(this ParallelQuery<Word> words) {
            return words.OfType<Determiner>();
        }
        /// <summary>
        /// Returns all Quantifiers in the collection.
        /// </summary>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <returns>All Quantifiers in the collection.</returns>
        public static ParallelQuery<Quantifier> GetQuantifiers(this ParallelQuery<Word> words) {
            return words.OfType<Quantifier>();
        }
        #endregion

        #endregion
    }

}