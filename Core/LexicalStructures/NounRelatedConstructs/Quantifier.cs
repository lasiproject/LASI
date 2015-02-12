using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents a quantifier word which specifies the quanitity of an IQuantifiable construct.
    /// </summary>
    public class Quantifier : Word, IQuantifier
    {
        #region Constructors

        /// <summary>
        /// Represents a quantifier which specifies the value, count, or degree, of some
        /// IQuantifiabe such as a GenericSingularNoun
        /// </summary>
        /// <param name="text">The text content of the quantifer.</param>
        public Quantifier(string text) : base(text) { }

        #endregion Constructors

        #region Properties

        /// <summary>Gets or sets the IQuantifiable instance which the IQuantifier quantifies.</summary>
        public virtual IQuantifiable Quantifies { get; set; }

        /// <summary>Gets or sets the IQuantifier instance which quantifies the IQuantifier.</summary>
        public IQuantifier QuantifiedBy { get; set; }

        #endregion Properties
    }
}