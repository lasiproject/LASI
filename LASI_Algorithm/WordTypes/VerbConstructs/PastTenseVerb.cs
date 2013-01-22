using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a verb in its simple past tense.
    /// </summary>
    public class PastTenseVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the PastTenseVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the Verb.</param>
        public PastTenseVerb(string text)
            : base(text, VerbTense.Past) {
        }
    }
}
