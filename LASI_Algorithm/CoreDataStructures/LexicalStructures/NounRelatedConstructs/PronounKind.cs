using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the specific, non-overlapping, kinds of roles which Pronouns can serve.
    /// </summary>
    public enum PronounKind : byte
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Male
        /// </summary>
        Male,
        /// <summary>
        /// MaleReflexive
        /// </summary>
        MaleReflexive,
        /// <summary>
        /// Female
        /// </summary>
        Female,
        /// <summary>
        /// FemaleReflexive
        /// </summary>
        FemaleReflexive,
        /// <summary>
        /// GenderNeurtral
        /// </summary>
        GenderNeurtral,
        /// <summary>
        /// GenderNeurtralReflexive
        /// </summary>
        GenderNeurtralReflexive,
        /// <summary>
        /// Plural
        /// </summary>
        Plural,
        /// <summary>
        /// PluralReflexive
        /// </summary>
        PluralReflexive,
        /// <summary>
        /// GenderAmbiguous
        /// </summary>
        GenderAmbiguous,
        /// <summary>
        /// GenderAmbiguousReflexive
        /// </summary>
        GenderAmbiguousReflexive,
        /// <summary>
        /// FirstPersonSingular
        /// </summary>
        FirstPersonSingular,
        /// <summary>
        /// FirstPersonPlural
        /// </summary>
        FirstPersonPlural,
        /// <summary>
        /// FirstPersonPluralReflexive
        /// </summary>
        FirstPersonPluralReflexive,
        /// <summary>
        /// SecondPerson
        /// </summary>
        SecondPerson,
        /// <summary>
        /// SecondPersonSingularReflexive
        /// </summary>
        SecondPersonSingularReflexive,
        /// <summary>
        /// SecondPersonPluralReflexive
        /// </summary>
        SecondPersonPluralReflexive,
        /// <summary>
        /// ThirdPersonGenderAmbiguousPlural
        /// </summary>
        ThirdPersonGenderAmbiguousPlural,
        /// <summary>
        /// ThirdPersonPluralReflexive
        /// </summary>
        ThirdPersonPluralReflexive,

    }
}
