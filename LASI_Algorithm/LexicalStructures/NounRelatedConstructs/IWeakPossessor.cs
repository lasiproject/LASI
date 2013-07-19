using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    interface IWeakPossessor : IPossesser
    {
        /// <summary>
        /// Gets or sets the possessing IEntity construct which possesses through the IWeakPossessor construct.
        /// </summary>
        IEntity PossessesFor {
            get;
            set;
        }
    }
}
