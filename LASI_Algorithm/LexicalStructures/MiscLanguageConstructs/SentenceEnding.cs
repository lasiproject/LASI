using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// A specialization of Punctuation which represents character which demarkate the end of a sentence.
    /// </summary>
    public class SentenceEnding : Punctuation
    {
        /// <summary>
        /// Initializes a new instance of the SentenceDelimiter class.
        /// </summary>
        /// <param name="endOfSentenceMarker">A character which denotes the end of a sentence (valid values are '?', '!', and '.'</param>
        /// <exception cref="ArgumentException">Thrown when a character not within the specified set of valid values is passed to the constructor.</exception>
        public SentenceEnding(char endOfSentenceMarker)
            : base(endOfSentenceMarker) {
            if (endOfSentenceMarker != '.' && endOfSentenceMarker != '!' &&
                endOfSentenceMarker != '?')
                throw new ArgumentException(String.Format("A sentence cannot end with the character {0}", endOfSentenceMarker));
        }
    }
}
