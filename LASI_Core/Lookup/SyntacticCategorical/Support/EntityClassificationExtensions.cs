using LASI.Core.ComparativeHeuristics;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods which augment specific enum Types for quick, centralized access to common usage patterns.
    /// </summary>
    /// <see cref="PronounKind"/>
    public static class EntityClassificationExtensions
    {
        #region PronounKind Value Extensions

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitely male.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are thought of as explicitely male, false otherwise.</returns>
        public static bool IsMale(this PronounKind kind) {
            return kind == PronounKind.Male || kind == PronounKind.MaleReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitely female.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are thought of as explicitely female, false otherwise.</returns>
        public static bool IsFemale(this PronounKind kind) {
            return kind == PronounKind.Female || kind == PronounKind.FemaleReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitely gender neutral.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are thought of as explicitely gender neutral, false otherwise.</returns>
        public static bool IsNeutral(this PronounKind kind) {
            return kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitely gender ambiguous.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are thought of as explicitely gender ambiguous, false otherwise.</returns>
        public static bool IsGenderAmbiguous(this PronounKind kind) {
            return !(kind.IsFemale() || kind.IsMale() || kind.IsNeutral());
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitely plural.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are thought of as explicitely plural, false otherwise.</returns>
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
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are reflexive.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are reflexive false, otherwise.</returns>
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
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are first person.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are first person, false otherwise.</returns>
        public static bool IsFirstPerson(this PronounKind kind) {
            return
                kind == PronounKind.FirstPersonPlural ||
                kind == PronounKind.FirstPersonPluralReflexive ||
                kind == PronounKind.FirstPersonSingular;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are second person.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are second person, false otherwise.</returns>
        public static bool IsSecondPerson(this PronounKind kind) {
            return
                kind == PronounKind.SecondPerson ||
                kind == PronounKind.SecondPersonPluralReflexive ||
                kind == PronounKind.SecondPersonSingularReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are third person.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>True if the PronounKind is among the semantic categories which are third person, false otherwise.</returns>
        public static bool IsThirdPerson(this PronounKind kind) {
            return !(kind.IsFirstPerson() || kind.IsSecondPerson());
        }
        #endregion

        #region Pronoun extensions


        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitely male.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are thought of as explicitely male, false otherwise.</returns>
        public static bool IsMale(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return kind == PronounKind.Male || kind == PronounKind.MaleReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitely female.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are thought of as explicitely female, false otherwise.</returns>
        public static bool IsFemale(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return kind == PronounKind.Female || kind == PronounKind.FemaleReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitely gender neutral.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are thought of as explicitely gender neutral, false otherwise.</returns>
        public static bool IsNeutral(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            //var intSet = new[] { 1, 2, 3, 4, 5, 6, 7 }.ToSet((i, j) => i % 2 == j % 2, i => i.GetHashCode());
            return kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitely gender ambiguous.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are thought of as explicitely gender ambiguous, false otherwise.</returns>
        public static bool IsGenderAmbiguous(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return !(kind.IsFemale() || kind.IsMale() || kind.IsNeutral());
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitely plural.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are thought of as explicitely plural, false otherwise.</returns>
        public static bool IsPlural(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return
                kind == PronounKind.Plural ||
                kind == PronounKind.PluralReflexive ||
                kind == PronounKind.FirstPersonPlural ||
                kind == PronounKind.FirstPersonPluralReflexive ||
                kind == PronounKind.SecondPersonPluralReflexive ||
                kind == PronounKind.ThirdPersonGenderAmbiguousPlural ||
                kind == PronounKind.ThirdPersonPluralReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are reflexive.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are reflexive false, otherwise.</returns>
        public static bool IsReflexive(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
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
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are first person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are first person, false otherwise.</returns>
        public static bool IsFirstPerson(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return
                kind == PronounKind.FirstPersonPlural ||
                kind == PronounKind.FirstPersonPluralReflexive ||
                kind == PronounKind.FirstPersonSingular;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are second person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are second person, false otherwise.</returns>
        public static bool IsSecondPerson(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return
                kind == PronounKind.SecondPerson ||
                kind == PronounKind.SecondPersonPluralReflexive ||
                kind == PronounKind.SecondPersonSingularReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are third person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>True if the Pronoun is among the semantic categories which are third person, false otherwise.</returns>
        public static bool IsThirdPerson(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return !(kind.IsFirstPerson() || kind.IsSecondPerson());
        }

        #endregion


        #region EntityKind <-> PronounKind Comparisons
        /// <summary>
        /// Determines if two Pronoun instances have the same gender.
        /// </summary>
        /// <param name="first">The first Pronoun.</param>
        /// <param name="second">The second Pronoun.</param>
        /// <returns>True if both Pronouns have the same gender, false otherwise.</returns>
        public static bool IsGenderEquivalentTo(this Pronoun first, Pronoun second) {
            var kind1 = first.PronounKind;
            var kind2 = second.PronounKind;
            return
                kind1.IsFemale() && kind2.IsFemale() ||
                kind1.IsMale() && kind2.IsMale() ||
                kind1.IsNeutral() && kind2.IsNeutral() ||
                kind1.IsGenderAmbiguous() && kind2.IsGenderAmbiguous();
        }

        /// <summary>
        /// Determines if a Pronoun and a ProperNoun instance have the same gender.
        /// </summary>
        /// <param name="first">The Pronoun.</param>
        /// <param name="second">The ProperNoun.</param>
        /// <returns>True if Pronoun and the ProperNoun have the same gender, false otherwise.</returns>
        public static bool IsGenderEquivalentTo(this  Pronoun first, ProperNoun second) {
            return first.Gender == second.GetGender();
        }
        /// <summary>
        /// Determines if a Pronoun and a ProperNoun instance have the same gender.
        /// </summary>
        /// <param name="first">The Pronoun.</param>
        /// <param name="second">The ProperNoun.</param>
        /// <returns>True if Pronoun and the ProperNoun have the same gender, false otherwise.</returns>
        public static bool IsGenderEquivalentTo(this  ProperNoun first, Pronoun second) {
            return first.GetGender() == second.Gender;
            //var pronounKind = second.PronounKind;
            //var entityKind = first.EntityKind;
            //return pronounKind.IsFemale() ?
            //    entityKind == EntityKind.PersonFemale :
            //    pronounKind.IsMale() ?
            //    entityKind == EntityKind.PersonMale :
            //    false;
        }
        public static bool IsGenderEquivalentTo(this  IEntity first, IEntity second) { return first.GetGender() == second.GetGender(); }

        #endregion
        #region Gender Helpers
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is male.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is male, false otherwise.</returns>
        public static bool IsMale(this Gender gender) { return gender == Gender.Male; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is female.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is female, false otherwise.</returns>
        public static bool IsFemale(this Gender gender) { return gender == Gender.Female; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is male or female.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is either male or female, false otherwise.</returns>
        public static bool IsMaleOrFemale(this Gender gender) { return gender == Gender.Male || gender == Gender.Female; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is neutral, false otherwise.</returns>
        public static bool IsNeutral(this Gender gender) { return gender == Gender.Neutral; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is either male or neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is either male neutral, false otherwise.</returns>
        public static bool IsMaleOrNeutral(this Gender gender) { return gender == Gender.Neutral || gender == Gender.Male; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is either female or neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is either female or neutral, false otherwise.</returns>
        public static bool IsFemaleOrNeutral(this Gender gender) { return gender == Gender.Neutral || gender == Gender.Female; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is either male, female, or neutral or not undefined.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is is either male, female, or neutral or not undefined., false otherwise.</returns>
        public static bool IsDefined(this Gender gender) { return gender != Gender.Unknown; }
        /// <summary>
        /// Gets a value indicating wether or not the Gender value is either neutral or undefined.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>True if the Gender is is either  neutral or undefined., false otherwise.</returns>
        public static bool IsNeutralOrUndefined(this Gender gender) { return gender == Gender.Unknown || gender == Gender.Neutral; }
        #endregion
    }
}
