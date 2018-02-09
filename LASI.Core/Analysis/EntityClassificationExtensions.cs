namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Defines extension methods which augment specific enum Types for quick, centralized access to
    /// common usage patterns.
    /// </summary>
    /// <seealso cref="PronounKind"/>
    /// <seealso cref="RelativePronounKind"/>
    /// <seealso cref="EntityKind"/>
    public static class EntityClassificationExtensions
    {
        #region PronounKind Extensions

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as
        /// explicitly male.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are thought of as
        /// explicitly male; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMale(this PronounKind kind) => kind == PronounKind.Male || kind == PronounKind.MaleReflexive;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as
        /// explicitly female.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are thought of as
        /// explicitly female; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFemale(this PronounKind kind) => kind == PronounKind.Female || kind == PronounKind.FemaleReflexive;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as
        /// explicitly gender neutral.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are thought of as
        /// explicitly gender neutral; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNeutral(this PronounKind kind) => kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as
        /// explicitly gender ambiguous.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are thought of as
        /// explicitly gender ambiguous; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGenderAmbiguous(this PronounKind kind) => !kind.IsFemale() && !kind.IsMale() && !kind.IsNeutral();

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are thought of as
        /// explicitly plural.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are thought of as
        /// explicitly plural; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPlural(this PronounKind kind) =>
            kind == PronounKind.Plural ||
            kind == PronounKind.PluralReflexive ||
            kind == PronounKind.FirstPersonPlural ||
            kind == PronounKind.FirstPersonPluralReflexive ||
            kind == PronounKind.SecondPersonPluralReflexive ||
            kind == PronounKind.ThirdPersonGenderAmbiguousPlural ||
            kind == PronounKind.ThirdPersonPluralReflexive;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are reflexive.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are reflexive
        /// false, otherwise.
        /// </returns>
        public static bool IsReflexive(this PronounKind kind) =>
            kind == PronounKind.FemaleReflexive ||
            kind == PronounKind.FirstPersonPluralReflexive ||
            kind == PronounKind.GenderAmbiguousReflexive ||
            kind == PronounKind.GenderNeurtralReflexive ||
            kind == PronounKind.MaleReflexive ||
            kind == PronounKind.PluralReflexive ||
            kind == PronounKind.SecondPersonPluralReflexive ||
            kind == PronounKind.SecondPersonSingularReflexive ||
            kind == PronounKind.ThirdPersonPluralReflexive;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are first person.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are first person;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFirstPerson(this PronounKind kind) =>
            kind == PronounKind.FirstPersonPlural ||
            kind == PronounKind.FirstPersonPluralReflexive ||
            kind == PronounKind.FirstPersonSingular;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are second person.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are second person;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSecondPerson(this PronounKind kind) =>
            kind == PronounKind.SecondPerson ||
            kind == PronounKind.SecondPersonPluralReflexive ||
            kind == PronounKind.SecondPersonSingularReflexive;

        /// <summary>
        /// Determines if the PronounKind is among the semantic categories which are third person.
        /// </summary>
        /// <param name="kind">The PronounKind to test.</param>
        /// <returns>
        /// <c>true</c> if the PronounKind is among the semantic categories which are third person;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsThirdPerson(this PronounKind kind) => !kind.IsFirstPerson() && !kind.IsSecondPerson();

        #endregion PronounKind Extensions

        #region Pronoun Extensions

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as
        /// explicitly male.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are thought of as
        /// explicitly male; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMale(this Pronoun pronoun) =>
            pronoun.PronounKind == PronounKind.Male ||
            pronoun.PronounKind == PronounKind.MaleReflexive;

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as
        /// explicitly female.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are thought of as
        /// explicitly female; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFemale(this Pronoun pronoun) =>
            pronoun.PronounKind == PronounKind.Female ||
            pronoun.PronounKind == PronounKind.FemaleReflexive;

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as
        /// explicitly gender neutral.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are thought of as
        /// explicitly gender neutral; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNeutral(this Pronoun pronoun)
        {
            var kind = pronoun.PronounKind;
            //var intSet = new[] { 1, 2, 3, 4, 5, 6, 7 }.ToSet((i, j) => i % 2 == j % 2, i => i.GetHashCode());
            return kind == PronounKind.GenderNeurtral || kind == PronounKind.GenderNeurtralReflexive;
        }

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as
        /// explicitly gender ambiguous.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are thought of as
        /// explicitly gender ambiguous; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGenderAmbiguous(this Pronoun pronoun) => pronoun.PronounKind.IsGenderAmbiguous();

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are thought of as
        /// explicitly plural.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are thought of as
        /// explicitly plural; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPlural(this Pronoun pronoun)
        {
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
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are reflexive false, otherwise.
        /// </returns>
        public static bool IsReflexive(this Pronoun pronoun) => pronoun.PronounKind.IsReflexive();

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are first person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are first person;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFirstPerson(this Pronoun pronoun) => pronoun.PronounKind.IsFirstPerson();

        /// <summary>
        /// Determines if the Pronoun is among the semantic categories which are second person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns>
        /// <c>true</c> if the Pronoun is among the semantic categories which are second person;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSecondPerson(this Pronoun pronoun) => pronoun.PronounKind.IsSecondPerson();

        /// <summary>
        /// Determines if the Pronoun is third person.
        /// </summary>
        /// <param name="pronoun">The Pronoun to test.</param>
        /// <returns><c>true</c> if the Pronoun is third person; otherwise, <c>false</c>.</returns>
        public static bool IsThirdPerson(this Pronoun pronoun) => !pronoun.IsFirstPerson() && !pronoun.IsSecondPerson();

        #endregion Pronoun Extensions

        #region EntityKind Extensions

        /// <summary>
        /// Determines if the EntityKind is plural.
        /// </summary>
        /// <param name="kind">The EntityKind to test.</param>
        /// <returns><c>true</c> if the EntityKind is plural; otherwise, <c>false</c>.</returns>
        public static bool IsMultiple(this EntityKind kind) =>
            kind == EntityKind.ThingMultiple ||
            kind == EntityKind.PersonMultiple ||
            kind == EntityKind.ActivityMultiple;

        #endregion EntityKind Extensions

        #region EntityKind <-> PronounKind Comparisons

        /// <summary>
        /// Determines if two Pronoun instances have the same gender.
        /// </summary>
        /// <param name="first">The first Pronoun.</param>
        /// <param name="second">The second Pronoun.</param>
        /// <returns><c>true</c> if both Pronouns have the same gender; otherwise, <c>false</c>.</returns>
        public static bool IsGenderEquivalentTo(this Pronoun first, Pronoun second) =>
            first.PronounKind.IsFemale() && second.PronounKind.IsFemale() ||
            first.PronounKind.IsMale() && second.PronounKind.IsMale() ||
            first.PronounKind.IsNeutral() && second.PronounKind.IsNeutral() ||
            first.PronounKind.IsGenderAmbiguous() && second.PronounKind.IsGenderAmbiguous();

        /// <summary>
        /// Determines if a Pronoun and a ProperNoun instance have the same gender.
        /// </summary>
        /// <param name="first">The Pronoun.</param>
        /// <param name="second">The ProperNoun.</param>
        /// <returns>
        /// <c>true</c> if Pronoun and the ProperNoun have the same gender; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGenderEquivalentTo(this Pronoun first, ProperNoun second) => first.Gender == second.GetGender();

        /// <summary>
        /// Determines if a Pronoun and a ProperNoun instance have the same gender.
        /// </summary>
        /// <param name="first">The Pronoun.</param>
        /// <param name="second">The ProperNoun.</param>
        /// <returns>
        /// <c>true</c> if Pronoun and the ProperNoun have the same gender; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGenderEquivalentTo(this ProperNoun first, Pronoun second) => first.GetGender() == second.Gender;

        /// <summary>
        /// Determines if two IEntity instances have the same gender.
        /// </summary>
        /// <param name="first">The first IEntity instance to compare.</param>
        /// <param name="second">The second IEntity instance to compare.</param>
        /// <returns><c>true</c> both IEntity instances have the same gender; otherwise, <c>false</c>.</returns>
        public static bool IsGenderEquivalentTo(this IEntity first, IEntity second) => first.GetGender() == second.GetGender();

        #endregion EntityKind <-> PronounKind Comparisons
    }
}