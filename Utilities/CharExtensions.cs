using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI
{
    /// <summary>
    /// Defines various useful methods for working with System.Char instances.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the character is a valid English language letter.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a letter; otherwise, false.</returns>
        public static bool IsEnglishLetter(this char value) {
            return value > 96 && value < 123 || value > 64 && value < 91;
        }
        /// <summary>
        /// Returns a value indicating whether the character is an uppercase English language letter.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is an uppercase letter; otherwise, false.</returns>
        public static bool IsUpper(this char value) {
            return char.IsUpper(value);
        }
        /// <summary>
        /// Returns a value indicating whether the character is a lowercase English language letter.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a lowercase letter; otherwise, false.</returns>
        public static bool IsLower(this char value) {
            return char.IsLower(value);
        }
        /// <summary>
        /// Returns a value indicating whether the character is a consonant.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a consonant; otherwise, false.</returns>
        public static bool IsConsonant(this char value) {
            return value == 'y' || value == 'Y' || value.IsEnglishLetter() && !value.IsVowel();
        }
        /// <summary>
        /// Returns a value indicating whether the character is a vowel.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a vowel; otherwise, false.</returns>
        public static bool IsVowel(this char value) {
            return VOWELS.IndexOf(value) >= 0;

        }
        private const string VOWELS = "aeiouyAEIOUY";
    }
}

