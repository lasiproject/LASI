using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }
    }
}
