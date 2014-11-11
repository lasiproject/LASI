using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Represents a verb in its simple form.
    /// </summary>  
    public class SimpleVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the SimpleVerb class.
        /// </summary>
        /// <param name="text">The text content of the SimpleVerb.</param>
        public SimpleVerb(string text) : base(text, VerbForm.Base) { }
    }
}
