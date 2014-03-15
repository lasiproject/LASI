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
        /// <param name="symbol">The literal character representation of the punctuator.</param>
        public Punctuator(char symbol)
            : base(symbol) {

            ActualCharacter = symbol;
            AliasString = PunctuationAliasMap.GetAliasStringForChar(ActualCharacter);
        }

        /// <summary>
        /// Initializes a new instances of the Punctuation class.
        /// </summary>
        /// <param name="punctuation">Text which is an alias for a punctuator character. e.g. "LEFT_SQUARE_BRACKET"</param>
        public Punctuator(string punctuation)
            : base(punctuation) {
            AliasString = punctuation;
            ActualCharacter = PunctuationAliasMap.GetCharForAliasString(AliasString);

        }
        /// <summary>
        /// Gets the literal punctuation character.
        /// </summary>
        public char ActualCharacter {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the alias string corresponding to the punctuation symbol.
        /// </summary>
        public string AliasString {
            get;
            private set;
        }

        /// <summary>
        /// Maps between certain punctuation characters and alias text.
        /// </summary>
        private static class PunctuationAliasMap
        {
            private static readonly IDictionary<string, char> aliasMap = new Dictionary<string, char> {
                {  "COMMA", ',' },
                {  "LEFT_SQUARE_BRACKET", '[' },
                { "RIGHT_SQUARE_BRACKET", ']' },
                { "PERIOD_CHARACTER_SYMBOL", '.' },
                { "END_OF_PARAGRAPH", '\n' }  
            };

            public static char GetCharForAliasString(string alias) {
                char result;
                return aliasMap.TryGetValue(alias, out result) ? result : ' ';
            }
            public static string GetAliasStringForChar(char actual) {

                var alias = from KV in aliasMap
                            where KV.Value == actual
                            select KV.Key;
                return alias.Any() ? alias.First() : actual.ToString();

            }

        }
        //public override int GetHashCode() {
        //    return ActualCharacter.GetHashCode();
        //}
        //public override bool Equals(object obj) {
        //    return ReferenceEquals(this, obj) || this == obj as Punctuator;
        //}
        //public static bool operator ==(Punctuator first, Punctuator second) {
        //    return first.ActualCharacter == second.ActualCharacter;
        //}
        //public static bool operator !=(Punctuator first, Punctuator second) {
        //    return !(first == second);
        //}
    }
}
