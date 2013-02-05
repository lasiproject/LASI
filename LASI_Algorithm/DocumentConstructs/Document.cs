using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    public class Document
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allWords">The collection of words which corresponds to all text in the document.</param>
        public Document(IEnumerable<Word> allWords) {
            Words = allWords.ToList();

            EstablishLexicalLinks();
            foreach (var w in allWords)
                w.ParentDoc = this;
        }
        public Document(IEnumerable<Sentence> allSentences) {
            _sentences = allSentences;
            Words = (from S in _sentences
                     from W in S.Words
                     select W).ToList();
            foreach (var w in Words)
                w.ParentDoc = this;
            foreach (var P in _paragraphs) {
                P.EstablishParent(this);
            }
        }
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
                w.ParentDoc = this;
        }

        #endregion

        #region Methods
        private void EstablishLexicalLinks() {
            for (int i = 1; i < Words.Count() - 1; ++i) {
                Words[i].PreviousWord = Words[i - 1];
                Words[i - 1].NextWord = Words[i];
            }

            var previousWord = Words[Words.Count - 1];
            if (Words.ToList().IndexOf(previousWord) > 0)
                previousWord.PreviousWord = Words[Words.Count - 2];
            else
                previousWord.PreviousWord = null;
            previousWord.NextWord = null;
        }

        public void PrintByLinkage() {
            var W = Words.First();
            while (W != null) {
                Console.Write(W.Text + " ");
                W = W.NextWord;
            }
        }


        #endregion

        #region Properties


        public IEnumerable<Sentence> Sentences {
            get {
                return (from P in _paragraphs
                        select P.Sentences) as IReadOnlyCollection<Sentence>;
            }

        }
        public IEnumerable<Paragraph> Paragraphs {
            get {
                return _paragraphs.ToList().AsReadOnly();
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