using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class TransPresentVPhrase : TransitiveVerbPhrase
    {
        public TransPresentVPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
