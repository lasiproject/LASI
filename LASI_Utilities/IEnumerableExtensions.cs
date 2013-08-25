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
    public static class IEnumerableExtensions
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
            return source.Aggregate("[ ", (sum, current) => sum += current + ", ").TrimEnd(' ', ',') + " ]";
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
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ]
        /// such that the string representation of each element is produced by calling its ToString method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char> delimsToUse) {
            return source.Aggregate(delimsToUse.Item1 + " ", (sum, current) => sum += current + ", ").TrimEnd(' ', ',') + " " + delimsToUse.Item1;
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
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char> delimsToUse, long lineLength) {
            int len = 2;
            return source.Aggregate(delimsToUse.Item1 + " ", (sum, current) => {
                var cETS = current.ToString() + ", ";
                len += cETS.Length;
                if (len > lineLength) {
                    len = cETS.Length;
                    sum += '\n';
                }
                return sum += cETS;
            }).TrimEnd(' ', ',') + " " + delimsToUse.Item2;
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="elementToString">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Func<T, string> elementToString) {
            return source.Aggregate("[ ", (sum, current) => sum += elementToString(current) + ", ").TrimEnd(' ', ',') + " ]";
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="elementToString">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char> delimsToUse, Func<T, string> elementToString) {
            return source.Aggregate(delimsToUse.Item1 + " ", (sum, current) => sum += elementToString(current) + ", ").TrimEnd(' ', ',') + " " + delimsToUse.Item2;
        }
        /// <summary>
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="elementToString">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, long lineLength, Func<T, string> elementToString) {
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
        /// Returns a formated string representation of the IEnumerable sequence with the pattern: [ elementToString(element0), elementToString(element1), ..., elementToString(elementN) ]
        /// such that the string representation of each element is produced by calling the provided elementToString function. The resultant string is line broken based on the provided line length.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the generic IEnumerable sequence.</typeparam>
        /// <param name="source">An IEnumerable sequence containing 0 or more Elements of type T.</param>
        /// <param name="delimsToUse">A value indicating the pair of delimiters to surround the elements.</param>
        /// <param name="lineLength">Indicates the number of characters after which a line break is to be inserted.</param>
        /// <param name="elementToString">The function used to produce a string representation for each element.</param>
        /// <returns>A formated string representation of the IEnumerable sequence with the pattern: [ element0, element1, ..., elementN ].</returns>
        public static string Format<T>(this IEnumerable<T> source, Tuple<char, char> delimsToUse, long lineLength, Func<T, string> elementToString) {
            int len = 2;
            return source.Aggregate(delimsToUse.Item1 + " ", (sum, current) => {
                var cETS = elementToString(current) + ", ";
                len += cETS.Length;
                if (len > lineLength) {
                    len = cETS.Length;
                    sum += '\n';
                }
                return sum += cETS;
            }).TrimEnd(' ', ',') + " " + delimsToUse.Item2;
        }

        #endregion

        #region Custom Query Operators

        /// <summary>
        /// Appends the given element to the sequence, yielding a new sequence consiting of the original sequence followed by the appended element.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="head">The sequence to which the element will be appended.</param>
        /// <param name="tail">The element to append to the sequence.</param>
        /// <returns>A new sequence consiting of the original sequence followed by the appended element..</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> head, T tail) {
            foreach (var i in head) { yield return i; }
            yield return tail;
        }
        /// <summary>
        /// Prepends the given element to the sequence, yielding a new sequence consiting of the prepended element followed by each element in the original sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="tail">The sequence to which the element will be prepended.</param>
        /// <param name="head">The element to prepend to the sequence.</param>
        /// <returns>A new sequence consiting of the prepended element followed by each element in the original sequence.</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head) {
            yield return head;
            foreach (var i in tail) { yield return i; }
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
        /// Returns a set representation of the given sequence using the specified IEqualityComparer to determine element uniqueness.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer implementation which will determine the distinctness of elements.</param>
        /// <returns>A set representation of the given sequence using the default IEqualityComparer for the given element type.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer) {
            return new HashSet<T>(source, comparer);
        }
        /// <summary>
        /// Splits the sequence into a sequence of sequences based on the provided chunk size.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to split into subsequences</param>
        /// <param name="chunkSize">The number of elements per subsquence</param>
        /// <returns>A sequence of sequences based on the provided chunk size.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize) {
            var partsToCreate = source.Count() / chunkSize + source.Count() % chunkSize == 0 ? 0 : 1;
            return from partIndex in Enumerable.Range(0, partsToCreate)
                   select source.Skip(partIndex * chunkSize).Take(chunkSize);

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
                customEquals = equals;
                customHasher = o => o == null ? 0 : 1;
            }
            public CustomComaparer(Func<T, T, bool> equals, Func<T, int> hasher) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                customEquals = equals;
                if (hasher == null)
                    throw new ArgumentNullException("hasher", "A null getHashCode function was provided.");
                customEquals = equals;
                customHasher = hasher;
            }
            #endregion

            #region Methods
            public override bool Equals(T x, T y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return customEquals(x, y);
            }
            public override int GetHashCode(T obj) {
                return customHasher(obj);
            }
            #endregion

            #region Fields
            private Func<T, T, bool> customEquals;
            private Func<T, int> customHasher;
            #endregion
        }

        #endregion


    }
}
