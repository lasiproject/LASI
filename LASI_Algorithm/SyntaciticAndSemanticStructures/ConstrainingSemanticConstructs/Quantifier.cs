using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a quantifier w which specifies the quanitity of an IQuantifiable construct.
    /// </summary>
    public class Quantifier : Word, IQuantifier
    {
        /// <summary>
        /// Represents a quantifier which specifies the value, count, or degree, of some IQuantifiabe such as a GenericSingularNoun
        /// </summary>
        /// <param name="text">/// <param name="text">the literal text content of the quantifer.</param></param>
        public Quantifier(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public virtual IQuantifiable Quantifies {
            get;
            set;
        }



    }
}
