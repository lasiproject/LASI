using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Represents a discrete textual source which has been parsed tagged and re-composed.
    /// Examples documents, paragraphs, and sentences.
    /// </summary>
    /// <seealso cref="Document"/>
    /// <seealso cref="Paragraph"/>
    /// <seealso cref="Sentence"/>
    public interface IReifiedTextual
    {
        /// <summary>
        /// Gets the constituent clauses of the textual structure.
        /// </summary>
        IEnumerable<Clause> Clauses { get; }
        /// <summary>
        /// Gets the constituent phrases of the textual structure.
        /// </summary>
        IEnumerable<Phrase> Phrases { get; }
        /// <summary>
        /// Gets the constituent words of the textual structure.
        /// </summary>
        IEnumerable<Word> Words { get; }
        /// <summary>
        /// Gets the constituent lexicals of the textual structure.
        /// </summary>
        IEnumerable<ILexical> Lexicals { get; }
        /// <summary>
        /// Gets the constituent entities of the textual structure.
        /// </summary>
        IEnumerable<IEntity> Entities { get; }
        /// <summary>
        /// Gets the constituent verbals of the Textual structure.
        /// </summary>
        IEnumerable<IVerbal> Verbals { get; }
        /// <summary>
        /// Gets the text of the Textual structure.
        /// </summary>
        string Text { get; }
    }
}