using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public class TransPastTenseVPhrase : TransitiveVerbPhrase
    {
        public TransPastTenseVPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
