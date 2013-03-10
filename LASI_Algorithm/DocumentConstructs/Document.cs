using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// a line structure containing all of he paragraph, sentence, phrase, and w objects in a document.
    /// Provides overalapping direct and indirect access to all of its children, 
    /// e.g. such as myDoc.Paragraphs.Sentences.Phrases.Words will get all the words in the document in linear order
    /// comparatively: myDoc.Words; yields the same collection.
    /// </summary>
    public class Document
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        /// <param name="allWords">The collection of paragraphs which contain all text in the document.</param>
        public Document(IEnumerable<Paragraph> allParagrpahs) {
            _paragraphs = allParagrpahs.ToList();
            _sentences = (from p in Paragraphs
                          from s in p.Sentences
                          select s).ToList();
            _phrases = (from s in Sentences
                        from r in s.Phrases
                        select r).ToList();
            _words = (from s in Sentences
                      from w in s.Words
                      select w).ToList();
            foreach (var p in _paragraphs) {
                p.EstablishParent(this);
            }
            EstablishLexicalLinks();
        }

        #endregion

        #region Methods

        private void EstablishLexicalLinks() {
            if (_words.Count > 1) {
                for (int i = 1; i < Words.Count(); ++i) {
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
            if (Phrases.Count() > 1) {
                var phraseList = Phrases.ToList();
                for (var i = 1; i < phraseList.Count; ++i) {
                    phraseList[i].PreviousPhrase = phraseList[i - 1];
                    phraseList[i - 1].NextPhrase = phraseList[i];
                }
            }

        }

        /// Returns the w instance at x location in the document 
        public Word WordAt(int loc) {
            if (loc < this._words.Count)
                return this.Words.ElementAt(loc);
            else
                throw new ArgumentOutOfRangeException("Document.WordAt");
        }

        /// Returns the text  of w instance at x location in the document
        public string WordTextAt(int loc) {
            if (loc < this._words.Count)
                return this.Words.ElementAt(loc).Text;
            else
                throw new ArgumentOutOfRangeException("Document.WordTextAt");
        }

        /// Returns the sentence instance at x location 
        public Sentence SentenceAt(int loc) {

            if (loc < this.Sentences.Count())
                return this.Sentences.ElementAt(loc);
            else
                throw new ArgumentOutOfRangeException("Document.SentenceAt");
        }

        ///  Returns the sentence instance text at x location
        public string SentenceTextAt(int loc) {
            if (loc < this.Sentences.Count())
                return this.Sentences.ElementAt(loc).Text;
            else
                throw new ArgumentOutOfRangeException("Document.SentenceTextAt");
        }

        /// <summary>
        /// Prints out the entire contents of the document, from left to right, by using the using the lexical links of each of its words.
        /// </summary>
        public void PrintByWordLinkage() {
            var w = Words.First();
            while (w != null) {
                Console.Write(w.Text + " ");
                w = w.NextWord;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Prints out the entire contents of the document, from left to right, by using the using the lexical links of each of its phrases.
        /// </summary>
        public void PrintByPhraseLinkage() {
            var r = Phrases.First();
            while (r != null) {
                Console.Write(r.Text + " ");
                r = r.NextPhrase;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns all of the Action identified within the docimument.
        /// </summary>
        /// <returns>all of the Action identified within the docimument.</returns>
        public IEnumerable<ITransitiveVerbial> GetActions() {
            var wordResults = from ITransitiveVerbial v in Words.GetVerbs()
                              select v;
            var phraseResults = from ITransitiveVerbial vp in Phrases.GetVerbPhrases()
                                select vp;
            return from a in wordResults.Concat(phraseResults)
                   orderby a as Word != null ? (a as Word).ID : (a as Phrase).Words.Last().ID ascending
                   select a;
        }

        /// <summary>
        /// Returns all of the word and phrase level entities identified in the document.
        /// </summary>
        /// <returns> All of the word and phrase level entities identified in the document.</returns>
        public IEnumerable<IEntity> GetEntities() {
            return from e in Words.OfType<IEntity>().Concat<IEntity>(Phrases.OfType<IEntity>())
                   orderby e is Word ? (e as Word).ID : (e as Phrase).Words.Last().ID ascending
                   select e;
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

        #endregion

        #region Fields

        private IList<Word> _words;
        private IList<Phrase> _phrases;
        private IList<Sentence> _sentences;
        private IList<Paragraph> _paragraphs;

        #endregion
    }
}