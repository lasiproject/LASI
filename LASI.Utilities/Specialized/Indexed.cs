using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities.SpecializedResultTypes
{
    /// <summary>
    /// Serves as a factory for instances of the <see cref="Indexed{T}"/> structure.
    /// </summary>
    public static class Indexed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Indexed{T}"/> structure with the specified value and index.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="element">The value.</param>
        /// <param name="index">The index.</param>
        /// <returns>A new instance of the <see cref="Indexed{T}"/> structure with the specified value and index.</returns>
        public static Indexed<T> Create<T>(T element, int index) => new Indexed<T>(element, index);
    }
    /// <summary>
    /// Represents a pairing of an element with its index in some ordering of some data structure.
    /// </summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    public struct Indexed<T> : IEquatable<Indexed<T>>
    {
        internal Indexed(T element, int index)
        {
            Element = element;
            Index = index;
        }
        /// <summary>
        /// Gets the element.
        /// </summary>
        public T Element { get; }
        /// <summary>
        /// Gets the element's index.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Tests the current <see cref="Indexed{T}"/> instance for equality with another.
        /// </summary>
        /// <param name="other">The <see cref="Indexed{T}"/> to compare with the current instance/</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public bool Equals(Indexed<T> other) => Index == other.Index && (Element?.Equals(other.Element) ?? other.Element == null);
        /// <summary>
        /// Tests the current <see cref="Indexed{T}"/> instance for equality with the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) => obj is Indexed<T> && Equals((Indexed<T>)obj);
        /// <summary>
        /// Gets a hash code for the <see cref="Indexed{T}"/> computed from its value and index.
        /// </summary>
        /// <returns>A hashcode for the <see cref="Indexed{T}"/>.</returns>
        public override int GetHashCode() => Index ^ Element.GetHashCode();
        /// <summary>
        /// Tests two <see cref="Indexed{T}"/> instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns><c>true</c> if the specified instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Indexed<T> left, Indexed<T> right) => left.Equals(right);
        /// <summary>
        /// Tests two <see cref="Indexed{T}"/> instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns><c>true</c> if the specified instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Indexed<T> left, Indexed<T> right) => !(left == right);
    }
}
