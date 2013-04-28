using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Thesauri;
namespace LASI.Algorithm
{

    /// <summary>
    /// Provides facilities to aid in the querying of IEnumerableCollections of Words.
    /// </summary>
    public static class IEnumerableOfWordExtensions
    {
        /// <summary>
        /// Retrives all words in the Word collection which compare equal to a given Word
        /// </summary>
        /// <param name="toMatch">The Word to match</param>
        /// <param name="words">A sequence of Word objects</param>
        /// <returns>a WordList containing all words which match the argument</returns>
        /// <see cref="Word"/>
        public static IEnumerable<Word> FindAllOccurances(this IEnumerable<Word> words,
            Word toMatch) {
            return from word in words
                   where word.Text == toMatch.Text && word.GetType() == toMatch.GetType()
                   select word;
        }
        public static IEnumerable<Word> FindAllOccurances(this IEnumerable<Word> words,
           Word toMatch, Func<Word, Word, bool> comparison) {
            return from W in words
                   where comparison(toMatch, W)
                   select W;
        }

        #region Verb Enumerable Overloads

        public static IEnumerable<Verb> FindAllOccurances(this IEnumerable<Word> words,
           Verb toMatch) {
            return from word in words.GetVerbs()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Verb> FindAllOccurances(this IEnumerable<Word> words,
                   Verb toMatch, Func<Verb, Verb, bool> comparison) {
            return from W in words.GetVerbs()
                   where comparison(toMatch, W)
                   select W;
        }
        #endregion

        #region Noun Enumerable Overloads

        public static IEnumerable<Noun> FindAllOccurances(this IEnumerable<Word> words,
           Noun toMatch) {
            return from word in words.GetNouns()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Noun> FindAllOccurances(this IEnumerable<Word> words,
                   Noun toMatch, Func<Noun, Noun, bool> comparison) {
            return from W in words.GetNouns()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion

        #region Pronoun Enumerable Overloads

        public static IEnumerable<Pronoun> FindAllOccurances(this IEnumerable<Word> words,
           Pronoun toMatch) {
            return from word in words.GetPronouns()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Pronoun> FindAllOccurances(this IEnumerable<Word> words,
                   Pronoun toMatch, Func<Pronoun, Pronoun, bool> comparison) {
            return from W in words.GetPronouns()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion

        #region Adjective Enumerable Overloads

        public static IEnumerable<Adjective> FindAllOccurances(this IEnumerable<Word> words,
           Adjective toMatch) {
            return from word in words.GetAdjectives()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Adjective> FindAllOccurances(this IEnumerable<Word> words,
                   Adjective toMatch, Func<Adjective, Adjective, bool> comparison) {
            return from W in words.GetAdjectives()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion

        #region Adverb Enumerable Overloads

        public static IEnumerable<Adverb> FindAllOccurances(this IEnumerable<Word> words,
           Adverb toMatch) {
            return from word in words.GetAdverbs()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Adverb> FindAllOccurances(this IEnumerable<Word> words,
                   Adverb toMatch, Func<Adverb, Adverb, bool> comparison) {
            return from W in words.GetAdverbs()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion


        /// <summary>
        /// Retrives all words in the collection which compare equal to a given Word or any of its provided synonyms.
        /// </summary>
        /// <param name="words"></param>
        /// <param name="toMatch">The verb to match</param>
        /// <param name="synonymProvider">The ThesaurusBase instance which provides the synonyms to also match against.</param>
        /// <returns>a WordList containing all words which match the argument or any of its provided synonyms.</returns>
        /// <see cref="Word"/>
        /// <seealso cref="ThesaurusBase"/>
        public static IEnumerable<Word> TextMatching(this IEnumerable<Word> words, ILexical toMatch, ThesaurusBase synonymProvider) {
            var matchTexts = synonymProvider[toMatch.Text];
            return from W in words
                   where matchTexts.Contains(W.Text)
                   select W;
        }
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
        public static IEnumerable<ModalAuxilary> GetModalAuxilaries(this IEnumerable<Word> words) {
            return words.OfType<ModalAuxilary>();
        }

        /// <summary>
        /// Returns all Determiners in the collection
        /// </summary>
        public static IEnumerable<Determiner> GetToDeterminers(this IEnumerable<Word> words) {
            return words.OfType<Determiner>();
        }
        /// <summary>
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        /// <param name="refererring"></param>
        /// <returns>All Pronouns in the collection that are bound as referencts of some entity.</returns>
        public static IEnumerable<Pronoun> Referencing(this IEnumerable<Pronoun> refererring) {
            return from ER in refererring
                   where ER.BoundEntity != null
                   select ER;
        }
        /// <summary>
        /// Returns all Pronouns in the collection that refer to the given entity.
        /// </summary>
        /// <param name="refererring"></param>
        /// <param name="referenced">The entity whose referencng pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static IEnumerable<Pronoun> Referencing(this IEnumerable<Pronoun> refererring, IEntity referenced) {
            return from ER in refererring
                   where ER.BoundEntity == referenced
                   select ER;
        }
        /// <summary>
        /// Returns all Pronouns in the collection that refer to any entity matching the given test condition.
        /// </summary>
        /// <param name="refererring"></param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static IEnumerable<Pronoun> Referencing(
            this IEnumerable<Pronoun> refererring,
            Func<IEntity, bool> condition
            ) {
            return from ER in refererring
                   where ER.BoundEntity != null && condition(ER.BoundEntity)
                   select ER;
        }
    }

}