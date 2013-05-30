using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role requirements for Descriptive constructs which can Describe an instance of any class which implements the IDescribable interface.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IDescriber interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    /// <see cref="IDescribable"/>
    public interface IDescriber : ILexical
    {
        /// <summary>
        /// Gets or sets the Entity which the IDescriber instance describes.
        /// </summary>
        IEntity Described {
            get;
            set;
        }
    }
}
