using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Verb in its past participle tense.
    /// </summary>
    public class PastPrtcplVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the PastPrtcplVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the Verb.</param>
        public PastPrtcplVerb(string text)
            : base(text, VerbTense.PastParticiple) {
        }
    }
}
