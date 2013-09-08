using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities.Text
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if the given string is not null, not empty, and contains at least one non white space character.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True if the value parameter is not null, not empty, and contains at least one non white space character.</returns>
        public static bool IsNotWsOrNull(this string value) { return !string.IsNullOrWhiteSpace(value); }
        public static string[] SplitRemoveEmpty(this string value, params char[] seperators) { return value.Split(seperators, StringSplitOptions.RemoveEmptyEntries); }
        public static string[] SplitRemoveEmpty(this string value, params string[] seperators) { return value.Split(seperators, StringSplitOptions.RemoveEmptyEntries); }
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
