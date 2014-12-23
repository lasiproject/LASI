using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI
{
    using LASI.Utilities;
    using Validator = Utilities.Contracts.Validators.ArgumentValidator;
    /// <summary>
    /// Defines various useful methods for working with IEnummerable sequences of any type.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Sequence String Formatting Methods

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source) {
            return source.Format(Tuple.Create('[', ',', ']'));
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, long lineLength) {
            return source.Format(Tuple.Create('[', ',', ']'), lineLength);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">The triple of delimiters specifying the beginning, separating, and ending characters.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters) {
            Validator.ThrowIfNull(source, "source", delimiters, "delimiters");
            return source.Aggregate(
                    new StringBuilder(delimiters.Item1 + " "),
                    (builder, e) => builder.Append(e.ToString() + delimiters.Item2 + ' '),
                    result => result.ToString().TrimEnd(' ', '\n', delimiters.Item2) + ' ' + delimiters.Item3);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ]
        /// such that the string representation of each element is produced by calling the provided selector function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="selector">The function used to produce a string representation for each element.</param>
        /// <returns>A a formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> selector) {
            return source.Format(Tuple.Create('[', ',', ']'), selector);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">The triple of delimiters specifying the beginning, separating, and ending characters.</param>
        /// <param name="selector">The function used to produce a string representation for each element.</param>
        /// <returns>formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, Func<T, string> selector) {
            Validator.ThrowIfNull(source, "source", selector, "selector", delimiters, "delimiters");
            return source.Select(selector).Format(delimiters);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">The triple of delimiters specifying the beginning, separating, and ending characters.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, long lineLength) {
            return source.Format(delimiters, lineLength, x => x.ToString());
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ]
        /// such that the string representation of each element is produced by calling the provided selector function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="selector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ].</returns>
        public static string Format<T>(this IEnumerable<T> source, long lineLength, Func<T, string> selector) {
            return source.Format(Tuple.Create('[', ',', ']'), lineLength, selector);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">The sequence containing 0 or more elements of type <typeparamref name="T"/> to format.</param>
        /// <param name="delimiters">The triple of delimiters specifying the beginning, separating, and ending characters.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="selector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        //public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, long lineLength, Func<T, string> selector) {
        //    Validator.ThrowIfNull(source, "source", delimiters, "delimiters", selector, "selector");
        //    Validator.ThrowIfLessThan(lineLength, "lineLength", 1, "Line length must be greater than 0.");
        //    int length = 2;
        //    return source
        //        .Select(e => selector(e) + delimiters.Item2)
        //        .Aggregate(
        //            new StringBuilder(delimiters.Item1),
        //            (builder, s) => {
        //                if (length + s.Length > lineLength) {
        //                    length = s.Length;
        //                    builder.Remove(builder.Length - 1, 1);
        //                    return builder.Append(' ').Append(s).Append('\n');
        //                } else {
        //                    length += s.Length + 1;
        //                    return builder.Append(' ').Append(s);
        //                }
        //                //length += s.Length + 2;
        //                //var breakBefore = length - 2 >= lineLength;
        //                //if (breakBefore) {
        //                //    length = s.Length + 2;
        //                //}
        //                //return (breakBefore ? builder.Append(s).Append(delimiters.Item2).Append('\n') : builder.Append(s).Append(delimiters.Item2).Append(' '));

        //            }, result => result.ToString().TrimEnd(' ', '\r', '\n', delimiters.Item2) + " " + delimiters.Item3);
        //}
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, long lineLength, Func<T, string> selector) {
            Validator.ThrowIfNull(source, "source", delimiters, "delimiters", selector, "selector");
            Validator.ThrowIfLessThan(lineLength, "lineLength", 1, "Line length must be greater than 0.");
            return source.Select(selector)
                .Aggregate(new
                {
                    Text = string.Empty,
                    Length = 0L
                }, (carry, e) => new
                {
                    Text = carry.Text + "\{e}\{delimiters.Item2}\{(carry.Length + e.Length) / lineLength > 0 ? '\n' : ' '}",
                    Length = carry.Length % lineLength
                },
                result => "\{delimiters.Item1} \{result.Text.TrimEnd(' ', '\n', delimiters.Item2)} \{delimiters.Item3}"
            );
        }
        
        #endregion

        #region Additional Query Operators

        /// <summary>
        /// Appends the given element to the sequence, yielding a new sequence consisting of the original sequence followed by the appended element.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="head">The sequence to which the element will be appended.</param>
        /// <param name="tail">The element to append to the sequence.</param>
        /// <returns>A new sequence consisting of the original sequence followed by the appended element..</returns>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> head, TSource tail) {
            Validator.ThrowIfNull(head, "head");
            foreach (var e in head) {
                yield return e;
            }
            yield return tail;
        }

        /// <summary>
        /// Returns a Tuple&lt;IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;&gt; representing the sequence bifurcated by the provided predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns>A Tuple&lt;IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;&gt; representing the sequence bifurcated by the provided predicate.</returns>
        public static Tuple<IEnumerable<T>, IEnumerable<T>> Bisect<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            var partitions = source.ToLookup(predicate);
            return Tuple.Create(partitions[true], partitions[false]);
        }

        /// <summary>
        /// Returns a Tuple&lt;TResult, TResult&gt; representing the sequence bifurcated by the provided predicate.
        /// A result selector function is used to project the two resulting sequences into a new form.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <typeparam name="TResult">The type of the items in the tupled result.</typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static Tuple<TResult, TResult> Bisect<T, TResult>(this IEnumerable<T> source, Func<T, bool> predicate, Func<IEnumerable<T>, TResult> resultSelector) {
            var partitions = source.Bisect(predicate);
            return Tuple.Create(resultSelector(partitions.Item1), resultSelector(partitions.Item2));
        }

        /// <summary>
        /// Returns the distinct elements of the given of the source sequence by applying the given key selector
        /// the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector which projects each element into a new form by which distinctness is determined.</param>        
        /// <returns>the distinct elements of the given of the source sequence by applying the given key selector
        /// the given projection.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            Validator.ThrowIfNull(source, "source", selector, "selector");
            return source.Distinct(
                new CustomComparer<TSource>(
                    (x, y) => selector(x).Equals(selector(y)),
                    x => selector(x).GetHashCode())
            );
        }

        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.Except(second,
                new CustomComparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
         
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.Intersect(second,
                new CustomComparer<TSource>(
                    (x, y) => selector(x).Equals(selector(y)),
                    x => selector(x).GetHashCode())
            );
        }

        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>        
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey> {
            return source.MaxBy(selector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>        
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) {
            Validator.ThrowIfNull(source, "source", selector, "selector");
            Validator.ThrowIfEmpty(source, "source");
            return source.OrderByDescending(selector, comparer).First();
        }

        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>        
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IComparable<TKey> {
            return source.MinBy(selector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the projected element.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="selector">Selector to use to pick the results to compare.</param>        
        /// <param name="comparer">Comparer to use to compare projected values.</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty.</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) {
            Validator.ThrowIfNull(source, "source", selector, "selector");
            Validator.ThrowIfEmpty(source, "source");
            return source.OrderBy(selector, comparer).First();
        }

        /// <summary>
        /// Determines whether no element of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param> 
        /// <returns>False if the source sequence contains any elements; otherwise, true.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source) => !source.Any();

        /// <summary>
        /// Determines whether no elements of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>False if any elements in the source sequence pass the test in the specified predicate; otherwise, true.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);

        /// <summary>
        /// Determines whether a parallel sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to check for emptiness.</param>
        /// <returns>False if the source sequence contains any elements; otherwise, true.</returns>
        public static bool None<T>(this ParallelQuery<T> source) => !source.Any();

        /// <summary>
        /// Determines in parallel whether no element of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>False if any elements in the source sequence pass the test in the specified predicate; otherwise, true.</returns>
        public static bool None<T>(this ParallelQuery<T> source, Func<T, bool> predicate) => !source.Any(predicate);

        /// <summary>
        /// A sequence of Tuple&lt;T, T,&gt; containing pairs of adjacent elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; from which to build a pairwise sequence.</param> 
        /// <returns>A sequence of Tuple&lt;T, T&gt; containing pairs of adjacent elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static IEnumerable<Tuple<T, T>> PairWise<T>(this IEnumerable<T> source) {
            Validator.ThrowIfNull(source, "source");
            Validator.ThrowIfEmpty(source, "source");
            var first = source.First();
            foreach (var next in source.Skip(1)) {
                yield return Tuple.Create(first, next);
                first = next;
            }
        }

        /// <summary>
        /// Prepends the given element to the sequence, yielding a new sequence consisting of the prepended element followed by each element in the original sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="tail">The sequence to which the element will be prepended.</param>
        /// <param name="head">The element to prepend to the sequence.</param>
        /// <returns>A new sequence consisting of the prepended element followed by each element in the original sequence.</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head) {
            Validator.ThrowIfNull(tail, "tail");
            yield return head;
            foreach (var i in tail) {
                yield return i;
            }
        }
        public static bool SequenceEqualBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.SequenceEqual(second,
                new CustomComparer<TSource>(
                    (x, y) => selector(x).Equals(selector(y)),
                    x => selector(x).GetHashCode()));
        }

        /// <summary>
        /// Determines if the source collection contains the exact same elements as the second, Ignoring duplicate elements and ordering.
        /// </summary>
        /// <typeparam name="T">Type of the source sequence</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>
        /// <returns> <c>true</c> if the given source sequence contain the same elements, irrespective or order and duplicate items, as the second sequence; otherwise, <c>false</c>.</returns>
        public static bool SetEqual<T>(this IEnumerable<T> first, IEnumerable<T> second) {
            return first.Except(second).None();
        }

        /// <summary>
        /// Determines if the source collection contains the exact same elements as the second using the specified IEqualityComparer&lt;TSource&gt;, Ignoring duplicate elements and ordering.
        /// </summary>
        /// <typeparam name="T">Type of the source sequence</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;TSource&gt; to compare values</param>
        /// <returns> <c>true</c> if the given source sequence contain the same elements, irrespective or order and duplicate items, as the second sequence; otherwise, <c>false</c>.</returns>
        public static bool SetEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer) {
            return first.Except(second).None();
        }

        /// <summary>
        /// Determines if the source collection contains the same elements as the second under the projection. Ignores duplicate elements and element ordering.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the source sequence</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>        
        /// <param name="selector">A function which extracts a key from each element by which equality is determined.</param>        
        /// <returns> <c>true</c> if the given source sequence contain the same elements, irrespective or order and duplicate items, as the second sequence; otherwise, <c>false</c>.</returns>
        public static bool SetEqualBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) {
            return first.Select(selector).SetEqual(second.Select(selector));
        }

        /// <summary>
        /// Splits the sequence into a sequence of sequences based on the provided chunk size.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to split into subsequences</param>
        /// <param name="chunkSize">The number of elements per subsequence</param>
        /// <returns>A sequence of sequences based on the provided chunk size.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize) {
            Validator.ThrowIfNull(source, "source");
            Validator.ThrowIfLessThan(chunkSize, "chunkSize", 1, "Value must be greater than 0.");
            var partsToCreate = source.Count() / chunkSize + source.Count() % chunkSize == 0 ? 0 : 1;
            return from partIndex in Enumerable.Range(0, partsToCreate)
                   select source.Skip(partIndex * chunkSize).Take(chunkSize);
        }

        /// <summary>
        /// Splits the sequence into a sequence of sequences delimited by the provided predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to split into subsequences</param>
        ///<param name="predicate">A predicate which returns true when an element will subdivide the source sequence.</param>
        /// <param name="discardDelimiter">True if delimiting elements should be discarded. The default is false.</param>
        /// <returns>A sequence of sequences based on the provided chunk size.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool discardDelimiter = false) {
            Validator.ThrowIfNull(source, "source", predicate, "predicate");
            var breakPoint = 0;
            var segment = source.TakeWhile((element, index) => {
                breakPoint = index;
                return index != 0 && predicate(element);
            });
            yield return segment.Skip(discardDelimiter ? 1 : 0);
            yield return source.Skip(breakPoint + (discardDelimiter ? 1 : 0)).Split(predicate, discardDelimiter).SelectMany(s => s);

        }

        /// <summary>
        /// Returns a HashSet representation of the given sequence using the default IEqualityComparer for the given element type.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <returns>A HashSet representation of the given sequence using the default System.Collections.Generic.IEqualityComparer for the given element type.</returns>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source) {
            return new HashSet<TSource>(source);
        }
        ///<summary> 
        /// Returns a HashSet representation of the given sequence using the specified IEqualityComparer to determine element uniqueness.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer implementation which will determine the distinctness of elements.</param>
        /// <returns>A HashSet representation of the given sequence using the default IEqualityComparer for the given element type.</returns>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer) {
            Validator.ThrowIfNull(comparer, "comparer");
            return new HashSet<TSource>(source, comparer);
        }
        /// <summary>
        /// Returns a HashSet representation of the given sequence using the specified IEqualityComparer to determine element uniqueness.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <param name="equals">A function Func&lt;T, T, bool&gt; to which will determine the distinctness of elements.</param>
        /// <param name="getHashCode">The function to extract a hash code from each element.</param>
        /// <returns>A HashSet representation of the given sequence using the default IEqualityComparer for the given element type.</returns>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> equals, Func<TSource, int> getHashCode) {
            Validator.ThrowIfNull(equals, "equals", getHashCode, "getHashCode");
            return new HashSet<TSource>(source, new CustomComparer<TSource>(equals, getHashCode));
        }
        public static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.Union(second,
                new CustomComparer<TSource>(
                    (x, y) => selector(x).Equals(selector(y)),
                    x => selector(x).GetHashCode()));
        }
        /// <summary>
        /// Merges three sequences by using the specified function to select elements.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
        /// <typeparam name="TThird">The type of the elements of the third input sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <param name="selector">A function that specifies how to merge the elements from the three sequences.</param>
        /// <returns>
        /// An System.Collections.Generic.IEnumerable&lt;TResult&gt; that contains merged elements from the three input sequences.
        /// </returns>
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(this IEnumerable<TFirst> first,
                IEnumerable<TSecond> second,
                IEnumerable<TThird> third,
                Func<TFirst, TSecond, TThird, TResult> selector) {
            return first
                .Zip(second, (a, b) => new { a, b })
                .Zip(third, (ab, c) => selector(ab.a, ab.b, c));
        }
        /// <summary>
        /// Merges four sequences by using the specified function to select elements.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of the first input sequence.</typeparam>
        /// <typeparam name="T2">The type of the elements of the second input sequence.</typeparam>
        /// <typeparam name="T3">The type of the elements of the third input sequence.</typeparam>
        /// <typeparam name="T4">The type of the elements of the fourth input sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <param name="fourth">The fourth sequence to merge.</param>
        /// <param name="selector">A function that specifies how to merge the elements from the four sequences.</param>
        /// <returns>
        /// An System.Collections.Generic.IEnumerable&lt;TResult&gt; that contains merged elements from the four input sequences.
        /// </returns>
        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, TResult>(
            this IEnumerable<T1> first,
                IEnumerable<T2> second,
                IEnumerable<T3> third,
                IEnumerable<T4> fourth,
                Func<T1, T2, T3, T4, TResult> selector) {
            return first
                .Zip(second, third, (a, b, c) => new { a, b, c })
                .Zip(fourth, (abc, d) => selector(abc.a, abc.b, abc.c, d));
        }

        #endregion

    }

}
