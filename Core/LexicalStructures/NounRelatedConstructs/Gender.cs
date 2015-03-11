using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Contains values corresponding to distinct genders.
    /// </summary> 
    /// <seealso cref="PronounKind">Defines the various kinds of Personal Pronouns.</see>
    /// <seealso cref="EntityKind">Defines the various kinds of Entities.</seealso>
    public enum Gender : byte
    {
        /// <summary>
        /// The default Gender value. Indicates an Unknown gender. 
        /// </summary>
        Undetermined = 0,
        /// <summary>
        /// Female
        /// </summary>
        Female,
        /// <summary>
        /// Male
        /// </summary>
        Male,
        /// <summary>
        /// Neutral
        /// </summary>
        Neutral
    }
}
