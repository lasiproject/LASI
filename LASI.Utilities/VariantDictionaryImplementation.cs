using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using LASI.Utilities.Validation;
using static System.Linq.Enumerable;

namespace LASI.Utilities
{
    class VariantDictionaryImplementation<Tkey, TValue> : IVariantDictionary<Tkey, TValue>, IEnumerable<(Tkey key, TValue value)>
    {
        public VariantDictionaryImplementation(IEnumerable<KeyValuePair<Tkey, TValue>> wrapped) : this(wrapped.Tupled()) { }
        public VariantDictionaryImplementation(IEnumerable<(Tkey key, TValue value)> wrapped)
        {
            represenation = new Dictionary<Tkey, TValue>(wrapped.ToDictionary(x => x.key, x => x.value));
        }

        public int Count => represenation.Count;

        public IEnumerable<Tkey> Keys => represenation.Keys;

        public TValue this[Tkey key] => represenation[key];

        public bool ContainsKey(Tkey key) => represenation.ContainsKey(key);

        public IEnumerator<IVariantKeyValuePair<Tkey, TValue>> GetEnumerator()
        {
            foreach (var pair in represenation.Select(KeyValuePair.Create))
            {
                yield return pair;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<(Tkey key, TValue value)> IEnumerable<(Tkey key, TValue value)>.GetEnumerator() => represenation.Tupled().GetEnumerator();

        readonly IReadOnlyDictionary<Tkey, TValue> represenation;

        struct KeyValuePair : IVariantKeyValuePair<Tkey, TValue>, IEquatable<KeyValuePair>
        {
            public static KeyValuePair Create(KeyValuePair<Tkey, TValue> from) => new KeyValuePair(from.Key, from.Value);

            KeyValuePair(Tkey key, TValue value) { Key = key; Value = value; }

            public Tkey Key { get; }
            public TValue Value { get; }

            public bool Equals(KeyValuePair other) => Key?.Equals(other.Key) is true && Value?.Equals(other.Value) is true;

            public override int GetHashCode()
            {
                var hashCode = 206514262;
                hashCode = hashCode * -1521134295 + base.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<Tkey>.Default.GetHashCode(Key);
                hashCode = hashCode * -1521134295 + EqualityComparer<TValue>.Default.GetHashCode(Value);
                return hashCode;
            }

            public override bool Equals(object obj) => obj is KeyValuePair pair && Equals(pair);

            public static bool operator ==(KeyValuePair pair1, KeyValuePair pair2) => pair1.Equals(pair2);
            public static bool operator !=(KeyValuePair pair1, KeyValuePair pair2) => !(pair1 == pair2);

            public static implicit operator KeyValuePair((Tkey key, TValue value) pair) => new KeyValuePair(pair.key, pair.value);
        }
    }
}