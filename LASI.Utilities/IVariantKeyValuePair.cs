using System;
using System.Collections.Generic;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines a key/value pair that is is variant in the value.
    /// </summary>
    public interface IVariantKeyValuePair<TKey, out TValue>
    {
        /// <summary>
        /// The key component of the variant key/value pair.
        /// </summary>
        TKey Key { get;  }
        /// <summary>
        /// The value component of the variant key/value pair.
        /// </summary>
        TValue Value { get; }
    }
}