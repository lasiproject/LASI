using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PastPrtcplVerbPhrase : VerbPhrase
    {
        public PastPrtcplVerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
