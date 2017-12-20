using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;

namespace LASI.Utilities.Tests
{
    using static Enumerable;
    using Fact = Xunit.FactAttribute;
    public class EnumerableExtensionsTest
    {
        #region Sequence String Formatting Methods

        [Fact]
        public void FormatUsesSquareBracketAndCommaByDefault()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2, 3 ]";
            var actual = target.Format();
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatAppliesSpecifiedStringSelector()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32, System.Int32, System.Int32, System.Int32 ]";
            var actual = target.Format(e => e.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatAppliesSpecifiedStringSelectorAndLineLength()
        {
            var target = Range(0, 4);
            var expected = "[ System.Int32,\nSystem.Int32,\nSystem.Int32,\nSystem.Int32 ]";
            var actual = target.Format(lineLength: 20, selector: x => x.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatUsesSpecifiedBracketingAndSeparator()
        {
            var target = Range(0, 4);
            var expected = "< 0; 1; 2; 3 >";
            var actual = target.Format(Tuple('<', ';', '>'));
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatTest4()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32; System.Int32; System.Int32; System.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), e => e.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatUsesSpecifiedBracketingAndLineLength()
        {
            var target = Range(0, 4).Select(x => x.GetType().FullName);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20);
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatUsesSpecifiedBracketingAndSeparatorAndStringSelectorAndLineLength()
        {
            var target = Range(0, 4);
            var expected = "{ System.Int32;\nSystem.Int32;\nSystem.Int32;\nSystem.Int32 }";
            var actual = target.Format(Tuple('{', ';', '}'), 20, x => x.GetType().FullName);
            Check.That(actual).Equals(expected);
        }

        [Fact]
        public void FormatEnforcesSpecifiedLineLengthTestOne()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2,\n3 ]";
            var actual = target.Format(9);
            Check.That(actual).Equals(expected);
        }
        [Fact]
        public void FormatEnforcesSpecifiedLineLengthTestTwo()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1, 2,\n3 ]";
            var actual = target.Format(10);
            Check.That(actual).Equals(expected);
        }
        [Fact]
        public void FormatEnforcesSpecifiedLineLengthTestThree()
        {
            var target = Range(0, 4);
            var expected = "[ 0, 1,\n2, 3 ]";
            var actual = target.Format(8);
            Check.That(actual).Equals(expected);
        }
        #endregion

        #region Additional Query Operators

        [Fact]
        public void PairWisePairsAdjacentElements()
        {
            var target = Range(12, 5).PairWise();
            Check.That(target).ContainsExactly((12, 13), (13, 14), (14, 15), (15, 16));
        }

        [Fact]
        public void MaxByYieldsValueWithMaxUnderProjectionTestOne()
        {
            var target = new[] { "apple", "chicken", "carrot" };
            Check.That(target.MaxBy(s => s.Length)).Equals("chicken");
        }

        [Fact]
        public void MaxByYieldsValueWithMaxUnderProjectionTestTwo()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Check.That(target.MaxBy(s => s[0], Comparer<int>.Default)).Equals("omega");
        }

        [Fact]
        public void MaxByYieldsValueWithMinUnderProjectionTestOne()
        {
            var target = new[] { "carrot", "apple", "chicken" };
            Check.That(target.MinBy(s => s.Length)).Equals("apple");
        }

        [Fact]
        public void MaxByYieldsValueWithMinUnderProjectionTestTwo()
        {
            var target = new[] { "alpha", "omega", "gamma" };
            Check.That(target.MinBy(s => s[0], Comparer<int>.Default)).Equals("alpha");
        }

        [Fact]
        public void DistinctByUsesSetSemanticsUnderProjection()
        {
            var target = new[] { "beach", "parrot", "peach", "seventh" };
            var result = target.DistinctBy(x => x.Length);
            Check.That(result.Count()).Equals(3);
            Check.That(result.Select(x => x.Length).Except(new[] { 7, 5, 6 })).IsEmpty();
        }

        [Fact]
        public void SetEqualUsesSetsemantics()
        {
            var expectedToBeSetEqual = new[] { 1, 2, 3 };

            Check.That(expectedToBeSetEqual.SetEqual(new[] { 3, 1, 2 })).IsTrue();
            Check.That(expectedToBeSetEqual.SetEqual(new[] { 3, 1, 2, 1 })).IsTrue();
            Check.That(expectedToBeSetEqual.SetEqual(new[] { 3, 2, 1, 2, 1, 3 })).IsTrue();
        }

        [Fact]
        public void SetEqualRequiresSymmetry()
        {
            var expectedToNotBeSetEqual = new[] { 1, 2, 3, 5 };

            Check.That(expectedToNotBeSetEqual.SetEqual(new[] { 3, 1, 2 })).IsFalse();
            Check.That(expectedToNotBeSetEqual.SetEqual(new[] { 3, 1, 2, 1 })).IsFalse();
            Check.That(expectedToNotBeSetEqual.SetEqual(new[] { 3, 2, 1, 2, 1, 3 })).IsFalse();
        }

        [Fact]
        public void SetEqualUsesSetsemanticsUnderProjection()
        {
            var first = new[] { "carrot", "apple", "chicken" };
            Check.That(first.SetEqualBy(new[] { "beach", "parrot", "peach", "seventh" }, s => s.Length)).IsTrue();
        }

        [Fact]
        public void ZipWithHasSameBehaviorAsZip()
        {
            var first = Range(1, 10);
            var second = Range(10, 20);

            var expected = first.Zip(second, selector);
            var zipWithed = first.Zip(second).With(selector);

            Check.That(expected).ContainsExactly(zipWithed);

            int selector(int x, int y) => x + y * y;
        }

        [Fact]
        public void Zip3Test()
        {
            Zip3TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25 },
                selector: (x, y, z) => x + y + z,
                expected: new[] { 14, 31, 36 });
            Zip3TestHelper(
                first: new[] { 2, 4, 6, 5 },
                second: new[] { 1, 3, 5, },
                third: new[] { 11, 24, 25 },
                selector: (x, y, z) => x + y + z,
                expected: new[] { 14, 31, 36 });
            Zip3TestHelper(
                first: new[] { 2, 4, 6, 5 },
                second: new[] { 1, 3, 5, 5 },
                third: new[] { 11, 24, 25 },
                selector: (x, y, z) => x + y + z,
                expected: new[] { 14, 31, 36 });
            Zip3TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5, 5 },
                third: new[] { 11, 24, 25, 5 },
                selector: (x, y, z) => x + y + z,
                expected: new[] { 14, 31, 36 });
            Zip3TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25, 5 },
                selector: (x, y, z) => x + y + z,
                expected: new[] { 14, 31, 36 });
        }

        private static void Zip3TestHelper<T1, T2, T3, TResult>(T1[] first, T2[] second, T3[] third, Func<T1, T2, T3, TResult> selector, TResult[] expected)
        {
            var result = first.Zip(second, third, selector).ToArray();
            Check.That(result).ContainsExactly(expected);
        }

        [Fact]
        public void Zip4Test()
        {
            Zip4TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6, 1 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5, 1 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25, 1 },
                fourth: new[] { 14, 31, 36 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36, 1 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6, 1 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36, 1 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5, 1 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36, 1 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6 },
                second: new[] { 1, 3, 5 },
                third: new[] { 11, 24, 25, 1 },
                fourth: new[] { 14, 31, 36, 1 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
            Zip4TestHelper(
                first: new[] { 2, 4, 6, 1 },
                second: new[] { 1, 3, 5, 1 },
                third: new[] { 11, 24, 25 },
                fourth: new[] { 14, 31, 36, 1 },
                selector: (a, b, c, d) => a + b + c + d,
                expected: new[] { 28, 62, 72 });
        }

        [Fact]
        public void AggregateWithIndexMatchesAggregateWhenIndexIsUnusedByAggregator()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate((sum, n, index) => sum + n)).Equals(values.Aggregate((sum, n) => sum + n));
        }

        [Fact]
        public void AggregateWithIndexMatchesAggregateWhenSeedIsSpecifiedAndIndexIsUnusedByAggregator()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(14, (sum, n, index) => sum + n)).Equals(values.Aggregate(14, (sum, n) => sum + n));
        }

        [Fact]
        public void AggregateWithIndexPassesCorrectIndex()
        {
            var values = Range(1, 10);
            var expected = values.Select((n, i) => n + i).Aggregate((sum, n) => sum + n);
            var results = values.Aggregate((sum, n, i) => sum + n + i);
            Check.That(results).IsEqualTo(expected);
        }

        [Fact]
        public void AggregateWithIndexSpecifiesExpectedIndexAndAppliesResultSelector()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(1, (sum, n, i) => sum + n + i, r => r * 2)).IsEqualTo(values.Select((n, i) => n + i).Aggregate(1, (sum, n) => sum + n, r => r * 2));
        }

        [Fact]
        public void AggregateWithIndexSpecifyingSeedPassesCorrectIndexTo()
        {
            var values = Range(1, 10);
            Check.That(values.Aggregate(string.Empty, (result, n, i) => $"{result}, {i}").TrimStart(',').TrimStart()).IsEqualTo(string.Join(", ", values.Select((n, i) => i)));
        }

        [Fact]
        public void WithIndicesUsesZeroBasedIndicies()
        {
            var indices = Range(10, 20).WithIndices().Select(e => e.index);
            Check.That(indices.First()).IsEqualTo(0);
        }

        [Fact]
        public void WithIndicesStepsUsesStepOfOne()
        {
            var indices = Range(10, 20).WithIndices().Select(e => e.index).ToList();
            Check.That(indices).ContainsExactly(Range(0, indices.Count));
        }

        #endregion

        #region Generic Helpers

        private static void Zip4TestHelper<T1, T2, T3, T4, TResult>(T1[] first, T2[] second, T3[] third, T4[] fourth, Func<T1, T2, T3, T4, TResult> selector, TResult[] expected)
        {
            var result = first.Zip(second, third, fourth, selector);
            Check.That(result).ContainsExactly(expected);
        }

        #endregion

        #region Helper Factories

        private static Tuple<T1, T2, T3> Tuple<T1, T2, T3>(T1 x, T2 y, T3 z) => System.Tuple.Create(x, y, z);

        #endregion
    }
}
