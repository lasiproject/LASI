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
        /// Retrives all words in the wd collection which compare equal to a given wd
        /// </summary>
        /// <param name="toMatch">The wd to match</param>
        /// <param name="words">A sequence of wd objects</param>
        /// <returns>A WordList containing all words which match the argument</returns>
        /// <see cref="wd"/>
        public static IEnumerable<T> FindAllOccurances<T>(this IEnumerable<T> words,
            T toMatch) where T : Word
        {
            return from word in words
                   where word.Text == toMatch.Text && word.GetType() == toMatch.GetType()
                   select word;
        }
        public static IEnumerable<T> FindAllOccurances<T>(this IEnumerable<T> words,
           T toMatch, Func<T, T, bool> comparison) where T : Word
        {
            return from W in words
                   where comparison(toMatch, W)
                   select W;
        }
        /// <summary>
        /// Returns all words in the Word collection which come after the given wd.
        /// </summary>
        /// <param name="words">A sequence of wd objects</param>
        /// <param name="wd">The delimiting wd</param>
        /// <returns></returns>
        public static IEnumerable<Word> GetAllAfter(this IEnumerable<Word> words, Word word)
        {
            return words.SkipWhile(w => w != word).Skip(1);
        }


        #region Verb Enumerable Extensions

        public static IEnumerable<Verb> FindAllOccurances(this IEnumerable<Word> words,
           Verb toMatch)
        {
            return from word in words.GetVerbs()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Verb> FindAllOccurances(this IEnumerable<Word> words,
                   Verb toMatch, Func<Verb, Verb, bool> comparison)
        {
            return from W in words.GetVerbs()
                   where comparison(toMatch, W)
                   select W;
        }
        #endregion

        #region Noun Enumerable Extensions

        public static IEnumerable<Noun> FindAllOccurances(this IEnumerable<Word> words,
           Noun toMatch)
        {
            return from word in words.GetNouns()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Noun> FindAllOccurances(this IEnumerable<Word> words,
                   Noun toMatch, Func<Noun, Noun, bool> comparison)
        {
            return from W in words.GetNouns()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion

        #region Pronoun Enumerable Overloads

        public static IEnumerable<Pronoun> FindAllOccurances(this IEnumerable<Word> words,
           Pronoun toMatch)
        {
            return from word in words.GetPronouns()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Pronoun> FindAllOccurances(this IEnumerable<Word> words,
                   Pronoun toMatch, Func<Pronoun, Pronoun, bool> comparison)
        {
            return from W in words.GetPronouns()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion

        #region Adjective Enumerable Overloads

        public static IEnumerable<Adjective> FindAllOccurances(this IEnumerable<Word> words,
           Adjective toMatch)
        {
            return from word in words.GetAdjectives()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Adjective> FindAllOccurances(this IEnumerable<Word> words,
                   Adjective toMatch, Func<Adjective, Adjective, bool> comparison)
        {
            return from W in words.GetAdjectives()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion

        #region Adverb Enumerable Overloads

        public static IEnumerable<Adverb> FindAllOccurances(this IEnumerable<Word> words,
           Adverb toMatch)
        {
            return from word in words.GetAdverbs()
                   where word.Text == toMatch.Text
                   select word;
        }
        public static IEnumerable<Adverb> FindAllOccurances(this IEnumerable<Word> words,
                   Adverb toMatch, Func<Adverb, Adverb, bool> comparison)
        {
            return from W in words.GetAdverbs()
                   where comparison(toMatch, W)
                   select W;
        }

        #endregion


        #region Syntactic Type Filtering Methods

        /// <summary>
        /// Returns all Adverbs in the collection
        /// </summary>
        public static IEnumerable<Adverb> GetAdverbs(this IEnumerable<Word> words)
        {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the collection.
        /// </summary>
        public static IEnumerable<Adjective> GetAdjectives(this IEnumerable<Word> words)
        {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the collection.
        /// </summary>
        public static IEnumerable<Noun> GetNouns(this IEnumerable<Word> words)
        {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all Pronouns in the collection
        /// </summary>
        public static IEnumerable<Pronoun> GetPronouns(this IEnumerable<Word> words)
        {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the collection
        /// </summary>
        public static IEnumerable<Verb> GetVerbs(this IEnumerable<Word> words)
        {
            return words.OfType<Verb>();
        }
        /// <summary>
        /// Returns all ToLinkers in the collection
        /// </summary>
        public static IEnumerable<ToLinker> GetToLinkers(this IEnumerable<Word> words)
        {
            return words.OfType<ToLinker>();
        }

        /// <summary>
        /// Returns all ModalAuxilaries in the collection
        /// </summary>
        public static IEnumerable<ModalAuxilary> GetModalAuxilaries(this IEnumerable<Word> words)
        {
            return words.OfType<ModalAuxilary>();
        }

        /// <summary>
        /// Returns all Determiners in the collection
        /// </summary>
        public static IEnumerable<Determiner> GetDeterminers(this IEnumerable<Word> words)
        {
            return words.OfType<Determiner>();
        }

        #endregion

        /// <summary>
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        /// <param name="refererring"></param>
        /// <returns>All Pronouns in the collection that are bound as referencts of some entity.</returns>
        public static IEnumerable<Pronoun> Referencing(this IEnumerable<Pronoun> refererring)
        {
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
        public static IEnumerable<Pronoun> Referencing(this IEnumerable<Pronoun> refererring, IEntity referenced)
        {
            return from ER in refererring
                   where ER.BoundEntity == referenced
                   select ER;
        }
        /// <summary>
        /// Returns all Pronouns in the collection that refer to any entity matching the given test verbalSelector.
        /// </summary>
        /// <param name="refererring"></param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static IEnumerable<Pronoun> Referencing(
            this IEnumerable<Pronoun> refererring,
            Func<IEntity, bool> condition
            )
        {
            return from ER in refererring
                   where ER.BoundEntity != null && condition(ER.BoundEntity)
                   select ER;
        }
    }

}