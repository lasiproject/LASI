using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities.SpecializedResultTypes
{
    public static class Indexed
    {
        public static Indexed<T> Create<T>(T element, int index) => new Indexed<T>(element, index);
    }
    public struct Indexed<T> : IEquatable<Indexed<T>>
    {
        internal Indexed(T element, int index)
        {
            Element = element;
            Index = index;
        }

        public T Element { get; }

        public int Index { get; }

        public bool Equals(Indexed<T> other) => Index == other.Index && (Element?.Equals(other.Element) ?? other.Element == null);

        public override bool Equals(object obj) => obj is Indexed<T> && Equals((Indexed<T>)obj);

        public override int GetHashCode() => Index ^ Element.GetHashCode();

        public static bool operator ==(Indexed<T> left, Indexed<T> right) => left.Equals(right);

        public static bool operator !=(Indexed<T> left, Indexed<T> right) => !(left == right);
    }
}
