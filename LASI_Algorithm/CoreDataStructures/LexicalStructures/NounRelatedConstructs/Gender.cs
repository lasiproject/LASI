using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core.ComparativeHeuristics
{
    /// <summary>
    /// Contains values corresponding to distinct genders.
    /// </summary>C:\Users\Dustin\Documents\GitHub\LASI\LASI_Algorithm\LexicalLookup\Gender.cs
    /// <see cref="PronounKind">Defines the various kinds of Personal Pronouns.</see>
    /// <seealso cref="EntityKind">Defines the various kinds of Entities.</seealso>
    public enum Gender : byte
    {
        /// <summary>
        /// The default NameGender value. Indicates an undefined gender. 
        /// Note: this is not the same as an unknown gender, which indicates a gender may exist but is not known
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
