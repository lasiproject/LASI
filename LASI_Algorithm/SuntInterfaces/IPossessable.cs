using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the behavioral contract for constructs, generally IEntity Implementors, which are "possessable" by other Entities.
    /// </summary>
    public interface IPossessable
    {
        /// <summary>
        /// Gets or sets the Entity which has been inferred as the "owner" of the IPossessable.
        /// </summary>
        IEntity Possesser {
            get;
            set;
        }
    }
}
