using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities.Text
{
    public static class CharExtensions
    {
        /// <summary>
        /// Returns a value indicating if the character is a consonant.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a consonant, false otherwise.</returns>
        public static bool IsConsonant(this char value) {
            return !value.IsVowel() && value.IsLetter();
        }
        /// <summary>
        /// Returns a value indicating if the character is a vowel.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a vowel, false otherwise.</returns>
        public static bool IsVowel(this char value) {
            unsafe {
                fixed (char* vowels = vwls)
                    for (int i = 0; i < 12; ++i) {
                        if (vowels[i] - value == 0)
                            return true;
                    }
            }
            return false;

        }
        /// <summary>
        /// Returns a value indicating if the character is a letter.
        /// </summary>
        /// <param name="value">The character to test.</param>
        /// <returns>True if the character is a letter, false otherwise.</returns>
        public static bool IsLetter(this char value) {
            return char.IsLetter(value);
        }
        private static readonly char[] vwls = { 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y' };


    }
}

