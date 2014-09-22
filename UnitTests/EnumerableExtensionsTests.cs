using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.UnitTests.TestHelpers;

namespace LASI.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region Sequence String Formatting Methods

        [TestMethod]
        public void FormatTest() {
            var target = Enumerable.Range(0, 4);
            var expected = "[ 0, 1, 2, 3 ]";
            var actual = target.Format();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void FormatTest2() {
            var target = Enumerable.Range(0, 4);
            var expected = "< 0; 1; 2; 3 >";
            var actual = target.Format(Tuple.Create('<', ';', '>'));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest3() {
            var target = Enumerable.Range(0, 4);
            var expected = "[ System.Int32, System.Int32, System.Int32, System.Int32 ]";
            var actual = target.Format(e => e.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest4() {
            var target = Enumerable.Range(0, 4);
            var expected = "{ System.Int32; System.Int32; System.Int32; System.Int32 }";
            var actual = target.Format(Tuple.Create('{', ';', '}'), e => e.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatTest5() {
            Assert.Fail();
        }

        [TestMethod]
        public void FormatTest6() {
            Assert.Fail();
        }

        [TestMethod]
        public void FormatTest7() {
            Assert.Fail();
        }
        #endregion

        #region Additional Query Operators

        [TestMethod]
        public void NoneTest() {
            var target1 = new[] { 1, 2, 3 };
            Assert.IsFalse(target1.None());
            var target2 = new int[] { };
            Assert.IsTrue(target2.None());
        }

        [TestMethod]
        public void NoneTest1() {
            var target = new[] { 1, 2, 3 };
            Assert.IsFalse(target.None(x => x < 2));
            Assert.IsTrue(target.None(x => x > 3));
        }

        [TestMethod]
        public void NoneTest2() {
            var target1 = new[] { 1, 2, 3 }.AsParallel();
            Assert.IsFalse(target1.None());
            var target2 = new int[] { }.AsParallel();
            Assert.IsTrue(target2.None());
        }

        [TestMethod]
        public void NoneTest3() {
            var target = new[] { 1, 2, 3 }.AsParallel();
            Assert.IsFalse(target.None(x => x < 2));
            Assert.IsTrue(target.None(x => x > 3));
        }

        [TestMethod]
        public void AppendTest() {
            var target = new[] { 1, 2, 3 };
            var expected = new[] { 1, 2, 3, 0 };
            var actual = target.Append(0);
            AssertHelper.AreSequenceEqual(expected, actual);
        }

        [TestMethod]
        public void PrependTest() {
            var target = new[] { 1, 2, 3 };
            var expected = new[] { 0, 1, 2, 3 };
            var actual = target.Prepend(0);
            AssertHelper.AreSequenceEqual(expected, actual);
        }

        [TestMethod]
        public void ToHashSetTest() {
            var target = Enumerable.Range(1, 100).ToHashSet();
            Assert.AreEqual(target.Count, 100);
            Assert.IsTrue(!target.Select(x => x % 2).ToHashSet().Except(new[] { 1, 0 }).Any());
        }

        [TestMethod]
        public void ToHashSetTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void ToHashSetTest2() {
            Assert.Fail();
        }

        [TestMethod]
        public void SplitTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void SplitTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void PairWiseTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void MaxByTest() {
            var target = new[] { "carrot", "apple", "chicken" };
            Assert.AreEqual(target.MaxBy(s => s.Length), "chicken");
        }

        [TestMethod]
        public void MaxByTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void MinByTest() {
            var target = new[] { "carrot", "apple", "chicken" };
            Assert.AreEqual(target.MinBy(s => s.Length), "apple");
        }

        [TestMethod]
        public void MinByTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void DistinctByTest() {
            var target = new[] { "beach", "parrot", "peach", "seventh" };
            var result = target.DistinctBy(x => x.Length);
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(!result.Select(x => x.Length).Except(new[] { 7, 5, 6 }).Any());
        }

        [TestMethod]
        public void SetEqualTest() {
            var first = new[] { 1, 2, 3 };
            Assert.IsTrue(first.SetEqual(new[] { 3, 1, 2 }));
            Assert.IsTrue(first.SetEqual(new[] { 3, 1, 2, 1 }));
            Assert.IsTrue(first.SetEqual(new[] { 3, 2, 1, 2, 1, 3 }));
        }

        [TestMethod]
        public void SetEqualByTest() {
            var first = new[] { "carrot", "apple", "chicken" };
            Assert.IsTrue(first.SetEqualBy(new[] { "beach", "parrot", "peach", "seventh" }, s => s.Length));
        }

        [TestMethod]
        public void ZipTest() {
            ZipTestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            ZipTestHelper(new[] { 2, 4, 6, 5 }, new[] { 1, 3, 5, }, new[] { 11, 24, 25 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            ZipTestHelper(new[] { 2, 4, 6, 5 }, new[] { 1, 3, 5, 5 }, new[] { 11, 24, 25 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            ZipTestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5, 5 }, new[] { 11, 24, 25, 5 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
            ZipTestHelper(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25, 5 }, (x, y, z) => x + y + z, new[] { 14, 31, 36 });
        }

        private static void ZipTestHelper<T1, T2, T3, TResult>(T1[] first, T2[] second, T3[] third, Func<T1, T2, T3, TResult> selector, TResult[] expected) {
            var result = first.Zip(second, third, selector).ToArray();
            Assert.AreEqual(result.Length, expected.Length);
            for (var i = 0; i < result.Length; ++i) {
                Assert.AreEqual(result[i], expected[i]);
            }
        }

        [TestMethod]
        public void ZipTest1() {
            ZipTestHelper1(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6, 1 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6 }, new[] { 1, 3, 5, 1 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25, 1 }, new[] { 14, 31, 36 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6, 1 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6 }, new[] { 1, 3, 5, 1 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6 }, new[] { 1, 3, 5 }, new[] { 11, 24, 25, 1 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
            ZipTestHelper1(new[] { 2, 4, 6, 1 }, new[] { 1, 3, 5, 1 }, new[] { 11, 24, 25 }, new[] { 14, 31, 36, 1 }, (a, b, c, d) => a + b + c + d, new[] { 28, 62, 72 });
        }

        private static void ZipTestHelper1<T1, T2, T3, T4, TResult>(T1[] first, T2[] second, T3[] third, T4[] fourth, Func<T1, T2, T3, T4, TResult> selector, TResult[] expected) {
            var result = first.Zip(second, third, fourth, selector).ToArray();
            Assert.AreEqual(result.Length, expected.Length);
            for (var i = 0; i < result.Length; ++i) {
                Assert.AreEqual(result[i], expected[i]);
            }
        }
        #endregion
    }
}