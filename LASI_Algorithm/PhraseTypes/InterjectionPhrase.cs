using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class InterjectionPhrase : Phrase
    {


        public InterjectionPhrase(List<Word> composed)
            : base(composed) {

        }

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }



        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
