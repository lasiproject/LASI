using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an interjection entity such as "by jove".
    /// </summary>
    public class InterjectionPhrase : Phrase
    {

        /// <summary>
        /// Initializes entity new instance of the InterjectionPhrase class.
        /// </summary>
        /// <param name="composed">The words which compose to form the InterjectionPhrase.</param>
        public InterjectionPhrase(List<Word> composed)
            : base(composed) {
        }


    }
}
