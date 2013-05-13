using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class Punctuator : Symbol
    {
        /// <summary>
        /// Initializes entity new instance of the Punctuator class.
        /// </summary>
        /// <param name="puncChar">The punctuation character symbol.</param>
        public Punctuator(char puncChar)
            : base(puncChar.ToString()) {
            ActualCharacter = puncChar;

            AliasString = PUNCTUATION_ALIAS_MAP[ActualCharacter];

        }

        /// <summary>
        /// Initializes entity new instances of the Punctuator class.
        /// </summary>
        /// <param name="puncString">Text which is an alias for entity punctuator character. e.g. "LEFT_SQUARE_BRACKET"</param>
        public Punctuator(string puncString)
            : base(puncString) {
            AliasString = puncString;
            //try {
                ActualCharacter = PUNCTUATION_ALIAS_MAP[AliasString];
            //}
            //catch (KeyNotFoundException) {
            //    System.Diagnostics.Debug.WriteLine("Punctuation Character  {0} has no defined text alias", ActualCharacter);
            //}

        }
        public char ActualCharacter {
            get;
            protected set;
        }
        public string AliasString {
            get;
            protected set;
        }
        public override string Text {
            get {
                return ActualCharacter.ToString();
            }
            protected set {
                base.Text = value;
            }
        }
        private static PunctuationAliasMap PUNCTUATION_ALIAS_MAP = new PunctuationAliasMap();



    }



    internal class PunctuationAliasMap
    {
        private Dictionary<string, char> aliasMap = new Dictionary<string, char> {
        {  "COMMA", ',' },
            {  "LEFT_SQUARE_BRACKET", '[' },
            { "RIGHT_SQUARE_BRACKET", ']' },
            { "PERIOD_CHARACTER_SYMBOL", '.' },
            { "END_OF_PARAGRAPH", '\n' }
        };
        public virtual char this[string alias] {
            get {
                char result;
                if (aliasMap.TryGetValue(alias, out result))
                    return result;
                else
                    return ' ';
            }
        }
        public virtual string this[char actual] {
            get {
                var alias = from KV in aliasMap
                            where KV.Value == actual
                            select KV.Key;
                return alias.Count() > 0 ? alias.First() : actual.ToString();
            }
        }

    }
}
