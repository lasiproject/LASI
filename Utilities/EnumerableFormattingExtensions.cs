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
        public static string Format<T>(this IEnumerable<T> source) => source.Format(DefaultDilimiters);

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
        public static string Format<T>(this IEnumerable<T> source, long lineLength) => source.Format(DefaultDilimiters, lineLength);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ] such that the string representation of each element
        /// is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="delimiters">
        /// The triple of delimiters specifying the beginning, separating, and ending characters.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters)
        {
            Validate.NotNull(source, "source", delimiters, "delimiters");
            return source.Aggregate(new StringBuilder().Append(delimiters.Item1).Append(' '),
                    (builder, e) => builder.Append(e.ToString() + delimiters.Item2 + ' '),
                    result => result.ToString().TrimEnd(' ', '\n', delimiters.Item2) + ' ' + delimiters.Item3);
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
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> selector) => source.Format(DefaultDilimiters, selector);

        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ] such that the string
        /// representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">
        /// An IEnumerable sequence containing 0 or more Elements of type T.
        /// </param>
        /// <param name="delimiters">
        /// The triple of delimiters specifying the beginning, separating, and ending characters.
        /// </param>
        /// <param name="selector">
        /// The function used to produce a string representation for each element.
        /// </param>
        /// <returns>
        /// formated string representation of the IEnumerable sequence with the pattern: [
        /// selector(element0), selector(element1), ..., selector(elementN) ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, Func<T, string> selector)
        {
            Validate.NotNull(source, "source", selector, "selector", delimiters, "delimiters");
            return source.Select(selector).Format(delimiters);
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
        /// <param name="delimiters">
        /// The triple of delimiters specifying the beginning, separating, and ending characters.
        /// </param>
        /// <param name="lineLength">
        /// Indicates the number of characters after which a line break is to be inserted.
        /// </param>
        /// <returns>
        /// A formated string representation of the IEnumerable sequence with the pattern: [
        /// element0, element1, ..., elementN ].
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, long lineLength) => source.Format(delimiters, lineLength, DefaultSelector);

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
        public static string Format<T>(this IEnumerable<T> source, long lineLength, Func<T, string> selector) => source.Format(DefaultDilimiters, lineLength, selector);

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
        /// <param name="delimiters">
        /// A three item tuple delimiters which will be used to format the result.
        /// </param>
        /// <param name="selector">
        /// The function used to produce a string representation for each element.
        /// </param>
        /// <returns>
        /// delimiters.Item1 selector(element0)delimiters.Item2 selector(element1)delimiter
        /// ...delimiters.Item2 selector(elementN) delimiters.Item3.
        /// </returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char, char> delimiters, long lineLength, Func<T, string> selector)
        {
            Validate.NotNull(source, "source", delimiters, "delimiters", selector, "selector");
            Validate.NotLessThan(lineLength, 1, "lineLength", "Line length must be greater than 0.");
            return source.Aggregate(
                    new
                    {
                        AccumulatedLineLength = 0L,
                        Builder = new StringBuilder(delimiters.Item1.ToString())
                    },
                    (z, element) =>
                    {
                        var text = selector(element) + delimiters.Item2;
                        var appendNewLine = z.AccumulatedLineLength + text.Length + 1 > lineLength;
                        return new
                        {
                            AccumulatedLineLength = appendNewLine ? text.Length : z.AccumulatedLineLength + text.Length + 1,
                            Builder = z.Builder.Append((appendNewLine ? '\n' : ' ') + text)
                        };
                    }).Builder.ToString().TrimEnd(' ', delimiters.Item2) + ' ' + delimiters.Item3;
        }

        private static readonly Tuple<char, char, char> DefaultDilimiters = Tuple.Create('[', ',', ']');
        private static string DefaultSelector<T>(T value) => value.ToString();
    }
}
