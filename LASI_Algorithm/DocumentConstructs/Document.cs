using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LASI.Utilities;


namespace LASI.Algorithm.DocumentConstructs
{
    /// <summary>
    /// A data structure containing all of the paragraph, sentence, a, and word objects in a document.
    /// Provides overalapping direct and indirect access to all of its children, 
    /// e.g. such as myDoc.Paragraphs.Sentences.Phrases.Words will get all the words in the document in linear order
    /// comparatively: myDoc.Words; yields the same collection.
    /// </summary>
    public sealed class Document
    {
        #region Constructors
        private List<Word> SynonymousGroups = new List<Word>();
        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        /// <param name="paragrpahs">The collection of paragraphs which contain all text in the document.</param>
        public Document(IEnumerable<Paragraph> paragrpahs) {
            _paragraphs = paragrpahs.ToList();
            _enumContainingParagraphs = (from p in _paragraphs
                                         where p.ParagraphKind == ParagraphKind.NumberedOrBullettedContent
                                         select p).ToList();

            AssignMembers(paragrpahs);
            foreach (var p in _paragraphs) {
                p.EstablishParent(this);
            }
            EstablishLexicalLinks();
        }

        private void AssignMembers(IEnumerable<Paragraph> paragrpahs) {
            _sentences = (from p in _paragraphs
                          from s in p.Sentences
                          where s.Words.GetVerbs().Any()
                          select s).ToList();
            _phrases = (from s in _sentences
                        from r in s.Phrases
                        select r).ToList();
            _words = (from s in _sentences
                      from w in s.Words.Concat(new[] { s.EndingPunctuation })
                      select w).ToList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Establishes the compositional linkages over all of the structures which comprise the Document.
        /// </summary>
        private void EstablishLexicalLinks() {
            if (_words.Count > 1) {
                for (int i = 1; i < _words.Count(); ++i) {
                    _words[i].PreviousWord = _words[i - 1];
                    _words[i - 1].NextWord = _words[i];
                }

                var lastWord = _words[_words.Count - 1];
                if (_words.IndexOf(lastWord) > 0)
                    lastWord.PreviousWord = _words[_words.Count - 1];
                else
                    lastWord.PreviousWord = null;
                lastWord.NextWord = null;
            }
            if (_phrases.Count() > 1) {

                for (var i = 1; i < _phrases.Count; ++i) {
                    _phrases[i].PreviousPhrase = _phrases[i - 1];
                    _phrases[i - 1].NextPhrase = _phrases[i];
                }
            }

        }



        /// <summary>
        /// Returns all of the Action identified within the docimument.
        /// </summary>
        /// <returns>all of the Action identified within the docimument.</returns>
        public IEnumerable<IVerbal> GetActions() {
            return from a in _words.GetVerbs().Concat<IVerbal>(_phrases.GetVerbPhrases())
                   orderby a is Word ? (a as Word).ID : (a as Phrase).Words.Last().ID ascending
                   select a;
        }

        /// <summary>
        /// Returns all of the word and phrase level describables identified in the document.
        /// </summary>
        /// <returns> All of the word and phrase level describables identified in the document.</returns>
        public IEnumerable<IEntity> GetEntities() {
            return from e in _words.OfType<IEntity>().Concat(Phrases.OfType<IEntity>())
                   orderby e is Word ? (e as Word).ID : (e as Phrase).Words.Last().ID ascending
                   select e;
        }
        /// <summary>
        /// Returns a representation of the Document as sequence of pages based on the given number sentences per page.
        /// </summary>
        /// <param name="sentencesPerPage">The number of sentences each page can contain. This varies inversely with the number of pages in the resulting sequence.</param>
        /// <returns>A representation of the Document as sequence of pages.</returns>
        public IEnumerable<Page> Paginate(int sentencesPerPage) {
            if (sentencesPerPage < 1) {
                throw new ArgumentOutOfRangeException(
                    "sentencesPerPage",
                    "The supplied page length was invalid. A page must be allowed to have at least 1 sentence."
                );
            }
            return from subSequence in Sentences.Split(sentencesPerPage)
                   select new Page(subSequence, this);
        }
        /// <summary>
        /// Returns a string representation of the current document. The result contains the entire textual contents of the Document, thus resulting in the instance's full materialization and reification.
        /// </summary>
        /// <returns>A string representation of the current document. The result contains the entire textual contents of the Document, thus resulting in the instance's full materialization and reification.</returns>
        public override string ToString() {
            return Paragraphs.Format(p => p + "\n\n");
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the Sentences the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Sentence> Sentences {
            get {
                return _sentences;
            }

        }

        /// <summary>
        /// Gets the Paragraphs the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Paragraph> Paragraphs {
            get {
                return _paragraphs.Except(_enumContainingParagraphs);
            }
        }


        /// <summary>
        /// Gets the Phrases the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Phrase> Phrases {
            get {
                return _phrases;
            }
        }
        /// <summary>
        /// Gets the Words the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Word> Words {
            get {
                return _words;
            }
        }

        /// <summary>
        /// The name of the file the Document instance was parsed from.
        /// </summary>
        public string FileName {
            get;
            set;
        }

        #endregion

        #region Fields

        private IList<Word> _words;
        private IList<Phrase> _phrases;
        private IList<Sentence> _sentences;
        private IList<Paragraph> _paragraphs;
        private IList<Paragraph> _enumContainingParagraphs;



        #endregion
        /// <summary>
        /// Represents a page of document. Pages are a somewhat arbitrary segement of a Document, containing some subset of the Sentences a document contains.
        /// </summary>
        public sealed class Page
        {
            /// <summary>
            /// Initializes a new instance of the Page class.
            /// </summary>
            /// <param name="sentences">The sentences which comprise the Page.</param>
            /// <param name="document">The Document to which the page belongs.</param>
            internal Page(IEnumerable<Sentence> sentences, Document document) {
                Document = document;
                Sentences = sentences;

            }

            /// <summary>
            /// Gets the sentences which comprise the Page.
            /// </summary>
            public IEnumerable<Sentence> Sentences {
                get;
                private set;
            }
            /// <summary>
            /// Gets the Document to which the page belongs.
            /// </summary>
            public Document Document {
                get;
                private set;
            }
        }
    }


}