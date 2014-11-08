using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Represents a discrete textual source which has been parsed tagged and re-composed.
    /// Examples documents, paragraphs, and sentences.
    /// </summary>
    /// <see cref="Document"/>
    /// <see cref="Paragraph"/>
    /// <see cref="Sentence"/>
    public interface IReifiedTextual
    {
        /// <summary>
        /// Gets the Clauses of the Textual structure.
        /// </summary>
        IEnumerable<Clause> Clauses { get; }
        /// <summary>
        /// Gets the Phrases of the Textual structure.
        /// </summary>
        IEnumerable<Phrase> Phrases { get; }
        /// <summary>
        /// Gets the Words of the Textual structure.
        /// </summary>
        IEnumerable<Word> Words { get; }
        /// <summary>
        /// Gets the Lexicals of the Textual structure.
        /// </summary>
        IEnumerable<ILexical> Lexicals { get; }
        /// <summary>
        /// Gets the Entities of the Textual structure.
        /// </summary>
        IEnumerable<IEntity> Entities { get; }
        /// <summary>
        /// Gets the Verbals of the Textual structure.
        /// </summary>
        IEnumerable<IVerbal> Verbals { get; }
        /// <summary>
        /// Gets the text of the Textual structure.
        /// </summary>
        string Text { get; }
    }
}