using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.DocumentStructures
{
    /// <summary>
    /// Represents a paragraph.
    /// </summary>
    public sealed class Paragraph
    {
        /// <summary>
        /// Initializes a new instance of the Paragraph class containing the given sentences and belonging to the given Document.
        /// </summary>
        /// <param name="sentences">The collection of sentences which comprise the Paragraph.</param>
        ///<param name="paragraphKind">Argument indicating the kind of parent.</param>
        public Paragraph(IEnumerable<Sentence> sentences, ParagraphKind paragraphKind) {
            ParagraphKind = paragraphKind;
            Sentences = sentences;
        }

        /// <summary>
        /// Establish the nested links between the Paragraph, its parent Document, and the sentences comprising it.
        /// </summary>
        /// <param name="parentDoc">The document instance to identified as the Paragraph's parent.</param>
        public void EstablishParent(Document parentDoc) {
            Document = parentDoc;
            foreach (var S in Sentences)
                S.EstablishParenthood(this);
        }
        /// <summary>
        /// Gets the sequence of Phrases which come after the given phrase through to the end of the Paragraph.
        /// </summary>
        /// <param name="start">The Phrase which bounds the sequence.</param>
        /// <returns>The sequence of Phrases which come after the given phrase through to the end of the Paragraph.</returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase start) {
            return Phrases.SkipWhile(r => r != start).Skip(1);
        }

        /// <summary>
        /// Returns a string representation of the Paragraph.
        /// </summary>
        /// <returns>A string representation of the Paragraph.</returns>
        public override string ToString() {
            return base.ToString() + ": " + Sentences.Count() + " sentences\n\"" + Text + "\"";
        }

        /// <summary>
        /// Gets the collection of Sentences which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Sentence> Sentences { get; private set; }

        /// <summary>
        /// Gets the collection of Words which comprise the Paragraph.
        /// </summary>Q
        public IEnumerable<Word> Words {
            get {
                return from S in Sentences
                       from W in S.Words
                       select W;
            }
        }
        /// <summary>
        /// Gets the collection of Phrases which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Phrase> Phrases {
            get {
                return from S in Sentences
                       from R in S.Phrases
                       select R;
            }
        }

        /// <summary>
        /// Gets the Document the Paragraph belongs to.
        /// </summary>
        public Document Document { get; private set; }
        /// <summary>
        /// Gets the ParagraphKind of the Paragraph.
        /// </summary>
        public ParagraphKind ParagraphKind { get; private set; }

        /// <summary>
        /// Gets the textual content of the Paragraph.
        /// </summary>
        public string Text {
            get {
                return text = text ?? string.Join(" ", Sentences.Select(s => s.Text));
            }
        }
        private string text;
    }
    /// <summary>
    /// Defines the Various Kinds of Paragraphs which a document may contain.
    /// </summary>
    public enum ParagraphKind
    {
        /// <summary>
        /// A paragraph containing one or more complete sentences.
        /// </summary>
        Default,
        /// <summary>
        /// A paragraph containing numbered or bulleted content.
        /// </summary>
        NumberedOrBullettedContent,
        /// <summary>
        /// A paragraph containing a heading.
        /// </summary>
        Heading
    }
}

