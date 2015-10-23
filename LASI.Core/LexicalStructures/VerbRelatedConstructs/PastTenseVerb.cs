using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Represents a verb in its simple past-tense form.
    /// </summary>
    public class PastTenseVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the PastTenseVerb class.
        /// </summary>
        /// <param name="text">The text content of the PastTenseVerb.</param>
        public PastTenseVerb(string text) : base(text) { }
    }
}
