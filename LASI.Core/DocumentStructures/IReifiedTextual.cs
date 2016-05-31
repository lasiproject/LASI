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
        /// The constituent clauses of the textual structure.
        /// </summary>
        IEnumerable<Clause> Clauses { get; }
        /// <summary>
        /// The constituent phrases of the textual structure.
        /// </summary>
        IEnumerable<Phrase> Phrases { get; }
        /// <summary>
        /// The constituent words of the textual structure.
        /// </summary>
        IEnumerable<Word> Words { get; }
        /// <summary>
        /// The constituent lexicals of the textual structure.
        /// </summary>
        IEnumerable<ILexical> Lexicals { get; }
        /// <summary>
        /// The constituent entities of the textual structure.
        /// </summary>
        IEnumerable<IEntity> Entities { get; }
        /// <summary>
        /// The constituent verbals of the Textual structure.
        /// </summary>
        IEnumerable<IVerbal> Verbals { get; }
        /// <summary>
        /// The text of the Textual structure.
        /// </summary>
        string Text { get; }
    }
}