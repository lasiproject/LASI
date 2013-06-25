using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LASI.Utilities;


namespace LASI.Algorithm.DocumentConstructs
{
    /// <summary>
    /// A line structure containing all of the paragraph, sentence, a, and adverb objects in a document.
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
                                         where p.ParagraphKind == ParagraphKind.EnumerationContent
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
                      from w in s.Words.Concat(s.EndingPunctuation.AsEnumerable())
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
            return from e in _words.GetNouns().Concat<IEntity>(_words.GetPronouns()).Concat<IEntity>(_phrases.GetNounPhrases())
                   orderby e is Word ? (e as Word).ID : (e as Phrase).Words.Last().ID ascending
                   select e;
        }
        //public IEnumerable<Page> Paginate(int charactersPerLine, int linePerPage) {
        //    var totalChars = Sentences.Sum(s => s.Text.Length) / (charactersPerLine * linePerPage) + 1;
        //    var lenC = 0;
        //    var toSkip = 0;
        //    return from i in Enumerable.Range(1, totalChars)
        //           let sents =
        //           from sentence in Sentences.Skip(toSkip).TakeWhile(s => {
        //               toSkip++;
        //               lenC += s.Text.Length;
        //               if (lenC < linePerPage * charactersPerLine)
        //                   return true;
        //               else {
        //                   lenC = 0;
        //                   return false;
        //               }
        //           })
        //           select sentence
        //           select new Page(sents, this);
        //}
        public IEnumerable<Page> Paginate(int sentencesPerPage) {
            var numPages = Sentences.Count() / sentencesPerPage + 1;
            return from i in Enumerable.Range(1, numPages).Select(i => i * sentencesPerPage)
                   from sentence in Sentences
                   group sentence by sentence.ID - sentencesPerPage < i into g
                   where g.Key
                   select new Page(g, this);

        }

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
                return _paragraphs;
            }
        }
        public IEnumerable<Paragraph> EnumContainingParagraphs {
            get {
                return _enumContainingParagraphs;
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

    }

    public class Page
    {
        internal Page(IEnumerable<Sentence> sentences, Document document) {
            Document = document;
            Sentences = sentences;
            Paragraphs = from s in sentences
                         group s by s.Paragraph into g
                         select g.Key;
        }

        internal Page(IEnumerable<Paragraph> paragraphs, Document document) {
            Document = document;
            Paragraphs = paragraphs;
            Sentences = from p in paragraphs
                        from s in p.Sentences
                        select s;
        }

        public IEnumerable<Sentence> Sentences {
            get;
            private set;
        }

        public IEnumerable<Paragraph> Paragraphs {
            get;
            private set;
        }
        public Document Document {
            get;
            private set;
        }
    }
}