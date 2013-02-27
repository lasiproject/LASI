using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{


    public static class IEnumerableOfWordExtensions
    {
        /// <summary>
        /// Retrives all words in the Word collection which compare equal to a given Word
        /// </summary>
        /// <param name="toMatch">The Word to match</param>
        /// <returns>A WordList containing all words which match the argument</returns>
        /// <see cref="Word"/>
        public static IEnumerable<Word> GetAllOccurances(this IEnumerable<Word> words, Word toMatch) {
            return from word in words
                   where word == toMatch
                   select word;
        }

        public static IEnumerable<Word> GetAllOccurances(this IEnumerable<Word> words, Word toMatch, Func<Word, Word, bool> comparison) {
            return from W in words
                   where comparison(toMatch, W)
                   select W;
        }

        /// <summary>
        /// Retrives all words in the collection which compare equal to a given Word or any of its provided synonyms.
        /// </summary>
        /// <param name="toMatch">The word to match</param>
        /// <param name="synonymProvider">The Thesaurus instance which provides the synonyms to also match against.</param>
        /// <returns>A WordList containing all words which match the argument or any of its provided synonyms.</returns>
        /// <see cref="Word"/>
        /// <seealso cref="Thesaurus"/>
        public static IEnumerable<Word> TextMatching(this IEnumerable<Word> words, ILexical toMatch, Thesaurus synonymProvider) {
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
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        /// <param name="refererring"></param>
        /// <returns>All Pronouns in the collection that are attached to refer an entity.</returns>
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
        /// <param name="referenced">The entity whose referencng pronouns will be returned.</param>
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