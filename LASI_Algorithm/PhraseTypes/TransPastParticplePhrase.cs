using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class TransPastPrtcplPhrase : TransitiveVerbPhrase
    {
        public TransPastPrtcplPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
