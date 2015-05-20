using System;
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
        #region IDictionary Extensions

        /// <summary>
        /// Gets the value with the specified key from the
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or default(TValue) if the
        /// key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <returns>
        /// The value with the specified key from the
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or default(TValue) if the
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
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the specified defaultValue
        /// if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValue">
        /// The value to return if the specified key is not present in the
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the specified defaultValue
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
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the result of invoking the
        /// specified defaultValueFactory function if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; from which to retrieve a value.
        /// </param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValueFactory">
        /// The function to create a default value if the specified key is not present in the
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </param>
        /// <returns>
        /// The value with the specified key from the
        /// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the result of invoking the
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
        /// The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;.
        /// </typeparam>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; to enumerate.
        /// </param>
        /// <param name="action">
        /// The action to perform on each
        /// <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}" /> in the <see cref="IDictionary{TKey, TValue}" />.
        /// </param>
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            foreach (var keyValuePair in dictionary)
            {
                action(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static IDictionary<TKey, Indexed<TValue>> WithIndex<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) =>
            dictionary
                .Select((entry, index) => new KeyValuePair<TKey, Indexed<TValue>>(entry.Key, Indexed.Create(entry.Value, index)))
                .ToDictionary();

        #endregion IDictionary Extensions
    }
}