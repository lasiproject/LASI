using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Represents a verb in the Singular Present Tense.
    /// </summary>
    public class SingularPresentVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the SingularPresentTenseVerb class.
        /// </summary>
        /// <param name="text">The text content of the SingularPresentTenseVerb.</param>
        public SingularPresentVerb(string text) : base(text) { }
    }
}
