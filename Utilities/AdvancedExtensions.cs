namespace LASI.Utilities.Advaned
{
    using FunctionExtensions;
    using System.Collections.Generic;
    using System.Linq;

    public static class AdvancedExtensions
    {
        /// <summary>
        /// Lifts the given <paramref name="value" /> into an <see cref="SupportTypes.Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="value">The value to lift into an <see cref="SupportTypes.Option{T}"/>.</param>
        /// <returns>An <see cref="SupportTypes.Option{T}"/> containing the specified value or an empty <see cref="SupportTypes.Option{T}"/> if the element is <c>null</c>.</returns>
        public static SupportTypes.Option<T> ToOption<T>(this T value) => SupportTypes.Option.ToOption(value);


        /// <summary>
        /// Filters an <see cref="IEnumerable{T}" />, yielding only those elements that are not null.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source" />.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to filter.</param>
        /// <returns>All elements in <paramref name="source" /> which are not null.</returns>
        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> source) where T : class => source.Where(IsNotNull);

        /// <summary>
        /// Filters an <see cref="ParallelQuery{T}" />, yielding only those elements that are not null.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source" />.</typeparam>
        /// <param name="source">The <see cref="ParallelQuery{T}" /> to filter.</param>
        /// <returns>All elements in <paramref name="source" /> which are not null.</returns>
        public static ParallelQuery<T> ExceptNull<T>(this ParallelQuery<T> source) where T : class => source.Where(IsNotNull);
    }
}