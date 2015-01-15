using System;
using System.Collections.Generic;

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
    public struct Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    {
        private Pair(T1 first, T2 second) {
            Key = first;
            Value = second;
        }
        public T1 Key { get; }
        public T2 Value { get; }

        public static implicit operator KeyValuePair<T1, T2>(Pair<T1, T2> pair) => new KeyValuePair<T1, T2>(pair.Key, pair.Value);

        public static implicit operator Pair<T1, T2>(KeyValuePair<T1, T2> keyValuePair) => new Pair<T1, T2>(keyValuePair.Key, keyValuePair.Value);

        internal static Pair<T1, T2> Create(T1 first, T2 second) => new Pair<T1, T2>(first, second);
        public bool Equals(Pair<T1, T2> other) => (Key?.Equals(other.Key) ?? false) && (Value?.Equals(other.Value) ?? false);
        public override bool Equals(object obj) => obj is Pair<T1, T2> && Equals((Pair<T1, T2>)obj);
        public override int GetHashCode() => Key?.GetHashCode() ?? 0 ^ Value?.GetHashCode() ?? 0;
        public static bool operator ==(Pair<T1, T2> left, Pair<T1, T2> right) => left.Equals(right);
        public static bool operator !=(Pair<T1, T2> left, Pair<T1, T2> right) => !(left == right);
        public static bool operator ==(Pair<T1, T2> left, KeyValuePair<T1, T2> right) => left.Equals(right);
        public static bool operator !=(Pair<T1, T2> left, KeyValuePair<T1, T2> right) => !(left == right);
        public static bool operator ==(KeyValuePair<T1, T2> left, Pair<T1, T2> right) => left.Equals(right);
        public static bool operator !=(KeyValuePair<T1, T2> left, Pair<T1, T2> right) => !(left == right);
    }
    public static class Pair
    {
        public static Pair<T1, T2> Create<T1, T2>(T1 first, T2 second) => Pair<T1, T2>.Create(first, second);
    }
}
