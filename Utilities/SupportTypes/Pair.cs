using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{

    /// <summary>
    /// Provides static methods for creating key value pairs.
    /// </summary>
    public static class KeyValuePair
    {
        /// <summary>
        /// Creates a new pair
        /// </summary>
        /// <typeparam name="TKey">The type of the pairs's first component.</typeparam>
        /// <typeparam name="TValue">The type of the pairs's second component.</typeparam>
        /// <param name="key">The value of the pairs's first component.</param>
        /// <param name="value">The value of the pairs's second component.</param>
        /// <returns>A pair whose value is (item1, item2).</returns>
        public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) { return new KeyValuePair<TKey, TValue>(key, value); }

    }
    public class Pair<T1, T2>
    {
        internal Pair(T1 first, T2 second) { First = first; Second = second; }
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
    }
    public class Pair
    {
        public static Pair<T1, T2> Create<T1, T2>(T1 first, T2 second) {
            return new Pair<T1, T2>(first, second);
        }
    }


}
