using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities.Validation;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a symbol at the Word level. </para>  
    /// <para> E.g. in the sentence "They had over $ 100" "$" is symbol acting as a word in that it is a separate token and is modified by 100. </para>
    /// </summary>
    public class Symbol : Word, IEquatable<Symbol>
    {
        /// <summary>
        /// Initializes a new instance of the Symbol class.
        /// </summary>
        /// <param name="literalSymbol">The text of the Symbol.</param>
        public Symbol(string literalSymbol) : base(literalSymbol)
        {
            Validate.NotLessThan(literalSymbol.Length, 1, nameof(literalSymbol), "a symbol must be comprised of at least one character");
            LiteralCharacter = literalSymbol[0];
        }
        /// <summary>
        /// Gets the literal punctuation character of the Punctuator.
        /// </summary>
        public char LiteralCharacter { get; }
        /// <summary>
        /// Determines if the given <see cref="Symbol"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">A <see cref="Symbol"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the given <see cref="Symbol"/> is equal to the current instance; <c>false</c> otherwise.</returns>
        public bool Equals(Symbol other) => this.LiteralCharacter == other?.LiteralCharacter;

        /// <summary>
        /// Determines if the given <see cref="object"/> is equal to the current instance.
        /// </summary>
        /// <param name="obj">An <see cref="object"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the given <see cref="object"/> is equal to the current instance; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj) => this.Equals(obj as Symbol);
        /// <summary>
        /// Gets a hash code for the Symbol.
        /// </summary>
        /// <returns>A hashcode for the Symbol</returns>
        public override int GetHashCode() => LiteralCharacter;
    }
}
