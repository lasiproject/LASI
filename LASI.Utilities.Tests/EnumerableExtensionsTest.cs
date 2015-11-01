using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using NFluent.Extensions;
using NFluent.Helpers;

namespace LASI.Utilities.Tests
{
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
        public void FormatUsesSquareBracketAndCommaByDefault()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2, 3 ]";
            var actual = target.Format();
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatAppliesSpecifiedStringSelector()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32, System.Int32, System.Int32, System.Int32 ]";
            var actual = target.Format(e => e.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatAppliesSpecifiedStringSelectorAndLineLength()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32,\nSystem.Int32,\nSystem.Int32,\nSystem.Int32 ]";
            var actual = target.Format(lineLength: 20, selector: x => x.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatUsesSpecifiedBracketingAndSeparator()
        {
            var target = Range(0, 4);
            var expected = "< 0; 1; 2; 3 >";
            var actual = target.Format(Tuple('<', ';', '>'));
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatTest4()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32; System.Int32; System.Int32; System.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), e => e.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatUsesSpecifiedBracketingAndLineLength()
        {
            var target = Range(0, 4).Select(x => x.GetType().FullName);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20);
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatUsesSpecifiedBracketingAndSeparatorAndStringSelectorAndLineLength()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20, x => x.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Test]
        public void FormatEnforcesSpecifiedLineLengthTestOne()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2,\n3 ]";
            var actual = target.Format(9);
            Check.That(actual).Equals(expected);

        }
        [Test]
        public void FormatEnforcesSpecifiedLineLengthTestTwo()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2,\n3 ]";
            var actual = target.Format(10);
            Check.That(actual).Equals(expected);
        }
        [Test]
        public void FormatEnforcesSpecifiedLineLengthTestThree()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1,\n2, 3 ]";
            var actual = target.Format(8);
            Check.That(actual).Equals(expected);
        }
        #endregion

        #region Additional Query Operators

        [Test]
        public void AppendInsertsValueAtEnd()
        {
            var values = new[] { 1, 2, 3 };
            Check.That(values.Append(0)).ContainsExactly(1, 2, 3, 0);
        }
        [Test]
        public void AppendInsertsActualValueAtEnd()
        {
            var elements = Range(0, 5).Select(x => new object());
            var toAppend = new object();
            elements.Append(toAppend).Should().EndWith(toAppend);
        }

        [Test]
        public void PrependInsertsValueAtStart()
        {
            var values = new[] { 1, 2, 3 };
            Check.That(values.Prepend(0)).ContainsExactly(0, 1, 2, 3);
        }

        [Test]
        public void AppendInsertsActualValueAtStart()
        {
            var elements = Range(0, 5).Select(x => new object());
            var toPrepend = new object();
            elements.Prepend(toPrepend).Should().StartWith(toPrepend);
        }

        [Test]
        public void PairWisePairsAdjacentElements()
        {
            var target = Range(12, 5).PairWise();
            Check.That(target).ContainsExactly(Pair(12, 13), Pair(13, 14), Pair(14, 15), Pair(15, 16));
        }

        [Test]
        public void MaxByYieldsValueWithMaxUnderProjectionTestOne()
        {
            var target = new[] { "apple", "chicken", "carrot" };
            Check.That(target.MaxBy(s => s.Length)).Equals("chicken");
        }

        [Test]
        public void MaxByYieldsValueWithMaxUnderProjectionTestTwo()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Check.That(target.MaxBy(s => s[0], Comparer<int>.Default)).Equals( "omega");
        }

        [Test]
        public void MaxByYieldsValueWithMinUnderProjectionTestOne()
        {
            var target = new[] { "carrot", "apple", "chicken" };
            Check.That(target.MinBy(s => s.Length)).Equals("apple");
        }

        [Test]
        public void MaxByYieldsValueWithMinUnderProjectionTestTwo()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Check.That(target.MinBy(s => s[0], Comparer<int>.Default)).Equals("alpha");
        }

        [Test]
        public void DistinctByUsesSetSemanticsUnderProjection()
        {
            var target = new[] { "beach", "parrot", "peach", "seventh" };
            var result = target.DistinctBy(x => x.Length);
            Check.That(result.Count()).Equals(3);
            Check.That(result.Select(x => x.Length).Except(new[] { 7, 5, 6 })).IsEmpty();
        }

        [Test]
        public void SetEqualUsesSetsemantics()
        {
            var values = new[] { 1, 2, 3 };
            Check.That(values.SetEqual(new[] { 3, 1, 2 })).IsTrue();
            Check.That(values.SetEqual(new[] { 3, 1, 2, 1 })).IsTrue();
            Check.That(values.SetEqual(new[] { 3, 2, 1, 2, 1, 3 })).IsTrue();
        }

        [Test]
        public void SetEqualUsesSetsemanticsUnderProjection()
        {
            var first = new[] { "carrot", "apple", "chicken" };
            Check.That(first.SetEqualBy(new[] { "beach", "parrot", "peach", "seventh" }, s => s.Length)).IsTrue();
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
            Check.That(result.Length).Equals(expected.Length);
            for (var i = 0; i < result.Length; ++i)
            {
                Check.That(result[i]).Equals(expected[i]);
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
        public void AggregateWithIndexMatchesAggregateWhenIndexIsUnusedByAggregator()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate((sum, n, index) => sum + n)).Equals(values.Aggregate((sum, n) => sum + n));
        }

        [Test]
        public void AggregateWithIndexMatchesAggregateWhenSeedIsSpecifiedAndIndexIsUnusedByAggregator()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(14, (sum, n, index) => sum + n)).Equals(values.Aggregate(14, (sum, n) => sum + n));
        }

        [Test]
        public void AggregateWithIndexPassesCorrectIndex()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate((sum, n, i) => sum + n + i)).Equals(values.Select((n, i) => n + i).Aggregate((sum, n) => sum + n));
        }

        [Test]
        public void AggregateWithIndexSpecifiesExpectedIndexAndAppliesResultSelector()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(1, (sum, n, i) => sum + n + i, r => r * 2)).Equals(values.Select((n, i) => n + i).Aggregate(1, (sum, n) => sum + n, r => r * 2));
        }

        [Test]
        public void AggregateWithIndexSpecifyingSeedPassesCorrectIndexTo()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(string.Empty, (result, n, i) => $"{result}, {i}").TrimStart(',').TrimStart()).Equals(string.Join(", ", values.Select((n, i) => i)));
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