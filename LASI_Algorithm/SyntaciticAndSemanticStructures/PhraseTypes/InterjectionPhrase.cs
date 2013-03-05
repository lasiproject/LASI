using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class InterjectionPhrase : Phrase
    {

        /// <summary>
        /// Initializes a new instance of the InterjectionPhrase class.
        /// </summary>
        /// <param name="composed">The words which compose to form the InterjectionPhrase.</param>
        public InterjectionPhrase(List<Word> composed)
            : base(composed) {
        }




        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
