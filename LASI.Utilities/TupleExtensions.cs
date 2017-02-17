using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public static class TupleExtensions
    {
        public static KeyValuePair<K, V> ToKeyValuePair<K, V>(this (K key, V value) t) => new KeyValuePair<K, V>(t.key, t.value);
        //public static void Deconstruct<T>(this (T x, T y, T z) t, out IEnumerable<T> result) => result = new[] { t.x, t.y, t.z };
        public static IEnumerator<T> GetEnumerator<T>(this ValueTuple<T, T> p)
        {
            var (x, y) = p;
            yield return x;
            yield return y;
        }
        public static IEnumerator<T> GetEnumerator<T>(this ValueTuple<T, T, T> p)
        {
            var (x, y, z) = p;
            yield return x;
            yield return y;
            yield return z;
        }
        public static IEnumerator<T> GetEnumerator<T>(this ValueTuple<T, T, T, T> p)
        {
            var (a, b, c, d) = p;
            yield return a;
            yield return b;
            yield return c;
            yield return d;
        }
        public static IEnumerator<T> GetEnumerator<T>(this ValueTuple<T, T, T, T, T> p)
        {
            var (a, b, c, d, e) = p;
            yield return a;
            yield return b;
            yield return c;
            yield return d;
            yield return e;
        }
        public static IEnumerator<T> GetEnumerator<T>(this ValueTuple<T, T, T, T, T, T> p)
        {
            var (a, b, c, d, e, f) = p;
            yield return a;
            yield return b;
            yield return c;
            yield return d;
            yield return e;
            yield return f;
        }
        public static IEnumerator<T> GetEnumerator<T>(this ValueTuple<T, T, T, T, T, T, T> p)
        {
            var (a, b, c, d, e, f, g) = p;
            yield return a;
            yield return b;
            yield return c;
            yield return d;
            yield return e;
            yield return f;
            yield return g;
        }
    }

    public class EnumerableTupleAdapter<T> : IEnumerable<T>
    {
        readonly IEnumerable<T> elements;
        EnumerableTupleAdapter(IEnumerable<T> elements) => this.elements = elements;

        public IEnumerator<T> GetEnumerator() => elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => elements.GetEnumerator();

        private static IEnumerable<T> Enumerate(IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        public static implicit operator EnumerableTupleAdapter<T>((T, T, T) triple) => new EnumerableTupleAdapter<T>(Enumerate(triple.GetEnumerator()));
        public static implicit operator EnumerableTupleAdapter<T>((T, T, T, T) triple) => new EnumerableTupleAdapter<T>(Enumerate(triple.GetEnumerator()));
        public static implicit operator EnumerableTupleAdapter<T>((T, T, T, T, T) triple) => new EnumerableTupleAdapter<T>(Enumerate(triple.GetEnumerator()));
        public static implicit operator EnumerableTupleAdapter<T>((T, T, T, T, T, T) triple) => new EnumerableTupleAdapter<T>(Enumerate(triple.GetEnumerator()));
        public static implicit operator EnumerableTupleAdapter<T>((T, T, T, T, T, T, T) triple) => new EnumerableTupleAdapter<T>(Enumerate(triple.GetEnumerator()));
    }
}
