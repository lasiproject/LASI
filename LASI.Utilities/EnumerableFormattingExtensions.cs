using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    /// <summary>
    /// Provides extension methods which assist in formatting <see cref="IEnumerable{T}"/>s for textual display.
    /// </summary>
    public static class EnumerableFormattingExtensions
    {
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ] such that the string representation of each element
        /// is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source) => source.Format(DefaultDeimiters);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ] such that the string representation of each element
        /// is produced by calling its ToString method. The resultant string is line broken based on
        /// the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="lineLength">
        /// Indicates the number of characters after which a line break is to be inserted.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, long lineLength) => source.Format(DefaultDeimiters, lineLength);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ] such that the string representation of each element
        /// is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="deimiters">
        /// The triple of delimiters specifying the beginning, separating, and ending characters.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, (char beginning, char separator, char ending) deimiters)
        {
            var (beginning, separator, ending) = deimiters;
            Validate.NotNull(source, "source", deimiters, "delimiters");
            return source.Aggregate(new StringBuilder().Append(beginning).Append(' '),
                    (builder, e) => builder.Append(e.ToString() + separator + ' '),
                    result => result.ToString().TrimEnd(' ', '\n', separator) + ' ' + ending);
        }

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ] such that the string
        /// representation of each element is produced by calling the provided selector function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="selector">
        /// The function used to produce a string representation for each element.
        /// </param>
        /// <returns>
        /// A a formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> selector) =>
            source.Format(DefaultDeimiters, selector);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ] such that the string
        /// representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="deimiters">
        /// The triple of delimiters specifying the beginning, separating, and ending characters.
        /// </param>
        /// <param name="selector">
        /// The function used to produce a string representation for each element.
        /// </param>
        /// <returns>
        /// formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, (char beginning, char separator, char ending) deimiters, Func<T, string> selector)
        {
            Validate.NotNull(source, "source", selector, "selector", deimiters, "delimiters");
            return source.Select(selector).Format(deimiters);
        }

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ] such that the string representation of each element
        /// is produced by calling its ToString method. The resultant string is line broken based on
        /// the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="deimiters">
        /// The triple of delimiters specifying the beginning, separating, and ending characters.
        /// </param>
        /// <param name="lineLength">
        /// Indicates the number of characters after which a line break is to be inserted.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, (char beginning, char separator, char ending) deimiters, long lineLength) =>
            source.Format(deimiters, lineLength, DefaultSelector);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ] such that the string
        /// representation of each element is produced by calling the provided selector function.
        /// The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="lineLength">
        /// Indicates the number of characters after which a line break is to be inserted.
        /// </param>
        /// <param name="selector">
        /// The function used to produce a string representation for each element.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, long lineLength, Func<T, string> selector) =>
            source.Format(DefaultDeimiters, lineLength, selector);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern:
        /// delimiters.Item1 selector(element0)delimiters.Item2 selector(element1)delimiter
        /// ...delimiters.Item2 selector(elementN) delimiters.Item3 such that the string
        /// representation of each element is produced by calling the provided selector function on
        /// each element of the sequence and separating their each resulting string with the second
        /// element of the provided tuple of delimiters. The resultant string is line broken based
        /// on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="lineLength">
        /// Indicates the number of characters after which a line break is to be inserted.
        /// </param>
        /// <param name="deimiters">
        /// A three item tuple delimiters which will be used to format the result.
        /// </param>
        /// <param name="selector">
        /// The function used to produce a string representation for each element.
        /// </param>
        /// <returns>
        /// delimiters.Item1 selector(element0)delimiters.Item2 selector(element1)delimiter
        /// ...delimiters.Item2 selector(elementN) delimiters.Item3.
        /// </returns>
        public static string Format<T>(
            this IEnumerable<T> source,
            (char beginning, char separator, char ending) deimiters,
            long lineLength,
            Func<T, string> selector)
        {
            Validate.NotNull(source, "source", deimiters, "delimiters", selector, "selector");
            lineLength.NotLessThan(1, "lineLength", "Line length must be greater than 0.");
            return source.Aggregate((accumulatedLineLength: 0L, builder: new StringBuilder(deimiters.beginning.ToString())),
                    (z, element) =>
                    {
                        var text = selector(element) + deimiters.separator;
                        var appendNewLine = z.accumulatedLineLength + text.Length + 1 > lineLength;
                        return
                        (
                            accumulatedLineLength: appendNewLine ? text.Length : z.accumulatedLineLength + text.Length + 1,
                            builder: z.builder.Append((appendNewLine ? '\n' : ' ') + text)
                        );
                    }).builder.ToString().TrimEnd(' ', deimiters.separator) + ' ' + deimiters.ending;
        }

        private static readonly (char beginning, char separator, char ending) DefaultDeimiters = ('[', ',', ']');

        private static string DefaultSelector<T>(T value) => value.ToString();
    }
}
