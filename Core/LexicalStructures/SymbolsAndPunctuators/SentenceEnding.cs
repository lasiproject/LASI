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
        /// <param name="ending">A character which denotes the end of a sentence (valid values are '?', '!', and '.'</param>
        /// <exception cref="ArgumentException">Thrown when a character not within the specified set of valid values is passed to the constructor.</exception>
        private SentenceEnding(char ending)
            : base(ending) {
            if (ending != '.' && ending != '!' && ending != '?') {
                throw new ArgumentException(string.Format("A sentence cannot end with the character {0}", ending));
            }
        }
        /// <summary>
        /// Gets a hash code for the SentenceEnding.
        /// </summary>
        /// <returns>A hashcode for the SetenceEnding</returns>
        public override int GetHashCode() {
            return LiteralCharacter;
        }
        /// <summary>
        /// Determines if the given SentenceEnding is equal to the current instance.
        /// </summary>
        /// <param name="obj">A SentenceEnding to compare with the current instance.</param>
        /// <returns>True if the given SentenceEnding is equal to the current instance; false otherwise.</returns>
        public override bool Equals(object obj) {
            return obj is SentenceEnding && (SentenceEnding)(obj) == this;
        }
        /// <summary>
        /// Determines if two Sentence Endings are equal.
        /// </summary>
        /// <param name="left">The first SentenceEnding to compare.</param>
        /// <param name="right">The second SentenceEnding to compare.</param>
        /// <returns>True if the Sentence Endings are equal; false otherwise.</returns>
        public static bool operator ==(SentenceEnding left, SentenceEnding right) {
            return left.LiteralCharacter == right.LiteralCharacter;
        }
        /// <summary>
        /// Determines if two Sentence Endings are not equal.
        /// </summary>
        /// <param name="left">The first SentenceEnding to compare.</param>
        /// <param name="right">The second SentenceEnding to compare.</param>
        /// <returns>True if the Sentence Endings are not equal; false otherwise.</returns>
        public static bool operator !=(SentenceEnding left, SentenceEnding right) {
            return !(left == right);
        }
        public static SentenceEnding ExclamationPoint {
            get { return new SentenceEnding('!'); }
        }
        public static SentenceEnding QuestionMark {
            get { return new SentenceEnding('?'); }
        }
        public static SentenceEnding Period {
            get { return new SentenceEnding('.'); }
        }
    }
}
