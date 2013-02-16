using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a TransitiveVerb in its past tense form.
    /// </summary>
    public class TransPastTenseVerb : TransitiveVerb
    {
        /// <summary>
        /// Initializes a new instance of the TransPastTenseVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the TransPastTenseVerb.</param>
        public TransPastTenseVerb(string text)
            : base(text) {
        }
    }
}
