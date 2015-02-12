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
    public class BaseVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the BaseVerb class.
        /// </summary>
        /// <param name="text">The text content of the BaseVerb.</param>
        public BaseVerb(string text) : base(text) { }
    }
}
