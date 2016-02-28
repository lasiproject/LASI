using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities
{
    using System.Numerics;
    using SpecializedResultTypes;
    using Validation;
    using static Enumerable;

    /// <summary>
    /// Defines various useful methods for working with IEnummerable sequences of any type.
    /// </summary>
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Applies an accumulator function over the sequence, incorporating each elements index
        /// into the calculation.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to aggregate over.</param>
        /// <param name="func">
        /// An accumulator function to be invoked on each element; the element's index, determined
        /// by enumeration order, is available as the third argument.
        /// </param>
        /// <returns>The final accumulator value.</returns>
        /// <exception cref="ArgumentNullException">Source or func is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Source contains no elements.</exception>
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func) =>
            source.Select(Indexed.Create)
                  .Aggregate((z, e) => Indexed.Create(
                      element: func(z.Element, e.Element, e.Index),
                     // this value is never used; it is simply present to make the result type align as required by the overload of Aggregate
                     index: e.Index
                  )).Element;

        /// <summary>
        /// Applies an accumulator function over the sequence, incorporating each element's index
        /// into the calculation. The specified seed value is used as the initial accumulator value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="source">
        /// An <see cref="System.Collections.Generic.IEnumerable{TSource}" /> to aggregate over.
        /// </param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">
        /// An accumulator function to be invoked on each element; the element's index, determined
        /// by enumeration order, is available as the third argument.
        /// </param>
        /// <returns>The final accumulator value.</returns>
        /// <exception cref="ArgumentNullException">Source or func is <c>null</c>.</exception>
        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, int, TAccumulate> func) =>
            source.WithIndices().Aggregate(seed, (z, e) => func(z, e.Element, e.Index));

        /// <summary>
        /// Applies an accumulator function over the sequence, incorporating each element's index
        /// into the calculation. The specified seed value is used as the initial accumulator value,
        /// and the specified function is used to select the result value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">
        /// An accumulator function to be invoked on each element; the element's index, determined
        /// by enumeration order, is available as the third argument.
        /// </param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.
        /// </param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <exception cref="ArgumentNullException">Source or func is <c>null</c>.</exception>
        public static TResult Aggregate<TSource, TAccumulate, TResult>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, int, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector) => resultSelector(source.Aggregate(seed, func));

        /// <summary>
        /// Enumerates a sequence from right to left, applying an accumulator function to each element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to aggregate over.</param>
        /// <param name="func">An accumulator function to be invoked on each element</param>
        /// <seealso cref="Enumerable.Aggregate{TSource}(IEnumerable{TSource}, Func{TSource, TSource, TSource})" />
        /// <returns>The final accumulator value.</returns>
        public static TSource AggregateRight<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func) => source.Reverse().Aggregate(func);

        /// <summary>
        /// Applies an accumulator function over the sequence, incorporating each elements index
        /// into the calculation.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to aggregate over.</param>
        /// <param name="func">
        /// An accumulator function to be invoked on each element; the element's index, determined
        /// by enumeration order, is available as the third argument.
        /// </param>
        /// <returns>The final accumulator value.</returns>
        /// <seealso cref="EnumerableExtensions.Aggregate{TSource}(IEnumerable{TSource}, Func{TSource, TSource, int, TSource})" />
        public static TSource AggregateRight<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func) => source.Reverse().Aggregate(func);

        /// <summary>
        /// Enumerates a sequence from right to left, applying an accumulator function which
        /// incorporating each element's index into the calculation. The specified seed value is
        /// used as the initial accumulator value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="source">
        /// An <see cref="System.Collections.Generic.IEnumerable{TSource}" /> to aggregate over.
        /// </param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">
        /// An accumulator function to be invoked on each element; the element's index, determined
        /// by enumeration order, is available as the third argument.
        /// </param>
        /// <returns>The final accumulator value.</returns>
        /// <exception cref="ArgumentNullException">Source or func is <c>null</c>.</exception>
        public static TAccumulate AggregateRight<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, int, TAccumulate> func) =>
            source.Reverse().Aggregate(seed, func);

        /// <summary>
        /// Enumerates a sequence from right to left, applying an accumulator function over a
        /// sequence, incorporating each element's index into the calculation. The specified seed
        /// value is used as the initial accumulator value, and the specified function is used to
        /// select the result value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">
        /// An accumulator function to be invoked on each element; the element's index, determined
        /// by enumeration order, is available as the third argument.
        /// </param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.
        /// </param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <exception cref="ArgumentNullException">Source or func is <c>null</c>.</exception>
        public static TResult AggregateRight<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, int, TAccumulate> func, Func<TAccumulate, TResult> resultSelector) =>
            resultSelector(source.AggregateRight(seed, func));

        /// <summary>
        /// Appends the given element to the sequence, yielding a new sequence consisting of the
        /// original sequence followed by the appended element.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="head">The sequence to which the element will be appended.</param>
        /// <param name="tail">The element to append to the sequence.</param>
        /// <returns>
        /// A new sequence consisting of the original sequence followed by the appended element..
        /// </returns>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> head, TSource tail)
        {
            Validate.NotNull(head, "head");
            foreach (var e in head)
            {
                yield return e;
            }
            yield return tail;
        }

        public static void ForEachGroup<T, TKey>(this IEnumerable<IGrouping<TKey, T>> groups, IDictionary<TKey, Action<IEnumerable<T>>> forGroups) where TKey : IComparable
        {
            var keyed = groups.ToDictionary(g => g.Key, g => g.AsEnumerable());
            foreach (var groupAction in forGroups)
            {
                var enumerable = keyed.GetValueOrDefault(groupAction.Key, Empty<T>);
                groupAction.Value(enumerable);
            }
        }
        public static void ForEachGroup<T, TKey>(this IEnumerable<IGrouping<TKey, T>> groups, IDictionary<TKey, Action<T>> forGrouped) where TKey : IComparable
        {
            var keyed = groups.ToDictionary(g => g.Key, g => g.AsEnumerable());
            foreach (var groupAction in forGrouped)
            {
                var enumerable = keyed.GetValueOrDefault(groupAction.Key, Empty<T>);
                foreach (var element in enumerable)
                {
                    groupAction.Value(element);
                }
            }
        }

        /// <summary>
        /// Returns the distinct elements of the given of the source sequence by applying the given
        /// key selector the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">
        /// Selector which projects each element into a new form by which distinctness is determined.
        /// </param>
        /// <returns>
        /// the distinct elements of the given of the source sequence by applying the given key
        /// selector the given projection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null
        /// </exception>
        /// <exception cref="InvalidOperationException"><paramref name="source" /> is empty</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            Validate.NotNull(source, "source", selector, "selector");
            return source.Distinct(
                Equality.Create<TSource>(
                    (x, y) => selector(x).Equals(selector(y)),
                    x => selector(x).GetHashCode())
            );
        }

        /// <summary>
        /// Transforms a possibly <c>null</c> <see cref="IEnumerable{T}" /> into an empty enumerable.
        /// Return an empty <see cref="IEnumerable{T}" /> if <paramref name="source" /> is
        /// <c>null</c>; otherwise <paramref name="source" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to transform.</param>
        /// <returns>
        /// An empty <see cref="IEnumerable{T}" /> if <paramref name="source" /> is <c>null</c>;
        /// otherwise <paramref name="source" />.
        /// </returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source) => source ?? Empty<T>();

        /// <summary>Produces the set difference of two sequences under the given projection.</summary>
        /// <typeparam name="TSource">The type of the elements in the two sequences.</typeparam>
        /// <typeparam name="TKey">The result type the of projection by which to compare elements.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="selector">The projection by which to compare elements.</param>
        /// <returns>
        /// A sequence that contains the set difference of the elements of two sequences under the
        /// given projection.
        /// </returns>
        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) =>
            first.Except(second, Equality.Create<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode())
            );
        /// <summary>Produces the set difference of two sequences under the given projections.</summary>
        /// <typeparam name="TSource">The type of the elements in the first sequence.</typeparam>
        /// <typeparam name="TOther">The type of the elements in the second sequence.</typeparam>
        /// <typeparam name="TKey">The result type of the projection by which to compare elements.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="keySelector">The key selector to project over the first sequence.</param>
        /// <param name="otherKeySelector">The key selector to project over the second sequence.</param>
        /// <returns>
        /// A sequence that contains the set difference of the elements of two sequences under the
        /// given projection.
        /// </returns>
        public static IEnumerable<TKey> ExceptBy<TSource, TOther, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TOther> second,
            Func<TSource, TKey> keySelector,
            Func<TOther, TKey> otherKeySelector
        ) => first.Select(keySelector).Except(second.Select(otherKeySelector));

        /// <summary>Produces the set intersection of two sequences under the given projection.</summary>
        /// <typeparam name="TSource">The type of the elements in the two sequences.</typeparam>
        /// <typeparam name="TKey">The result type of projection by which to compare elements.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="selector">The projection by which to compare elements.</param>
        /// <returns>
        /// A sequence that contains the set intersection of the elements of two sequences under the
        /// given projection.
        /// </returns>
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) =>
            first.Intersect(second, Equality.Create<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode())
            );

        /// <summary>Returns the maximal element of the given sequence, based on the given projection.</summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null
        /// </exception>
        /// <exception cref="InvalidOperationException"><paramref name="source" /> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
            where TKey : IComparable<TKey> => source.MaxBy(selector, Comparer<TKey>.Default);

        /// <summary>Returns the maximal element of the given sequence, based on the given projection.</summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null
        /// </exception>
        /// <exception cref="InvalidOperationException"><paramref name="source" /> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) where TKey : IComparable<TKey>
        {
            Validate.NotNull(source, "source", selector, "selector");
            Validate.NotEmpty(source, "source");
            return source.OrderByDescending(selector, comparer).First();
        }

        /// <summary>Returns the minimal element of the given sequence, based on the given projection.</summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null
        /// </exception>
        /// <exception cref="InvalidOperationException"><paramref name="source" /> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey> => source.MinBy(selector, Comparer<TKey>.Default);

        /// <summary>Returns the minimal element of the given sequence, based on the given projection.</summary>
        /// <typeparam name="TSource">Type of the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the projected element.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="selector">Selector to use to pick the results to compare.</param>
        /// <param name="comparer">Comparer to use to compare projected values.</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException"><paramref name="source" /> is empty.</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) where TKey : IComparable<TKey>
        {
            Validate.NotNull(source, "source", selector, "selector");
            Validate.NotEmpty(source, "source");
            return source.OrderBy(selector, comparer).First();
        }

        /// <summary>A sequence of Tuple&lt;T, T,&gt; containing pairs of adjacent elements.</summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">
        /// An System.Collections.Generic.IEnumerable&lt;T&gt; from which to build a pairwise sequence.
        /// </param>
        /// <returns>A sequence of Tuple&lt;T, T&gt; containing pairs of adjacent elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source" /> has exactly one element.</exception>
        public static IEnumerable<Pair<T, T>> PairWise<T>(this IEnumerable<T> source)
        {
            Validate.NotNull(source, nameof(source));
            if (source.Count() == 1) { throw new InvalidOperationException("If source is not empty, it must have have more than 1 element."); }
            var first = source.First();
            foreach (var next in source.Skip(1))
            {
                yield return Pair.Create(first, next);
                first = next;
            }
        }

        /// <summary>
        /// Prepends the given element to the sequence, yielding a new sequence consisting of the
        /// prepended element followed by each element in the original sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="tail">The sequence to which the element will be prepended.</param>
        /// <param name="head">The element to prepend to the sequence.</param>
        /// <returns>
        /// A new sequence consisting of the prepended element followed by each element in the
        /// original sequence.
        /// </returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
        {
            Validate.NotNull(tail, "tail");
            yield return head;
            foreach (var i in tail)
            {
                yield return i;
            }
        }

        /// <summary>
        /// Determines whether two sequences are equal by comparing their elements under the given projection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
        /// <typeparam name="TKey">The type of the projected elements.</typeparam>
        /// <param name="first">An <see cref="IEnumerable{T}" /> to compare to <paramref name="second" />.</param>
        /// <param name="second">
        /// An <see cref="IEnumerable{T}" /> to compare to the first sequence <paramref name="second" />.
        /// </param>
        /// <param name="selector">
        /// A function to project the elements of the two sequences for comparison.
        /// </param>
        /// <returns>
        /// true if the two source sequences are of equal length and their corresponding elements
        /// compare equal under the given projection; otherwise, false.
        /// </returns>
        public static bool SequenceEqualBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) =>
            first.SequenceEqual(second, Equality.Create<TSource>((x, y) => selector(x).Equals(selector(y)), e => selector(e).GetHashCode()));

        #region Statistical

        /// <summary>
        /// Calculates the percentage of values in the sequence which match the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of values to representing all elements in question.</param>
        /// <param name="predicate">The predicate used to delineate elements.</param>
        /// <returns>The percentage of values in the sequence which match the specified predicate.</returns>
        public static double PercentWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
            source.Aggregate(new { Length = 0, Matched = 0 },
                    (s, e) => new { Length = s.Length + 1, Matched = s.Matched + (predicate(e) ? 1 : 0) },
                    tally => (double)tally.Matched / tally.Length) * 100;

        /// <summary>Calculates the percentage of true values in the collection of Boolean values.</summary>
        /// <param name="delineated">
        /// The collection of boolean values to for which to calculate the percent that are <c>== true</c>.
        /// </param>
        /// <returns>The percentage of true values in the collection of Boolean values.</returns>
        public static double PercentTrue(this IEnumerable<bool> delineated) => delineated.PercentWhere(v => v);

        #endregion Statistical

        #region Product

        /// <summary>Calculates the Product of a sequence of <see cref="Complex" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static Complex Product(this IEnumerable<Complex> source) => source.Aggregate(Complex.One, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="BigInteger" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static BigInteger Product(this IEnumerable<BigInteger> source) => source.Aggregate(BigInteger.One, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="long" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static long Product(this IEnumerable<long> source) => source.Aggregate(1L, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="int" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static int Product(this IEnumerable<int> source) => source.Aggregate(1, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="decimal" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static decimal Product(this IEnumerable<decimal> source) => source.Aggregate(1M, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="double" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static double Product(this IEnumerable<double> source) => source.Aggregate(1D, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="float" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>The product of all values in the source sequence.</returns>
        public static float Product(this IEnumerable<float> source) => source.Aggregate(1F, (z, y) => z * y);

        /// <summary>Calculates the Product of a sequence of <see cref="bool" /> values.</summary>
        /// <param name="source">The sequence of elements to test.</param>
        /// <returns>
        /// <c>true</c> if all values in the source sequence are equal to <c>true</c>; otherwise <c>false</c>.
        /// </returns>
        public static bool All(this IEnumerable<bool> source) => source.All(b => b);

        #endregion Product
        /// <summary> Applies an accumulator over the sequence yielding each intermediate value.
        /// </summary> 
        /// <typeparam name="T">
        /// The type of the elements in the sequence.
        /// </typeparam>
        /// <param name="source">A sequence to scan.</param>  
        /// <param name="func">A function to invoke on each element in source and the
        /// accumulator value.</param>
        ///  <returns>The sequence starting with the first element in <paramref name="source"/> and ending with the final accumulation.</returns>
        public static IEnumerable<T> Scan<T>(this IEnumerable<T> source, Func<T, T, T> func)
        {
            Validate.NotNull(source, nameof(source), func, nameof(func));
            var accumulated = source.First();
            yield return accumulated;
            foreach (var e in source.Skip(1))
            {
                yield return accumulated = func(accumulated, e);
            }
        }

        /// <summary> Applies an accumulator over the sequence yielding each intermediate value. The
        /// resulting sequence begins with the specified seed and ends with the final aggregate.
        /// </summary> 
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam> 
        /// <param name="source">A sequence to scan.</param> <param name="seed">A an initial seed
        /// value.</param>
        /// <param name="func">A function to invoke on each element in source and the
        /// accumulator value.
        /// </param> 
        /// <returns>The sequence starting with <paramref name="seed"/> and ending with the final accumulation.</returns>
        public static IEnumerable<TAccumulate> Scan<T, TAccumulate>(this IEnumerable<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
        {
            Validate.NotNull(source, nameof(source), func, nameof(func));
            yield return seed;
            var accumulated = seed;
            foreach (var e in source)
            {
                yield return accumulated = func(accumulated, e);
            }
        }

        /// <summary>
        /// Determines if the source collection contains the exact same elements as the second,
        /// Ignoring duplicate elements and ordering.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>
        /// <returns>
        /// <c>true</c> if the given source sequence contain the same elements, irrespective or
        /// order and duplicate items, as the second sequence; otherwise, <c>false</c>.
        /// </returns>
        public static bool SetEqual<T>(this IEnumerable<T> first, IEnumerable<T> second) => first.SetEqual(second, EqualityComparer<T>.Default);

        /// <summary>
        /// Determines if the source collection contains the exact same elements as the second using
        /// the specified IEqualityComparer&lt;TSource&gt;, Ignoring duplicate elements and ordering.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>
        /// <param name="comparer">
        /// An System.Collections.Generic.IEqualityComparer&lt;TSource&gt; to compare values
        /// </param>
        /// <returns>
        /// <c>true</c> if the given source sequence contain the same elements, irrespective or
        /// order and duplicate items, as the second sequence; otherwise, <c>false</c>.
        /// </returns>
        public static bool SetEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer) =>
            !first.Except(second).Any() && !second.Except(first).Any();

        /// <summary>
        /// Determines if the source collection contains the same elements as the second under the
        /// projection. Ignores duplicate elements and element ordering.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the source sequence</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>
        /// <param name="selector">
        /// A function which extracts a key from each element by which equality is determined.
        /// </param>
        /// <returns>
        /// <c>true</c> if the given source sequence contain the same elements, irrespective or
        /// order and duplicate items, as the second sequence; otherwise, <c>false</c>.
        /// </returns>
        public static bool SetEqualBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) =>
            first.Select(selector).SetEqual(second.Select(selector));
        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.Dictionary{Key,Value}"/> from the IEnumerable&lt;<see cref="KeyValuePair{TKey,TValue}"/>&gt;.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="source">The IEnumerable&lt;<see cref="KeyValuePair{TKey,TValue}"/>&gt; from which to construct the dictionary.</param>
        /// <returns>A dictionary comprised of the contents of the IEnumerable&lt;<see cref="KeyValuePair{TKey,TValue}"/>&gt;.</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) => source.ToDictionary(e => e.Key, e => e.Value);

        /// <summary>Produces the set union of two sequences under the given projection.</summary>
        /// <typeparam name="TSource">The type of the elements in the two sequences.</typeparam>
        /// <typeparam name="TKey">The result type of projection by which to compare elements.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="selector">The projection by which to compare elements.</param>
        /// <returns>
        /// A sequence that contains the set union of the elements of two sequences under the given projection.
        /// </returns>
        public static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) =>
            first.Union(second, Equality.Create<TSource>((x, y) => selector(x).Equals(selector(y)), x => selector(x).GetHashCode()));

        /// <summary>Merges three sequences by using the specified function to select elements.</summary>
        /// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
        /// <typeparam name="TThird">The type of the elements of the third input sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <param name="selector">
        /// A function that specifies how to merge the elements from the three sequences.
        /// </param>
        /// <returns>
        /// An System.Collections.Generic.IEnumerable&lt;TResult&gt; that contains merged elements
        /// from the three input sequences.
        /// </returns>
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(this IEnumerable<TFirst> first,
                IEnumerable<TSecond> second,
                IEnumerable<TThird> third,
                Func<TFirst, TSecond, TThird, TResult> selector) =>
            first.Zip(second, (a, b) => new { a, b }).Zip(third, (ab, c) => selector(ab.a, ab.b, c));

        /// <summary>Merges four sequences by using the specified function to select elements.</summary>
        /// <typeparam name="T1">The type of the elements of the first input sequence.</typeparam>
        /// <typeparam name="T2">The type of the elements of the second input sequence.</typeparam>
        /// <typeparam name="T3">The type of the elements of the third input sequence.</typeparam>
        /// <typeparam name="T4">The type of the elements of the fourth input sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <param name="fourth">The fourth sequence to merge.</param>
        /// <param name="selector">
        /// A function that specifies how to merge the elements from the four sequences.
        /// </param>
        /// <returns>
        /// An System.Collections.Generic.IEnumerable&lt;TResult&gt; that contains merged elements
        /// from the four input sequences.
        /// </returns>
        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, TResult>(
            this IEnumerable<T1> first,
                IEnumerable<T2> second,
                IEnumerable<T3> third,
                IEnumerable<T4> fourth,
                Func<T1, T2, T3, T4, TResult> selector) => first
                .Zip(second, third, (a, b, c) => new { a, b, c })
                .Zip(fourth, (abc, d) => selector(abc.a, abc.b, abc.c, d));

        public static IEnumerable<ZipHelper.ZipResult<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second) =>
            first.Zip(second, (x, y) => ZipHelper.Create(x, y));

        public static IEnumerable<TResult> With<T1, T2, TResult>(this IEnumerable<ZipHelper.ZipResult<T1, T2>> zipped, Func<T1, T2, TResult> selector) =>
            zipped.Select(x => ZipHelper.Apply(x, selector));

        /// <summary>
        /// Projects each element of the sequence into a new form incorporating its index.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence of values to project.</param>
        /// <returns>
        /// A sequence which pair each element of the source sequence with its index in that sequence.
        /// </returns>
        public static IEnumerable<Indexed<T>> WithIndices<T>(this IEnumerable<T> source) => source.Select(Indexed.Create);

        /// <summary>
        /// Invokes each action in the IEnumerable&lt;<see cref="Action"/>&gt;.
        /// </summary>
        /// <param name="actions">The sequence of actions to invoke.</param>
        public static void InvokeAll(this IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                action();
            }
        }

        public static class ZipHelper
        {
            internal static ZipResult<T1, T2> Create<T1, T2>(T1 x, T2 y) => new ZipResult<T1, T2>
            {
                First = x,
                Second = y
            };

            public class ZipResult<T1, T2>
            {
                internal T1 First { get; set; }
                internal T2 Second { get; set; }

            }
            public static TResult Apply<T1, T2, TResult>(ZipResult<T1, T2> zipResult, Func<T1, T2, TResult> f) => f(zipResult.First, zipResult.Second);

        }
    }
}