using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI
{
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
            if (lineLength < 1)
                throw new ArgumentOutOfRangeException("lineLength", lineLength, "Line length must be greater than 0.");
            return source.Format(Tuple.Create('[', ',', ']'), lineLength);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">A value indicating the pair of delimiters to surround the elements.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters) {
            return source.Aggregate(new StringBuilder(delimiters.Item1 + " "), (sum, current) => sum.Append(current.ToString() + delimiters.Item2 + ' ')).ToString().TrimEnd(' ', delimiters.Item2) + ' ' + delimiters.Item3;
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
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");
            return source.Aggregate(new StringBuilder("[ "), (sum, current) => sum.Append(selector(current) + ", ")).ToString().TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="selector">The function used to produce a string representation for each element.</param>
        /// <returns>formated string representation of the IEnumerable sequence with the pattern: [ selector(element0), selector(element1), ..., selector(elementN) ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, Func<T, string> selector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");
            return source.Select(selector).Format(delimiters);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">A value indicating the pair of delimiters to surround the elements.</param>
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
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimiters">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="selector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, long lineLength, Func<T, string> selector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (delimiters == null)
                throw new ArgumentNullException("delimiters");
            if (lineLength < 1)
                throw new ArgumentOutOfRangeException("lineLength", lineLength, "Line length must be greater than 0.");
            if (selector == null)
                throw new ArgumentNullException("selector");
            int len = 2;
            return source.Aggregate(new StringBuilder(delimiters.Item1.ToString()).Append(' '),
                    (accumulator, e) => {
                        var cETS = selector(e) + delimiters.Item2 + " ";
                        len += cETS.Length;
                        if (len >= lineLength) {
                            len = cETS.Length;
                            accumulator.Append('\n');
                            len = 0;
                        }
                        return accumulator.Append(cETS);
                    }).ToString().TrimEnd(' ', delimiters.Item2) + " " + delimiters.Item3;
        }


        #endregion

        #region Generator Methods
        /// <summary>
        /// Generates a sequence of integral numbers within the specified range.
        /// </summary>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="count">The number of sequential integers to generate.</param>
        /// <returns>
        /// A lazily evaluated sequence of integral numbers. 
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// to is less than 0.-or-start + count -1 is larger than System.Int32.MaxValue."
        ///</exception>
        public static IEnumerable<int> Until(this int start, int count) {
            if (start - count < 0) { throw new ArgumentOutOfRangeException("to", start - count, "Cannot generate a sequence of fewer than 0 values."); }
            return Enumerable.Range(start, count);
        }

        #endregion

        #region Additional Query Operators

        /// <summary>
        /// Determines whether no elements of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>False if any elements in the source sequence pass the test in the specified predicate; otherwise, true.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
            return !source.Any(predicate);
        }
        /// <summary>
        /// Determines whether no element of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param> 
        /// <returns>False if the source sequence contains any elements; otherwise, true.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source) {
            return !source.Any();
        }
        /// <summary>
        /// Determines in parallel whether no element of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>False if any elements in the source sequence pass the test in the specified predicate; otherwise, true.</returns>
        public static bool None<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate) {
            return !source.Any(predicate);
        }
        /// <summary>
        /// Determines whether a parallel sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to check for emptiness.</param>
        /// <returns>False if the source sequence contains any elements; otherwise, true.</returns>
        public static bool None<T>(this ParallelQuery<T> source) {
            return !source.Any();
        }
        /// <summary>
        /// Appends the given element to the sequence, yielding a new sequence consiting of the original sequence followed by the appended element.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="head">The sequence to which the element will be appended.</param>
        /// <param name="tail">The element to append to the sequence.</param>
        /// <returns>A new sequence consiting of the original sequence followed by the appended element..</returns>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> head, TSource tail) {
            if (head == null)
                throw new ArgumentNullException("head");
            foreach (var i in head) { yield return i; }
            yield return tail;
        }
        /// <summary>
        /// Prepends the given element to the sequence, yielding a new sequence consiting of the prepended element followed by each element in the original sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="tail">The sequence to which the element will be prepended.</param>
        /// <param name="head">The element to prepend to the sequence.</param>
        /// <returns>A new sequence consiting of the prepended element followed by each element in the original sequence.</returns>
        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> tail, TSource head) {
            if (tail == null)
                throw new ArgumentNullException("tail");
            yield return head;
            foreach (var i in tail) { yield return i; }
        }
        /// <summary>
        /// Returns a HashSet representation of the given sequence using the default IEqualityComparer for the given element type.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <returns>A HashSet representation of the given sequence using the default System.Collections.Generic.IEqualityComparer for the given element type.</returns>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source) {
            if (source == null)
                throw new ArgumentNullException("source");
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
            if (source == null)
                throw new ArgumentNullException("source");
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
            if (source == null)
                throw new ArgumentNullException("source");
            return new HashSet<TSource>(source, new CustomComparer<TSource>(equals, getHashCode));
        }


        /// <summary>
        /// Splits the sequence into a sequence of sequences based on the provided chunk size.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to split into subsequences</param>
        /// <param name="chunkSize">The number of elements per subsquence</param>
        /// <returns>A sequence of sequences based on the provided chunk size.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (chunkSize < 1)
                throw new ArgumentOutOfRangeException("chunkSize", chunkSize, "Value must be greater than 0.");
            var partsToCreate = source.Count() / chunkSize + source.Count() % chunkSize == 0 ? 0 : 1;
            return from partIndex in Enumerable.Range(0, partsToCreate)
                   select source.Skip(partIndex * chunkSize).Take(chunkSize);
        }
        /// <summary>
        /// Splits the sequence into a sequence of sequences delimited by the provided predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to split into subsequences</param>
        ///<param name="predicate">A predicate which returns true when an element will subdevide the source sequence.</param>
        /// <param name="discardDelimiter">True if delimiting elements should be discarded. The default is false.</param>
        /// <returns>A sequence of sequences based on the provided chunk size.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool discardDelimiter = false) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            var breakPoint = 0;

            var segment = source.TakeWhile((element, index) => {
                breakPoint = index;
                return index != 0 && predicate(element);
            });
            yield return segment.Skip(discardDelimiter ? 1 : 0);
            yield return source.Skip(breakPoint + (discardDelimiter ? 1 : 0)).Split(predicate, discardDelimiter).SelectMany(s => s);

        }

        /// <summary>
        /// A sequence of Tuple&lt;T, T,&gt; containing pairs of adjacent elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; from which to build a pairwise sequence.</param> 
        /// <returns>A sequence of Tuple&lt;T, T&gt; containing pairs of adjacent elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static IEnumerable<Tuple<T, T>> PairWise<T>(this IEnumerable<T> source) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (source.None())
                throw new ArgumentException("Sequence contains no elements", "source");
            T first = source.First();
            foreach (var element in source.Skip(1)) {
                yield return Tuple.Create(first, element);
                first = element;
            }
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
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
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
            if (source == null)
                throw new ArgumentNullException("source");
            if (source.None())
                throw new InvalidCastException("Sequence Contains no elements");
            if (selector == null)
                throw new ArgumentNullException("selector");
            return MinMaxImplementation<TSource, TKey>(source, selector, MinMax.Max);
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
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
            return source.MinBy(selector, Comparer<TKey>.Default);
        }
        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>        
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (source.None()) { throw new InvalidCastException("Sequence Contains no elements"); }
            if (selector == null) { throw new ArgumentNullException("selector"); }
            return MinMaxImplementation<TSource, TKey>(source, selector, MinMax.Min);
        }
        private static TSource MinMaxImplementation<TSource, TMax>(IEnumerable<TSource> source, Func<TSource, TMax> selector, MinMax minmax) {
            var orderedByProjection =
                from e in source
                select new { Element = e, Value = selector(e) } into withMax
                orderby withMax.Value descending
                select withMax.Element;

            return minmax == MinMax.Max ? orderedByProjection.First() : orderedByProjection.Last();
        }
        private enum MinMax { Min, Max }


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
            if (source == null) { throw new ArgumentNullException("source"); }
            if (selector == null) { throw new ArgumentNullException("selector"); }
            return source.Distinct(
                new CustomComparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.Except(second,
                new CustomComparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }

        static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.Intersect(second,
                new CustomComparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.Union(second,
                new CustomComparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        static bool SequenceEqualBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return first.SequenceEqual(second,
                new CustomComparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        /// <summary>
        /// Determines if the source collection contains the exact same elements as the second, Ignoring duplicate elements and ordering.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>
        /// <returns>True if the given source sequence contain the same elements, irrespective or order and duplicate items, as the second sequence; otherwise, false.</returns>
        public static bool SetEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) {
            return first.Intersect(second).None();
        }
        /// <summary>
        /// Determines if the source collection contains the same elements as the second under the projection. Ignores duplicate elements and element ordering.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the source sequence</typeparam>
        /// <param name="first">The source sequence</param>
        /// <param name="second">The sequence to compare against.</param>        
        /// <param name="selector">A function which extracts a key from each element by which equality is determined.</param>        
        /// <returns>True if the given source sequence contain the same elements, irrespective or order and duplicate items, as the second sequence; otherwise, false.</returns>
        public static bool SetEqualBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> selector) {
            return first.Select(selector).SetEqual(second.Select(selector));
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
        /// An System.Collections.Generic.IEnumerable&lt;TResult&gt; that contains merged elements
        /// of three input sequences.
        /// </returns>
        public static IEnumerable<TResult> Zip<TFirst,
            TSecond,
            TThird,
            TResult>(this IEnumerable<TFirst> first,
                IEnumerable<TSecond> second,
                IEnumerable<TThird> third,
                Func<TFirst, TSecond, TThird, TResult> selector) {
            return first.Zip(second, (x, y) => new { x, y }).Zip(third, (a, b) => selector(a.x, a.y, b));
        }
        /// <summary>
        /// Merges four sequences by using the specified function to select elements.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
        /// <typeparam name="TThird">The type of the elements of the third input sequence.</typeparam>
        /// <typeparam name="TFourth">The type of the elements of the fourth input sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <param name="fourth">The fourth sequence to merge.</param>
        /// <param name="selector">A function that specifies how to merge the elements from the four sequences.</param>
        /// <returns>
        /// An System.Collections.Generic.IEnumerable&lt;TResult&gt; that contains merged elements
        /// of three input sequences.
        /// </returns>
        public static IEnumerable<TResult> Zip<TFirst,
            TSecond,
            TThird,
            TFourth,
            TResult>(this IEnumerable<TFirst> first,
                IEnumerable<TSecond> second, IEnumerable<TThird> third,
                IEnumerable<TFourth> fourth,
                Func<TFirst, TSecond, TThird, TFourth, TResult> selector) {
            return first.Zip(second, third, (a, b, c) => new { a, b, c }).Zip(fourth, (abc, d) => selector(abc.a, abc.b, abc.c, d));
        }



        #endregion

        #region Helpers

        /// <summary>
        /// An EqualityComparer{T} whose Equals and GetHashCode implementations are specified by functions provided as constructor arguments.
        /// </summary>
        /// <typeparam name="T">The type of objects to compare.</typeparam>
        public class CustomComparer<T> : EqualityComparer<T>
        {
            #region Constructors
            /// <summary>
            /// Initializes a new instance of the CustomComparer class which will use the provided equals function. to define element equality.
            /// </summary>
            /// <param name="equals">A function which determines if two objects of type T are equal.</param>
            /// <exception cref="System.ArgumentNullException">The provided <paramref name="equals"/> function is null.</exception>
            /// <remarks>
            /// A custom hashing function is automatically provided, ensuring that equality comparisons take place except when reference is null.
            /// While this provides clean, customizable semantics for set operations, more expensive to use having a complexity of N^2
            /// </remarks>
            public CustomComparer(Func<T, T, bool> equals) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                this.equals = equals;
                getHashCode = o => o == null ? 0 : 1;
            }
            /// <summary>
            /// Initializes a new instance of the CustomComparer class which will use the provided equals and get hashcode functions.
            /// </summary>
            /// <param name="equals">A function which determines if two objects of type T are equal.</param>
            /// <param name="getHashCode">A function which generates a hash code from an element of type T.</param>
            /// <exception cref="System.ArgumentNullException">The provided <paramref name="equals"/> function or <paramref name="getHashCode"/> is null.</exception>
            /// <remarks>Proper usage requires that elements which will compare equal under the specified equals function will also produce identical hashcodes.
            /// Elements may yield identical hash codes, without being considered equal.
            /// </remarks>

            public CustomComparer(Func<T, T, bool> equals, Func<T, int> getHashCode) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                if (getHashCode == null)
                    throw new ArgumentNullException("getHashCode", "A null getHashCode function was provided.");
                this.equals = equals;
                this.getHashCode = getHashCode;
            }
            #endregion

            #region Methods
            /// <summary>
            /// Determines whether two objects of type T are equal.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>true if the specified objects are equal; otherwise, false.</returns>
            public override bool Equals(T x, T y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return equals(x, y);
            }
            /// <summary>
            /// Serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.
            /// </summary>
            /// <param name="obj">The object for which to get a hash code.</param>
            /// <returns>A hash code for the specified object.</returns>
            public override int GetHashCode(T obj) {
                return getHashCode(obj);
            }
            #endregion

            #region Fields
            private Func<T, T, bool> equals;
            private Func<T, int> getHashCode;
            #endregion
        }

        #endregion

    }
}
