using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a complete sentence in an English written work.
    /// </summary>
    public class Sentence
    {
        /// <summary>
        /// Initializes a new instance of the Sentence class who contents are the given Phrases.
        /// </summary>
        /// <param name="phrases">The 0 or more Phrases which comprise the Sentence.</param>
        public Sentence(params Phrase[] phrases) {
            Clauses = new[] { new Clause(from P in phrases select P) };
        }
        /// <summary>
        /// Initializes a new instance of the Sentence class who contents are the given phrases and ends with the given SentencePunctuator.
        /// </summary>
        /// <param name="phrases">The sequence of Phrases which comprises the sentence.</param>
        ///<param name="sentencePunctuation">The SentencePunctuator which terminates the Sentence. If not provided, the default value of "." will be substituted.</param>
        public Sentence(IEnumerable<Phrase> phrases, SentencePunctuation sentencePunctuation = null) {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunctuation = sentencePunctuation == null ?
            new SentencePunctuation('.') :
            sentencePunctuation;
        }
        /// <summary>
        /// Initializes a new instance of the Sentence class who contents are the given words and ends with the given SentencePunctuator.
        /// </summary>
        /// <param name="words">The sequence of Words which comprises the Sentence.</param>
        /// <param name="sentencePunctuation">The SentencePunctuator which terminates the Sentence. If not provided, the default value of "." will be substituted.</param>
        public Sentence(IEnumerable<Word> words, SentencePunctuation sentencePunctuation = null) {
            Clauses = new[] { new Clause(from W in words select W) };
            EndingPunctuation = sentencePunctuation == null ?
               new SentencePunctuation('.') :
               sentencePunctuation;
        }
        /// <summary>
        /// Initializes a new instance of the Sentence class who contents are the given Clauses.
        /// </summary>
        /// <param name="clauses">The sequence of Clauses which comprises the Sentence.</param>
        public Sentence(IEnumerable<Clause> clauses) {
            Clauses = clauses;
            EndingPunctuation =
                new SentencePunctuation('.');
        }
        /// <summary>
        /// Initializes a new instance of the Sentence class who contents are the given Clauses.
        /// </summary>
        /// <param name="clauses">The sequence of Clauses which comprises the Sentence.</param>
        /// /// <param name="sentencePunctuation">The SentencePunctuator which terminates the Sentence. If not provided, the default value of "." will be substituted.</param>
        public Sentence(IEnumerable<Clause> clauses, SentencePunctuation sentencePunctuation = null) {
            Clauses = clauses;
            EndingPunctuation = sentencePunctuation == null ?
                new SentencePunctuation('.') :
                sentencePunctuation;
        }
        /// <summary>
        /// Gets all Phrases within the Sentence that lexically come after the given Phrase.
        /// </summary>
        /// <param name="phrase">The Phrase from which to start.</param>
        /// <returns>all Phrases within the Sentence that lexically come after the provided Phrase</returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase phrase) {
            return Phrases.SkipWhile(r => r != phrase).Skip(1);
        }



        /// <summary>
        /// Gets the ending punctuation character of the sentence.
        /// </summary>
        public SentencePunctuation EndingPunctuation {
            get;
            set;
        }

        /// <summary>
        /// Establishes the nested links between the Sentence, its parent Paragraph and the clauses which comprise it.
        /// </summary>
        /// <param name="paragraph">The Paragraph containing the Sentence.</param>
        public void EstablishParenthood(Paragraph paragraph) {
            ParentParagraph = paragraph;
            foreach (var C in Clauses)
                C.EstablishParent(this);
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
        /// Gets or sets the Paragraph to which the Sentence belongs.
        /// </summary>
        public Paragraph ParentParagraph {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the ParentDocument to which the Sentence Belongs.
        /// </summary>
        public Document ParentDocument {
            get {
                return ParentParagraph.ParentDocument;
            }
        }

        /// <summary>
        /// Returns a string representation of the Sentence.
        /// </summary>
        /// <returns>a string representation of the Sentence.</returns>
        public override string ToString() {
            return base.ToString() + " \"" + Text + "\"";
        }


    }

}
