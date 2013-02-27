using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class RoughListPhrase : Phrase
    {
        public RoughListPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }

     

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }
    }
}
