using System;
using System.Collections.Generic;
using LASI.Utilities.Specialized.Enhanced.Linq.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Shared.Test.Assertions;
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
        private static void AssertTestSuiteCommonAssertions<T>(IList<T> expected, IList<T> actual)
        {
            Assert.IsNotNull(actual);
            // expected and actual contain the same items.
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = target.AsEnumerable().Skip(1).ToList();
            List<int> actual = target.Skip(1);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(0);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest3()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(-1);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest4()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(-1);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest5()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(-140);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest6()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Skip(10);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest7()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Skip(11);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTest8()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Skip(110);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 5);
            List<int> actual = target.Take(5);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(10);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest3()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(11);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest4()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(101);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest5()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Take(0);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest6()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Take(-1);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest7()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Take(-101);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void TakeTest8()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(10);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTakeTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(2, 5);
            List<int> actual = target.Skip(2).Take(5);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        [TestMethod]
        public void SkipTakeTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(0).Take(10);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        public void TakeSkipTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(4, 1);
            List<int> actual = target.Take(5).Skip(4);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        public void TakeSkipTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(10).Skip(0);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }
        private static List<T> List<T>(params T[] values) => values.ToList();
        private static List<int> Range(int start, int count) => Enumerable.Range(start, count).ToList();
    }
}
