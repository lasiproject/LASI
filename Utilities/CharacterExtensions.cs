using System.Collections.Generic;
using System.Linq;

namespace LASI
{
    /// <summary>
    /// Defines various useful methods for working with characters.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the character is a valid letter.
        /// </summary>
        /// <param name="c">
        /// The character to test.
        /// </param>
        /// <returns>
        /// True if the character is a letter; otherwise, false.
        /// </returns>
        public static bool IsAlphabetic(this char c) => c > 96 && c < 123 || c > 64 && c < 91;

        /// <summary>
        /// Returns a value indicating whether the character is a consonant.
        /// </summary>
        /// <param name="c">
        /// The character to test.
        /// </param>
        /// <returns>
        /// True if the character is a consonant; otherwise, false.
        /// </returns>
        public static bool IsConsonant(this char c) => c == 'y' || c == 'Y' || c.IsAlphabetic() && !c.IsVowel();

        /// <summary>
        /// Returns a value indicating whether the character is a lowercase letter.
        /// </summary>
        /// <param name="c">
        /// The character to test.
        /// </param>
        /// <returns>
        /// True if the character is a lowercase letter; otherwise, false.
        /// </returns>
        public static bool IsLower(this char c) => char.IsLower(c);

        /// <summary>
        /// Returns a value indicating whether the character is an uppercase letter.
        /// </summary>
        /// <param name="c">
        /// The character to test.
        /// </param>
        /// <returns>
        /// True if the character is an uppercase letter; otherwise, false.
        /// </returns>
        public static bool IsUpper(this char c) => char.IsUpper(c);

        /// <summary>
        /// Returns a value indicating whether the character is a vowel.
        /// </summary>
        /// <param name="c">
        /// The character to test.
        /// </param>
        /// <returns>
        /// True if the character is a vowel; otherwise, false.
        /// </returns>
        public static bool IsVowel(this char c) => VOWELS.IndexOf(c) >= 0;

        /// <summary>
        /// Converts the value of the Unicode character to its lowercase equivalent.
        /// </summary>
        /// <param name="c">
        /// The Unicode character to convert.
        /// </param>
        /// <returns>
        /// The lowercase equivalent of c, or the unchanged value of c, if c is already lowercase or
        /// not alphabetic.
        /// </returns>
        public static char ToLower(this char c) => char.ToLower(c);

        /// <summary>
        /// Converts the value of the Unicode character to its uppercase equivalent.
        /// </summary>
        /// <param name="c">
        /// The Unicode character to convert.
        /// </param>
        /// <returns>
        /// The uppercase equivalent of c, or the unchanged value of c, if c is already lowercase or
        /// not alphabetic.
        /// </returns>
        public static char ToUpper(this char c) => char.ToUpper(c);

        /// <summary>
        /// Determines whether two characters are considered equal when viewed in a case Insensitive manner.
        /// </summary>
        /// <param name="value">The first character to compare.</param>
        /// <param name="other">The second character to compare</param>
        /// <returns>True if the given characters considered are equal; otherwise, false.</returns>
        /// <remarks>This comparison uses the default culture.</remarks>
        public static bool EqualsIgnoreCase(this char a, char b) => a.ToUpper() == b.ToUpper();

        private const string VOWELS = "aeiouyAEIOUY";
    }
}