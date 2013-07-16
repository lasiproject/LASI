using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.DocumentConstructs
{
    /// <summary>
    /// Represents a single sentence.
    /// </summary>
    public sealed class Sentence
    {

        private Sentence() {
            ID = IDProvider++;
            EndingPunctuation = EndingPunctuation ?? new SentenceDelimiter('.');
        }

        /// <summary>
        /// Initializes a new instance of the Sentence class.
        /// </summary>
        /// <param name="phrases">The sequence of Phrase elements which comprise the Sentence.</param>
        /// <param name="sentencePunctuation">The SentenceDelimiter which terminates the Sentence. If not provided, a period will be assumed, and an instance of SentenceDelimiter created to represent it.</param>
        public Sentence(IEnumerable<Phrase> phrases, SentenceDelimiter sentencePunctuation = null)
            : this() {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunctuation = sentencePunctuation;
        }
        /// <summary>
        /// Initializes a new instance of the Sentence class.
        /// </summary>
        /// <param name="words">The sequence of Word elements which comprise the Sentence.</param>
        /// <param name="sentencePunctuation">The SentenceDelimiter which terminates the Sentence. If not provided, a period will be assumed, and an instance of SentenceDelimiter created to represent it.</param>
        public Sentence(IEnumerable<Word> words, SentenceDelimiter sentencePunctuation = null)
            : this() {
            Clauses = new[] { new Clause(from W in words select W) };
            EndingPunctuation = sentencePunctuation;
        }
        /// <summary>
        /// Initializes a new instance of the Sentence class.
        /// </summary>
        /// <param name="clauses">The sequence of Clause elements which comprise the Sentence.</param>
        /// <param name="sentencePunctuation">The SentenceDelimiter which terminates the Sentence. If not provided, a period will be assumed, and an instance of SentenceDelimiter created to represent it.</param>
        public Sentence(IEnumerable<Clause> clauses, SentenceDelimiter sentencePunctuation = null)
            : this() {
            Clauses = clauses;
            EndingPunctuation = sentencePunctuation;
        }
        /// <summary>
        /// Returns the Phrase elements in the Sentence, following and not including the given Phrase. 
        /// </summary>
        /// <param name="phrase">The Phrase from which to start.</param>
        /// <returns>The Phrase elements in the Sentence, following and not including the given Phrase. </returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase phrase) {
            return Phrases.SkipWhile(r => r != phrase).Skip(1);
        }



        /// <summary>
        /// Gets the ending punctuation character of the sentence.
        /// </summary>
        public SentenceDelimiter EndingPunctuation {
            get;
            private set;
        }

        /// <summary>
        /// Establishes the linkages between the Sentence, its parent Paragraph, and its child Clauses.
        /// </summary>
        /// <param name="parent">The Paragraph to which the Sentence belongs.</param>
        public void EstablishParenthood(Paragraph parent) {
            Paragraph = parent;
            foreach (var C in Clauses)
                C.EstablishParent(this);
        }


        /// <summary>
        /// Gets the sequence of Clauses which comprise the sentence.
        /// </summary>
        public IEnumerable<Clause> Clauses {
            get;
            private set;
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
                return from c in Clauses
                       from P in Phrases
                       from W in P.Words
                       select W;
            }
        }
        /// <summary>
        /// Gets the concatenated text content of all of the Words which compose the Sentence.
        /// </summary>
        public string Text {
            get {
                return (Phrases.Aggregate("", (sum, currentPhrase) => sum + " " + currentPhrase.Text) + EndingPunctuation.Text).Trim();
            }
        }



        /// <summary>
        /// Gets the Paragraph to which the Sentence belongs.
        /// </summary>
        public Paragraph Paragraph {
            get;
            private set;
        }
        /// <summary>
        /// Gets the Document to which the Sentence Belongs.
        /// </summary>
        public Document Document {
            get {
                return Paragraph != null ? Paragraph.Document : null;
            }
        }

        /// <summary>
        /// Returns a string representation of the Sentence.
        /// </summary>
        /// <returns>A string representation of the Sentence.</returns>
        public override string ToString() {
            return base.ToString() + " \"" + Text + "\"";
        }

        /// <summary>
        /// for subject binder
        /// </summary>
        public bool isStandard = true;

        private static int IDProvider;
        /// <summary>
        /// Gets the unique ID number of the Sentence.
        /// </summary>
        public int ID {
            get;
            private set;
        }
    }

}
