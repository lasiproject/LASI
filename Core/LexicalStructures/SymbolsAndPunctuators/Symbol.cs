using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a symbol at the Word level. </para>  
    /// <para> E.g. in the sentence "They had over $ 100" "$" is symbol acting as a word in that it is a separate token and is modified by 100. </para>
    /// </summary>
    public class Symbol : Word
    {
        /// <summary>
        /// Initializes a new instance of the Symbol class.
        /// </summary>
        /// <param name="character">The single character of the Symbol.</param>
        public Symbol(char character) : base(character.ToString()) { LiteralCharacter = character; }
        /// <summary>
        /// Initializes a new instance of the Symbol class.
        /// </summary>
        /// <param name="text">The text of the Symbol.</param>
        public Symbol(string text) : this(text.First()) { }
        /// <summary>
        /// Gets the literal punctuation character of the Punctuator.
        /// </summary>
        public char LiteralCharacter { get; }
    }
}
