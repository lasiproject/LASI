using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using NFluent.Extensions;
using NFluent.Helpers;

namespace LASI.Utilities.Tests
{
    using Shared.Test.Assertions;
    using Test = TestMethodAttribute;
    using static Enumerable;
    using FluentAssertions;

    [TestClass]
    public class EnumerableExtensionsTest
    {
        private static Pair<T1, T2> Pair<T1, T2>(T1 x, T2 y) => LASI.Utilities.Pair.Create(x, y);
        private static Tuple<T1, T2, T3> Tuple<T1, T2, T3>(T1 x, T2 y, T3 z) => System.Tuple.Create(x, y, z);

        #region Sequence String Formatting Methods

        [Test]
        public void Format_Uses_SquareBracket_And_Comma_By_Default()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2, 3 ]";
            var actual = target.Format();
            Check.That(actual).IsEqualTo(expected);
        }

        [Test]
        public void Format_Applies_Specified_String_Selector()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32, System.Int32, System.Int32, System.Int32 ]";
            var actual = target.Format(e => e.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Format_Applies_Specified_String_Selector_And_Line_Length()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32,\nSystem.Int32,\nSystem.Int32,\nSystem.Int32 ]";
            var actual = target.Format(lineLength: 20, selector: x => x.GetType().FullName);
            Check.That(actual).IsEqualTo(expected);
        }

        [Test]
        public void Format_Uses_Specified_Bracketing_And_Separator()
        {
            var target = Range(0, 4);
            var expected = "< 0; 1; 2; 3 >";
            var actual = target.Format(Tuple('<', ';', '>'));
            Check.That(actual).IsEqualTo(expected);
        }

        [Test]
        public void FormatTest4()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32; System.Int32; System.Int32; System.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), e => e.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Format_Uses_Specified_Bracketing_And_Line_Length()
        {
            var target = Range(0, 4).Select(x => x.GetType().FullName);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Format_Uses_Specified_Bracketing_And_Separator_And_String_Selector_And_Line_Length()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20, x => x.GetType().FullName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Format_Enforces_Specified_Line_Length_Test_One()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2,\n3 ]";
            var actual = target.Format(9);
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void Format_Enforces_Specified_Line_Length_Test_Two()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2,\n3 ]";
            var actual = target.Format(10);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Format_Enforces_Specified_Line_Length_Test_Three()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1,\n2, 3 ]";
            var actual = target.Format(8);
            Assert.AreEqual(expected, actual);

        }
        #endregion

        #region Additional Query Operators

        [Test]
        public void Append_Inserts_Value_At_End()
        {
            var values = new[] { 1, 2, 3 };
            Check.That(values.Append(0)).ContainsExactly(1, 2, 3, 0);
        }
        [Test]
        public void Append_Inserts_Actual_Value_At_End()
        {
            var elements = Range(0, 5).Select(x => new object());
            var toAppend = new object();
            elements.Append(toAppend).Should().EndWith(toAppend);
        }

        [Test]
        public void PrependInserts_Value_At_Start()
        {
            var values = new[] { 1, 2, 3 };
            Check.That(values.Prepend(0)).ContainsExactly(0, 1, 2, 3);
        }

        [Test]
        public void Append_Inserts_Actual_Value_At_Start()
        {
            var elements = Range(0, 5).Select(x => new object());
            var toPrepend = new object();
            elements.Prepend(toPrepend).Should().StartWith(toPrepend);
        }

        [Test]
        public void PairWise_Pairs_Adjacent_Elements()
        {
            var target = Range(12, 5).PairWise();
            Check.That(target).ContainsExactly(Pair(12, 13), Pair(13, 14), Pair(14, 15), Pair(15, 16));
        }

        [Test]
        public void MaxBy_Yields_Value_With_Max_Under_Projection_Test_One()
        {
            var target = new[] { "apple", "chicken", "carrot" };
            Check.That(target.MaxBy(s => s.Length)).IsEqualTo("chicken");
        }

        [Test]
        public void MaxBy_Yields_Value_With_Max_Under_Projection_Test_Two()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Assert.AreEqual(target.MaxBy(s => s[0], Comparer<int>.Default), "omega");
        }

        [Test]
        public void MaxBy_Yields_Value_With_Min_Under_Projection_Test_One()
        {
            var target = new[] { "carrot", "apple", "chicken" };
            Assert.AreEqual(target.MinBy(s => s.Length), "apple");
        }

        [Test]
        public void MaxBy_Yields_Value_With_Min_Under_Projection_Test_Two()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Assert.AreEqual(target.MinBy(s => s[0], Comparer<int>.Default), "alpha");
        }

        [Test]
        public void DistinctBy_Uses_Set_Semantics_Under_Projection()
        {
            var target = new[] { "beach", "parrot", "peach", "seventh" };
            var result = target.DistinctBy(x => x.Length);
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(!result.Select(x => x.Length).Except(new[] { 7, 5, 6 }).Any());
        }

        [Test]
        public void SetEqual_Uses_Set_semantics()
        {
            var values = new[] { 1, 2, 3 };
            Assert.IsTrue(values.SetEqual(new[] { 3, 1, 2 }));
            Assert.IsTrue(values.SetEqual(new[] { 3, 1, 2, 1 }));
            Assert.IsTrue(values.SetEqual(new[] { 3, 2, 1, 2, 1, 3 }));
        }

        [Test]
        public void SetEqual_Uses_Set_semantics_Under_Projection()
        {
            var first = new[] { "carrot", "apple", "chicken" };
            Assert.IsTrue(first.SetEqualBy(new[] { "beach", "parrot", "peach", "seventh" }, s => s.Length));
        }

        [Test]
        public void Zip3Test()
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

        [Test]
        public void Zip4Test()
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

        [Test]
        public void Aggregate_With_Index_Matches_Aggregate_When_Index_Is_Unused_By_Aggregator()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate((sum, n, index) => sum + n)).IsEqualTo(values.Aggregate((sum, n) => sum + n));
        }

        [Test]
        public void Aggregate_With_Index_Matches_Aggregate_When_Seed_Is_Specified_And_Index_Is_Unused_By_Aggregator()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(14, (sum, n, index) => sum + n)).IsEqualTo(values.Aggregate(14, (sum, n) => sum + n));
        }

        [Test]
        public void Aggregate_With_Index_Passes_Correct_Index()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate((sum, n, i) => sum + n + i)).IsEqualTo(values.Select((n, i) => n + i).Aggregate((sum, n) => sum + n));
        }

        [Test]
        public void Aggregate_With_Index_Specifies_Expected_Index_And_Applies_Result_Selector()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(1, (sum, n, i) => sum + n + i, r => r * 2))
                .IsEqualTo(values.Select((n, i) => n + i).Aggregate(1, (sum, n) => sum + n, r => r * 2));
        }

        [Test]
        public void Aggregate_With_Index_Specifying_Seed_Passes_Correct_Index_To()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(string.Empty, (result, n, i) => $"{result}, {i}").TrimStart(',').TrimStart())
                .IsEqualTo(string.Join(", ", values.Select((n, i) => i)));
        }

        #endregion

        #region Generic Helpers

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