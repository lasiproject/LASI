using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Reprepsents entity verb in its simple past-tense form.
    /// </summary>
    public class PastTenseVerb : Verb
    {
        /// <summary>
        /// Initializes entity new instance of the PastTenseVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the PastTenseVerb.</param>
        public PastTenseVerb(string text)
            : base(text, VerbTense.Past) {
        }
    }
}
