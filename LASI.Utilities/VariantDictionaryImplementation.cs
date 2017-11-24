using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace LASI.Utilities {
    internal class VariantDictionaryImplementation<Tkey, TValue> : IVariantDictionary<Tkey, TValue> {
        public VariantDictionaryImplementation(IReadOnlyDictionary<Tkey, TValue> wrapped) {
            represenation = wrapped;
        }

        public IEnumerable<TValue> Values => represenation.Values;

        public int Count => represenation.Count;

        public TValue this[Tkey key] => represenation[key];

        public bool ContainsKey(Tkey key) => represenation.ContainsKey(key);

        public IEnumerator<IVariantKeyValuePair<Tkey, TValue>> GetEnumerator() {
            foreach (var pair in represenation.Select(KeyValuePair.Create)) {
                yield return pair;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool TryGetValue(Tkey key, out TValue value) {
            return represenation.TryGetValue(key, out value);
        }
        private readonly IReadOnlyDictionary<Tkey, TValue> represenation;

        private struct KeyValuePair : IVariantKeyValuePair<Tkey, TValue>, IEquatable<KeyValuePair> {
            public static KeyValuePair Create(KeyValuePair<Tkey, TValue> from) => new KeyValuePair(from.Key, from.Value);

            private KeyValuePair(Tkey key, TValue value) { Key = key; Value = value; }

            public Tkey Key { get; private set; }
            public TValue Value { get; private set; }

            public bool Equals(KeyValuePair other) => Equals(other as IVariantKeyValuePair<Tkey, TValue>);
        }
    }
}
