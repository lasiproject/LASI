using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

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
        /// <summary>
        /// Initializes a new instance of the InvertedClauseBeginPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the InvertedClauseBeginPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the InvertedClauseBeginPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of InvertedClauseBeginPhrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public InvertedClauseBeginPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
    }
}
