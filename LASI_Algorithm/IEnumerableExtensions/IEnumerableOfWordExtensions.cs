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
        /// Retrives all words in the Word collection which compare equal to a given Word or any of its provided synonyms.
        /// </summary>
        /// <param name="toMatch">The word to match</param>
        /// <param name="synonymProvider">The Thesaurus instance which provides the synonyms to also match against.</param>
        /// <returns>A WordList containing all words which match the argument or any of its provided synonyms.</returns>
        /// <see cref="Word"/>
        /// <seealso cref="Thesaurus"/>
        public static IEnumerable<Word> GetAllOccurances(this IEnumerable<Word> words, Word toMatch, Thesaurus synonymProvider) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all Prepositions in the Word collection.
        /// </summary>
        public static IEnumerable<Preposition> GetPrepositions(this IEnumerable<Word> words) {
            return words.OfType<Preposition>();
        }
        /// <summary>
        /// Returns all Adverbs in the Word collection.
        /// </summary>
        public static IEnumerable<Adverb> GetAdverbs(this IEnumerable<Word> words) {
            return words.OfType<Adverb>();
        }
        /// <summary>
        /// Returns all Adjectives in the Word collection.
        /// </summary>
        public static IEnumerable<Adjective> GetAdjectives(this IEnumerable<Word> words) {
            return words.OfType<Adjective>();
        }
        /// <summary>
        /// Returns all Nouns in the Word collection.
        /// </summary>
        public static IEnumerable<Noun> GetNouns(this IEnumerable<Word> words) {
            return words.OfType<Noun>();
        }
        /// <summary>
        /// Returns all Pronouns in the WordList
        /// </summary>
        public static IEnumerable<Pronoun> GetPronouns(this IEnumerable<Word> words) {
            return words.OfType<Pronoun>();
        }
        /// <summary>
        /// Returns all Verbs in the WordList
        /// </summary>
        public static IEnumerable<Verb> GetVerbs(this IEnumerable<Word> words) {
            return words.OfType<Verb>();
        }
        public static IEnumerable<TransitiveVerb> GetTransitiveVerbs(this IEnumerable<Word> words) {
            return words.OfType<TransitiveVerb>();
        }

        public static string GetText(this IEnumerable<Word> words, string separater = " ") {
            return words.Aggregate("", (result, word) => result = result + separater + word.Text);
        }
    }

}