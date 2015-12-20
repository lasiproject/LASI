using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// <see cref="ConcurrentDictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// <see cref="Dictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// <see cref="Dictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// <see cref="IDictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// <see cref="IDictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> or <see cref="default(TValue)"/> if the
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
        /// The value with the specified key from the <see cref="IReadOnlyDictionary{TKey, TValue}"/> or <see cref="<see cref="default(TValue)"/>"/> if the
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

        public static IReadOnlyDictionary<TKey, Indexed<TValue>> WithIndices<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary) =>
            dictionary
                .Select((entry, index) => new KeyValuePair<TKey, Indexed<TValue>>(entry.Key, Indexed.Create(entry.Value, index)))
                .ToDictionary();

        #endregion IDictionary Extensions
    }
}