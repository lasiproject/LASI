using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Utilities.Contracts.Validators
{
    /// <summary>
    /// Provides helper methods for argument validatation.
    /// </summary>
    public static class ArgumentValidator
    {
        // TODO: Add overloads which do not require an argument name to be specified and take advantage of the increased maintainability offered by the forthcoming nameof operator.
        #region Null Checking
        /// <summary>
        /// Validates the specified argument, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument to validate.</typeparam>
        /// <param name="value">The argument to validate.</param>
        /// <param name="argumentName">The name of the value being validated in the context of the calling method.</param>
        public static void ThrowIfNull<T>(T value, string argumentName) {
            if (value == null) { throw new ArgumentNullException(argumentName, "Value cannot be null."); }
        }
        /// <summary>
        /// Validates the specified argument, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="value">The argument to validate.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="message">A message that provides additional detail as to why the argument may not be null.</param>
        public static void ThrowIfNull<T>(T value, string argumentName, string message) {
            if (value == null) { throw new ArgumentNullException(argumentName, message); }
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <param name="value1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="value2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        public static void ThrowIfNull<T1, T2>(T1 value1, string name1, T2 value2, string name2) {
            ThrowIfNull(value1, name1);
            ThrowIfNull(value2, name2);
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <param name="value1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="value2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        /// <param name="value3">The third argument to validate.</param>
        /// <param name="name3">The name of the third argument.</param>
        public static void ThrowIfNull<T1, T2, T3>(T1 value1, string name1, T2 value2, string name2, T3 value3, string name3) {
            ThrowIfNull(value1, name1, value2, name2);
            ThrowIfNull(value3, name3);
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <typeparam name="T4">The type of the fourth argument</typeparam>
        /// <param name="value1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="value2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        /// <param name="value3">The third argument to validate.</param>
        /// <param name="name3">The name of the third argument.</param>      
        /// <param name="value4">The fourth argument to validate.</param>
        /// <param name="name4">The name of the fourth argument.</param>
        public static void ThrowIfNull<T1, T2, T3, T4>(T1 value1, string name1, T2 value2, string name2, T3 value3, string name3, T4 value4, string name4) {
            ThrowIfNull(value1, name1, value2, name2, value3, name3);
            ThrowIfNull(value4, name4);
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <typeparam name="T4">The type of the fourth argument</typeparam>
        /// <typeparam name="T5">The type of the fifth argument</typeparam>
        /// <param name="value1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="value2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        /// <param name="value3">The third argument to validate.</param>
        /// <param name="name3">The name of the third argument.</param>      
        /// <param name="value4">The fourth argument to validate.</param>
        /// <param name="name4">The name of the fourth argument.</param>
        /// <param name="value5">The fifth argument to validate.</param>
        /// <param name="name5">The name of the fifth argument.</param>
        public static void ThrowIfNull<T1, T2, T3, T4, T5>(T1 value1, string name1, T2 value2, string name2, T3 value3, string name3, T4 value4, string name4, T5 value5, string name5) {
            ThrowIfNull(value1, name1, value2, name2, value3, name3, value4, name4);
            ThrowIfNull(value5, name5);
        }
        /// <summary>
        /// Validates the specified argument, raising a System.ArgumentOutOfRangeException if it is less than the specified minimum.
        /// </summary>
        /// <typeparam name="T">The type of the argument to validate.</typeparam>
        /// <param name="value">The argument to validate.</param>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="min">The minimum value to validate against.</param>
        /// <param name="message">A message that provides additional detail as to why the argument failed validation.</param>
        public static void ThrowIfLessThan<T>(T value, string name, T min, string message) where T : IComparable<T> {
            if (value.CompareTo(min) < 0) { throw new ArgumentOutOfRangeException(name, value.ToString(), message); }
        }
        /// <summary>
        /// Validates the specified argument, raising a System.ArgumentOutOfRangeException if it is greater than the specified maximum.
        /// </summary>
        /// <typeparam name="T">The type of the argument to validate.</typeparam>
        /// <param name="value">The argument to validate.</param>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="max">The maximum value to validate against.</param>
        /// <param name="message">A message that provides additional detail as to why the argument failed validation.</param>
        public static void ThrowIfGreaterThan<T>(T value, string name, T max, string message) where T : IComparable<T> {
            if (value.CompareTo(max) > 0) { throw new ArgumentOutOfRangeException(name, value.ToString(), message); }
        }
        /// <summary>
        /// Validates the specified argument, raising a System.ArgumentOutOfRangeException if it is greater than the specified maximum or less than the specified minimum.
        /// </summary>
        /// <typeparam name="T">The type of the argument to validate.</typeparam>
        /// <param name="value">The argument to validate.</param>
        /// <param name="name">The name of the argument to validate.</param>
        /// <param name="min">The minimum value to validate against.</param>
        /// <param name="max">The maximum value to validate against.</param>
        /// <param name="message">A message that provides additional detail as to why the argument failed validation.</param>
        public static void ThrowIfOutOfRange<T>(T value, string name, T min, T max, string message) where T : IComparable<T> {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0) { throw new ArgumentOutOfRangeException(name, value.ToString(), message); }
        }
        /// <summary>
        /// Validates the specified IEnumerable argument, raising a System.ArgumentException if it is contains no elements.
        /// </summary>
        /// <param name="value">The argument to validate.</param>
        /// <param name="name">The name of the argument to validate.</param>
        public static void ThrowIfEmpty<T>(IEnumerable<T> value, string name) {
            if (!value.Any()) { throw new ArgumentException("Sequence contains no elements", name); }
        }
        #endregion
    }
}
