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
        public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) {
            return new KeyValuePair<TKey, TValue>(key, value);
        }
    }
    public struct Pair<T1, T2>
    {
        private Pair(T1 first, T2 second) { Key = first; Value = second; }
        public T1 Key { get; }
        public T2 Value { get; }
        public static implicit operator KeyValuePair<T1, T2>(Pair<T1, T2> pair) {
            return new KeyValuePair<T1, T2>(pair.Key, pair.Value);
        }
        public static implicit operator Pair<T1, T2>(KeyValuePair<T1, T2> keyValuePair) {
            return new Pair<T1, T2>(keyValuePair.Key, keyValuePair.Value);
        }
        internal static Pair<T1, T2> Create(T1 first, T2 second) => new Pair<T1, T2>(first, second);
    }
    public static class Pair
    {
        public static Pair<T1, T2> Create<T1, T2>(T1 first, T2 second) => Pair<T1, T2>.Create(first, second);
    }
}
