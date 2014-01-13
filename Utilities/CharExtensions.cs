using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines various useful methods for working with System.Char instances.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Returns a value indicating if the character is a consonant.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a consonant, false otherwise.</returns>
        public static bool IsConsonant(this char value) {
            return value == 'y' || value == 'Y' || value.IsEnglishLetter() && !value.IsVowel();
        }
        /// <summary>
        /// Returns a value indicating if the character is a vowel.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a vowel, false otherwise.</returns>
        public static bool IsVowel(this char value) {
            return vowels.Contains(value);

        }
        /// <summary>
        /// Returns a value indicating if the character is a letter.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a letter, false otherwise.</returns>
        public static bool IsEnglishLetter(this char value) {
            return (value > 96 && value < 123) || (value > 64 && value < 91);
        }
        private static readonly ISet<char> vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y' }.ToHashSet();
    }
}

