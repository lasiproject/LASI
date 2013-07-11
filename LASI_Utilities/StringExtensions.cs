using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if the string is not Empty And not Null.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>True if the string is neither Empty nor Null, false otherwise.</returns>
        public static bool IsNotEmpty(this string str) {
            return !(string.IsNullOrEmpty(str));
        }
        /// <summary>
        /// Determines if the string is not White Space And not Null.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>True if the string is neither White Space nor Null, false otherwise.</returns>
        public static bool IsNotWhiteSpace(this string str) {
            return !(string.IsNullOrWhiteSpace(str));
        }
    }
}
