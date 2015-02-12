using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.Tests
{
    using Shared.Test.Assertions;
    using static Enumerable;
    [TestClass]
    public class EnumerableExtensionsTests
    {
        private static System.Tuple<T1, T2> Tuple<T1, T2>(T1 x, T2 y) => System.Tuple.Create(x, y);
        private static System.Tuple<T1, T2, T3> Tuple<T1, T2, T3>(T1 x, T2 y, T3 z) => System.Tuple.Create(x, y, z);

        #region Sequence String Formatting Methods

        [TestMethod]
        public void FormatTest()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2, 3 ]";
            var actual = target.Format();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest1()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32,\nSystem.Int32,\nSystem.Int32,\nSystem.Int32 ]";
            var actual = target.Format(20, x => x.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest2()
        {
            var target = Range(0, 4);
            var expected = "< 0; 1; 2; 3 >";
            var actual = target.Format(Tuple('<', ';', '>'));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest3()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32, System.Int32, System.Int32, System.Int32 ]";
            var actual = target.Format(e => e.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest4()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32; System.Int32; System.Int32; System.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), e => e.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest5()
        {
            var target = Range(0, 4).Select(x => x.GetType().FullName);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest6()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20, x => x.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest7()
        {
            var target = Range(0, 4);
            var expected1 = "[ 0, 1, 2,\n3 ]";
            var result1 = target.Format(9);
            Assert.AreEqual(expected1, result1);
            var expected2 = "[ 0, 1, 2,\n3 ]";
            var result2 = target.Format(10);
            Assert.AreEqual(expected2, result2);

        }
        #endregion

        #region Additional Query Operators

        [TestMethod]
        public void AppendTest()
        {
            var target = new[] { 1, 2, 3 };
            var expected = new[] { 1, 2, 3, 0 };
            var actual = target.Append(0);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }

        [TestMethod]
        public void PrependTest()
        {
            var target = new[] { 1, 2, 3 };
            var expected = new[] { 0, 1, 2, 3 };
            var actual = target.Prepend(0);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }

        [TestMethod]
        public void ToHashSetTest()
        {
            var target = Range(1, 100).ToHashSet();
            Assert.AreEqual(target.Count, 100);
            Assert.IsTrue(!target.Select(x => x % 2).ToHashSet().Except(new[] { 1, 0 }).Any());
        }

        [TestMethod]
        public void ToHashSetTest1()
        {
            var target = new[] { 'A', 'B', 'C', 'a', 'b', 'c' };
            var expected = new HashSet<char> { 'A', 'B', 'C', 'a', 'b', 'c' };
            var actual = target.ToHashSet();
            EnumerableAssert.AreSetEqual(expected, actual);
        }

        [TestMethod]
        public void ToHashSetTest2()
        {
            var caseInsensitiveComparer = Utilities.ComparerFactory.CreateEquality<char>((a, b) => a.EqualsIgnoreCase(b), c => c.ToUpper().GetHashCode());
            var target = new char[6] { 'A', 'B', 'C', 'a', 'b', 'c' };
            var expected = new HashSet<char>(caseInsensitiveComparer) { 'A', 'B', 'C', 'a', 'b', 'c' };
            var actual = target.ToHashSet(caseInsensitiveComparer);
            Assert.IsTrue(3 == expected.Count && 3 == actual.Count);
            EnumerableAssert.AreSetEqual(expected, actual);
            foreach (var c in target)
            {
                Assert.IsTrue(expected.Contains(c));
            }
        }

        [TestMethod]
        public void PairWiseTest()
        {
            var target = Range(0, 5);
            var expected = new[] { Tuple(0, 1), Tuple(1, 2), Tuple(2, 3), Tuple(3, 4) };
            var actual = target.PairWise();
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void MaxByTest()
        {
            var target = new[] { "carrot", "apple", "chicken" };
            Assert.AreEqual(target.MaxBy(s => s.Length), "chicken");
        }

        [TestMethod]
        public void MaxByTest1()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Assert.AreEqual(target.MaxBy(s => s[0], Comparer<int>.Default), "omega");
        }

        [TestMethod]
        public void MinByTest()
        {
            var target = new[] { "carrot", "apple", "chicken" };
            Assert.AreEqual(target.MinBy(s => s.Length), "apple");
        }

        [TestMethod]
        public void MinByTest1()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Assert.AreEqual(target.MinBy(s => s[0], Comparer<int>.Default), "alpha");
        }

        [TestMethod]
        public void DistinctByTest()
        {
            var target = new[] { "beach", "parrot", "peach", "seventh" };
            var result = target.DistinctBy(x => x.Length);
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(!result.Select(x => x.Length).Except(new[] { 7, 5, 6 }).Any());
        }

        [TestMethod]
        public void SetEqualTest()
        {
            var first = new[] { 1, 2, 3 };
            Assert.IsTrue(first.SetEqual(new[] { 3, 1, 2 }));
            Assert.IsTrue(first.SetEqual(new[] { 3, 1, 2, 1 }));
            Assert.IsTrue(first.SetEqual(new[] { 3, 2, 1, 2, 1, 3 }));
        }

        [TestMethod]
        public void SetEqualByTest()
        {
            var first = new[] { "carrot", "apple", "chicken" };
            Assert.IsTrue(first.SetEqualBy(new[] { "beach", "parrot", "peach", "seventh" }, s => s.Length));
        }

        [TestMethod]
        public void ZipTest()
        {
            Zip3TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            Zip3TestHelper(new[] { 2, 4, 6, 5 }, new[] { 1, 3, 5, }, new[] { 11, 24, 25 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            Zip3TestHelper(new[] { 2, 4, 6, 5 }, new[] { 1, 3, 5, 5 }, new[] { 11, 24, 25 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            Zip3TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5, 5 }, new[] { 11, 24, 25, 5 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            Zip3TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25, 5 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
        }

        private static void Zip3TestHelper<T1, T2, T3, TResult>(T1[] first, T2[] second, T3[] third, Func<T1, T2, T3, TResult> selector, TResult[] expected)
        {
            var result = first.Zip(second, third, selector).ToArray();
            Assert.AreEqual(result.Length, expected.Length);
            for (var i = 0; i < result.Length; ++i)
            {
                Assert.AreEqual(result[i], expected[i]);
            }
        }

        [TestMethod]
        public void ZipTest1()
        {
            Zip4TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6, 1 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5, 1 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25, 1 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6, 1 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5, 1 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25, 1 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            Zip4TestHelper(new[] { 2, 4, 6, 1 }, new[] { 1, 3, 5, 1 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
        }

        private static void Zip4TestHelper<T1, T2, T3, T4, TResult>(T1[] first, T2[] second, T3[] third, T4[] fourth, Func<T1, T2, T3, T4, TResult> selector, TResult[] expected)
        {
            var result = first.Zip(second, third, fourth, selector).ToArray();
            Assert.AreEqual(result.Length, expected.Length);
            for (var i = 0; i < result.Length; ++i)
            {
                Assert.AreEqual(result[i], expected[i]);
            }
        }
        #endregion
    }
}