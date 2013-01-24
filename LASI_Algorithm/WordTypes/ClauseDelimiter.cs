using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a punctation character which signifies the beginning or ending a clause.
    /// </summary>
    public class ClauseDelimiter : Punctuator
    {
        /// <summary>
        /// Initializes a new isntance of the ClauseDelimiter class. 
        /// </summary>
        /// <param name="punc">The punctuation literal character delimiting the clause.</param>
        public ClauseDelimiter(char punc)
            : base(punc) {
        }
        public ClauseDelimiter(string punctuationText) : base(punctuationText) {
        }
    }
}
