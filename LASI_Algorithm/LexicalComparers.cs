using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;

using LASI.Algorithm.Lookup;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides access to predefined and customizable IEqualityComparer implementations which operate on instances of applicable ILexical constructs.
    /// </summary>
    /// <typeparam name="T">Any Type which implements the ILexical interface. E.g. Word or Phrase</typeparam>
    /// <see cref="ILexical"/>
    public static class LexicalComparers<T> where T : LASI.Algorithm.ILexical
    {
        private static TextualComparer textual = new TextualComparer();
        private static AliasComparer alias = new AliasComparer();
        private static SimilarityComparer<IEntity> similarity = new SimilarityComparer<IEntity>();
        private static AliasOrSimilarityComparer<IEntity> aliasOrSimilarity = new AliasOrSimilarityComparer<IEntity>();
        /// <summary>
        /// Alias based comparer where if not textually equivalent if will check to see if the NounPhrases are aliases for each other
        /// </summary>
        public static AliasComparer Alias {
            get {
                return alias;
            }
        }
        public static AliasOrSimilarityComparer<IEntity> AliasOrSimilarity {
            get {
                return aliasOrSimilarity;
            }
        }

        ///// <summary>
        ///// SimilarityComparer based comparer where if not textually equivalent if will check to see if the NounPhrases are similar tp eachother.
        ///// </summary>
        //public static SimilarityComparer<NounPhrase> Similarity {
        //    get {
        //        return similarity;
        //    }
        //}
        /// <summary>
        /// Text based comparer which compares the key text of two NounPhrases to see if they are Identical.
        /// All end comparers provided here perform key text comparions implicitely.
        /// </summary>
        public static TextualComparer Textual {
            get {
                return textual;
            }
        }


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
        public static IEqualityComparer<T> CreateCustom(Func<T, T, bool> equals) {
            return new CustomComparer(equals);
        }


        #region Specialized IEqualityComparer implementations for various ILexical constructs


        /// <summary>
        /// Basic, naive comparer that only takes into account the Text property values of ILexical instances.
        /// </summary>
        public class TextualComparer : IEqualityComparer<T>
        {
            protected internal TextualComparer() {
            }
            public bool Equals(T x, T y) {
                return x.Text == y.Text;
            }

            public int GetHashCode(T obj) {
                return obj.Text.GetHashCode();
            }
        }
        public class SimilarityComparer<R> : IEqualityComparer<R>
            where R : IEntity
        {
            protected internal SimilarityComparer() {
            }
            public bool Equals(R x, R y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return x.Text == y.Text || x.IsSimilarTo(y);

            }

            public int GetHashCode(R obj) {
                return obj == null ? 0 : 1;
            }
        }
        public class AliasComparer : IEqualityComparer<IEntity>
        {
            protected internal AliasComparer() {
            }
            public bool Equals(IEntity x, IEntity y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return x.Text == y.Text || x.IsAliasFor(y);
            }

            public int GetHashCode(IEntity obj) {
                return obj == null ? 0 : 1;
            }


        }
        public class AliasOrSimilarityComparer<S> : IEqualityComparer<S>
            where S : IEntity
        {
            protected internal AliasOrSimilarityComparer() {
            }
            public bool Equals(S x, S y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return x.Text == y.Text || x.IsAliasFor(y) || (x).IsSimilarTo(y);
            }

            public int GetHashCode(S obj) {
                return obj == null ? 0 : 1;
            }
        }
        public class CustomComparer : IEqualityComparer<T>
        {
            private Func<T, T, bool> customEquals;
            private Func<T, int> customGetHashCode;
            /// <summary>
            /// Initializes a new instance of the CustomComparer class conforming to the logic of the provided equals function.
            /// </summary>
            /// <param name="equals">The function used test two elements for equality.</param>
            /// <exception cref="ArgumentNullException">Thrown if the provided equality function is null.</exception>
            protected internal CustomComparer(Func<T, T, bool> equals) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                customEquals = equals;
                customGetHashCode = ((T obj) => {
                    return obj == null ? 0 : 1;
                });
            }
            /// <summary>
            /// Initializes a new instance of the CustomComparer class conforming to the logic of the provided equals and getHashCode functions.
            /// </summary>
            /// <param name="equals">The function used test two elements for equality.</param>
            /// <param name="getHashCode">The function to extract a hash code from each element.</param>
            /// <exception cref="ArgumentNullException">Thrown if either the provided equality or the provided getHashCode functions is null.</exception>
            protected internal CustomComparer(Func<T, T, bool> equals, Func<T, int> getHashCode) {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                customEquals = equals;
                if (getHashCode == null)
                    throw new ArgumentNullException("getHashCode", "A null equals function was provided.");
                customGetHashCode = getHashCode;
            }
            public bool Equals(T x, T y) {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return customEquals(x, y);
            }

            public int GetHashCode(T obj) {
                return customGetHashCode(obj);
            }
        }

        #endregion


    }
}
