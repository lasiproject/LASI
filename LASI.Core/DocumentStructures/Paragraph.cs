using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

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
        /// <param name="kind">Indicates the kind of Paragraph.</param>
        /// <param name="sentences">The sentences which comprise the Paragraph.</param>
        public Paragraph(ParagraphKind kind, IEnumerable<Sentence> sentences)
        {
            ParagraphKind = kind;
            Sentences = sentences;
        }
        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="kind">Indicates the kind of Paragraph.</param>
        /// <param name="first">The first sentence of the Paragraph</param>
        /// <param name="rest">The rest of sentences which comprise the Paragraph.</param>
        public Paragraph(ParagraphKind kind, Sentence first, params Sentence[] rest) : this(kind, rest.Prepend(first)) { }

        /// <summary>
        /// Establish the nested links between the Paragraph, its parent Document, and the sentences comprising it.
        /// </summary>
        /// <param name="parentDocument">The document instance to identified as the Paragraph's parent.</param>
        internal void EstablishTextualLinks(Document parentDocument)
        {
            Document = parentDocument;
            foreach (var sentence in Sentences)
            {
                sentence.EstablishTextualLinks(this);
            }
        }
        /// <summary>
        /// Returns the sequence of Phrases which come after the given phrase through to the end of the Paragraph.
        /// </summary>
        /// <param name="startAfter">The Phrase which bounds the sequence.</param>
        /// <returns>The sequence of Phrases which come after the given phrase through to the end of the Paragraph.</returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase startAfter) => Phrases.SkipWhile(p => p != startAfter).Skip(1);


        /// <summary>
        /// Returns a string representation of the Paragraph.
        /// </summary>
        /// <returns>A string representation of the Paragraph.</returns>
        public override string ToString() => $"{base.ToString()}: {Sentences.Count()} sentences\n\"{Text}\"";


        /// <summary>
        /// The collection of Sentences which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Sentence> Sentences { get; }

        /// <summary>
        /// The collection of Words which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Word> Words => Sentences.SelectMany(s => s.Words);

        /// <summary>
        /// The collection of Phrases which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Phrase> Phrases => Sentences.SelectMany(s => s.Phrases);
        
        /// <summary>
        /// The collection of Clauses which comprise the Paragraph.
        /// </summary>
        public IEnumerable<Clause> Clauses => Sentences.SelectMany(s => s.Clauses);

        /// <summary>
        /// The Document the Paragraph belongs to.
        /// </summary>
        public Document Document { get; private set; }
        /// <summary>
        /// The ParagraphKind of the Paragraph.
        /// </summary>
        public ParagraphKind ParagraphKind { get; }

        /// <summary>
        /// The textual content of the Paragraph.
        /// </summary>
        public string Text => text ?? (text = string.Join(" ", Sentences.Select(s => s.Text)));

        /// <summary>
        /// Returns an enumeration of all constituent Lexical structures of the Paragraph.
        /// Lexical structures that contain other Lexical structures, such as Clauses, will be followed by their constituents.
        /// </summary>
        public IEnumerable<ILexical> Lexicals => Sentences.SelectMany(c => c.Phrases.SelectMany(p => p.Words));
        /// <summary>
        /// Gets all of the Entities in the Paragraph.
        /// </summary>
        public IEnumerable<IEntity> Entities => Lexicals.OfEntity();
        /// <summary>
        /// Gets all of the Verbals in the Paragraph.
        /// </summary>
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

