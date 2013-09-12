using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an interjection phrase such as "by jove".
    /// </summary>
    public class InterjectionPhrase : Phrase
    {

        /// <summary>
        /// Initializes a new instance of the InterjectionPhrase class.
        /// </summary>
        /// <param name="composed">The words which compose to form the InterjectionPhrase.</param>
        public InterjectionPhrase(IEnumerable<Word> composed)
            : base(composed) {
        }


    }
}
