using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
     /// <summary>
    /// A phrase which indicates the possible start of an Inverted Clause.
    /// </summary>
    public class InvertedClauseBeginPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the InvertedClauseBeginPhrase class.
        /// </summary>
        /// <param name="composed">The words which comprise the InvertedClauseBeginPhrase.</param>
        public InvertedClauseBeginPhrase(IEnumerable<Word> composed)
            : base(composed) {
        }
    }
}
