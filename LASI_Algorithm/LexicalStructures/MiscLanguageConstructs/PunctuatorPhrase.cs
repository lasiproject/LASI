using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.SyntaciticAndSemanticStructures
{
    public class PunctuatorPhrase : Phrase
    {
        public PunctuatorPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {

            SignificantPunctution = composedWords.Last(p => p is Punctuation) as Punctuation;
        }

        public Punctuation SignificantPunctution {
            get;
            protected set;
        }
    }
}
