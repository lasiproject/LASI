using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// A specialization of System.Collections.Generic.List with provides methods to designed to simplify and facilitate the LASI project
    /// <seealso cref="System.Collections.Generic.List"/>
    /// </summary>
    public class WordList : IEnumerable<Word>
    {
        private IEnumerable<Word> items;
        #region Constructors

        /// <summary>
        /// Constructs an initially empty WordList
        /// </summary>
        public WordList() {
            items = new List<Word>();
        }

        /// <summary>
        /// Constructs a WordList with an initial set of Words
        /// </summary>
        /// <param name="initialWords">A collection of words which will comprise the initial contents of the WordList</param>
        public WordList(IEnumerable<Word> initialWords) {
            items = initialWords;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrives all words in the WordList which compare equal to a given Word
        /// </summary>
        /// <param name="toMatch">The Word to match</param>
        /// <returns>A WordList containing all words which match the argument</returns>
        /// <see cref="Word"/>
        public virtual WordList GetAllOccurances(Word toMatch) {
            return (WordList) from word in this
                              where word.Text == toMatch.Text
                              select word;
        }

        /// <summary>
        /// Retrives all words in the WordList which compare equal to a given Word or any of its provided synonyms
        /// </summary>
        /// <param name="toMatch">The word to match</param>
        /// <param name="synonymProvider">The Thesaurus instance which provides the synonyms to also match against</param>
        /// <returns>A WordList containing all words which match the argument or any of its provided synonyms</returns>
        /// <see cref="Word"/>
        /// <seealso cref="Thesaurus"/>
        public virtual WordList GetAllOccurances(Word toMatch, SynonymSet synonymProvider) {
            /* return (WordList)from outer in this
                              join inner in synonymProvider[toMatch] on outer equals inner
                              select outer;
             *
         */
            throw new NotImplementedException();
        }





        #endregion

        #region Properties

        public int Count {
            get {
                return items.Count();
            }
        }

        /// <summary>
        /// Returns all Nouns in the WordList
        /// </summary>
        public virtual IEnumerable<Noun> Nouns {
            get {
                return this.OfType<Noun>();
            }
        }
        /// <summary>
        /// Returns all Verbs in the WordList
        /// </summary>
        public virtual VerbList Verbs {
            get {
                return new VerbList(this.OfType<Verb>());
            }
        }
        public virtual VerbList TransitiveVerbs {
            get {
                return (VerbList) from word in this
                                  let tV = word as TransitiveVerb
                                  where tV != null && tV.DirectObject != null && tV.IndirectObject == null
                                  select tV;
            }
        }
        /// <summary>
        /// Returns all Pronouns in the WordList
        /// </summary>
        public virtual IEnumerable<Pronoun> Pronouns {
            get {
                return this.OfType<Pronoun>();
            }
        }
        /// <summary>
        /// Returns all Adjectives in the WordList
        /// </summary>
        public virtual IEnumerable<Adjective> Adjectives {
            get {
                return this.OfType<Adjective>();
            }
        }
        /// <summary>
        /// Returns all Adverbs in the WordList
        /// </summary>
        public virtual IEnumerable<Adverb> Adverbs {
            get {
                return this.OfType<Adverb>();
            }
        }
        /// <summary>
        /// Returns all Prepositions in the WordList
        /// </summary>
        public virtual IEnumerable<Preposition> Prepositions {
            get {
                return this.OfType<Preposition>();
            }
        }
        /// <summary>
        /// Gets or Sets the Word at the specified index in the WordList
        /// </summary>
        /// <param name="index">The 0 based index into the List</param>
        /// <returns>The Word at the specified index</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is less than 0 or greater than the number of Words in the List -1</exception>
        public virtual Word this[int index] {
            get {
                try {
                    return items.ToList()[index];
                } catch (ArgumentOutOfRangeException ex) {
                    throw new ArgumentOutOfRangeException(index >= 0 ?
                        String.Format("The given index: {0}, was > than the last index: {1}, in the WordList.", index, items.Count()) :
                        String.Format("The given index: {0} was negative.", index), ex);
                }
            }
        }

       

        #endregion

        #region Operators


        #endregion

        public IEnumerator<Word> GetEnumerator() {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }




}