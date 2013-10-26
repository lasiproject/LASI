using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Descriptive constructs which can Describe an instance of any class which implements the IDescribable interface.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IDescriptor interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    /// <see cref="IDescribable"/>
    public interface IDescriptor : ILexical
    {
        /// <summary>
        /// Gets or sets the Entity which the IDescriptor instance describes.
        /// </summary>
        IDescribable Describes {
            get;
            set;
        }
    }
}
