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
        /// Initializes a new instance of the Punctuator class.
        /// </summary>
        /// <param name="punctuation">The single character string which comprises the Punctuator"</param>
        public Punctuator(string punctuation)
            : base(punctuation) {
            //if (punctuation.Length != 1) {
            //    throw new ArgumentException(
            //        string.Format("The supplied string must only contain single character\nprovided value: {0}", punctuation),
            //        "punctuation"
            //    );
            //}
            ActualCharacter = punctuation[0];
            AliasString = PunctuationAliasMap.GetAliasStringForChar(ActualCharacter);
        }
        /// <summary>
        /// Gets the literal punctuation character of the Punctuator.
        /// </summary>
        public char ActualCharacter {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the alias string corresponding to the Punctuator.
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
                return aliasMap[alias];
            }
            public static string GetAliasStringForChar(char actual) {

                var alias = from KV in aliasMap
                            where KV.Value == actual
                            select KV.Key;
                return alias.Any() ? alias.First() : actual.ToString();

            }

        }

    }
}
