using System.Collections.Generic;

namespace LASI.Utilities
{
    public static class KeyValuePairExtensions
    {
        public static void Deconstruct<TKey, TValue>(
            this KeyValuePair<TKey, TValue> pair,
            out TKey key,
            out TValue value) =>
                           (key, value) = (pair.Key, pair.Value);

        public static void Deconstruct<TKey, TValue>(
            this IVariantKeyValuePair<TKey, TValue> pair,
            out TKey key,
            out TValue value) =>
                           (key, value) = (pair.Key, pair.Value);
    }
}