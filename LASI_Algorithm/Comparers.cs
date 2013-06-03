using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;

using LASI.Algorithm.Thesauri;

namespace LASI.Algorithm
{

    public static class IEnumerableExt
    {


        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> elements, Func<T, T, bool> comparison) where T : ILexical
        {
            return elements.Distinct(Comparisons<T>.CreateCustom(comparison));

        }
        public static bool Contains<T>(this IEnumerable<T> elements, T element, Func<T, T, bool> comparison) where T : ILexical
        {
            return elements.Contains(element, Comparisons<T>.CreateCustom(comparison));

        }
    }
    public static class Comparisons<T> where T : LASI.Algorithm.ILexical
    {
        private static TextualComparer textual = new TextualComparer();
        private static AliasComparer<IAliasableEntity> alias = new AliasComparer<IAliasableEntity>();
        private static SimilarityComparer<NounPhrase> similarity = new SimilarityComparer<NounPhrase>();
        private static AliasOrSimilarityComparer<NounPhrase> aliasOrSimilarity = new AliasOrSimilarityComparer<NounPhrase>();

        ///// <summary>
        ///// AliasComparer based comparer where if not textually equivalent if will check to see if the NounPhrases are aliases for each
        ///// </summary>
        public static AliasComparer<IAliasableEntity> Alias
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
        /// All other comparers provided here perform literal text comparions implicitely.
        /// </summary>
        public static TextualComparer Textual
        {
            get
            {
                return textual;
            }
        }


        /// <summary>
        /// Creates a custom NounPhraes comparer which uses the given function to compare values and a null or not null only sensitive Hash function. 
        /// </summary>
        /// <param name="equals">The function to use to determine equality Equality.</param>
        /// <returns>A custom NounPhraes comparer which uses the given function to compare values and a null or not null only sensitive Hash function.
        /// </returns>
        public static IEqualityComparer<T> CreateCustom(Func<T, T, bool> equals)
        {

            return CreateCustom(equals, t => HashNounPhraseEquality(t));
        }
        /// <summary>
        /// Creates a custom NounPhraes comparer which uses the given functions to compare values and produce Hash codes.
        /// </summary>
        /// <param name="equals">The function to use to determine equality Equality.</param>
        /// <param name="getHashCode">The function to use to generate Hash codes.</param>
        /// <returns>A custom NounPhraes comparer which uses the given functions to compare values and produce Hash codes.
        /// </returns>
        public static IEqualityComparer<T> CreateCustom(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            if (equals == null) {
                throw new ArgumentNullException("equals", "A null equals function was provided.");
            } else
                return new CustomComparer<T>(equals, getHashCode);
        }

        private static int HashNounPhraseEquality(ILexical np)
        {
            return np != null ? 1 : 0;
        }

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
                return HashNounPhraseEquality(obj);
            }
        }
        public class SimilarityComparer<R> : IEqualityComparer<R>
            where R : NounPhrase, ISimilarityComparablePhrase<R>
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
                return HashNounPhraseEquality(obj);
            }
        }
        public class AliasComparer<S> : IEqualityComparer<S>
            where S : IAliasableEntity
        {
            protected internal AliasComparer()
            {
            }
            public bool Equals(S x, S y)
            {
                return x.Text == y.Text || x.IsAliasFor(y);
            }

            public int GetHashCode(S obj)
            {
                return HashNounPhraseEquality(obj);
            }


        }
        public class AliasOrSimilarityComparer<S> : IEqualityComparer<S>
            where S : Phrase, IEntity, IAliasableEntity
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
                return HashNounPhraseEquality(obj);
            }
        }
        public class CustomComparer<S> : IEqualityComparer<S> where S : T
        {
            private Func<S, S, bool> customEquals;
            private Func<T, int> customGetHashCode;
            protected internal CustomComparer(Func<S, S, bool> equals, Func<T, int> getHashCode = null)
            {
                customEquals = equals;
                customGetHashCode = getHashCode ?? ((T o) =>
                {
                    return o != null ? 1 : 0;
                });
            }
            public bool Equals(S x, S y)
            {
                return customEquals(x, y);
            }

            public int GetHashCode(S obj)
            {
                return customGetHashCode(obj);
            }
        }


    }
}
