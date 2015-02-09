using System;
using System.Collections.Generic;
using LASI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LASI.Core.Tests.TestHelpers;

namespace LASI.Utilities.Tests
{
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void SelectTest1()
        {
            List<int> target = List(0, 1, 2, 3);
            Func<int, string> projection = x => x.ToString();
            var expected = target.AsEnumerable().Select(projection).ToList();
            var actual = target.Select(projection);
            AssertTestSuiteCommonAssertions(expected, actual);
        }
        [TestMethod]
        public void SelectTest2()
        {
            List<int> target = List(0, 1, 2, 3);
            var expected = (from x in target.AsEnumerable() select x.ToString()).ToList();
            var actual = from x in target select x.ToString();
            AssertTestSuiteCommonAssertions(expected, actual);
        }
        [TestMethod]
        public void WhereTest1()
        {
            List<int> target = List(0, 1, 2, 3);
            Func<int, bool> predicate = x => x % 2 == 0;
            var expected = target.AsEnumerable().Where(predicate).ToList();
            var actual = target.Where(predicate);
            AssertTestSuiteCommonAssertions(expected, actual);
        }
        [TestMethod]
        public void WhereTest2()
        {
            List<int> target = List(0, 1, 2, 3);
            var expected = (from x in target.AsEnumerable() where x % 2 == 0 select x).ToList();
            var actual = from x in target where x % 2 == 0 select x;
            AssertTestSuiteCommonAssertions(expected, actual);
        }

        [TestMethod]
        public void SelectManyTest1()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            var expected = target.AsEnumerable().SelectMany(xs => xs).ToList();
            var actual = target.SelectMany(xs => xs);
            AssertTestSuiteCommonAssertions(expected, actual);
        }
        [TestMethod]
        public void SelectManyTest2()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            var expected = (from xs in target.AsEnumerable()
                            from x in xs
                            select x).ToList();
            var actual = from xs in target
                         from x in xs
                         select x;
            AssertTestSuiteCommonAssertions(expected, actual);
        }
        [TestMethod]
        public void SelectManyTest3()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            var expected = target.AsEnumerable().Where(xs => xs.Any(x => x % 4 == 0)).SelectMany(xs => xs).ToList();
            var actual = target.Where(xs => xs.Any(x => x % 4 == 0)).SelectMany(xs => xs);
            AssertTestSuiteCommonAssertions(expected, actual);
        }
        [TestMethod]
        public void SelectManyTest4()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            var expected = (from xs in target.AsEnumerable()
                            where xs.Any(x => x % 4 == 0)
                            from x in xs
                            select x).ToList();
            var actual = from xs in target
                         where xs.Any(x => x % 4 == 0)
                         from x in xs
                         select x;
            AssertTestSuiteCommonAssertions(expected, actual);
        }


        private static void AssertTestSuiteCommonAssertions<T>(IEnumerable<T> expected, IEnumerable<T> actual) where T : IEquatable<T>
        {
            // expected and actual are of both System.collections.Generic.List<T> instances.
            Assert.IsTrue(expected is List<T>);
            Assert.IsTrue(actual is List<T>);
            // expected and actual are of the same runtime type
            Assert.AreEqual(expected.GetType(), actual.GetType());
            // expected and actual contain the same items.
            EnumerableAssert.AreSequenceEqual(expected, actual);

        }

        private static List<T> List<T>(params T[] values) => values.ToList();
    }
}
