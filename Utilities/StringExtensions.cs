using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    /// <summary>Defines various useful methods for working with System.String instances.</summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the given string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns> <c>true</c> if the value parameter is null or System.String.Empty, or if value consists exclusively of white-space characters.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Returns a string array that contains the nonempty substrings in this string that are
        /// delimited by elements of a specified Unicode character array.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="seperator">
        /// An array of Unicode characters that delimit the substrings in this string, an empty
        /// array that contains no delimiters, or null.
        /// </param>
        /// <returns>
        /// An array whose elements contain the nonempty substrings in this string that are
        /// delimited by one or more characters in separator.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        public static string[] SplitRemoveEmpty(this string value, params char[] seperator)
        {
            Validate.NotNull(value, "value");
            return value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a string array that contains the nonempty substrings in this string that are
        /// delimited by elements of a specified string array.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="seperator">
        /// An array of strings that delimit the substrings in this string, an empty array that
        /// contains no delimiters, or null.
        /// </param>
        /// <returns>
        /// An array whose elements contain the nonempty substrings in this string that are
        /// delimited by one or more characters in separator.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        /// <remarks>
        /// 
        /// </remarks>
        public static string[] SplitRemoveEmpty(this string value, params string[] seperator)
        {
            Validate.NotNull(value, "value");
            return value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of any of the specified strings in the
        /// current instance have been removed.
        /// </summary>
        /// <param name="value">The string to filter.</param>
        /// <param name="remove">Zero or more substrings whose occurrences will be removed.</param>
        /// <returns>
        /// A new string in which all occurrences of any of the specified strings in the current
        /// instance have been removed.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the array of strings to remove contains an empty string.
        /// </exception>
        public static string RemoveSubstrings(this string value, params string[] remove) => value.RemoveSubstrings(StringComparison.CurrentCulture, remove);
        public static string RemoveSubstrings(this string value, StringComparison comparison, params string[] remove)
        {
            Validate.NotNull(value, "value");
            if (remove.Contains(string.Empty))
            {
                throw new ArgumentException($"The string[] {nameof(remove)} contained an empty string", nameof(remove));
            }
            foreach (var r in remove)
            {
                value = value.Replace(r, string.Empty);
            }
            return value;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of any of the specified Unicode characters
        /// in the current instance are replaced with another specified Unicode character.
        /// </summary>
        /// <param name="value">The string to filter.</param>
        /// <param name="anyOf">Zero or more characters to remove from the string.</param>
        /// <returns>
        /// A new string in which all occurrences of any of the specified Unicode characters in the
        /// current instance are replaced with another specified Unicode character.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        public static string RemoveAnyOf(this string value, params char[] anyOf)
        {
            Validate.NotNull(value, "value");
            return RemoveSubstrings(value, anyOf.Select(c => c.ToString()).ToArray());
        }

        /// <summary>
        /// Determines whether two strings are considered equal when viewed in a case Insensitive manner.
        /// </summary>
        /// <param name="value">The first string to compare.</param>
        /// <param name="other">The second string to compare</param>
        /// <returns><c>true</c> if the given strings are equal; otherwise, <c>false</c>.</returns>
        /// <remarks>Implemented using an Ordinal Case Insensitive Comparison.</remarks>
        public static bool EqualsIgnoreCase(this string value, string other) => string.Equals(value, other, StringComparison.OrdinalIgnoreCase);

        public static string CollapseSpaces(this string value) => string.Join(" ", value.SplitRemoveEmpty(' ', '\t').Select(s => s.Trim()));

        /// <summary>
        /// Transforms the input string, adding a space before each capital letter except the first.
        /// </summary>
        /// <param name="value">The string to transform.</param>
        /// <returns>A new string with a space inserted before each capital letter but the first.s</returns>
        public static string SpaceByCase(this string value) => value?.Aggregate(
                new StringBuilder(),
                (builder, c, index) =>
                    index > 0 && c.IsUpper() ?
                    builder.Append(' ').Append(c) :
                    builder.Append(c)
            ).ToString() ?? "";
        public static string ToPath(this IEnumerable<string> pathSegements) => System.IO.Path.Combine(pathSegements.ToArray());
    }
}