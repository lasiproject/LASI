using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LASI.Algorithm.Lookup;
namespace LASI.Algorithm
{

    /// <summary>
    /// Defines extension methods for sequences of Word instances.
    /// </summary>
    /// <see cref="Word"/>
    public static class IEnumerableOfWordExtensions
    {
        /// <summary>
        /// Retrives all words in the words collection which compare equal to a given w
        /// </summary>
        /// <typeparam name="T">Word or any Type derriving from it.</typeparam>
        /// <param name="toMatch">The w to match</param>
        /// <param name="words">A sequence of w instances</param>
        /// <returns>A WordList containing all words which match the argument</returns>
        /// <see cref="Word"/>
        public static IEnumerable<T> FindLexicalMatches<T>(this IEnumerable<T> words,
            T toMatch) where T : Word {
            return from word in words
                   where word.Text == toMatch.Text && word.GetType() == toMatch.GetType()
                   select word;
        }
        /// <summary>
        /// Finds all Words in the sequence which are equivalent to the Word to match, based on the logic of the provided comparison function.
        /// </summary> 
        /// <typeparam name="T">Word or any Type derriving from it.</typeparam>
        /// <param name="words">The sequence of Words to filter.</param>
        /// <param name="toMatch">A Word to match against.</param>
        /// <param name="comparison">The function to use to compare Words.</param>
        /// <returns>All nouns in the which compare equal to the Word to match in the provided comparison function.</returns>
        public static IEnumerable<T> FindLexicalMatches<T>(this IEnumerable<T> words,
           T toMatch, Func<T, T, bool> comparison) where T : Word {
            return from W in words
                   where comparison(toMatch, W)
                   select W;
        }
        /// <summary>
        /// Returns all words in the Word collection which come after the given word.
        /// </summary>
        /// <param name="words">A sequence of word objects</param>
        /// <param name="word">The delimiting word</param>
        /// <returns></returns>
        public static IEnumerable<Word> GetWordsAfter(this IEnumerable<Word> words, Word word) {
            return words.SkipWhile(w => w != word).Skip(1);
        }


        #region Verb Enumerable Extensions

        //public static IEnumerable<Verb> FindLexicalMatches(this IEnumerable<Word> words,
        //   Verb toMatch) {
        //    return from word in words.GetVerbs()
        //           where word.Text == toMatch.Text
        //           select word;
        //}
        //public static IEnumerable<Verb> FindLexicalMatches(this IEnumerable<Word> words,
        //           Verb toMatch, Func<Verb, Verb, bool> comparison) {
        //    return from W in words.GetVerbs()
        //           where comparison(toMatch, W)
        //           select W;
        //}
        #endregion

        #region Noun Enumerable Extensions

        ///// <summary>
        ///// Finds all nouns in the sequence whose text is equivalent to the noun to match.
        ///// </summary>
        ///// <param name="words">The sequence of nouns to filter.</param>
        ///// <param name="toMatch">A noun to match against.</param>
        ///// <returns>All nouns in the sequence whose text is equivalent to the noun to match.</returns>
        //public static IEnumerable<Noun> FindLexicalMatches(this IEnumerable<Word> words,
        //   Noun toMatch) {
        //    return from word in words.GetNouns()
        //           where word.Text == toMatch.Text
        //           select word;
        //}
        ///// <summary>
        ///// Finds all nouns in the sequence which are equivalent to the noun to match, based on the logic of the provided comparison function.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">A noun to match against.</param>
        ///// <param name="comparison">The function to use to compare Nouns.</param>
        ///// <returns>All nouns in the sequence whose text is equivalent to the noun to match.</returns>
        //public static IEnumerable<Noun> FindLexicalMatches(this IEnumerable<Word> words,
        //           Noun toMatch, Func<Noun, Noun, bool> comparison) {
        //    return from W in words.GetNouns()
        //           where comparison(toMatch, W)
        //           select W;
        //}

        #endregion

        #region Pronoun Enumerable Overloads
        ///// <summary>
        ///// Finds all Pronouns in the sequence whose text is equivalent to the Pronoun to match.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">A Pronoun to match against.</param>
        ///// <returns>All Pronouns in the sequence whose text is equivalent to the Pronoun to match.</returns>
        //public static IEnumerable<Pronoun> FindLexicalMatches(this IEnumerable<Word> words,
        //   Pronoun toMatch) {
        //    return from word in words.GetPronouns()
        //           where word.Text == toMatch.Text
        //           select word;
        //}
        ///// <summary>
        ///// Finds all Pronouns in the sequence which are equivalent to the Pronoun to match, based on the logic of the provided comparison function.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">A noun to match against.</param>
        ///// <param name="comparison">The function to use to compare Pronouns.</param>
        ///// <returns>All Pronouns in the sequence whose text is equivalent to the Pronoun to match.</returns>
        //public static IEnumerable<Pronoun> FindLexicalMatches(this IEnumerable<Word> words,
        //           Pronoun toMatch, Func<Pronoun, Pronoun, bool> comparison) {
        //    return from W in words.GetPronouns()
        //           where comparison(toMatch, W)
        //           select W;
        //}

        #endregion

        #region Adjective Enumerable Overloads

        ///// <summary>
        ///// Finds all Adjectives in the sequence whose text is equivalent to the Adjective to match.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">An Adjective to match against.</param>
        ///// <returns>All Adjectives in the sequence whose text is equivalent to the Adjective to match.</returns>
        //public static IEnumerable<Adjective> FindLexicalMatches(this IEnumerable<Word> words,
        //   Adjective toMatch) {
        //    return from word in words.GetAdjectives()
        //           where word.Text == toMatch.Text
        //           select word;
        //}
        ///// <summary>
        ///// Finds all Adjectives in the sequence which are equivalent to the Adjective to match, based on the logic of the provided comparison function.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">An Adjective to match against.</param>
        ///// <param name="comparison">The function to use to compare Adjectives.</param>
        ///// <returns>All Adjectives in the sequence whose text is equivalent to the Adjective to match.</returns>
        //public static IEnumerable<Adjective> FindLexicalMatches(this IEnumerable<Word> words,
        //           Adjective toMatch, Func<Adjective, Adjective, bool> comparison) {
        //    return from W in words.GetAdjectives()
        //           where comparison(toMatch, W)
        //           select W;
        //}

        #endregion

        #region Adverb Enumerable Overloads
        ///// <summary>
        ///// Finds all Adverbs in the sequence whose text is equivalent to the Adverb to match.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">An Adverb to match against.</param>
        ///// <returns>All Adverbs in the sequence whose text is equivalent to the Adverb to match.</returns>
        //public static IEnumerable<Adverb> FindLexicalMatches(this IEnumerable<Word> words,
        //   Adverb toMatch) {
        //    return from word in words.GetAdverbs()
        //           where word.Text == toMatch.Text
        //           select word;
        //}
        ///// <summary>
        ///// Finds all Adverb in the sequence which are equivalent to the Adverb to match, based on the logic of the provided comparison function.
        ///// </summary>
        ///// <param name="words">The sequence of Words to filter.</param>
        ///// <param name="toMatch">An Adverb to match against.</param>
        ///// <param name="comparison">The function to use to compare Adverbs.</param>
        ///// <returns>All Adverbs in the sequence whose text is equivalent to the Adverb to match.</returns>
        //public static IEnumerable<Adverb> FindLexicalMatches(this IEnumerable<Word> words,
        //           Adverb toMatch, Func<Adverb, Adverb, bool> comparison) {
        //    return from W in words.GetAdverbs()
        //           where comparison(toMatch, W)
        //           select W;
        //}

        #endregion


        #region Syntactic Noun Filtering Methods

        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        public static IEnumerable<Adverb> GetAdverbs(this IEnumerable<Word> words) {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        public static IEnumerable<Adjective> GetAdjectives(this IEnumerable<Word> words) {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        public static IEnumerable<Noun> GetNouns(this IEnumerable<Word> words) {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all ProperNouns in the collection.
        /// </summary>
        public static IEnumerable<ProperNoun> GetProperNouns(this IEnumerable<Word> words) {
            return words.OfType<ProperNoun>();
        }
        /// <summary>
        /// Returns all Pronouns in the collection
        /// </summary>
        public static IEnumerable<Pronoun> GetPronouns(this IEnumerable<Word> words) {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the collection
        /// </summary>
        public static IEnumerable<Verb> GetVerbs(this IEnumerable<Word> words) {
            return words.OfType<Verb>();
        }
        /// <summary>
        /// Returns all ToLinkers in the collection
        /// </summary>
        public static IEnumerable<ToLinker> GetToLinkers(this IEnumerable<Word> words) {
            return words.OfType<ToLinker>();
        }

        /// <summary>
        /// Returns all ModalAuxilaries in the collection
        /// </summary>
        public static IEnumerable<ModalAuxilary> GetModals(this IEnumerable<Word> words) {
            return words.OfType<ModalAuxilary>();
        }

        /// <summary>
        /// Returns all Determiners in the collection
        /// </summary>
        public static IEnumerable<Determiner> GetDeterminers(this IEnumerable<Word> words) {
            return words.OfType<Determiner>();
        }

        #endregion


    }

}