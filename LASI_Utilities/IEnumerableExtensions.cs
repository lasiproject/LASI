using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <returns>Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source) {
            return source.Aggregate("[ ", (sum, current) => sum += current + ", ", aggr => aggr.TrimEnd(' ', ',') + " ]");
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="elementToString">The function used to produce a string representation for each element.</param>
        /// <returns>Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> elementToString) {
            return source.Aggregate("[ ", (sum, current) => sum += elementToString(current) + ", ", aggr => aggr.TrimEnd(' ', ',') + " ]");
        }
    }
}
