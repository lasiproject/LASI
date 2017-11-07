using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// A Punctuator which acts as a sentence terminator.
    /// </summary>
    /// <seealso cref="Sentence"/>
    public sealed class SentenceEnding : Punctuator, IEquatable<SentenceEnding>
    {
        /// <summary>
        /// Initializes a new instance of the SentenceEnding class.
        /// </summary>
        /// <param name="ending">A character which denotes the end of a sentence (valid values are '?', '!', and '.'</param>
        /// <exception cref="ArgumentException">Thrown when a character not within the specified set of valid values is passed to the constructor.</exception>
        private SentenceEnding(char ending)
            : base(ending)
        {
            if (ending != '.' && ending != '!' && ending != '?')
            {
                throw new ArgumentException($"A sentence cannot end with the character {ending}");
            }
        }
        /// <summary>
        /// Gets a hash code for the SentenceEnding.
        /// </summary>
        /// <returns>A hashcode for the SetenceEnding</returns>
        public override int GetHashCode() => LiteralCharacter;

        /// <summary>
        /// Determines if the given <see cref="SentenceEnding"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">A <see cref="SentenceEnding"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the given <see cref="SentenceEnding"/> is equal to the current instance; <c>false</c> otherwise.</returns>
        public bool Equals(SentenceEnding other) => LiteralCharacter == other?.LiteralCharacter;

        /// <summary>
        /// Determines if the given <see cref="object"/> is equal to the current instance.
        /// </summary>
        /// <param name="obj">An <see cref="object"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the given <see cref="object"/> is equal to the current instance; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj) => this.Equals(obj as SentenceEnding);

        /// <summary>
        /// Determines if two Sentence Endings are equal.
        /// </summary>
        /// <param name="left">The first SentenceEnding to compare.</param>
        /// <param name="right">The second SentenceEnding to compare.</param>
        /// <returns> <c>true</c> if the Sentence Endings are equal; false otherwise.</returns>
        public static bool operator ==(SentenceEnding left, SentenceEnding right) => left.Equals(right);

        /// <summary>
        /// Determines if two Sentence Endings are not equal.
        /// </summary>
        /// <param name="left">The first SentenceEnding to compare.</param>
        /// <param name="right">The second SentenceEnding to compare.</param>
        /// <returns> <c>true</c> if the Sentence Endings are not equal; false otherwise.</returns>
        public static bool operator !=(SentenceEnding left, SentenceEnding right) => !(left == right);
        /// <summary>
        /// A factory property which creates and yields a new ExclamationPoint when referenced.
        /// </summary>
        public static SentenceEnding ExclamationPoint => new SentenceEnding('!');
        /// <summary>
        /// A factory property which creates and yields a new QuestionMark when referenced.
        /// </summary>
        public static SentenceEnding QuestionMark => new SentenceEnding('?');
        /// <summary>
        /// A factory property which creates and yields a new Period when referenced.
        /// </summary>
        public static SentenceEnding Period => new SentenceEnding('.');
    }
}
