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
        // TODO: Add overloads which do not require an argument name to be specified and instead rely on the forthcoming nameof operator.
        #region Null Checking
        /// <summary>
        /// Validates the specified value, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument to validate.</typeparam>
        /// <param name="argument">The argument to validate.</param>
        /// <param name="argumentName">The name of the value being validated in the context of the calling method.</param>
        public static void ThrowIfNull<T>(this T argument, string argumentName) {
            if (argument == null) { throw new ArgumentNullException(argumentName, "Value cannot be null."); }
        }

        /// <summary>
        /// Validates the specified value, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument to validate.</typeparam>
        /// <param name="argument">The argument to validate.</param>
        /// <param name="argumentName">The name of the value being validated in the context of the calling method.</param>
        /// <param name="message">A message that provides additional detail as to why the argument may not be null.</param>
        public static void ThrowIfNull<T>(this T argument, string argumentName, string message) {
            if (argument == null) { throw new ArgumentNullException(argumentName, message); }
        }

        public static void ThrowIfNull(params object[] arguments) {
            for (int i = 0; i < arguments.Length; ++i) {
                ThrowIfNull(arguments[i], "arg_" + i);
            }
        }

        public static void ThrowIfNull<T1, T2>(T1 argument1, string name1, T2 argument2, string name2) {
            ThrowIfNull(argument1, name1);
            ThrowIfNull(argument2, name2);
        }
        public static void ThrowIfNull<T1, T2, T3>(T1 argument1, string name1, T2 argument2, string name2, T3 argument3, string name3) {
            ThrowIfNull(argument1, name1, argument2, name2);
            ThrowIfNull(argument3, name3);
        }
        public static void ThrowIfNull<T1, T2, T3, T4>(T1 argument1, string name1, T2 argument2, string name2, T3 argument3, string name3, T4 argument4, string name4) {
            ThrowIfNull(argument1, name1, argument2, name2, argument3, name3);
            ThrowIfNull(argument4, name4);
        }
        public static void ThrowIfNull<T1, T2, T3, T4, T5>(T1 argument1, string name1, T2 argument2, string name2, T3 argument3, string name3, T4 argument4, string name4, T5 argument5, string name5) {
            ThrowIfNull(argument1, name1, argument2, name2, argument3, name3, argument4, name4);
            ThrowIfNull(argument5, name5);
        }
        #endregion
    }
}
