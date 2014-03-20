using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Quantifiable constructs, generally Nouns or NounPhrases 
    /// e.g. in the sentence "I have 2 apples.", "apples" is Quantifiable. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library,
    /// the Quantifiable interface provides for generalization and abstraction over many otherwise disparate element types and Type hierarchies. </para>
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
