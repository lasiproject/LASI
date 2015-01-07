using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    using Validator = Validator;
    /// <summary>
    /// Provides various methods for Dictionary types.
    /// </summary>
    public static class DictionaryExtensions
    {
        #region IDictionary Extensions
        /// <summary>
        /// Gets the value with the specified key from the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the default(TValue) if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</typeparam>
        /// <typeparam name="TValue">The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</typeparam>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; from which to retrieve a value.</param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <returns>
        /// The value with the specified key from the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the default(TValue) if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            Validator.ThrowIfNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }

        /// <summary>
        /// Gets the value with the specified key from the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the specified defaultValue if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</typeparam>
        /// <typeparam name="TValue">The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</typeparam>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; from which to retrieve a value.</param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValue">The value to return if the specified key is not present in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</param>
        /// <returns>
        /// The value with the specified key from the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the specified defaultValue if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
            Validator.ThrowIfNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Gets the value with the specified key from the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the result of invoking the specified defaultValueFactory function if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</typeparam>
        /// <typeparam name="TValue">The type of the values in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</typeparam>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; from which to retrieve a value.</param>
        /// <param name="key">The key for which to retrieve a value.</param>
        /// <param name="defaultValueFactory">The function to create a default value if the specified key is not present in the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;</param>
        /// <returns>
        /// The value with the specified key from the System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; or the result of invoking the specified defaultValueFactory function if the key does not exist.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory) {
            Validator.ThrowIfNull(dictionary, "dictionary", key, "key");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
        }
        #endregion
    }
}
