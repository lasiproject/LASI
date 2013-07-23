using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines, very broadly, the basic kinds of Entities which are likely to be expressed.
    /// </summary>
    public enum EntityKind : byte
    {
        /// <summary>
        /// UNDEFINED 
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// Person
        /// </summary>
        Person,
        /// <summary>
        /// Location
        /// </summary>
        Location,
        /// <summary>
        /// Organization
        /// </summary>
        Organization,
        /// <summary>
        /// ProperUnknown
        /// </summary>
        ProperUnknown,
        /// <summary>
        /// Thing
        /// </summary>
        Thing,
        /// <summary>
        /// ThingUnknown
        /// </summary>
        ThingUnknown,
        /// <summary>
        /// Activitiy
        /// </summary>
        Activitiy,

    }
}
