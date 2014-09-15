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
        /// <param name="argument">The argument to validate.</param>
        /// <param name="argumentName">The name of the value being validated in the context of the calling method.</param>
        public static void ThrowIfNull<T>(this T argument, string argumentName) {
            if (argument == null) { throw new ArgumentNullException(argumentName, "Value cannot be null."); }
        }
        /// <summary>
        /// Validates the specified argument, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">The argument to validate.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="message">A message that provides additional detail as to why the argument may not be null.</param>
        public static void ThrowIfNull<T>(this T argument, string argumentName, string message) {
            if (argument == null) { throw new ArgumentNullException(argumentName, message); }
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <param name="argument1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="argument2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        public static void ThrowIfNull<T1, T2>(T1 argument1, string name1, T2 argument2, string name2) {
            ThrowIfNull(argument1, name1);
            ThrowIfNull(argument2, name2);
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <param name="argument1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="argument2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        /// <param name="argument3">The third argument to validate.</param>
        /// <param name="name3">The name of the third argument.</param>
        public static void ThrowIfNull<T1, T2, T3>(T1 argument1, string name1, T2 argument2, string name2, T3 argument3, string name3) {
            ThrowIfNull(argument1, name1, argument2, name2);
            ThrowIfNull(argument3, name3);
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <typeparam name="T4">The type of the fourth argument</typeparam>
        /// <param name="argument1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="argument2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        /// <param name="argument3">The third argument to validate.</param>
        /// <param name="name3">The name of the third argument.</param>      
        /// <param name="argument4">The fourth argument to validate.</param>
        /// <param name="name4">The name of the fourth argument.</param>
        public static void ThrowIfNull<T1, T2, T3, T4>(T1 argument1, string name1, T2 argument2, string name2, T3 argument3, string name3, T4 argument4, string name4) {
            ThrowIfNull(argument1, name1, argument2, name2, argument3, name3);
            ThrowIfNull(argument4, name4);
        }
        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument</typeparam>
        /// <typeparam name="T2">The type of the second argument</typeparam>
        /// <typeparam name="T3">The type of the third argument</typeparam>
        /// <typeparam name="T4">The type of the fourth argument</typeparam>
        /// <typeparam name="T5">The type of the fifth argument</typeparam>
        /// <param name="argument1">The first argument to validate.</param>
        /// <param name="name1">The name of the first argument.</param>
        /// <param name="argument2">The second argument to validate.</param>
        /// <param name="name2">The name of the second argument.</param>
        /// <param name="argument3">The third argument to validate.</param>
        /// <param name="name3">The name of the third argument.</param>      
        /// <param name="argument4">The fourth argument to validate.</param>
        /// <param name="name4">The name of the fourth argument.</param>
        /// <param name="argument5">The fifth argument to validate.</param>
        /// <param name="name5">The name of the fifth argument.</param>
        public static void ThrowIfNull<T1, T2, T3, T4, T5>(T1 argument1, string name1, T2 argument2, string name2, T3 argument3, string name3, T4 argument4, string name4, T5 argument5, string name5) {
            ThrowIfNull(argument1, name1, argument2, name2, argument3, name3, argument4, name4);
            ThrowIfNull(argument5, name5);
        }

        public static void ThrowIfLessThan<T>(T argument, T min, string name, string message) where T : IComparable<T> {
            if (argument.CompareTo(min) < 0) { throw new ArgumentOutOfRangeException(name, argument.ToString(), message); }
        }
        public static void ThrowIfGreaterThan<T>(T argument, T max, string name, string message) where T : IComparable<T> {
            if (argument.CompareTo(max) > 0) { throw new ArgumentOutOfRangeException(name, argument.ToString(), message); }
        }
        public static void ThrowIfOutOfRange<T>(T argument, T min, T max, string name, string message) where T : IComparable<T> {
            if (argument.CompareTo(min) < 0 || argument.CompareTo(max) > 0) { throw new ArgumentOutOfRangeException(name, argument.ToString(), message); }
        }

        public static void ThrowIfEmpty<T>(IEnumerable<T> argument, string name) {
            if (argument.None()) { throw new ArgumentException("Sequence contains no elements", name); }
        }
        #endregion
    }
}
