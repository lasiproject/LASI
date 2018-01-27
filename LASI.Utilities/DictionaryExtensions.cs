using LASI.Utilities.Validation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LASI.Utilities
{
    /// <summary>Provides various extension methods for manipulating dictionaries.</summary>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static class DictionaryExtensions
    {
        public static IReadOnlyDictionary<TKey, TValue> Create<TKey, TValue>((TKey key, TValue value) first, params (TKey key, TValue value)[] rest) =>
            rest.Prepend(first).ToDictionary(pair => pair.key, pair => pair.value);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key) =>
                             dictionary.GetValueOrDefault(key, () => default);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
                             dictionary.GetValueOrDefault(key, () => defaultValue);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory) =>
            dictionary.TryGetValue(key, out var value) ? value : defaultValueFactory();

        public static (bool has, TValue value) TryGet<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary,
            TKey key) =>
                                               (dictionary.TryGetValue(
            key,
            out var value), value);

        public static ConcurrentDictionary<TKey, (TValue value, int index)> WithIndices<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary) =>
            new ConcurrentDictionary<TKey, (TValue, int)>(dictionary.Select((entry, index) => new KeyValuePair<TKey, (TValue, int)>(entry.Key, (entry.Value, index))));

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
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.GetValueOrDefault(key, () => default);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
            dictionary.GetValueOrDefault(key, () => defaultValue);

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
            Validate.NotNull(dictionary, nameof(dictionary), key, nameof(key));
            return dictionary.TryGetValue(key, out var value)
                ? value
                : defaultValueFactory();
        }

        public static Dictionary<TKey, (TValue value, int index)> WithIndices<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) =>
            dictionary.Select((entry, index) => (entry.Key, (entry.Value, index))).ToDictionary(x => x.Key, x => x.Item2);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.GetValueOrDefault(key, () => default);

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
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue) =>
                             dictionary.GetValueOrDefault(
            key,
            () => defaultValue);

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
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueFactory)
        {
            Validate.NotNull(dictionary, nameof(dictionary), key, nameof(key), defaultValueFactory, nameof(defaultValueFactory));
            return (dictionary.TryGetValue(key, out var value)) ? value : defaultValueFactory();
        }

        public static IDictionary<TKey, (TValue value, int index)> WithIndices<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) =>
            dictionary.Select((entry, index) => new KeyValuePair<TKey, (TValue value, int index)>(entry.Key, (entry.Value, index))).ToDictionary();

        public static (bool has, TValue value) TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            (dictionary.TryGetValue(key, out var value), value);

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
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key) =>
                             dictionary.GetValueOrDefault(
            key,
            () => default);

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
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue) =>
                             dictionary.GetValueOrDefault(
            key,
            () => defaultValue);

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
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueFactory)
        {
            Validate.NotNull(
                dictionary,
                nameof(dictionary),
                key,
                nameof(key),
                defaultValueFactory,
                nameof(defaultValueFactory));
            return dictionary.TryGetValue(
                    key,
                    out var value)
                ? value
                : defaultValueFactory();
        }

        public static (bool has, TValue value) TryGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
            (dictionary.TryGetValue(key, out var value), value);

        #endregion

        #region IVariantDictionary Extensions

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
        public static TValue GetValueOrDefault<TKey, TValue>(this IVariantDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.GetValueOrDefault(key, () => default);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this IVariantDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
            dictionary.GetValueOrDefault(key, () => defaultValue);

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
        public static TValue GetValueOrDefault<TKey, TValue>(this IVariantDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory) =>
            dictionary.ContainsKey(key) ? dictionary[key] : defaultValueFactory();


        public static bool TryGetValue<TKey, TValue>(this IVariantDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            var containsKey = dictionary.ContainsKey(key);
            value = containsKey ? dictionary[key] : default;
            return containsKey;
        }
        #endregion
    }
}
