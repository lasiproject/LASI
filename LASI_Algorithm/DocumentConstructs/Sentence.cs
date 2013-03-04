using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class Sentence
    {
        public Sentence(params Phrase[] phrases) {
            Clauses = new[] { new Clause(from P in phrases select P) };

        }
        public Sentence(IEnumerable<Phrase> phrases, SentenceDelimiter EOSText = null) {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunctuation = EOSText == null ?
            new SentenceDelimiter('.') :
            EOSText;
        }
        public Sentence(IEnumerable<Word> words, SentenceDelimiter EOSText = null) {
            Clauses = new[] { new Clause(from W in words select W) };
            EndingPunctuation = EOSText == null ?
            new SentenceDelimiter('.') :
            EOSText;
        }
        public Sentence(IEnumerable<Clause> clauses) {
            Clauses = clauses;
            EndingPunctuation =
                new SentenceDelimiter('.');
        }
        /// <summary>
        /// Gets the ending punctuation character of the sentence.
        /// </summary>
        public SentenceDelimiter EndingPunctuation {
            get;
            set;
        }


        ///<summary>
        /// Returns the number of Words in a sentence
        /// </summary>
        public int GetWordCount() {
            return Words.Count();
        }

        /// <summary>
        /// Returns the number of Clauses in a sentence
        /// </summary>
        public int GetClauseCount() {
            return Clauses.Count();
        }

        /// <summary>
        /// Returns the number of Phrases in a sentence
        /// </summary>
        public int GetPhraseCount() {
            return Phrases.Count();
        }

        /// <summary>
        /// Gets the sequence of Clauses which comprise the sentence.
        /// </summary>
        public IEnumerable<Clause> Clauses {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the sequence of Phrases which comprise the sentence.
        /// </summary>
        /// 
        public IEnumerable<Phrase> Phrases {
            get {
                return from C in Clauses
                       from P in C.Phrases
                       select P;
            }
        }
        /// <summary>
        /// Gets the sequence of Words which comprise the sentence.
        /// </summary>
        public IEnumerable<Word> Words {
            get {
                return from P in Phrases
                       from W in P.Words
                       select W;
            }
        }
        /// <summary>
        /// Gets the concatenated text content of all of the Words which compose the Sentence.
        /// </summary>
        public string Text {
            get {
                return Phrases.Aggregate("", (txt, P) => txt + " " + P.Text);
            }
        }


        public void EstablishParenthood(Paragraph paragraph) {
            ParentParagraph = paragraph;
            ParentDocument = paragraph.ParentDocument;
            foreach (var C in Clauses)
                C.EstablishParent(this);
        }

        /// <summary>
        /// Gets or sets the Paragraph to which the Sentence belongs.
        /// </summary>
        public Paragraph ParentParagraph {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ParentDocument to which the Sentence Belongs.
        /// </summary>
        public Document ParentDocument {
            get;
            set;
        }

        /// <summary>
        /// Returns a string representation of the Sentence.
        /// </summary>
        /// <returns>A string representation of the Sentence.</returns>
        public override string ToString() {
            return base.ToString() + "\"" + Text + "\"";
        }


    }

}
