using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Word which does not correspond to a known syntactic category.
    /// This may be the result of a Tagging error or a Tag-Parsing error.
    /// </summary>
    public class UnknownWord : Word
    {
        /// <summary>
        /// Initializes a new instance of the UndeterminedWord  class.
        /// </summary>
        /// <param name="text">The text content of the word.</param>
        public UnknownWord(string text) : base(text) { }
    }
}
