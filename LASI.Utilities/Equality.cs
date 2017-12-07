using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    /// <summary>
    /// Provides static methods for the creation of <see cref="IEqualityComparer{T}"/> instances.
    /// </summary>
    public static class Equality
    {
        /// <summary>
        /// Creates an <see cref="IEqualityComparer{T}"/> which will use the provided function to
        /// determine equality, and define hash codes purely in terms of nullity.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="equals">A function which determines if two objects of type T are equal.</param>
        /// <exception cref="ArgumentNullException">
        /// The provided <paramref name="equals"/> function is null.
        /// </exception>
        /// <returns>
        /// An <see cref="IEqualityComparer{T}"/> which will use the provided function to determine
        /// equality, and defines hash codes purely in terms of nullity.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The intent of the functionality provided is to simplify the use of LINQ methods, such as
        /// <see cref="Enumerable.Contains{TSource}(IEnumerable{TSource}, TSource, IEqualityComparer{TSource})"/>
        /// and <see cref="Enumerable.Distinct{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/> which require an
        /// IEqualityComparer as opposed to a simple predicate function. Because the custom comparer
        /// created produces identical hashcodes for non null objects, it allows for these methods
        /// to behave more transparently, but introduced. This however means that the overhead of
        /// using the returned comparer in a LINQ query is substantial. Specifically, the complexity
        /// of a calling methods such as IEnumerable&lt;T&gt;.Contains(item, (x, y) =&gt; x == y)
        /// and IEnumerable&lt;T&gt;.Distinct((x, y) =&gt; x == y) approaches O(N^2). Comparatively,
        /// calls which use the default equality comparers, including those that do not take an
        /// <see cref="IEqualityComparer{T}"/> only approach O(N).
        /// </para>
        /// </remarks>
        /// <seealso cref="Enumerable.Distinct{TSource}(IEnumerable{TSource})"/>
        /// <seealso cref="Enumerable.Distinct{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
        /// <seealso cref="Enumerable.Except{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
        /// <seealso cref="Enumerable.Except{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>
        /// <seealso cref="Enumerable.Intersect{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
        /// <seealso cref="Enumerable.Intersect{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>
        /// <seealso cref="Enumerable.Contains{TSource}(IEnumerable{TSource}, TSource, IEqualityComparer{TSource})"/>
        /// <seealso cref="Enumerable.SequenceEqual{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
        /// <seealso cref="Enumerable.SequenceEqual{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>
        /// <seealso cref="EnumerableExtensions.SetEqual{T}(IEnumerable{T}, IEnumerable{T})"/>
        /// <seealso cref="EnumerableExtensions.SetEqual{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
        public static IEqualityComparer<T> Create<T>(Func<T, T, bool> equals)
        {
            Validate.NotNull(equals, nameof(equals));
            return new ComparerWithCutomEqualsAndNullityBasedHashing<T>(equals);
        }

        /// <summary>
        /// Creates a new <see cref="IEqualityComparer{T}"/> which will use the provided equality
        /// and hashing functions.
        /// </summary>
        /// <param name="equals">A function which determines if two objects of type T are equal.</param>
        /// <param name="getHashCode">
        /// A function which generates a hash code from an element of type T.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The provided <paramref name="equals"/> or <paramref name="getHashCode"/> function is null.
        /// </exception>
        /// <returns>
        /// A new <see cref="IEqualityComparer{T}"/> which will use the provided equality and
        /// hashing functions.
        /// </returns>
        /// <remarks>
        /// Proper usage requires that elements which will compare equal under the specified equals
        /// function will also produce identical hashcodes. Elements may yield identical hash codes,
        /// without being considered equal.
        /// </remarks>
        public static IEqualityComparer<T> Create<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            Validate.NotNull(equals, nameof(equals));
            Validate.NotNull(getHashCode, nameof(getHashCode));
            return new ComparerWithCutomEqualsAndGetHashCode<T>(equals, getHashCode);
        }

        sealed class ComparerWithCutomEqualsAndNullityBasedHashing<T> : EqualityComparer<T>
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ComparerWithCutomEqualsAndNullityBasedHashing{T}"/> class which will use
            /// the provided function to define element equality.
            /// </summary>
            /// <param name="equals">
            /// A function which determines if two objects of type T are equal.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// The provided <paramref name="equals"/> function is null.
            /// </exception>
            /// <remarks>
            /// A custom hashing function is automatically provided, ensuring that equality
            /// comparisons take place except when reference is null. While this provides clean,
            /// customizable semantics for set operations, more expensive to use having a complexity
            /// of N^2
            /// </remarks>
            public ComparerWithCutomEqualsAndNullityBasedHashing(Func<T, T, bool> equals)
            {
                this.equals = equals;
            }

            /// <summary>Determines whether two objects of type T are equal.</summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns><c>true</c> if the specified objects are equal; otherwise, <c>false</c>.</returns>
            public override bool Equals(T x, T y) => equals(x, y);

            /// <summary>
            /// Serves as a hash function for the specified object for hashing algorithms and data
            /// structures, such as a hash table.
            /// </summary>
            /// <param name="obj">The object for which to get a hash code.</param>
            /// <returns>A hash code for the specified object.</returns>
            public override int GetHashCode(T obj) => Default.Equals(obj, default) ? 0 : 1;

            readonly Func<T, T, bool> equals;
        }

        /// <summary>
        /// An EqualityComparer&lt;T&gt; whose Equals and GetHashCode implementations are specified
        /// upon construction.
        /// </summary>
        /// <typeparam name="T">The type of objects to compare.</typeparam>
        /// <remarks>
        /// <para>
        /// The primary purpose of this class is to allow for the ad hoc, inline creation of an
        /// IEqualityComparer&lt;T&gt; from arbitrary functions. This allows for the easy use of
        /// Query Operators taking an IEqualityComparer&lt;T&gt; as an argument.
        /// </para>
        /// <para>
        /// Proper usage requires that elements which will compare equal under the specified equals
        /// function will also produce identical hashcodes. Elements may yield identical hash codes,
        /// without being considered equal.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// An equality comparer which makes determinations based on the specified comparison function.
        /// var fuzzilyDistinctNps = nps.Distinct(new CustomComparer&lt;Phrase&gt;((x, y) =&gt; x.IsSimilarTo(y));
        /// </code>
        /// <code>
        /// // An equality comparer which makes determinations based on the specified comparison and hashing functions.
        /// var fuzzilyDistinctNps = nps.Distinct(new CustomComparer&lt;Phrase&gt;((x, y) =&gt; x.IsSimilarTo(y), x =&gt; x == null? 0 : 1);
        /// </code>
        /// </example>
        sealed class ComparerWithCutomEqualsAndGetHashCode<T> : EqualityComparer<T>
        {
            /// <summary>
            /// Initializes a new instance of the CustomComparer class which will use the provided
            /// equality and hashing functions.
            /// </summary>
            /// <param name="equals">
            /// A function which determines if two objects of type T are equal.
            /// </param>
            /// <param name="getHashCode">
            /// A function which generates a hash code from an element of type T.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// The provided <paramref name="equals"/> or <paramref name="getHashCode"/> function is null.
            /// </exception>
            /// <remarks>
            /// Proper usage requires that elements which will compare equal under the specified
            /// equals function will also produce identical hashcodes. Elements may yield identical
            /// hash codes, without being considered equal.
            /// </remarks>
            public ComparerWithCutomEqualsAndGetHashCode(Func<T, T, bool> equals, Func<T, int> getHashCode)
            {
                this.equals = equals;
                this.getHashCode = getHashCode;
            }

            /// <summary>Determines whether two objects of type T are equal.</summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns><c>true</c> if the specified objects are equal; otherwise, <c>false</c>.</returns>
            public override bool Equals(T x, T y) => ReferenceEquals(x, null) ? ReferenceEquals(y, null) : equals(x, y);

            /// <summary>
            /// Serves as a hash function for the specified object for hashing algorithms and data
            /// structures, such as a hash table.
            /// </summary>
            /// <param name="obj">The object for which to get a hash code.</param>
            /// <returns>A hash code for the specified object.</returns>
            public override int GetHashCode(T obj) => getHashCode(obj);

            readonly Func<T, T, bool> equals;
            readonly Func<T, int> getHashCode;
        }
    }
}