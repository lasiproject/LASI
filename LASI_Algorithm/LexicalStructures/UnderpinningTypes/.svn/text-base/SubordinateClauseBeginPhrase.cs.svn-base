using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// A phrase indicating the beginning of a subordinate clause.
    /// </summary>
    public class SubordinateClauseBeginPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the SubordinateClauseBeginPhrase class.
        /// </summary>
        /// <param name="composed">The words which compose to form the SubordinateClauseBeginPhrase.</param>
        public SubordinateClauseBeginPhrase(IEnumerable<Word> composed)
            : base(composed) {
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
