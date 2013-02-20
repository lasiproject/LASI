using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the behavior of a Descriptive construct which can Describe an instance of any class which implements the IDescribable interface
    /// </summary>
    /// <see cref="IDescribable"/>
    public interface IDescriber : ILexical
    {
        /// <summary>
        /// Gets or sets the Entity which the IDescriber instance describes.
        /// </summary>
        IEntity Describes {
            get;
            set;
        }
    }
}
