using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shared.Test.Assertions
{
    public static class EnumerableAssert
    {
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer)
        {
            Assert.IsTrue(expected.SequenceEqual(actual, comparer));
        }
        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.IsFalse(expected.Except(actual).Any());
        }
        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer)
        {
            Assert.IsFalse(expected.Except(actual, comparer).Any());
        }
    }
}
