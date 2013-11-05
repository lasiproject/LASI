using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.DocumentStructures
{
    /// <summary>
    /// Represents a single sentence.
    /// </summary>
    public sealed class Sentence
    {
        #region Constructors
        private Sentence() {
        }

        /// <summary>
        /// Initializes a new instance of the Sentence class.
        /// </summary>
        /// <param name="phrases">The sequence of Phrase elements which comprise the Sentence.</param>
        /// <param name="sentencePunctuation">The SentenceEnding which terminates the Sentence. If not provided, a period will be assumed, and an instance of SentenceEnding created to represent it.</param>
        public Sentence(IEnumerable<Phrase> phrases, SentenceEnding sentencePunctuation)
            : this() {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunctuation = sentencePunctuation ?? new SentenceEnding('.');
        }

        /// <summary>
        /// Initializes a new instance of the Sentence class.
        /// </summary>
        /// <param name="clauses">The sequence of Clause elements which comprise the Sentence.</param>
        /// <param name="sentencePunctuation">The SentenceEnding which terminates the Sentence. If not provided, a period will be assumed, and an instance of SentenceEnding created to represent it.</param>
        public Sentence(IEnumerable<Clause> clauses, SentenceEnding sentencePunctuation)
            : this() {
            Clauses = clauses;
            EndingPunctuation = sentencePunctuation ?? new SentenceEnding('.');
        }
        /// <summary>
        /// Returns the Phrase elements in the Sentence, following and not including the given Phrase. 
        /// </summary>
        /// <param name="phrase">The Phrase from which to start.</param>
        /// <returns>The Phrase elements in the Sentence, following and not including the given Phrase. </returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase phrase) {
            return Phrases.SkipWhile(r => r != phrase).Skip(1);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a string representation of the Sentence.
        /// </summary>
        /// <returns>A string representation of the Sentence.</returns>
        public override string ToString() {
            return base.ToString() + " \"" + Text + "\"";
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the ending punctuation character of the sentence.
        /// </summary>
        public SentenceEnding EndingPunctuation { get; private set; }

        /// <summary>
        /// Establishes the linkages between the Sentence, its parent Paragraph, and its child Clauses.
        /// </summary>
        /// <param name="parent">The Paragraph to which the Sentence belongs.</param>
        public void EstablishParenthood(Paragraph parent) {
            EndsParagraph = this == parent.Sentences.Last();
            BeginsParagraph = this == parent.Sentences.First();

            Paragraph = parent;
            foreach (var C in Clauses)
                C.EstablishParent(this);
        }



        /// <summary>
        /// Gets the sequence of Clauses which comprise the sentence.
        /// </summary>
        public IEnumerable<Clause> Clauses { get; private set; }
        /// <summary>
        /// Gets the sequence of Phrases which comprise the sentence.
        /// </summary>
        /// 
        public IEnumerable<Phrase> Phrases { get { return (from C in Clauses from P in C.Phrases select P).Append(new SymbolPhrase(new[] { EndingPunctuation })); } }
        /// <summary>
        /// Gets the sequence of Words which comprise the sentence.
        /// </summary>
        public IEnumerable<Word> Words { get { return (from C in Clauses from P in C.Phrases from W in P.Words select W); } }
        /// <summary>
        /// Gets the concatenated text content of all of the Words which compose the Sentence.
        /// </summary>
        public string Text {
            get {
                return (Phrases.Take(Phrases.Count() - 1).Aggregate("", (sum, currentPhrase) => sum + " " + currentPhrase.Text) + Phrases.Last().Text).Trim();
            }
        }



        /// <summary>
        /// Gets the Paragraph to which the Sentence belongs.
        /// </summary>
        public Paragraph Paragraph { get; private set; }
        /// <summary>
        /// Gets the Document to which the Sentence Belongs.
        /// </summary>
        public Document Document {
            get {
                return Paragraph != null ? Paragraph.Document : null;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating wether the Sentence is an inverted sentence.
        /// </summary>
        public bool IsInverted { get; set; }


        #endregion

        /// <summary>
        /// Gets a value indicating wether the Sentence is the first Sentence in its Paragraph.
        /// </summary>
        public bool BeginsParagraph { get; private set; }
        /// <summary>
        /// Gets a value indicating wether the Sentence is the Last Sentence in its Paragraph.
        /// </summary>
        public bool EndsParagraph { get; private set; }
    }

}
