using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities.Validation;
using System.Text;
using LASI.Utilities;

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
        public Symbol(string literalSymbol) : base(SymbolAliasMap.FromAlias(literalSymbol))
        {
            LiteralCharacter = Text.Length == 1 ? Text[0] : '\0';
        }
        /// <summary>
        /// Gets the literal punctuation character of the <see cref="Punctuator"/>.
        /// </summary>
        public char LiteralCharacter { get; }
        /// <summary>
        /// Determines if the given <see cref="Symbol"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">A <see cref="Symbol"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the given <see cref="Symbol"/> is equal to the current instance; <c>false</c> otherwise.</returns>
        public bool Equals(Symbol other) => this.Text == other?.Text;

        /// <summary>
        /// Determines if the given <see cref="object"/> is equal to the current instance.
        /// </summary>
        /// <param name="obj">An <see cref="object"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the given <see cref="object"/> is equal to the current instance; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj) => Equals(obj as Symbol);
        /// <summary>
        /// Gets a hash code for the Symbol.
        /// </summary>
        /// <returns>A hashcode for the Symbol</returns>
        public override int GetHashCode() => LiteralCharacter;

        public static bool operator ==(Symbol left, Symbol right) => left?.Equals(right) ?? right == null;
        public static bool operator !=(Symbol left, Symbol right) => !(left == right);

        /// <summary>
        /// Maps between certain punctuation characters and alias text.
        /// </summary>
        protected static class SymbolAliasMap
        {
            private static readonly IDictionary<string, string> aliasMap = new Dictionary<string, string>
            {
                ["COMMA"] = ",",
                ["LEFT_SQUARE_BRACKET"] = "[",
                ["RIGHT_SQUARE_BRACKET"] = "]",
                ["PERIOD_CHARACTER_SYMBOL"] = ".",
                ["END_OF_PARAGRAPH"] = "'\n"
            };

            public static string FromAlias(string alias) => aliasMap.GetValueOrDefault(alias, alias);

            public static string ToAlias(string actual) => aliasMap.FirstOrDefault(kvp => kvp.Value == actual).Key ?? actual;
            public static string ToAlias(char actual) => aliasMap.FirstOrDefault(kvp => kvp.Value.Length == 0 && kvp.Value[0] == actual).Key ?? actual.ToString();
        }
    }
}
