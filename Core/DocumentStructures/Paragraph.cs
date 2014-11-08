using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Represents a paragraph.
    /// </summary>
    public sealed class Paragraph : IReifiedTextual
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
        /// <param name="parentDocument">The document instance to identified as the Paragraph's parent.</param>
        public void EstablishParent(Document parentDocument) {
            Document = parentDocument;
            foreach (var sentence in Sentences) {
                sentence.EstablishParenthood(this);
            }
        }
        /// <summary>
        /// Gets the sequence of Phrases which come after the given phrase through to the end of the Paragraph.
        /// </summary>
        /// <param name="startAfter">The Phrase which bounds the sequence.</param>
        /// <returns>The sequence of Phrases which come after the given phrase through to the end of the Paragraph.</returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase startAfter) {
            return Phrases.SkipWhile(r => r != startAfter).Skip(1);
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
        /// </summary>
        public IEnumerable<Word> Words {
            get {
                return from sentence in Sentences
                       from word in sentence.Words
                       select word;
            }
        }
        /// <summary>
        /// Gets the collection of Phrases which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Phrase> Phrases {
            get {
                return from sentence in Sentences
                       from phrase in sentence.Phrases
                       select phrase;
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
        public string Text { get { return text = text ?? string.Join(" ", Sentences.Select(sentence => sentence.Text)); } }

        public IEnumerable<Clause> Clauses { get { return Sentences.SelectMany(sentence => sentence.Clauses); } }


        /// <summary>
        /// Returns an enumeration of all constituent Lexical structures of the Paragraph.
        /// Lexical structures with Lexical that contain others, such as Clauses, will be followed by their constituents.
        /// </summary>
        public IEnumerable<ILexical> Lexicals {
            get {
                foreach (var clause in Clauses) {
                    yield return clause;
                    foreach (var phrase in clause.Phrases) {
                        yield return phrase;
                        foreach (var word in phrase.Words) {
                            yield return word;
                        }
                    }
                }
            }
        }
        public IEnumerable<IEntity> Entities { get { return Lexicals.OfEntity(); } }
        public IEnumerable<IVerbal> Verbals { get { return Lexicals.OfVerbal(); } }

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

