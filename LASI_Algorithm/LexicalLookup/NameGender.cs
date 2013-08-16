using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Lookup
{
    /// <summary>
    /// Contains values corresponding to distinct genders.
    /// </summary>
    /// <see cref="PronounKind">Defines the various kinds of Personal Pronouns.</see>
    /// <seealso cref="EntityKind">Defines the various kinds of Entities.</seealso>
    public enum NameGender
    {
        /// <summary>
        /// The default NameGender value. Indicates an undefined gender. 
        /// Note: this is not the same as an unknown gender, which indicates a gender may exist but is not known
        /// </summary>
        UNDEFINED = 0,
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
