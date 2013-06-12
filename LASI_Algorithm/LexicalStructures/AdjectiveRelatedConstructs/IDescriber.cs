using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Descriptive constructs which can Describe an instance of any class which implements the IDescribable interface.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IDescriptor interface provides for generalization and abstraction over wd and Phrase types.
    /// </summary>
    /// <see cref="IDescribable"/>
    public interface IDescriptor : ILexical
    {
        /// <summary>
        /// Gets or sets the Entity which the IDescriptor instance describes.
        /// </summary>
        IEntity Described {
            get;
            set;
        }
    }
}
