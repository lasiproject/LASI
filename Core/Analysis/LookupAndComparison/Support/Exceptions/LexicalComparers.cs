using LASI;
using LASI.Core.Heuristics;
using LASI.Core.Patternization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Provides access to predefined and customizable IEqualityComparer implementations which operate on instances of applicable ILexical types.
    /// </summary>
    /// <see cref="ILexical"/>
    public static class LexicalComparers
    {


        /// <summary>
        /// Creates a custom IEqualityComparer which always uses the provided comparison function to compare instances. 
        /// This fully bypasses the hash checks for non null references.
        /// </summary>
        /// <param name="equals">The function to determine equality Equality.</param>
        /// <returns>A custom NounPhraes comparer which uses the given function to compare values and a null or not null only sensitive Hash function.
        /// </returns>
        /// <remarks>The intent of the functionality provided is to simplify
        /// the use of LINQ methods, such as IEnumerable.Contains(item) and IEnumerable.Distinct() 
        /// which require an IEqualityComparer as opposed to a simple predicate function.
        /// Because the custom comparer created bypasses hash code equality assumptions, it allows for these methods to behave more transparently.
        /// This however means that the overhead of using the returned comparer in a LINQ query is substantial. 
        /// Specifically, the complexity of a calling IEnumerable.Contains(CreateCustom(Func)) or IEnumerable.Distinct(CreateCustom(Func))
        /// approaches O(N^2), where as calls which use the default reference based, hash if possible comparers, IEqualityComparers only approach approach O(N).
        /// </remarks>
        public static IEqualityComparer<TLexical> Create<TLexical>(Func<TLexical, TLexical, bool> equals) where TLexical : ILexical
        {
            if (equals == null)
                throw new ArgumentNullException("equals", "A null equals function was provided.");
            return CustomComparer<TLexical>.Create(equals);
        }
        /// <summary>
        /// Gets a IEqualityComparer&lt;ILexical&gt; which uses a default, case-sensitive textual matching function.
        /// </summary>
        public static IEqualityComparer<ILexical> Textual
        {
            get { return CustomComparer<ILexical>.Create((l1, l2) => l1.Text == l2.Text, l => l.Text.GetHashCode()); }
        }
    }

    /// <summary>
    /// A Custom EqualityComparer whose equals and hashing functions are supplied as constructor arguments.
    /// </summary>
    class CustomComparer<T> : EqualityComparer<T> where T : ILexical
    {
        private Func<T, T, bool> equals;
        private Func<T, int> getHashCode;
        /// <summary>
        /// Initializes a new instance of the CustomComparer class conforming to the logic of the provided equals function.
        /// </summary>
        /// <param name="equals">The function used test two elements for equality.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided equality function is null.</exception>
        private CustomComparer(Func<T, T, bool> equals)
        {
            this.equals = equals;
            getHashCode = ((T obj) =>
            {
                return obj == null ? 0 : 1;
            });
        }
        /// <summary>
        /// Initializes a new instance of the CustomComparer class conforming to the logic of the provided equals and getHashCode functions.
        /// </summary>
        /// <param name="equals">The function used test two elements for equality.</param>
        /// <param name="getHashCode">The function to extract a hash code from each element.</param>
        /// <exception cref="ArgumentNullException">Thrown if either the provided equality or the provided getHashCode functions is null.</exception>
        private CustomComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            if (equals == null)
                throw new ArgumentNullException("equals", "A null equals function was provided.");
            this.equals = equals;
            if (getHashCode == null)
                throw new ArgumentNullException("getHashCode", "A null getHashCode function was provided.");
            this.getHashCode = getHashCode;
        }

        /// <summary>
        /// Determines whether the specified IEntity implementors are equal based the comparison function around which the CustomComparer was constructed.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public override bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);
            else
                return equals(x, y);
        }
        /// <summary>
        /// Always returns 1 unless the given object is null in which case 0 is returned.
        /// </summary>
        /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
        /// <returns>Always 1 unless the given object is null in which case 0 is returned.</returns>
        public override int GetHashCode(T obj)
        {
            return getHashCode(obj);
        }
        #region Static Factory Methods
        /// <summary>
        /// Creates a custom IEqualityComparer which always uses the provided comparison function to compare instances. 
        /// This fully bypasses the hash checks for non null references.
        /// </summary>
        /// <param name="equals">The function to determine equality Equality.</param>
        /// <returns>A custom comparer which uses the given function to compare values and a null or not null only sensitive Hash function.
        /// </returns>
        /// <remarks>The intent of the functionality provided is to simplify
        /// the use of LINQ methods, such as IEnumerable.Contains(item) and IEnumerable.Distinct() 
        /// which require an IEqualityComparer as opposed to a simple predicate function.
        /// Because the custom comparer created bypasses hash code equality assumptions, it allows for these methods to behave more transparently.
        /// This however means that the overhead of using the returned comparer in a LINQ query is substantial. 
        /// Specifically, the complexity of a calling IEnumerable.Contains(CreateCustom(Func)) or IEnumerable.Distinct(CreateCustom(Func))
        /// approaches O(N^2), where as calls which use the default reference based, hash if possible comparers, IEqualityComparers only approach approach O(N).
        /// </remarks>
        public static CustomComparer<T> Create(Func<T, T, bool> equals)
        {
            return new CustomComparer<T>(equals);
        }
        public static CustomComparer<T> Create(Func<T, T, bool> equals, Func<T, int> hashing)
        {
            return new CustomComparer<T>(equals, hashing);
        }

        #endregion
    }

}
