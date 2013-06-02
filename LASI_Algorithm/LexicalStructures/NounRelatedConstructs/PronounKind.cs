using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public enum PronounKind
    {
        Male,
        MaleReflexive,
        Female,
        FemaleReflexive,
        GenderNeurtral,
        GenderNeurtralReflexive,
        Plural,
        PluralReflexive,
        GenderAmbiguous,
        GenderAmbiguousReflexive,
        FirstPersonSingular,
        FirstPersonPlural,
        FirstPersonPluralReflexive,
        SecondPerson,
        SecondPersonSingularReflexive,
        SecondPersonPluralReflexive,
        ThirdPersonGenderAmbiguousPlurals,
        ThirdPersonPluraralReflexive,
        Undefined
    }
}
