using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// A data structure containing all of he paragraph, sentence, phrase, and word objects in a document.
    /// Provides overalapping direct and indirect access to all of its children, 
    /// E.g. such as myDoc.Paragraphs.Sentences.Phrases.Words will get all the words in the document in linear order
    /// comparatively: myDoc.Words; yields the same collection.
    /// </summary>
    public class Document
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        /// <param name="allWords">The collection of words which corresponds to all text in the document.</param>
        public Document(IEnumerable<Word> allWords) {
            Words = allWords.ToList();

            EstablishLexicalLinks();
            foreach (var w in allWords)
                w.ParentDocument = this;
        }
        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        /// <param name="allWords">The collection of sentences which contain all text in the document.</param>
        public Document(IEnumerable<Sentence> allSentences) {
            _sentences = allSentences;
            Words = (from S in _sentences
                     from W in S.Words
                     select W).ToList();
            EstablishLexicalLinks();
            foreach (var w in Words)
                w.ParentDocument = this;
            foreach (var P in _paragraphs) {
                P.EstablishParent(this);
            }
        }
        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        /// <param name="allWords">The collection of paragraphs which contain all text in the document.</param>
        public Document(IEnumerable<Paragraph> allParagrpahs) {
            _paragraphs = allParagrpahs;
            foreach (var P in _paragraphs) {
                P.EstablishParent(this);
            }
            _sentences = from P in _paragraphs
                         from S in P.Sentences
                         select S;
            Words = (from S in _sentences
                     from W in S.Words
                     select W).ToList();
            foreach (var w in Words)
                w.ParentDocument = this;
            EstablishLexicalLinks();
        }

        #endregion

        #region Methods

        private void EstablishLexicalLinks() {
            if (Words.Count > 1) {
                for (int i = 1; i < Words.Count(); ++i) {
                    Words[i].PreviousWord = Words[i - 1];
                    Words[i - 1].NextWord = Words[i];
                }

                var lastWord = Words[Words.Count - 1];
                if (Words.IndexOf(lastWord) > 0)
                    lastWord.PreviousWord = Words[Words.Count - 1];
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

        /// Returns the word instance at x location in the document 
        public Word WordAt(int loc)
        {
            Word wrd = new Word("NoWordAtLoc");
            if (loc < this.Words.Count)
                return this.Words.ElementAt(loc);
            else
                return wrd;
        }

        /// Returns the text  of word instance at x location in the document
        public string WordTextAt(int loc)
        {
            string strng = "NoWordAtLoc";
            if(loc < this.Words.Count)
                return this.Words.ElementAt(loc).Text;
            else
                return strng;

        }

        /// Returns the sentence instance at x location 
        public Sentence SentenceAt(int loc)
        {
            List<Word> sent = new List<Word>();
            sent.Add(new Word("No"));
            sent.Add(new Word("Sentence"));
            sent.Add(new Word("At"));
            sent.Add(new Word("This"));
            sent.Add(new Word("Location."));

            Sentence s1 = new Sentence(sent);
            if (loc < this.Sentences.Count())
                return this.Sentences.ElementAt(loc);
            else
                return s1;
        }

        ///  Returns the sentence instance text at x location
        public string SentenceTextAt(int loc)
        {
            if (loc < this.Sentences.Count())
                return this.Sentences.ElementAt(loc).Text;
            else
                return "No Sentence at this Location.";
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

        #endregion

        #region Properties


        /// <summary>
        /// Gets the Sentences the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Sentence> Sentences {
            get {
                return from P in _paragraphs
                       from S in P.Sentences
                       select S;
            }

        }

        /// <summary>
        /// Gets the Paragraphs the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Paragraph> Paragraphs {
            get {
                return _paragraphs.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the Phrases the document contains in linear, left to right order.
        /// </summary>
        public IEnumerable<Phrase> Phrases {
            get {
                return from S in _sentences
                       from R in S.Phrases
                       select R;
            }
        }

        #endregion

        #region Fields

        private IEnumerable<Paragraph> _paragraphs = new List<Paragraph>();
        private IEnumerable<Sentence> _sentences = new List<Sentence>();
        public List<Word> Words {
            get;
            private set;
        }


        #endregion
    }
}