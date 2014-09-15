using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Contracts.Validators;

namespace LASI
{
    /// <summary>
    /// Defines various useful methods for working with System.String instances.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the given string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is null or System.String.Empty, or if value consists exclusively of white-space characters.
        ///</returns>
        public static bool IsNullOrWhiteSpace(this string value) { return string.IsNullOrWhiteSpace(value); }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are
        /// delimited by elements of a specified Unicode character array. No empty entries are included.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="seperator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null.</param>
        /// <returns>
        /// An array whose elements contain the substrings in this string that are delimited
        /// by one or more characters in separator. No empty strings will be included.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Thrown when value is null.</exception>
        public static string[] SplitRemoveEmpty(this string value, params char[] seperator) {
            ArgumentValidator.ThrowIfNull(value, "value");
            return value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// Returns a string array that contains the substrings in this string that are
        /// delimited by elements of a specified string array. No empty entries are included.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="seperator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null.</param>
        /// <returns>
        /// An array whose elements contain the substrings in this string that are delimited
        /// by one or more characters in separator. No empty strings will be included.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Thrown when value is null.</exception>
        public static string[] SplitRemoveEmpty(this string value, params string[] seperator) {
            ArgumentValidator.ThrowIfNull(value, "value");
            return value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// Returns a new string in which all occurrences of any of the specified strings
        /// in the current instance have been removed.
        /// </summary>
        /// <param name="value">The string to filter.</param>
        /// <param name="remove">Zero or more substrings whose occurences will be removed.</param>
        /// <returns>A new string in which all occurrences of any of the specified strings
        /// in the current instance have been removed.</returns>         
        /// <exception cref="System.ArgumentNullException">Thrown when value is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the array of strings to remove contains an empty string.</exception>
        public static string RemoveElements(this string value, params string[] remove) {
            ArgumentValidator.ThrowIfNull(value, "value");
            if (remove.Contains(string.Empty))
                throw new ArgumentException("The string[] remove contained an empty string", "remove");
            foreach (var r in remove) {
                value = value.Replace(r, string.Empty);
            }
            return value;
        }
        /// <summary>
        /// Returns a new string in which all occurrences of any of the specified Unicode characters
        /// in the current instance are replaced with another specified Unicode character.
        /// </summary>
        /// <param name="value">The string to filter.</param>
        /// <param name="remove">Zero or more characters to remove from the string.</param>
        /// <returns>A new string in which all occurrences of any of the specified Unicode characters
        /// in the current instance are replaced with another specified Unicode character.</returns>  
        /// <exception cref="System.ArgumentNullException">Thrown when value is null.</exception>
        public static string RemoveElements(this string value, params char[] remove) {
            ArgumentValidator.ThrowIfNull(value, "value");
            return RemoveElements(value, (from c in remove select c.ToString()).ToArray());
        }

        /// <summary>
        /// Determines whether two strings should be considered equal when viewed in a case agnoistic manner.
        /// </summary>
        /// <param name="value">The first string to compare.</param>
        /// <param name="other">The second string to compare</param>
        /// <returns>True if the given strings are equal; otherwise, false.</returns>
        /// <remarks>Implemented using an Ordinal Case Insensitive Comparison.</remarks>
        public static bool EqualsIgnoreCase(this string value, string other) {
            return value.Equals(other, StringComparison.OrdinalIgnoreCase);
        }

    }
}
