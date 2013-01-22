using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class UndeterminedPhrase : Phrase
    {
        public UndeterminedPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
