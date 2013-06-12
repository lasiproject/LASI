using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;

using LASI.Algorithm.Thesauri;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides access to predefined and customizable IEqualityComparer implementations which operate on instances of applicable ILexical constructs.
    /// </summary>
    /// <typeparam name="T">Any type which implements the ILexical interface. E.g. Word or Phrase</typeparam>
    /// <see cref="ILexical"/>
    public static class Comparisons<T> where T : LASI.Algorithm.ILexical
    {
        private static TextualComparer textual = new TextualComparer();
        private static AliasComparer alias = new AliasComparer();
        private static SimilarityComparer<NounPhrase> similarity = new SimilarityComparer<NounPhrase>();
        private static AliasOrSimilarityComparer<NounPhrase> aliasOrSimilarity = new AliasOrSimilarityComparer<NounPhrase>();
        /// <summary>
        /// AliasComparer based comparer where if not textually equivalent if will check to see if the NounPhrases are aliases for each
        /// </summary>
        public static AliasComparer Alias
        {
            get
            {
                return alias;
            }
        }
        public static AliasOrSimilarityComparer<NounPhrase> AliasOrSimilarity
        {
            get
            {
                return aliasOrSimilarity;
            }
        }

        /// <summary>
        /// SimilarityComparer based comparer where if not textually equivalent if will check to see if the NounPhrases are similar tp eachother.
        /// </summary>
        public static SimilarityComparer<NounPhrase> Similarity
        {
            get
            {
                return similarity;
            }
        }
        /// <summary>
        /// Text based comparer which compares the literal text of two NounPhrases to see if they are Identical.
        /// All end comparers provided here perform literal text comparions implicitely.
        /// </summary>
        public static TextualComparer Textual
        {
            get
            {
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
        public static IEqualityComparer<T> CreateCustom(Func<T, T, bool> equals)
        {
            if (equals == null) {
                throw new ArgumentNullException("equals", "A null equals function was provided.");
            } else
                return new CustomComparer(equals);
        }
        ///// <summary>
        ///// Creates a custom IEqualityComparer comparer which uses the given functions to compare values and produce Hash codes.
        ///// </summary>
        ///// <param name="equals">The function to determine equality Equality.</param>
        ///// <param name="hasher">The function to generate Hash codes.</param>
        ///// <returns>A custom IEqualityComparer comparer which uses the given functions to compare values and produce Hash codes.
        ///// </returns>
        //public static IEqualityComparer<T> CreateCustom(Func<T, T, bool> equals, Func<T, int> getHashCode)
        //{
        //    if (equals == null) {
        //        throw new ArgumentNullException("equals", "A null equals function was provided.");
        //    } else
        //        return new CustomComparer(equals, getHashCode);
        //}

        #region Specialized IEqualityComparer implementations for various ILexical constructs



        public class TextualComparer : IEqualityComparer<T>
        {
            protected internal TextualComparer()
            {
            }
            public bool Equals(T x, T y)
            {
                return x.Text == y.Text;
            }

            public int GetHashCode(T obj)
            {
                return obj == null ? 0 : 1;
            }
        }
        public class SimilarityComparer<R> : IEqualityComparer<R>
            where R : NounPhrase
        {
            protected internal SimilarityComparer()
            {
            }
            public bool Equals(R x, R y)
            {
                return x.Text == y.Text || x.IsSimilarTo(y);

            }

            public int GetHashCode(R obj)
            {
                return obj == null ? 0 : 1;
            }
        }
        public class AliasComparer : IEqualityComparer<IEntity>
        {
            protected internal AliasComparer()
            {
            }
            public bool Equals(IEntity x, IEntity y)
            {
                return x.Text == y.Text || x.IsAliasFor(y);
            }

            public int GetHashCode(IEntity obj)
            {
                return obj == null ? 0 : 1;
            }


        }
        public class AliasOrSimilarityComparer<S> : IEqualityComparer<S>
            where S : Phrase, IEntity
        {
            protected internal AliasOrSimilarityComparer()
            {
            }
            public bool Equals(S x, S y)
            {
                return x.Text == y.Text || x.IsAliasFor(y) || (x as NounPhrase).IsSimilarTo(y as NounPhrase);
            }

            public int GetHashCode(S obj)
            {
                return obj == null ? 0 : 1;
            }
        }
        public class CustomComparer : IEqualityComparer<T>
        {
            private Func<T, T, bool> customEquals;
            private Func<T, int> customGetHashCode;
            protected internal CustomComparer(Func<T, T, bool> equals)
            {
                customEquals = equals;
                customGetHashCode = ((T obj) =>
                {
                    return obj == null ? 0 : 1;
                });
            }
            protected internal CustomComparer(Func<T, T, bool> equals, Func<T, int> hasher)
            {
                customEquals = equals;
                customGetHashCode = hasher;
            }
            public bool Equals(T x, T y)
            {
                return customEquals(x, y);
            }

            public int GetHashCode(T obj)
            {
                return customGetHashCode(obj);
            }
        }

        #endregion


    }
}
