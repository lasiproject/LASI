using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.DocumentConstructs
{
    public class Sentence
    {
        public Sentence(params Phrase[] phrases) {
            Clauses = new[] { new Clause(from P in phrases select P) };
        }
        public Sentence(IEnumerable<Phrase> phrases, SentenceDelimiter sentencePunctuation = null) {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunctuation = sentencePunctuation == null ?
            new SentenceDelimiter('.') :
            sentencePunctuation;
        }
        public Sentence(IEnumerable<Word> words, SentenceDelimiter sentencePunctuation = null) {
            Clauses = new[] { new Clause(from W in words select W) };
            EndingPunctuation = sentencePunctuation == null ?
               new SentenceDelimiter('.') :
               sentencePunctuation;
        }

        public Sentence(IEnumerable<Clause> clauses, SentenceDelimiter sentencePunctuation = null) {
            Clauses = clauses;
            EndingPunctuation = sentencePunctuation == null ?
                new SentenceDelimiter('.') :
                sentencePunctuation;
        }

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
                return (Phrases.Aggregate("", (txt, P) => txt + " " + P.Text) + EndingPunctuation.Text).Trim();
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
                return Paragraph.Document;
            }
        }

        /// <summary>
        /// Returns a string representation of the Sentence.
        /// </summary>
        /// <returns>A string representation of the Sentence.</returns>
        public override string ToString() {
            return base.ToString() + " \"" + Text + "\"";
        }

        //for subject binder
        public bool isStandard = true;


    }

}
