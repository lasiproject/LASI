using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// A specialization of Punctuation which represents character which demarcate the end of a sentence.
    /// </summary>
    public sealed class SentenceEnding : Punctuator
    {
        /// <summary>
        /// Initializes a new instance of the SentenceEnding class.
        /// </summary>
        /// <param name="sentenceEnding">A character which denotes the end of a sentence (valid values are '?', '!', and '.'</param>
        /// <exception cref="ArgumentException">Thrown when a character not within the specified set of valid values is passed to the constructor.</exception>
        public SentenceEnding(char sentenceEnding)
            : base(sentenceEnding) {
            if (sentenceEnding != '.' && sentenceEnding != '!' &&
                sentenceEnding != '?')
                throw new ArgumentException(string.Format("A sentence cannot end with the character {0}", sentenceEnding));
        }
        public override int GetHashCode() {
            return LiteralCharacter;
        }
        public override bool Equals(object obj) {
            return obj is SentenceEnding && (SentenceEnding)(obj) == this;
        }
        public static bool operator ==(SentenceEnding left, SentenceEnding right) {
            return left.LiteralCharacter == right.LiteralCharacter;
        }
        public static bool operator !=(SentenceEnding left, SentenceEnding right) {
            return !(left == right);
        }
    }
}
