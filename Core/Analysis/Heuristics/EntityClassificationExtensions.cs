using LASI.Core.Heuristics;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods which augment specific enum Types for quick, centralized access to common usage patterns.
    /// </summary>
    /// <seealso cref="PronounKind"/>
    /// <seealso cref="RelativePronounKind"/>
    /// <seealso cref="EntityKind"/>
    public static class EntityClassificationExtensions
    {
        #region PronounKind Extensions

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitly male.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are thought of as explicitly male; otherwise, <c>false</c>.</returns>
        public static bool IsMale(this PronounKind kind) {
            return kind == PronounKind.Male || kind == PronounKind.MaleReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitly female.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are thought of as explicitly female; otherwise, <c>false</c>.</returns>
        public static bool IsFemale(this PronounKind kind) {
            return kind == PronounKind.Female || kind == PronounKind.FemaleReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitly gender neutral.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are thought of as explicitly gender neutral; otherwise, <c>false</c>.</returns>
        public static bool IsNeutral(this PronounKind kind) {
            return kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitly gender ambiguous.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are thought of as explicitly gender ambiguous; otherwise, <c>false</c>.</returns>
        public static bool IsGenderAmbiguous(this PronounKind kind) {
            return !(kind.IsFemale() || kind.IsMale() || kind.IsNeutral());
        }
        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as explicitly plural.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are thought of as explicitly plural; otherwise, <c>false</c>.</returns>
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
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are reflexive false, otherwise.</returns>
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
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are first person; otherwise, <c>false</c>.</returns>
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
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are second person; otherwise, <c>false</c>.</returns>
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
        /// <returns> <c>true</c> if the PronounKind is among the semantic categories which are third person; otherwise, <c>false</c>.</returns>
        public static bool IsThirdPerson(this PronounKind kind) => !(kind.IsFirstPerson() || kind.IsSecondPerson());
        #endregion

        #region Pronoun Extensions

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitly male.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are thought of as explicitly male; otherwise, <c>false</c>.</returns>
        public static bool IsMale(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return kind == PronounKind.Male || kind == PronounKind.MaleReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitly female.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are thought of as explicitly female; otherwise, <c>false</c>.</returns>
        public static bool IsFemale(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return kind == PronounKind.Female || kind == PronounKind.FemaleReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitly gender neutral.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are thought of as explicitly gender neutral; otherwise, <c>false</c>.</returns>
        public static bool IsNeutral(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            //var intSet = new[] { 1, 2, 3, 4, 5, 6, 7 }.ToSet((i, j) => i % 2 == j % 2, i => i.GetHashCode());
            return kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitly gender ambiguous.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are thought of as explicitly gender ambiguous; otherwise, <c>false</c>.</returns>
        public static bool IsGenderAmbiguous(this Pronoun pronoun) {
            var kind = pronoun.PronounKind;
            return !(kind.IsFemale() || kind.IsMale() || kind.IsNeutral());
        }
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as explicitly plural.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are thought of as explicitly plural; otherwise, <c>false</c>.</returns>
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
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are reflexive false, otherwise.</returns>
        public static bool IsReflexive(this Pronoun pronoun) => pronoun.PronounKind.IsReflexive();
        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are first person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are first person; otherwise, <c>false</c>.</returns>
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
        /// <returns> <c>true</c> if the Pronoun is among the semantic categories which are second person; otherwise, <c>false</c>.</returns>
        public static bool IsSecondPerson(this Pronoun pronoun) => pronoun.PronounKind.IsSecondPerson();
        /// <summary>
        /// Determines if the Pronoun is third person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns> <c>true</c> if the Pronoun is third person; otherwise, <c>false</c>.</returns>
        public static bool IsThirdPerson(this Pronoun pronoun) => !pronoun.IsFirstPerson() && !pronoun.IsSecondPerson();


        #endregion

        #region EntityKind Extensions
        /// <summary>
        /// Determimes if the EntityKind is plural.
        /// </summary>
        /// <param name="kind">The EntityKind to test.</param>
        /// <returns> <c>true</c> if the EntityKind is plural; otherwise, <c>false</c>.</returns>
        public static bool IsMultiple(this EntityKind kind) {
            return kind == EntityKind.ThingMultiple || kind == EntityKind.PersonMultiple || kind == EntityKind.ActivityMultiple;
        }
        #endregion

        #region EntityKind <-> PronounKind Comparisons
        /// <summary>
        /// Determines if two Pronoun instances have the same gender.
        /// </summary>
        /// <param name="first">The first Pronoun.</param>
        /// <param name="second">The second Pronoun.</param>
        /// <returns> <c>true</c> if both Pronouns have the same gender; otherwise, <c>false</c>.</returns>
        public static bool IsGenderEquivalentTo(this Pronoun first, Pronoun second) {
            var firstKind = first.PronounKind;
            var kind2 = second.PronounKind;
            return
                firstKind.IsFemale() && kind2.IsFemale() ||
                firstKind.IsMale() && kind2.IsMale() ||
                firstKind.IsNeutral() && kind2.IsNeutral() ||
                firstKind.IsGenderAmbiguous() && kind2.IsGenderAmbiguous();
        }

        /// <summary>
        /// Determines if a Pronoun and a ProperNoun instance have the same gender.
        /// </summary>
        /// <param name="first">The Pronoun.</param>
        /// <param name="second">The ProperNoun.</param>
        /// <returns> <c>true</c> if Pronoun and the ProperNoun have the same gender; otherwise, <c>false</c>.</returns>
        public static bool IsGenderEquivalentTo(this Pronoun first, ProperNoun second) {
            return first.Gender == second.GetGender();
        }
        /// <summary>
        /// Determines if a Pronoun and a ProperNoun instance have the same gender.
        /// </summary>
        /// <param name="first">The Pronoun.</param>
        /// <param name="second">The ProperNoun.</param>
        /// <returns> <c>true</c> if Pronoun and the ProperNoun have the same gender; otherwise, <c>false</c>.</returns>
        public static bool IsGenderEquivalentTo(this ProperNoun first, Pronoun second) {
            return first.GetGender() == second.Gender;
        }
        /// <summary>
        /// Determines if two IEntity instances have the same gender.
        /// </summary>
        /// <param name="first">The first IEntity instance to compare.</param>
        /// <param name="second">The second IEntity instance to compare.</param>
        /// <returns> <c>true</c> both IEntity instances have the same gender; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGenderEquivalentTo(this IEntity first, IEntity second) {
            return first.GetGender() == second.GetGender();
        }

        #endregion

        #region Gender Extensions

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is male.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is male; otherwise, <c>false</c>.</returns>
        public static bool IsMale(this Gender gender) {
            return gender == Gender.Male;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is female.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is female; otherwise, <c>false</c>.</returns>
        public static bool IsFemale(this Gender gender) {
            return gender == Gender.Female;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is male or female.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is either male or female; otherwise, <c>false</c>.</returns>
        public static bool IsMaleOrFemale(this Gender gender) {
            return gender == Gender.Male || gender == Gender.Female;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is neutral; otherwise, <c>false</c>.</returns>
        public static bool IsNeutral(this Gender gender) {
            return gender == Gender.Neutral;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either male or neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is either male neutral; otherwise, <c>false</c>.</returns>
        public static bool IsMaleOrNeutral(this Gender gender) {
            return gender == Gender.Neutral || gender == Gender.Male;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either female or neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is either female or neutral; otherwise, <c>false</c>.</returns>
        public static bool IsFemaleOrNeutral(this Gender gender) {
            return gender == Gender.Neutral || gender == Gender.Female;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either male, female, or neutral or not undefined.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is either male, female, or neutral or not undefined.; otherwise, <c>false</c>.</returns>
        public static bool IsDefined(this Gender gender) {
            return gender != Gender.Undetermined;
        }
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either neutral or undefined.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns> <c>true</c> if the Gender is either  neutral or undefined.; otherwise, <c>false</c>.</returns>
        public static bool IsNeutralOrUndefined(this Gender gender) {
            return gender == Gender.Undetermined || gender == Gender.Neutral;
        }
        #endregion
    }
}
