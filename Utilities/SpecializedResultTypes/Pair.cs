using System;
using System.Collections.Generic;

namespace LASI.Utilities
{
    /// <summary>
    /// Serves as a factory for instances of the <see cref="Pair{T1, T2}"/> structure.
    /// </summary>
    public static class Pair
    {
        /// <summary>
        /// Creates a new pair
        /// </summary>
        /// <typeparam name="T1">The type of the pairs's first component.</typeparam>
        /// <typeparam name="T2">The type of the pairs's second component.</typeparam>
        /// <param name="first">The value of the pairs's first component.</param>
        /// <param name="second">The value of the pairs's second component.</param>
        /// <returns>A pair whose value is (first, second).</returns>
        public static Pair<T1, T2> Create<T1, T2>(T1 first, T2 second) => new Pair<T1, T2>(first, second);
    }
    /// <summary>
    /// Defines a generic pair.
    /// </summary>
    /// <typeparam name="T1">The type of the first component of the pair.</typeparam>
    /// <typeparam name="T2">The type of the second component of the pair.</typeparam>
    public struct Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    {
        internal Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
        /// <summary>
        /// The first component of the pair.
        /// </summary>
        public T1 First { get; }
        /// <summary>
        /// The second component of the pair.
        /// </summary>
        public T2 Second { get; }
        /// <summary>
        /// Defines an implicit conversion to an instance of the <see cref= "KeyValuePair{T1,T2}"/> structure.
        /// </summary>
        /// <param name="pair">The pair undergoing the conversion/</param>
        public static implicit operator KeyValuePair<T1, T2>(Pair<T1, T2> pair) => new KeyValuePair<T1, T2>(pair.First, pair.Second);
        /// <summary>
        /// Defines an implicit conversion from an instance of the <see cref="KeyValuePair{T1,T2}"/> to an instance of the <see cref= "Pair{T1,T2}"/> structure.
        /// </summary>
        /// <param name="keyValuePair">The key value pair undergoing the conversion.</param>
        public static implicit operator Pair<T1, T2>(KeyValuePair<T1, T2> keyValuePair) => new Pair<T1, T2>(keyValuePair.Key, keyValuePair.Value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Pair<T1, T2> other) => (First?.Equals(other.First) ?? other.First == null) && (Second?.Equals(other.Second) ?? other.Second == null);
        /// <summary>
        /// Determines if the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">An object to test for equality.</param>
        /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise <c>false</c>.</returns>
        public override bool Equals(object obj) => obj is Pair<T1, T2> && Equals((Pair<T1, T2>)obj);
        /// <summary>
        /// Gets a hashcode for current instance.
        /// </summary>
        /// <returns>A hashcode for current instance.</returns>
        public override int GetHashCode() => First?.GetHashCode() ?? 0 ^ Second?.GetHashCode() ?? 0;
        /// <summary>
        /// Performs an equality comparison between two instances of the <see cref="Pair{T1,T2}"/> structure.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><c>true</c> if the pairs are equal; otherwise <c>false</c>.</returns>
        public static bool operator ==(Pair<T1, T2> left, Pair<T1, T2> right) => left.Equals(right);
        /// <summary>
        /// Performs an inequality comparison between two instances of the <see cref="Pair{T1,T2}"/> structure.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><c>true</c> if the pairs are not equal; otherwise <c>false</c>.</returns>
        public static bool operator !=(Pair<T1, T2> left, Pair<T1, T2> right) => !(left == right);
    }
}
