using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines, very broadly, the basic kinds of Entities which are likely to be expressed.
    /// </summary>
    public enum EntityKind : byte
    {
        /// <summary>
        /// UNDEFINED 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Person
        /// </summary>
        Person,
        /// <summary>
        /// A group of Persons
        /// </summary>
        PersonMultiple,
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
        /// ThingUnknown
        /// </summary>
        ThingUnknown,
        /// <summary>
        /// Thing Multiple
        /// </summary>
        ThingUnknownMultiple,
        /// <summary>
        /// Activitiy: Generally corresonds to a gerund or a gerund-dominated phrase.
        /// E.g. in the sentence "He really enjoys attacking.", "attacking" refers to a behavior, the act of attacking, and is thus an activity.
        /// </summary>
        Activity,
    }
}
