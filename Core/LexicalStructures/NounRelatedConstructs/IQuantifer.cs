using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Quantifier constructs, generally Nouns or NounPhrases e.g. in the sentence "I have 2 apples.", "2" is a Quantifier. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IQuantifier interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies. </para>
    /// </summary>
    /// <see cref="IQuantifiable"/>
    /// <see cref="IDeterminable"/>
    public interface IQuantifier : IQuantifiable
    {
        /// <summary>
        /// Gets or sets the Quantifiable instance which the Quantifier quantifies.
        /// </summary>
        /// <remarks></remarks>
        IQuantifiable Quantifies {
            get;
            set;
        }
    }
}
