namespace LASI.Core.Heuristics
{
    public static class GenderExtensions
    {
        /// <summary>
        /// Gets a value indicating whether or not the Gender value is male.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is male; otherwise, <c>false</c>.</returns>
        public static bool IsMale(this Gender gender) => gender == Gender.Male;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is female.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is female; otherwise, <c>false</c>.</returns>
        public static bool IsFemale(this Gender gender) => gender == Gender.Female;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is male or female.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is either male or female; otherwise, <c>false</c>.</returns>
        public static bool IsMaleOrFemale(this Gender gender) => gender == Gender.Male || gender == Gender.Female;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is neutral; otherwise, <c>false</c>.</returns>
        public static bool IsNeutral(this Gender gender) => gender == Gender.Neutral;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either male or neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is either male neutral; otherwise, <c>false</c>.</returns>
        public static bool IsMaleOrNeutral(this Gender gender) => gender == Gender.Neutral || gender == Gender.Male;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either female or neutral.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is either female or neutral; otherwise, <c>false</c>.</returns>
        public static bool IsFemaleOrNeutral(this Gender gender) => gender == Gender.Neutral || gender == Gender.Female;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either male, female, or
        /// neutral or not undefined.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns>
        /// <c>true</c> if the Gender is either male, female, or neutral or not undefined.;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDetermined(this Gender gender) => gender != Gender.Undetermined;

        /// <summary>
        /// Gets a value indicating whether or not the Gender value is either neutral or undefined.
        /// </summary>
        /// <param name="gender">The Gender value to test.</param>
        /// <returns><c>true</c> if the Gender is either neutral or undefined.; otherwise, <c>false</c>.</returns>
        public static bool IsNeutralOrUndetermined(this Gender gender) => gender == Gender.Undetermined || gender == Gender.Neutral;
    }
}