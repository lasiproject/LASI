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
            Assert.Fail();
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
            Assert.Fail();
        }

        [TestMethod]
        public void MaxByTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void MinByTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void MinByTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void DistinctByTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void SetEqualTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void SetEqualByTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void ZipTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void ZipTest1() {
            Assert.Fail();
        }
        #endregion
    }
}