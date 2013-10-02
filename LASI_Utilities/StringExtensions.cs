using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities 
{
    /// <summary>
    /// Defines various useful methods for working with System.String instances.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if the given string is not null, not empty, and contains at least one non white space character.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True if the value parameter is not null, not empty, and contains at least one non white space character.</returns>
        public static bool IsNotWsOrNull(this string value) { return !string.IsNullOrWhiteSpace(value); }

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
        public static string[] SplitRemoveEmpty(this string value, params char[] seperator) { return value.Split(seperator, StringSplitOptions.RemoveEmptyEntries); }
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
        public static string[] SplitRemoveEmpty(this string value, params string[] seperator) { return value.Split(seperator, StringSplitOptions.RemoveEmptyEntries); }
        /// <summary>
        /// Returns a new string in which all occurrences of any of the specified strings
        /// in the current instance have been removed.
        /// </summary>
        /// <param name="value">The string to filter.</param>
        /// <param name="remove">Zero or more strings to remove from the string.</param>
        /// <returns>A new string in which all occurrences of any of the specified strings
        /// in the current instance have been removed.</returns> 
        /// <exception cref="System.ArgumentException">Thrown when one or more of the strings to remove was the empty string ("").</exception>
        public static string RemoveElements(this string value, params string[] remove) {
            if (remove.Contains(string.Empty)) { throw new ArgumentException("The string[] removed contained an empty string", "remove"); }
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
        public static string RemoveElements(this string value, params char[] remove) {
            return RemoveElements(value, remove.Select(c => c.ToString()).ToArray());
        }
    }
}
