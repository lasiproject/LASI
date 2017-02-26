using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities.SpecializedResultTypes;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    /// <summary>Provides various methods for Dictionary types.</summary>
    public static class DictionaryExtensions
    {
        #region ConcurrentDictionary Extensions

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or default (<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="ConcurrentDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            Validate.NotNull(dictionary, nameof(dictionary), key, nameof(key));
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="ConcurrentDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValue">
        /// The value to return if the specified key is not present in the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="ConcurrentDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValueFactory">
        /// The function to create a default value if the specified key is not present in the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
        }

        /// <summary>
        /// Invokes the specified action for each <see cref="KeyValuePair{TKey, TValue}" /> in the <see cref="ConcurrentDictionary{TKey, TValue}" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="ConcurrentDictionary{TKey, TValue}"/> to enumerate.
        /// </param>
        /// <param name="action">
        /// The action to perform on each
        /// <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}" /> in the <see cref="ConcurrentDictionary{TKey, TValue}" />.
        /// </param>
        [System.Diagnostics.DebuggerStepThrough]
        public static void ForEach<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            foreach (var keyValuePair in dictionary)
            {
                action(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static ConcurrentDictionary<TKey, Indexed<TValue>> WithIndices<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary) =>
            new ConcurrentDictionary<TKey, Indexed<TValue>>(dictionary
                .Select((entry, index) => new KeyValuePair<TKey, Indexed<TValue>>(entry.Key, Indexed.Create(entry.Value, index))));

        #endregion IDictionary Extensions

        #region Dictionary Extensions

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="Dictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="Dictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="Dictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            Validate.NotNull(dictionary, nameof(dictionary), key, nameof(key));
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="Dictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="Dictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValue">
        /// The value to return if the specified key is not present in the
        /// <see cref="Dictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="Dictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="Dictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="Dictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValueFactory">
        /// The function to create a default value if the specified key is not present in the
        /// <see cref="Dictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="Dictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
        }

        /// <summary>
        /// Invokes the specified action for each <see cref="KeyValuePair{TKey, TValue}" /> in the <see cref="Dictionary{TKey, TValue}" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="Dictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="Dictionary{TKey, TValue}"/> to enumerate.
        /// </param>
        /// <param name="action">
        /// The action to perform on each
        /// <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}" /> in the <see cref="Dictionary{TKey, TValue}" />.
        /// </param>
        [System.Diagnostics.DebuggerStepThrough]
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            foreach (var keyValuePair in dictionary)
            {
                action(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static Dictionary<TKey, Indexed<TValue>> WithIndices<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) =>
            dictionary
                .Select((entry, index) => new KeyValuePair<TKey, Indexed<TValue>>(entry.Key, Indexed.Create(entry.Value, index)))
                .ToDictionary();

        #endregion Dictionary Extensions

        #region IDictionary Extensions

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            Validate.NotNull(dictionary, nameof(dictionary), key, nameof(key));
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValue">
        /// The value to return if the specified key is not present in the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValueFactory">
        /// The function to create a default value if the specified key is not present in the
        /// <see cref="IDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="IDictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
        }

        /// <summary>
        /// Invokes the specified action for each <see cref="KeyValuePair{TKey, TValue}" /> in the <see cref="IDictionary{TKey, TValue}" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IDictionary{TKey, TValue}"/> to enumerate.
        /// </param>
        /// <param name="action">
        /// The action to perform on each
        /// <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}" /> in the <see cref="IDictionary{TKey, TValue}" />.
        /// </param>
        [System.Diagnostics.DebuggerStepThrough]
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            foreach (var keyValuePair in dictionary)
            {
                action(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static IDictionary<TKey, Indexed<TValue>> WithIndices<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => dictionary
                .Select((entry, index) => new KeyValuePair<TKey, Indexed<TValue>>(entry.Key, Indexed.Create(entry.Value, index)))
                .ToDictionary();

        #endregion IDictionary Extensions

        #region IReadOnlyDictionary Extensions

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IReadOnlyDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <returns>
        /// The value with the specified key from the <see cref="IReadOnlyDictionary{TKey, TValue}"/> or default(<typeparamref name="TValue"/>) if the
        /// key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            Validate.NotNull(dictionary, nameof(dictionary), key, nameof(key));
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IReadOnlyDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValue">
        /// The value to return if the specified key is not present in the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> or the specified defaultValue
        /// if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Gets the value with the specified key from the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IReadOnlyDictionary{TKey, TValue}"/> from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValueFactory">
        /// The function to create a default value if the specified key is not present in the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory)
        {
            Validate.NotNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
        }

        /// <summary>
        /// Invokes the specified action for each <see cref="KeyValuePair{TKey, TValue}" /> in the <see cref="IDictionary{TKey, TValue}" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </typeparam>
        /// <param name="dictionary">
        /// The <see cref="IReadOnlyDictionary{TKey, TValue}"/> to enumerate.
        /// </param>
        /// <param name="action">
        /// The action to perform on each
        /// <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}" /> in the <see cref="IDictionary{TKey, TValue}" />.
        /// </param>
        [System.Diagnostics.DebuggerStepThrough]
        public static void ForEach<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            foreach (var keyValuePair in dictionary)
            {
                action(keyValuePair.Key, keyValuePair.Value);
            }
        }

        private class DefaultingDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
        {
            public DefaultingDictionary(IDictionary<TKey, TValue> wrapped, Func<TKey, TValue> defaultValueFactory)
            {
                this.wrapped = wrapped;
                this.defaultValueFactory = defaultValueFactory;
            }
            readonly IDictionary<TKey, TValue> wrapped;
            readonly Func<TKey, TValue> defaultValueFactory;

            public IEnumerable<TKey> Keys => wrapped.Keys;

            public IEnumerable<TValue> Values => wrapped.Values;

            public int Count => wrapped.Count;

            ICollection<TKey> IDictionary<TKey, TValue>.Keys => ((IDictionary<TKey, TValue>)wrapped).Keys;

            ICollection<TValue> IDictionary<TKey, TValue>.Values => ((IDictionary<TKey, TValue>)wrapped).Values;

            public bool IsReadOnly => wrapped.IsReadOnly;

            TValue IDictionary<TKey, TValue>.this[TKey key]
            {
                get
                {
                    return this[key];
                }
                set
                {
                    wrapped[key] = value;
                }
            }

            public TValue this[TKey key] => wrapped.GetValueOrDefault(key, defaultValueFactory(key));

            public bool ContainsKey(TKey key) => wrapped.ContainsKey(key);

            public bool TryGetValue(TKey key, out TValue value) => wrapped.TryGetValue(key, out value);

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => wrapped.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Add(TKey key, TValue value)
            {
                wrapped.Add(key, value);
            }

            public bool Remove(TKey key) => wrapped.Remove(key);

            public void Add(KeyValuePair<TKey, TValue> item)
            {
                wrapped.Add(item);
            }

            public void Clear()
            {
                wrapped.Clear();
            }

            public bool Contains(KeyValuePair<TKey, TValue> item) => wrapped.Contains(item);

            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            {
                wrapped.CopyTo(array, arrayIndex);
            }

            public bool Remove(KeyValuePair<TKey, TValue> item) => wrapped.Remove(item);
        }

        #endregion IDictionary Extensions
    }
}