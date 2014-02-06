using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
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
            return source.Format(Tuple.Create('[', ',', ']'), lineLength);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimsToUse) {
            return source.Aggregate(new StringBuilder(delimsToUse.Item1 + " "), (sum, current) => sum.Append(current.ToString() + delimsToUse.Item2 + ' ')).ToString().TrimEnd(' ', delimsToUse.Item2) + ' ' + delimsToUse.Item3;
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="stringSelector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> stringSelector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (stringSelector == null)
                throw new ArgumentNullException("stringSelector");
            return source.Aggregate(new StringBuilder("[ "), (sum, current) => sum.Append(stringSelector(current) + ", ")).ToString().TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="stringSelector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimsToUse, Func<T, string> stringSelector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (stringSelector == null)
                throw new ArgumentNullException("stringSelector");
            return source.Select(stringSelector).Format(delimsToUse);
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="stringSelector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, long lineLength, Func<T, string> stringSelector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (stringSelector == null)
                throw new ArgumentNullException("stringSelector");
            int len = 2;
            return source.Aggregate(new StringBuilder("[ "),
                (sum, current) => {
                    var cETS = stringSelector(current) + ", ";
                    len += cETS.Length;
                    if (len > lineLength) {
                        len = cETS.Length;
                        sum.Append('\n');
                    }
                    return sum.Append(cETS);
                }).ToString().TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="stringSelector">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimsToUse, long lineLength, Func<T, string> stringSelector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (stringSelector == null)
                throw new ArgumentNullException("stringSelector");
            int len = 2;
            return source.Aggregate(delimsToUse.Item1 + " ",
                (sum, current) => {
                    var cETS = stringSelector(current) + delimsToUse.Item2 + ' ';
                    len += cETS.Length;
                    if (len > lineLength) {
                        len = cETS.Length;
                        sum += '\n';
                    }
                    return sum + cETS;
                }).TrimEnd(' ', delimsToUse.Item2) + ' ' + delimsToUse.Item3;
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimsToUse, long lineLength) {
            if (source == null)
                throw new ArgumentNullException("source");
            int len = 2;
            return source.Aggregate(new StringBuilder(delimsToUse.Item1 + " "),
                (sum, current) => {
                    var cETS = current.ToString() + delimsToUse.Item2 + ' ';
                    len += cETS.Length;
                    if (len > lineLength) {
                        len = cETS.Length;
                        sum.Append('\n');
                    }
                    return sum.Append(cETS);
                }).ToString().TrimEnd(' ', delimsToUse.Item2) + " " + delimsToUse.Item3;
        }

        #endregion

        #region Generator Methods
        /// <summary>
        /// Generates a sequence of integral numbers within the specified range.
        /// </summary>
        /// <param name="from">The value of the first integer in the sequence.</param>
        /// <param name="to">The number of sequential integers to generate.</param>
        /// <returns>
        /// A lazily evaluated sequence of integral numbers. 
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// to is less than 0.-or-start + count -1 is larger than System.Int32.MaxValue."
        ///</exception>
        public static IEnumerable<int> To(this int from, int to) {
            if (to - from < 0) { throw new ArgumentOutOfRangeException("to", from - to, "Cannot generate a sequence of fewer than 0 values."); }
            return Enumerable.Range(from, to + 1 - from);
        }

        #endregion

        #region Custom Query Operators

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
            return new HashSet<TSource>(source, new CustomComaparer<TSource>(equals, getHashCode));
        }
        /// <summary>
        /// Returns a SortedSet representation of the given sequence using the default IEqualityComparer for the given element type.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting SortedSet.</param>
        /// <returns>A System.Collections.Generic.SortedSet representation of the given sequence using the default System.Collections.Generic.IEqualityComparer for the given element type.</returns>
        public static SortedSet<TSource> ToSortedSet<TSource>(this IEnumerable<TSource> source) {
            if (source == null)
                throw new ArgumentNullException("source");
            return new SortedSet<TSource>(source);
        }
        /// <summary>
        /// Returns a SortedSet representation of the given sequence using the default IEqualityComparer for the given element type.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting SortedSet.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer implementation which will determine the distinctness of elements.</param>
        /// <returns>A System.Collections.Generic.SortedSet representation of the given sequence using the default System.Collections.Generic.IEqualityComparer for the given element type.</returns>
        public static SortedSet<TSource> ToSortedSet<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer) where TSource : IComparable<TSource> {
            if (source == null)
                throw new ArgumentNullException("source");
            return new SortedSet<TSource>(source, comparer);
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


        static IEnumerable<Tuple<TSource, TSource>> PairWise<TSource>(this IEnumerable<TSource> source) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (source.None())
                throw new ArgumentException("Sequence contains no elements", "source");
            TSource first = source.First();
            foreach (var element in source.Skip(1)) {
                yield return Tuple.Create(first, element);
                first = element;
            }
        }
        static IEnumerable<Tuple<TPairItem, TPairItem>> PairWise<T, TPairItem>(this IEnumerable<T> source, Func<T, TPairItem> itemSelector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (itemSelector == null)
                throw new ArgumentNullException("itemSelector");
            if (source.None())
                throw new ArgumentException("Sequence contains no elements", "source");
            T first = source.First();
            foreach (var element in source.Skip(1)) {
                yield return Tuple.Create(itemSelector(first), itemSelector(element));
                first = element;
            }
        }
        static IEnumerable<Tuple<TResult1, TResult2>> PairWise<TSource, TResult1, TResult2>(this IEnumerable<TSource> source, Func<TSource, TResult1> item1Selector, Func<TSource, TResult2> item2Selector) {
            if (source == null)
                throw new ArgumentNullException("source");
            if (item1Selector == null)
                throw new ArgumentNullException("selector1");
            if (item2Selector == null)
                throw new ArgumentNullException("selector2");
            if (source.None())
                throw new ArgumentException("Sequence contains no elements", "source");
            TSource first = source.First();
            foreach (var element in source.Skip(1)) {
                yield return Tuple.Create(item1Selector(first), item2Selector(element));
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
            if (source == null)
                throw new ArgumentNullException("source");
            if (source.None())
                throw new InvalidCastException("Sequence Contains no elements");
            if (selector == null)
                throw new ArgumentNullException("selector");
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



        static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return source.Distinct(
                new CustomComaparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> source, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return source.Except(second,
                new CustomComaparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }

        static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> source, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return source.Intersect(second,
                new CustomComaparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> source, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return source.Union(second,
                new CustomComaparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        static bool SequenceEqualBy<TSource, TKey>(this IEnumerable<TSource> source, IEnumerable<TSource> second, Func<TSource, TKey> selector) where TKey : IEquatable<TKey> {
            return source.SequenceEqual(second,
                new CustomComaparer<TSource>(
                (x, y) => selector(x).Equals(selector(y)),
                x => selector(x).GetHashCode()));
        }
        #endregion

        #region Internal Support Types

        /// <summary>
        /// An EqualityComparer{T} whose Equals and GetHashCode implementations are specified by functions provided as constructor arguments.
        /// </summary>
        /// <typeparam name="T">The type of objects to compare.</typeparam>
        private class CustomComaparer<T> : EqualityComparer<T>
        {
            #region Constructors
            public CustomComaparer(Func<T, T, bool> equals) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                this.equals = equals;
                getHashCode = o => o == null ? 0 : 1;
            }
            public CustomComaparer(Func<T, T, bool> equals, Func<T, int> getHashCode) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                if (getHashCode == null)
                    throw new ArgumentNullException("getHashCode", "A null getHashCode function was provided.");
                this.equals = equals;
                this.getHashCode = getHashCode;
            }
            #endregion

            #region Methods
            public override bool Equals(T x, T y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return equals(x, y);
            }
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
