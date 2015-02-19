using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.Specialized.Enhanced.Linq.List
{
    /// <summary>
    /// Provides a set of methods for querying collections of type <see cref="IList{T}" /> allowing
    /// queries over lists to transparently return <see cref="IList{T}" /> instead of <see
    /// cref="IEnumerable{T}" />
    /// </summary>
    /// <seealso cref="Enumerable"/>
    /// <seealso cref="IList{T}"/>
    /// <seealso cref="List{T}"/>
    public static class ListExtensions
    {
        /// <summary>
        /// Projects each element of a list into a new form.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="R">The type of the value returned by selector.</typeparam>
        /// <param name="list">A list of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// A <see cref="IList{R}"/> whose elements are the result of invoking the transform function on each element of source.
        ///</returns>
        public static IList<R> Select<T, R>(this IList<T> list, Func<T, R> selector) =>
            list.AsEnumerable().Select(selector).ToList();

        /// <summary>
        /// Filters a lsit of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="list">A <see cref="IList{T}" /> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A <see cref="IList{T}" /> that contains elements from the input list that satisfy the condition.
        /// </returns>
        public static IList<T> Where<T>(this IList<T> list, Func<T, bool> predicate) =>
            list.AsEnumerable().Where(predicate).ToList();

        /// <summary>
        /// Projects each element of a list to an <see cref="IEnumerable{T}" /> and flattens the
        /// resulting sequences into one list.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="R">The type of the elements of the sequence returned by selector.</typeparam>
        /// <param name="list">A list of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// A <see cref="IList{R}" /> whose elements are the result of invoking the one-to-many
        /// transform function on each element of the input list.
        /// </returns>
        public static IList<R> SelectMany<T, R>(this IList<T> list, Func<T, IEnumerable<R>> selector) =>
            list.AsEnumerable().SelectMany(selector).ToList();

        /// <summary>
        /// Projects each element of a list to an <see cref="IEnumerable{T}" />, flattens the
        /// resulting sequences into one list, and invokes a result selector function on each
        /// element therein.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="TCollection">
        /// The type of the intermediate elements collected by collectionSelector.
        /// </typeparam>
        /// <typeparam name="R">The type of the value returned by selector.</typeparam>
        /// <param name="list">A <see cref="List{T}" /> to project.</param>
        /// <param name="collectionSelector">
        /// A transform function to apply to each element of the input list.
        /// </param>
        /// <param name="resultSelector">
        /// A transform function to apply to each element of the intermediate sequence.
        /// </param>
        /// <returns>
        /// A <see cref="IList{R}" /> whose elements are the result of invoking the one-to-many
        /// transform function collectionSelector on each element of source and then mapping each of
        /// those sequence elements and their corresponding source element to a result element.
        /// </returns>
        public static IList<R> SelectMany<T, TCollection, R>(this IList<T> list, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, R> resultSelector) =>
            list.AsEnumerable().SelectMany(collectionSelector, resultSelector).ToList();

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a list.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list</typeparam>
        /// <param name="source">The <see cref="List{T}" /> to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>
        /// A <see cref="List{T}" /> that contains the specified number of elements from the start
        /// of the input sequence.
        /// </returns>
        public static List<T> Take<T>(this List<T> source, int count)
        {
            if (count < 0) { return new List<T>(); }
            if (count > source.Count) { return source.GetRange(0, source.Count); }
            return source.GetRange(0, count);
        }

        /// <summary>
        /// Bypasses a specified number of elements in a list and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list</typeparam>
        /// <param name="source">The <see cref="List{T}" /> to return elements from.</param>
        /// <param name="count">
        /// The number of elements to skip before returning the remaining elements.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}" /> that contains the elements that occur after the specified index
        /// in the input list.
        /// </returns>
        public static List<T> Skip<T>(this List<T> source, int count)
        {
            if (count < 0) { return source.GetRange(0, source.Count); }
            if (count > source.Count) { return new List<T>(); }
            return source.GetRange(count, source.Count - count);
        }
        /// <summary>
        /// Performs the specified action on each element of the <see cref="System.Collections.Generic.IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list</typeparam>
        /// <param name="list">The list over which to execute.</param>
        /// <param name="action">The <see cref="System.Action{T}"/> delegate to perform on each element of the <see cref="System.Collections.Generic.IList{T}"/>.</param>
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                action(list[i]);
            }
        }
        public static IReadOnlyList<T> AsReadOnly<T>(this IList<T> list) => list as IReadOnlyList<T> ?? list.ToList();
    }
}