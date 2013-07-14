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
        private void deterimineEndOfClause() {
            EndOfClause = Sentence.Words.SkipWhile(w => w != Words.Last()).First(w => w is Punctuation) as Punctuation;
        }
        /// <summary>
        /// Gets or sets the Punctuation which terminates the Clause.
        /// </summary>
        public Punctuation EndOfClause {
            get;
            set;
        }
    }
}
