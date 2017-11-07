using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents a punctuation character at the Word level.
    /// </summary>
    public class Punctuator : Symbol
    {
        /// <summary>
        /// Initializes a new instance of the Punctuator class.
        /// </summary>
        /// <param name="punctuation">The literal character representation of the punctuator.</param>
        public Punctuator(char punctuation) : this(punctuation.ToString()) { }

        /// <summary>
        /// Initializes a new instance of the Punctuator class.
        /// </summary>
        /// <param name="punctuation">The single character string which comprises the Punctuator"</param>
        public Punctuator(string punctuation) : base(punctuation)
        {
            AliasString = SymbolAliasMap.ToAlias(LiteralCharacter);
        }

        /// <summary>
        /// Gets the alias string corresponding to the Punctuator.
        /// </summary>
        public string AliasString { get; }

    }
}
