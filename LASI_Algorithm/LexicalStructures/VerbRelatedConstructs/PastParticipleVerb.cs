using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Reprepsents entity verb in its past-participle-tense.
    /// </summary>
    public class PastParticipleVerb : Verb
    {
        /// <summary>
        /// Initializes entity new instance of the PastParticipleVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the PastParticipleVerb.</param>
        public PastParticipleVerb(string text)
            : base(text, VerbTense.PastParticiple) {
        }
    }
}
