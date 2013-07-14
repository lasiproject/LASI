using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods which augment specific enum Types for quick, centralized access to common usage patterns.
    /// </summary>
    /// <see cref="PronounKind"/>
    public static class EnumTypeExtensions
    {
        #region PronounKind value extensions

        public static bool IsMale(this PronounKind kind) {
            return kind == PronounKind.Male || kind == PronounKind.MaleReflexive;
        }
        public static bool IsFemale(this PronounKind kind) {
            return kind == PronounKind.Female || kind == PronounKind.FemaleReflexive;
        }
        public static bool IsGenderNeutral(this PronounKind kind) {
            return kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;
        }
        public static bool IsGenderAmbiguous(this PronounKind kind) {
            return !(kind.IsFemale() || kind.IsMale() || kind.IsGenderNeutral());
        }
        public static bool IsPlural(this PronounKind kind) {
            return
                kind == PronounKind.Plural ||
                kind == PronounKind.PluralReflexive ||
                kind == PronounKind.FirstPersonPlural ||
                kind == PronounKind.FirstPersonPluralReflexive ||
                kind == PronounKind.SecondPersonPluralReflexive ||
                kind == PronounKind.ThirdPersonGenderAmbiguousPlural ||
                kind == PronounKind.ThirdPersonPluralReflexive;
        }
        public static bool IsReflexive(this PronounKind kind) {
            return
                kind == PronounKind.FemaleReflexive ||
                kind == PronounKind.FirstPersonPluralReflexive ||
                kind == PronounKind.GenderAmbiguousReflexive ||
                kind == PronounKind.GenderNeurtralReflexive ||
                kind == PronounKind.MaleReflexive ||
                kind == PronounKind.PluralReflexive ||
                kind == PronounKind.SecondPersonPluralReflexive ||
                kind == PronounKind.SecondPersonSingularReflexive ||
                kind == PronounKind.ThirdPersonPluralReflexive;
        }
        public static bool IsFirstPerson(this PronounKind kind) {
            return
                kind == PronounKind.FirstPersonPlural ||
                kind == PronounKind.FirstPersonPluralReflexive ||
                kind == PronounKind.FirstPersonSingular;
        }
        public static bool IsSecondPerson(this PronounKind kind) {
            return
                kind == PronounKind.SecondPerson ||
                kind == PronounKind.SecondPersonPluralReflexive ||
                kind == PronounKind.SecondPersonSingularReflexive;
        }
        public static bool IsThirdPerson(this PronounKind kind) {
            return !(kind.IsFirstPerson() || kind.IsSecondPerson());
        }

        #endregion


    }
}
