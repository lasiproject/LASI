using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public class TransPresentVPhrase : TransitiveVerbPhrase
    {
        public TransPresentVPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
