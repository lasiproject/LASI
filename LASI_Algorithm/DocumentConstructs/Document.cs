using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// A line structure containing all of he paragraph, sentence, phrase, and w objects in a document.
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
            _paragraphs = allParagrpahs;
            _sentences = from P in Paragraphs
                         from S in P.Sentences
                         select S;
            _phrases = from S in Sentences
                       from R in S.Phrases
                       select R;
            _words = (from S in Sentences
                      from W in S.Words
                      select W).ToList();
            foreach (var w in Words)
                w.ParentDocument = this;
            foreach (var P in _paragraphs) {
                P.EstablishParent(this);
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
            var W = Words.First();
            while (W != null) {
                Console.Write(W.Text + " ");
                W = W.NextWord;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Prints out the entire contents of the document, from left to right, by using the using the lexical links of each of its phrases.
        /// </summary>
        public void PrintByPhraseLinkage() {
            var P = Phrases.First();
            while (P != null) {
                Console.Write(P.Text + " ");
                P = P.NextPhrase;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns all of the Action identified within the docimument.
        /// </summary>
        /// <returns>all of the Action identified within the docimument.</returns>
        public IEnumerable<ITransitiveAction> GetActions() {
            var wordResults = from ITransitiveAction V in Words.GetVerbs()
                              select V;
            var phraseResults = from ITransitiveAction VP in Phrases.GetVerbPhrases()
                                select VP;
            return from A in wordResults.Concat(phraseResults)
                   orderby A as Word != null ? (A as Word).ID : (A as Phrase).Words.Last().ID ascending
                   select A;
        }

        /// <summary>
        /// Returns all of the word and phrase level entities identified in the document.
        /// </summary>
        /// <returns> All of the word and phrase level entities identified in the document.</returns>
        public IEnumerable<IEntity> GetEntities() {
            return from E in Words.OfType<IEntity>().Concat<IEntity>(Phrases.OfType<IEntity>())
                   orderby E is Word ? (E as Word).ID : (E as Phrase).Words.Last().ID ascending
                   select E;
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
        private IEnumerable<Phrase> _phrases;
        private IEnumerable<Sentence> _sentences;
        private IEnumerable<Paragraph> _paragraphs;




        #endregion
    }
}