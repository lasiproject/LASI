using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PresentTenseVerbPhrase : VerbPhrase
    {
        public PresentTenseVerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
    }
}
