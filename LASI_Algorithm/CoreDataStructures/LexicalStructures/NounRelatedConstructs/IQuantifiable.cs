using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Quantififiable constructs, generally Nouns or NounPhrases e.g. in the sentence "I have 2 apples.", "apples" is Quantifiable.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the Quantifiable interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies.
    /// </summary>
    public interface IQuantifiable : ILexical
    {
        /// <summary>
        /// Gets or sets the Quantifier instance which quantifies the IQuantifiable.
        /// </summary>
        IQuantifier QuantifiedBy {
            get;
            set;
        }
    }
}
