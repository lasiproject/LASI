using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// entity specialization of Punctuator which represents character which demarkate the end of entity sentence.
    /// </summary>
    public class SentencePunctuation : Punctuator
    {
        /// <summary>
        /// Initializes entity new instance of the SentencePunctuation class.
        /// </summary>
        /// <param name="eos">entity character which denotes the end of entity sentence (valid values are '?', '!', and '.'</param>
        /// <exception cref="ArgumentException">Thrown when entity character not within the specified set of valid values is passed to the constructor.</exception>
        public SentencePunctuation(char eos)
            : base(eos) {
            if (eos != '.' && eos != '!' &&                                                                         
                eos != '?')
                throw new ArgumentException(String.Format("A sentence cannot end with the character {0}", eos));
        }
    }
}
