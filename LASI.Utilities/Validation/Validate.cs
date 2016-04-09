using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.Validation
{
    /// <summary>Provides helper methods for value validation.</summary>
    [System.Diagnostics.DebuggerStepThrough]
    public static class Validate
    {
        #region Nullity Validation

        /// <summary>
        /// Validates the specified value, raising a <see cref="ArgumentNullException"/> if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">
        /// The name of the value being validated in the context of the calling method.
        /// </param>
        /// <exception cref="ArgumentNullException">The value was null.</exception>
        public static void NotNull<T>(T value, string name = "value")
        {
            if (ReferenceEquals(value, null))
            {
                FailWithArgumentNullException(name);
            }
        }

        /// <summary>
        /// Validates the specified value, raising a <see cref="ArgumentNullException"/> if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="message">
        /// A message that provides additional detail as to why the value may not be null.
        /// </param>
        /// <exception cref="ArgumentNullException">One of the values was null.</exception>
        public static void NotNull<T>(T value, string name, string message)
        {
            if (ReferenceEquals(value, null))
            {
                FailWithArgumentNullException(name, message);
            }
        }

        /// <summary>
        /// Validates the specified arguments, raising a <see cref="ArgumentNullException"/> if any of them
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
        /// Validates the specified arguments, raising a <see cref="ArgumentNullException"/> if any of them
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
            NotNull(
                value1, name1,
                value2, name2
            );
            NotNull(value3, name3);
        }

        /// <summary>
        /// Validates the specified arguments, raising a <see cref="ArgumentNullException"/> if any of them
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
            NotNull(
                value1, name1,
                value2, name2,
                value3, name3
            );
            NotNull(value4, name4);
        }

        /// <summary>
        /// Validates the specified arguments, raising a <see cref="ArgumentNullException"/> if any of them
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
            NotNull(
                value1, name1,
                value2, name2,
                value3, name3,
                value4, name4
            );
            NotNull(value5, name5);
        }

        #endregion Nullity Validation

        #region Arity Validation

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
                FailWithArgumentException(name, message);
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
        public static void NeitherNullNorEmpty<T>(this IEnumerable<T> value, string name, string message = null)
        {
            NotNull(value, name, message);
            NotEmpty(value, name, message);
        }

        #endregion Arity Validation

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
                FailWithOutOfRangeException(
                    actualValue: value,
                    paramName: argumentName,
                    message: message ?? $"The argument, {argumentName}, was less than the required minimum\n. Required: {argumentName} >= {minimum}; Recieved: {value}"
                );
            }
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
                FailWithOutOfRangeException(
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
        public static void WithinRange<T>(this T value, T minimum, T maximum, string name) where T : IComparable<T>, IEquatable<T>
        {
            NotLessThan(value, minimum, name ?? nameof(value), null);
            NotGreaterThan(value, maximum, name ?? nameof(value), null);
        }

        /// <summary>
        /// Validates that the specified value exists in the specified set of values, throwing a
        /// <see cref="ArgumentException" /> if does is not.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="collection">The collection in which must contain the value.</param>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value which must exist.</param>
        public static void ExistsIn<T>(IEnumerable<T> collection, T value, string name)
        {
            ExistsIn(collection, value, EqualityComparer<T>.Default, name);
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
        /// <param name="name">The name of the value which must exist.</param>
        public static void ExistsIn<T>(IEnumerable<T> collection, T value, IEqualityComparer<T> comparer, string name)
        {
            if (!collection.Contains(value, comparer))
            {
                FailWithArgumentException(name, $"{name} must be a member of the set {collection.Format()}. Actual value: {value}.");
            }
        }

        /// <summary>
        /// Validates that the specified value is not present in the specified set of values, throwing a <see cref="ArgumentException" /> if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="collection">The collection in which must contain the value.</param>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value which must not exist.</param>
        public static void DoesNotExistIn<T>(IEnumerable<T> collection, T value, string name, string message = null)
        {
            DoesNotExistIn(collection, value, EqualityComparer<T>.Default, name, message);
        }

        /// <summary>
        /// Validates that the specified value is not present in the specified set of values, throwing a <see cref="ArgumentException" /> if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="collection">The collection in which must contain the value.</param>
        /// <param name="name">The name of the value which must not exist.</param>
        public static void DoesNotExistIn<T>(IEnumerable<T> collection, IEnumerable<T> values, string name, string message = null)
        {
            foreach (var value in values)
            {
                DoesNotExistIn(collection, value, EqualityComparer<T>.Default, name, message);
            }
        }

        /// <summary>
        /// Validates that the specified value is not present in the specified set of values using the specified <see cref="IEqualityComparer{T}" />, throwing a <see cref="ArgumentException" /> if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the value to validate.</typeparam>
        /// <param name="collection">The collection in which must not contain the value.</param>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the value which must not exist.</param>
        /// <param name="comparer">The comparer to use to test for the presence of the value.</param>
        public static void DoesNotExistIn<T>(IEnumerable<T> collection, T value, IEqualityComparer<T> comparer, string name, string message = null)
        {
            if (collection.Contains(value, comparer))
            {
                FailWithArgumentException(name, message ?? $"{collection.Format()} must not contain the member {value}.");
            }
        }

        #region Conditional Validation
        /// <summary>
        /// Validates that at least one of the specified conditions holds, raising a <see cref="ArgumentException"/> if both are false.
        /// </summary>
        /// <param name="conditionOne">The first condition to test.</param>
        /// <param name="conditionTwo">The second condition to test.</param>
        /// <param name="message">A message detailing error information.</param>
        public static void Either(bool conditionOne, bool conditionTwo, string message)
        {
            if (!(conditionOne || conditionTwo))
            {
                FailWithArgumentException(message);
            }
        }
        public static void False(bool value, string message)
        {
            if (value) FailWithArgumentException(message);
        }
        public static void False<TFailWith>(bool value) where TFailWith : Exception, new()
        {
            if (value) throw new TFailWith();
        }
        public static void False<TFailWith>(bool value, Func<TFailWith> failWith) where TFailWith : Exception
        {
            if (value) throw failWith();
        }
        public static void False<TFailWith>(bool value, string message, Func<string, TFailWith> failWith) where TFailWith : Exception
        {
            if (value) throw failWith(message);
        }
        public static void True(bool value, string message)
        {
            if (!value) FailWithArgumentException(message);
        }
        public static void True<TFailWith>(bool value) where TFailWith : Exception, new()
        {
            if (!value) throw new TFailWith();
        }
        public static void True<TFailWith>(bool value, Func<TFailWith> failWith) where TFailWith : Exception
        {
            if (!value) throw failWith();
        }
        public static void True<TFailWith>(bool value, string message, Func<string, TFailWith> failWith) where TFailWith : Exception
        {
            if (!value) throw failWith(message);
        }
        #endregion Conditional Validation

        #endregion Range Validation

        #region Exception Helpers

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="name">The name of the argument which caused the exception.</param>
        private static void FailWithArgumentNullException(string name)
        {
            throw new ArgumentNullException(name);
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="name">The name of the argument which caused the exception.</param>
        /// <param name="message">A message describing the error.</param>
        private static void FailWithArgumentNullException(string name, string message)
        {
            throw new ArgumentNullException(name, message);
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="actualValue">The value which caused the exception.</param>
        /// <param name="paramName">The name of the argument which caused the exception.</param>
        /// <param name="message">A message describing the error.</param>
        private static void FailWithOutOfRangeException<T>(T actualValue, string paramName, string message) where T : IComparable<T>, IEquatable<T>
        {
            throw new ArgumentOutOfRangeException(paramName, actualValue, message);
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="name">The name of the argument which caused the exception.</param>
        /// <param name="message">A message describing the error.</param>
        private static void FailWithArgumentException(string name, string message)
        {
            throw new ArgumentException(message, name);
        }
        /// <summary>
        /// Throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        private static void FailWithArgumentException(string message)
        {
            throw new ArgumentException(message);
        }
        #endregion Exception Helpers
    }
}