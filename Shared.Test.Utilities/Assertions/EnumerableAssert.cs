using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shared.Test.Assertions
{
    public static class EnumerableAssert
    {
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) =>
            AreSequenceEqual(expected, actual, string.Empty);

        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message) =>
            AreSequenceEqual(expected, actual, EqualityComparer<T>.Default, message);
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer) =>
            AreSequenceEqual(expected, actual, comparer, string.Empty);
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message)
        {
            try
            {
                Assert.IsTrue(expected.SequenceEqual(actual, comparer));
            }
            catch (AssertFailedException e)
            {
                var m = ProcessMessage(message);
                throw new AssertFailedException(m + $"{nameof(EnumerableAssert)}.{nameof(AreSequenceEqual)} failed.\nExpected: {expected.ToArray()}\nActual: {actual.ToArray()}", e);
            }
        }
        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) =>
            AreSetEqual(expected, actual, string.Empty);

        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message) =>
            AreSetEqual(expected, actual, EqualityComparer<T>.Default, message);

        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer) =>
            AreSetEqual(expected, actual, comparer, string.Empty);
        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message)
        {
            try
            {
                var set = new HashSet<T>(expected, comparer);
                set.SymmetricExceptWith(actual);
                Assert.AreEqual(set.Count, 0);
            }
            catch (AssertFailedException e)
            {
                var m = ProcessMessage(message);
                var set = new HashSet<T>(expected, comparer);
                set.SymmetricExceptWith(actual);
                var missingElements = from element in set select element;
                throw new AssertFailedException(m + $"{nameof(EnumerableAssert)}.{nameof(AreSetEqual)} failed.\nThe following expected elements not found:\n{string.Join(", ", missingElements)}", e);
            }
        }
        public static void IsEmpty<T>(IEnumerable<T> enumerable) => IsEmpty(enumerable, null);
        public static void IsEmpty<T>(IEnumerable<T> enumerable, string message)
        {

            try
            {
                Assert.IsFalse(enumerable.Any());
            }
            catch (AssertFailedException e)
            {
                var m = ProcessMessage(message);
                throw new AssertFailedException(m + $"{nameof(EnumerableAssert) }.{nameof(IsEmpty)} failed.", e);
            }
        }

        public static void Contains<T>(IEnumerable<T> enumerable, T item) => Contains(enumerable, item, string.Empty);
        public static void Contains<T>(IEnumerable<T> enumerable, T item, string message)
        {
            try
            {
                Assert.IsTrue(enumerable.Contains(item));
            }
            catch (AssertFailedException e)
            {
                var m = ProcessMessage(message);
                throw new AssertFailedException(m + $"{nameof(EnumerableAssert)}.{nameof(Contains)} failed.", e);
            }
        }
        public static void DoesNotContain<T>(IEnumerable<T> enumerable, T item) => DoesNotContain(enumerable, item, string.Empty);
        public static void DoesNotContain<T>(IEnumerable<T> enumerable, T item, string message)
        {
            try
            {
                Assert.IsFalse(enumerable.Contains(item));
            }
            catch (AssertFailedException e)
            {
                var m = ProcessMessage(message);
                throw new AssertFailedException(m + $"{nameof(EnumerableAssert)}.{nameof(Contains)} failed.\nEnumerable: {string.Join(", ", enumerable)}", e);
            }
        }

        private static string ProcessMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message + "\nAdditional Messages: ";
        }
    }
}
