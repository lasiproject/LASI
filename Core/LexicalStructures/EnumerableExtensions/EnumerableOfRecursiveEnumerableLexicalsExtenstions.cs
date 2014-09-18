using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of ILexical instances which are in themselves
    /// sequences of ILexical instances of arbitrary recursive depth.
    /// </summary>
    /// <see cref="ILexical"/>
    public static partial class LexicalEnumerable
    {
        /// <summary>
        /// Returns a recursively defined enumerable collection which, when enumerated, will yield the members of each subsequence,
        /// along with the element providing it, in turn. Traversal is arbitrarily deep.
        /// </summary>
        /// <typeparam name="T">The Type of the elements of the source sequence. Must be a Type which implements the ILexical interface.</typeparam>
        /// <param name="source">
        /// The sequence of potentially IEnumerrable of T to enumerate recursively.
        /// </param>
        /// <returns>A recursively defined enumerable collection which, when enumerated, will yield the members of each subsequence, along with the element providing it, in turn.
        /// </returns>
        public static IEnumerable<T> AsRecursiveEnumerable<T>(this IEnumerable<T> source) where T : class, ILexical {
            if (source == null) { yield break; }
            var stack = new Stack<IEnumerable<T>>();
            stack.Push(source.OfType<T>());
            while (stack.Count > 0) {
                foreach (var item in stack.Pop()) {
                    yield return item;
                    var children = item as IEnumerable<T>;
                    if (children != null) {
                        stack.Push(children.OfType<T>());
                    }
                }
            }
        }
    }
}
