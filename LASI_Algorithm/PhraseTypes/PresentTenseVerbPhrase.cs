using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public class PresentTenseVerbPhrase : VerbPhrase
    {
        public PresentTenseVerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
