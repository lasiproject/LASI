using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI;
using LASI.Core;
 using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.UnitTests.TestHelpers
{
    static class AssertHelper
    {
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        public static void AreSequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer) {
            Assert.IsTrue(expected.SequenceEqual(actual, comparer));
        }
        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
            Assert.IsTrue(expected.Except(actual).None());
        }
        public static void AreSetEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer) {
            Assert.IsTrue(expected.Except(actual, comparer).None());
        }
        //public static void AreSequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second, string message) {
        //    try {
        //        Assert.IsTrue(first.SequenceEqual(second));
        //    }
        //    catch (AssertFailedException x) {
        //        throw new AssertFailedException(message, x);
        //    }
        //}
    }
}
