using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Core
{
    /// <summary>
    /// A phrase which indicates the possible start of a Simple Declarative Clause.
    /// </summary>
    public class SimpleDeclarativeClauseBeginPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the SimpleDeclarativeClauseBeginPhrase class.
        /// </summary>
        /// <param name="composed">The words which comprise the SimpleDeclarativeClauseBeginPhrase.</param>
        public SimpleDeclarativeClauseBeginPhrase(IEnumerable<Word> composed)
            : base(composed) {
        }
        /// <summary>
        /// Initializes a new instance of the SimpleDeclarativeClauseBeginPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the SimpleDeclarativeClauseBeginPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the SimpleDeclarativeClauseBeginPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of SimpleDeclarativeClauseBeginPhrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public SimpleDeclarativeClauseBeginPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

    }

}
