using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of ILexical instances which are in themselves sequences of ILexical instances of arbitrary recursive depth.
    /// </summary>
    /// <see cref="ILexical"/>
    static class IEnumerableOfRecursiveIEnumerableILexicalsExtenstions
    {
        /// <summary>
        /// Returns a recursively defined enumerable collection which, when enumerated, will yield the members of each subsequence, along with the element providing it, in turn.
        /// Traversal is arbitrarily deep.
        /// </summary>
        /// <typeparam name="T">The Type of the elements of the source sequence. Must be a Type which implements the ILexical interface.</typeparam>
        /// <param name="source">The sequence of potentially IEnumerrable of T to enumerate recursively.</param>
        /// <returns>A recursively defined enumerable collection which, when enumerated, will yield the members of each subsequence, along with the element providing it, in turn.</returns>
        public static IEnumerable<T> GetRecursiveEnumerator<T>(this IEnumerable<T> source) where T : class,ILexical {
            foreach (var e in source) {
                var inner = e as IEnumerable<T>;
                if (inner != null) {
                    foreach (var t in inner.GetRecursiveEnumerator()) { yield return t; }
                } else {
                    yield return e;
                }
            }
        }
    }
}
