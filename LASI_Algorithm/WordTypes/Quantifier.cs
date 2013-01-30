using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents a quantifier word which specifies the quanitity of an IQuantifiable construct.
    /// </summary>
    public class Quantifier : Word, IQuantifier
    {
        /// <summary>
        /// 
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
