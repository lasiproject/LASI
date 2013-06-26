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
            return source.Aggregate("[ ", (sum, current) => sum += current + ", ").TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <returns>Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, int lineLength) {
            int len = 2;
            return source.Aggregate("[ ", (sum, current) => {
                var cETS = current.ToString() + ", ";
                len += cETS.Length;
                if (len > lineLength) {
                    len = cETS.Length;
                    sum += '\n';
                }
                return sum += cETS;
            }).TrimEnd(' ', ',') + " ]";
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
            return source.Aggregate("[ ", (sum, current) => sum += elementToString(current) + ", ").TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="elementToString">The function used to produce a string representation for each element.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <returns>Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> elementToString, int lineLength) {
            int len = 2;
            return source.Aggregate("[ ", (sum, current) => {
                var cETS = elementToString(current) + ", ";
                len += cETS.Length;
                if (len > lineLength) {
                    len = cETS.Length;
                    sum += '\n';
                }
                return sum += cETS;
            }).TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a set representation of the given sequence using the default IEqualityComparer for the given element type.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <returns>A set representation of the given sequence using the default System.Collections.Generic.IEqualityComparer for the given element type.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> source) {
            return new HashSet<T>(source);
        }
        /// <summary>
        /// Returns a set representation of the given sequence using the default IEqualityComparer for the given element type.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer implementation which will determine the distinctness of elements.</param>
        /// <returns>A set representation of the given sequence using the default IEqualityComparer for the given element type.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer) {
            return new HashSet<T>(source, comparer);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize) {
            var partsToCreate = source.Count() / chunkSize + source.Count() % chunkSize == 0 ? 0 : 1;
            return from partIndex in Enumerable.Range(0, partsToCreate)
                   select source.Skip(partIndex * chunkSize).Take(chunkSize);

        }

    }
}
