using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// A specialization of Punctuator which represents character which demarkate the end of a sentence.
    /// </summary>
    public class SentenceDelimiter : Punctuator
    {
        /// <summary>
        /// Initializes a new instance of the SentenceDelimiter class.
        /// </summary>
        /// <param name="eos">A character which denotes the end of a sentence (valid values are '?', '!', and '.'</param>
        /// <exception cref="ArgumentException">Thrown when a character not within the specified set of valid values is passed to the constructor.</exception>
        public SentenceDelimiter(char eos)
            : base(eos) {
            if (eos != '.' && eos != '!' && eos != '?')
                throw new ArgumentException(String.Format("A sentence cannot end with the character {0}", eos));
        }
    }
}
