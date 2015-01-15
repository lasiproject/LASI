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
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="sentences">The sentences which comprise the Paragraph.</param>
        ///<param name="kind">Indicates the kind of paragraph.</param>
        public Paragraph(IEnumerable<Sentence> sentences, ParagraphKind kind) {
            ParagraphKind = kind;
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
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase startAfter) => Phrases.SkipWhile(r => r != startAfter).Skip(1);


        /// <summary>
        /// Returns a string representation of the Paragraph.
        /// </summary>
        /// <returns>A string representation of the Paragraph.</returns>
        public override string ToString() => base.ToString() + ": " + Sentences.Count() + " sentences\n\"" + Text + "\"";


        /// <summary>
        /// Gets the collection of Sentences which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Sentence> Sentences { get; }

        /// <summary>
        /// Gets the collection of Words which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Word> Words => Sentences.SelectMany(sentence => sentence.Words);
        /// <summary>
        /// Gets the collection of Phrases which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Phrase> Phrases => Sentences.SelectMany(sentence => sentence.Phrases);
        /// <summary>
        /// Gets the collection of Clauses which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Clause> Clauses => Sentences.SelectMany(sentence => sentence.Clauses);

        /// <summary>
        /// Gets the Document the Paragraph belongs to.
        /// </summary>
        public Document Document { get; private set; }
        /// <summary>
        /// Gets the ParagraphKind of the Paragraph.
        /// </summary>
        public ParagraphKind ParagraphKind { get; }

        /// <summary>
        /// Gets the textual content of the Paragraph.
        /// </summary>
        public string Text => text = text ?? string.Join(" ", Sentences.Select(sentence => sentence.Text));

        /// <summary>
        /// Returns an enumeration of all constituent Lexical structures of the Paragraph.
        /// Lexical structures that contain other Lexical structures, such as Clauses, will be followed by their constituents.
        /// </summary>
        public IEnumerable<ILexical> Lexicals => Clauses.SelectMany(c => c.Phrases.SelectMany(p => p.Words));

        public IEnumerable<IEntity> Entities => Lexicals.OfEntity();

        public IEnumerable<IVerbal> Verbals => Lexicals.OfVerbal();

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
        Enumeration,
        /// <summary>
        /// A paragraph containing a heading.
        /// </summary>
        Heading,
    }
}

