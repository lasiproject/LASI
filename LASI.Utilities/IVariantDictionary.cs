using System;
using System.Collections.Generic;

namespace LASI.Utilities
{
    /// <summary>
    /// Represents a generic covariant, read-only collection of key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the read-only dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the read-only dictionary.</typeparam>
    public interface IVariantDictionary<TKey, out TValue> : IEnumerable<IVariantKeyValuePair<TKey, TValue>>, IReadOnlyCollection<IVariantKeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        TValue this[TKey key] { get; }

        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// <c>true</c> if the read-only dictionary contains an element that has the specified key; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
        bool ContainsKey(TKey key);
    }
}