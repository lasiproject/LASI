using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.Validation
{
    /// <summary>Provides helper methods for value validation.</summary>
    [System.Diagnostics.DebuggerStepThrough]
    public static class Validate
    {
        #region Null Validation

        /// <summary>
        /// Validates the specified value, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="argumentName">
        /// The name of the value being validated in the context of the calling method.
        /// </param>
        /// <exception cref="ArgumentNullException">The value was null.</exception>
        public static void NotNull<T>(this T value, string argumentName = "value")
        {
            if (value == null) { throw new ArgumentNullException(argumentName, "Argument may not be null."); }
        }

        /// <summary>
        /// Validates the specified value, raising a System.ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="argumentName">The name of the value.</param>
        /// <param name="message">
        /// A message that provides additional detail as to why the value may not be null.
        /// </param>
        /// <exception cref="ArgumentNullException">One of the values was null.</exception>
        public static void NotNull<T>(T value, string argumentName, string message)
        {
            if (value == null) { throw new ArgumentNullException(argumentName, message); }
        }

        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them
        /// are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <param name="value1">The first value to validate.</param>
        /// <param name="name1">The name of the first value.</param>
        /// <param name="value2">The second value to validate.</param>
        /// <param name="name2">The name of the second value.</param>
        /// <exception cref="ArgumentNullException">One of the values was null.</exception>
        public static void NotNull<T1, T2>(T1 value1, string name1, T2 value2, string name2)
        {
            NotNull(value1, name1);
            NotNull(value2, name2);
        }

        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them
        /// are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <param name="value1">The first value to validate.</param>
        /// <param name="name1">The name of the first value.</param>
        /// <param name="value2">The second value to validate.</param>
        /// <param name="name2">The name of the second value.</param>
        /// <param name="value3">The third value to validate.</param>
        /// <param name="name3">The name of the third value.</param>
        /// <exception cref="ArgumentNullException">One of the values was null.</exception>
        public static void NotNull<T1, T2, T3>(T1 value1, string name1, T2 value2, string name2, T3 value3, string name3)
        {
            NotNull(value1, name1, value2, name2);
            NotNull(value3, name3);
        }

        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them
        /// are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <param name="value1">The first value to validate.</param>
        /// <param name="name1">The name of the first value.</param>
        /// <param name="value2">The second value to validate.</param>
        /// <param name="name2">The name of the second value.</param>
        /// <param name="value3">The third value to validate.</param>
        /// <param name="name3">The name of the third value.</param>
        /// <param name="value4">The fourth value to validate.</param>
        /// <param name="name4">The name of the fourth value.</param>
        /// <exception cref="ArgumentNullException">One of the values was null.</exception>
        public static void NotNull<T1, T2, T3, T4>(T1 value1, string name1, T2 value2, string name2, T3 value3, string name3, T4 value4, string name4)
        {
            NotNull(value1, name1, value2, name2, value3, name3);
            NotNull(value4, name4);
        }

        /// <summary>
        /// Validates the specified arguments, raising a System.ArgumentNullException if any of them
        /// are null.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <param name="value1">The first value to validate.</param>
        /// <param name="name1">The name of the first value.</param>
        /// <param name="value2">The second value to validate.</param>
        /// <param name="name2">The name of the second value.</param>
        /// <param name="value3">The third value to validate.</param>
        /// <param name="name3">The name of the third value.</param>
        /// <param name="value4">The fourth value to validate.</param>
        /// <param name="name4">The name of the fourth value.</param>
        /// <param name="value5">The fifth value to validate.</param>
        /// <param name="name5">The name of the fifth value.</param>
        /// <exception cref="ArgumentNullException">One of the values was null.</exception>
        public static void NotNull<T1, T2, T3, T4, T5>(T1 value1, string name1, T2 value2, string name2, T3 value3, string name3, T4 value4, string name4, T5 value5, string name5)
        {
            NotNull(value1, name1, value2, name2, value3, name3, value4, name4);
            NotNull(value5, name5);
        }

        #endregion Null Validation

        #region Range Validation

        /// <summary>
        /// Validates the specified value, raising a System.ArgumentOutOfRangeException if it is
        /// less than the specified minimum.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="minimum">The minimum value to validate against.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <param name="message">
        /// A message that provides additional detail as to why failed validation.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">The value was less the specified minimum.</exception>
        public static void NotLessThan<T>(this T value, T minimum, string name = null, string message = null)
            where T : IComparable<T>, IEquatable<T>
        {
            if (value.CompareTo(minimum) < 0)
            {
                var argumentName = name ?? nameof(value);
                throw CreateOutOfRangeException(
                    actualValue: value,
                    paramName: argumentName,
                    message: message ?? $"The argument, {argumentName}, was less than the required minimum\n. Required: {argumentName} >= {minimum}; Recieved: {value}"
                );
            }
        }

        private static ArgumentOutOfRangeException CreateOutOfRangeException<T>(T actualValue, string paramName, string message) where T : IComparable<T>, IEquatable<T>
        {
            return new ArgumentOutOfRangeException(paramName, actualValue, message);
        }

        /// <summary>
        /// Validates the specified value, raising a System.ArgumentOutOfRangeException if it is
        /// greater than the specified maximum.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="maximum">The maximum value to validate against.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <param name="message">
        /// A message that provides additional detail as to why the value failed validation.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">The value was greater than the specified maximum.</exception>
        public static void NotGreaterThan<T>(this T value, T maximum, string name = null, string message = null) where T : IComparable<T>, IEquatable<T>
        {
            if (value.CompareTo(maximum) > 0)
            {
                var argumentName = name ?? "value";
                throw CreateOutOfRangeException(
                    actualValue: value,
                    paramName: argumentName,
                    message: message ?? $"The argument, {argumentName}, was greater than the required maximum\n. Required: {argumentName} <= {maximum}; Recieved: {value}"
                );
            }
        }

        /// <summary>
        /// Validates the specified value, raising a System.ArgumentOutOfRangeException if it is
        /// greater than the specified maximum or less than the specified minimum.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="minimum">The minimum value to validate against.</param>
        /// <param name="maximum">The maximum value to validate against.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <exception cref="ArgumentOutOfRangeException">The value was less than the specified minimum or greater than the specified maximum.</exception>
        public static void WithinRange<T>(
            this T value,
            T minimum, T maximum,
            string name = "value") where T : IComparable<T>, IEquatable<T>
        {
            NotLessThan(value, minimum, name ?? nameof(value), null);
            NotGreaterThan(value, maximum, name ?? nameof(value), null);
        }

        /// <summary>
        /// Validates that the specified IEnumerable value, raising an <see cref="ArgumentException"/> if it is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="value">The sequence to validate.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <param name="message">
        /// A message that provides additional detail as to why the value failed validation.
        /// </param>
        /// <remarks>
        /// In order to validate that the given sequence contains at least one element, a portion of sequence must be materialized.
        /// If the sequence is described by a stateful or transient iterator, there is no guarantee that it will not be empty when it is actually consumed.
        /// The validation is described by the <see cref="Enumerable.Any{TSource}(IEnumerable{TSource})"/> method.
        /// </remarks>
        public static void NotEmpty<T>(this IEnumerable<T> value, string name = "value", string message = "Sequence cannot be empty")
        {
            if (!value.Any())
            {
                throw new ArgumentException($"Sequence cannot be empty; {name}");
            }
        }

        /// <summary>
        /// Validates the specified <see cref="IEnumerable{T}" />, raising a
        /// <see cref="ArgumentException" /> if the it is null or contains no elements. This
        /// implicitly materializes the sequence; see the remarks section for further details.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <remarks>
        /// In order to validate that the given sequence contains at least one element, a portion of
        /// sequence must be materialized. If the sequence is described by a stateful or transient
        /// iterator, there is no guarantee that it will not be empty when it is actually consumed.
        /// The validation is described by the
        /// <see cref="Enumerable.Any{TSource}(IEnumerable{TSource})" /> method.
        /// </remarks>
        public static void NeitherNullNorEmpty<T>(this IEnumerable<T> value, string name)
        {
            NotNull(value, name);
            NotEmpty(value, name);
        }

        /// <summary>
        /// Validates that the specified value exists in the specified set of values, throwing a
        /// <see cref="ArgumentException" /> if does is not.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <param name="collection">The collection in which must contain the value.</param>
        public static void ExistsIn<T>(this T value, string name, IEnumerable<T> collection)
        {
            ExistsIn(value, name, collection, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Validates that the specified value exists in the specified array of values, throwing a
        /// <see cref="ArgumentException" /> if it does not.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value to validate.</param>
        /// <param name="values">The array of values in which to ensure membership.</param>
        public static void ExistsIn<T>(this T value, string name, params T[] values)
        {
            ExistsIn(value, name, values.AsEnumerable(), EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Validates that the specified value exists in the specified set of values using the
        /// specified <see cref="IEqualityComparer{T}" />, throwing a
        /// <see cref="ArgumentException" /> if it is not.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="collection">The collection in which must contain the value.</param>
        /// <param name="value">The value to validate.</param>
        /// <param name="comparer">The comparer to use to validate that the value exists.</param>
        /// <param name="name">The name of the value to validate.</param>
        public static void ExistsIn<T>(this T value, string name, IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            if (!collection.Contains(value, comparer))
                throw new ArgumentException($"{name} must be a member of the set {collection.Format()}. Actual value: {value}.", name);
        }

        #endregion Range Validation
    }
}