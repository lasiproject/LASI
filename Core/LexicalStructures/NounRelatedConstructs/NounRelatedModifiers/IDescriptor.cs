using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Descriptive constructs which descriptively modify Entity constructs.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IDescriptor interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    /// <see cref="IDescribable"/>
    public interface IDescriptor : ILexical
    {
        /// <summary>
        /// Gets or sets the Entity which the Descriptor instance describes.
        /// </summary>
        IEntity Describes {
            get;
            set;
        }
    }
}
