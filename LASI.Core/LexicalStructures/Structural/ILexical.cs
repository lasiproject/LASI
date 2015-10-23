using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Defines the broad role requirements for the weightable, countable textual elements, of a written work. Along with the other
    /// interfaces in the Syntactic Interfaces Library, the ILexical interface provides for generalization and abstraction over many
    /// otherwise disparate element types and type hierarchies.
    /// </summary>
    public interface ILexical : IWeightable
    {
        /// <summary>
        /// Gets the textual content of the Lexical element.
        /// </summary>
        string Text { get; }
    }
}