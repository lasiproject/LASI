using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class SubordinateClauseBeginPhrase : Phrase
    {
        public SubordinateClauseBeginPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        void deterimineEndOfClause() {
            EndOfClause = ParentSentence.Words.SkipWhile(w => w != Words.Last()).First(w => w is Punctuator) as Punctuator;
        }

        public Punctuator EndOfClause {
            get;
            set;
        }
    }
}
