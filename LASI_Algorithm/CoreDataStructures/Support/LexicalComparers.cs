using LASI;
using LASI.Algorithm.LexicalLookup;
using LASI.Algorithm.Aliasing;
using LASI.Algorithm.Patternization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides access to predefined and customizable IEqualityComparer implementations which operate on instances of applicable ILexical constructs.
    /// </summary>
    /// <typeparam name="TLexical">Any Type which implements the ILexical interface. E.g. Word or Phrase</typeparam>
    /// <see cref="ILexical"/>
    public static class LexicalComparers<TLexical> where TLexical : LASI.Algorithm.ILexical
    {
        private static TextualComparer textual = new TextualComparer();
        private static AliasComparer alias = new AliasComparer();
        private static SimilarityComparer similarity = new SimilarityComparer();
        private static AliasOrSimilarityComparer<IEntity> aliasOrSimilarity = new AliasOrSimilarityComparer<IEntity>();
        /// <summary>
        /// Alias based comparer where if not textually equivalent if will check to see if the Entities are aliases for each other.
        /// All of comparers provided here perform textual comparions implicitely.
        /// Because the GetHashCode implementation of the comparer forces collisions, the comparer is suitable for use with Standard Query operators such as Distinct, Contains, Union, Interset, Except, and SequenceEqual. 
        /// </summary>
        public static AliasComparer Alias
        {
            get
            {
                return (typeof(TLexical).GetInterfaces()).Contains(typeof(IEntity)) ? alias : null;

            }
        }
        /// <summary>
        /// Gets an IEntity comparer object which compares based on currently defined Aliases and Similarity comparisons,  as opposed to strict textual or reference equality.
        /// All of comparers provided here perform textual comparions implicitely.
        /// Because the GetHashCode implementation of the comparer forces collisions, the comparer is suitable for use with Standard Query operators such as Distinct, Contains, Union, Interset, Except, and SequenceEqual. 
        /// </summary>
        public static AliasOrSimilarityComparer<IEntity> AliasOrSimilarity
        {
            get
            {
                return aliasOrSimilarity;
            }
        }

        /// <summary>
        /// SimilarityComparer based comparer where if not textually equivalent if will check to see if the NounPhrases are similar tp eachother.
        /// All of comparers provided here perform textual comparions implicitely.
        /// Because the GetHashCode implementation of the comparer forces collisions, the comparer is suitable for use with Standard Query operators such as Distinct, Contains, Union, Interset, Except, and SequenceEqual. 
        /// </summary>
        public static SimilarityComparer Similarity
        {
            get
            {
                return similarity;
            }
        }
        /// <summary>
        /// Text based comparer which compares the key textual of two NounPhrases to see if they are Identical.
        /// All of comparers provided here perform textual comparions implicitely.
        /// /// Because the GetHashCode implementation of the comparer forces collisions, the comparer is suitable for use with Standard Query operators such as Distinct, Contains, Union, Interset, Except, and SequenceEqual. 
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
        public static CustomComparer<TLexical> CreateCustom(Func<TLexical, TLexical, bool> equals)
        {
            return new CustomComparer<TLexical>(equals);
        }
        public static CustomComparer<TLexical> CreateCustom(Func<TLexical, bool> equals)
        {
            return new CustomComparer<TLexical>((x, y) => equals(y));
        }

        #region Specialized IEqualityComparer implementations for various ILexical constructs


        /// <summary>
        /// Basic, naive comparer that only takes into account the Text property values of ILexical instances.
        /// </summary>
        public class TextualComparer : EqualityComparer<TLexical>
        {
            /// <summary>
            /// Initializes a new isntance of the TextualComparer class.
            /// </summary>
            protected internal TextualComparer()
            {
            }
            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <param name="x">The first object of type T to compare.</param>
            /// <param name="y">The second object of type T to compare.</param>
            /// <returns>true if the specified objects are equal; otherwise, false.</returns>
            public override bool Equals(TLexical x, TLexical y)
            {
                return x.Text == y.Text;
            }

            /// <summary>
            /// Always returns 1 unless the given object is null in which case 0 is returned.
            /// </summary>
            /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
            /// <returns>Always 1 unless the given object is null in which case 0 is returned.</returns>
            public override int GetHashCode(TLexical obj)
            {
                return obj.Text.GetHashCode();
            }
        }
        /// <summary>
        /// An IEquality Comparer implementation using a: Similarity implies Equality definition.
        /// </summary> 
        public class SimilarityComparer : EqualityComparer<TLexical>
        {
            /// <summary>
            /// Initializes a new isntance of the SimilarityComparer class.
            /// </summary>
            protected internal SimilarityComparer()
            {
            }
            /// <summary>
            /// Determines whether the specified IEntity implementors are equal based on wether or not they are considered similar by the associated LexicalLookup logic.
            /// </summary>
            /// <param name="x">The first object of type T to compare.</param>
            /// <param name="y">The second object of type T to compare.</param>
            /// <returns>true if the specified objects are equal; otherwise, false.</returns>
            public override bool Equals(TLexical x, TLexical y)
            {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else {
                    Func<bool> tc = () => x.Text == y.Text;
                    return x.Match().Yield<bool>()
                           .Case<IEntity>(X => y.Match().Yield<bool>()
                                   .Case<IEntity>(Y => tc() || X.IsSimilarTo(Y))
                               .Result())
                           .Case<IVerbal>(X => y.Match().Yield<bool>()
                                   .Case<IVerbal>(Y => tc() || X.IsSimilarTo(Y))
                               .Result())
                           .Case<IDescriptor>(X => y.Match().Yield<bool>()
                                   .Case<IDescriptor>(Y => tc() || X.IsSimilarTo(Y))
                               .Result())
                           .Case<IAdverbial>(X => y.Match().Yield<bool>()
                                   .Case<IAdverbial>(Y => tc() || X.IsSimilarTo(Y))
                               .Result())
                            .Result();
                }
            }

            /// <summary>
            /// Always returns 1 unless the given object is null in which case 0 is returned.
            /// </summary>
            /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
            /// <returns>Always 1 unless the given object is null in which case 0 is returned.</returns>
            public override int GetHashCode(TLexical obj)
            {
                return obj == null ? 0 : 1;
            }
        }
        /// <summary>
        /// An IEquality Comparer implementation using a: known Alias relationship implies Equality definition.
        /// </summary>
        public class AliasComparer : EqualityComparer<IEntity>
        {
            /// <summary>
            /// Initializes a new isntance of the AliasComparer class.
            /// </summary>
            protected internal AliasComparer()
            {
            }
            /// <summary>
            /// Determines whether the specified IEntity implementors are equal based on wether or not one is a defined alias for the other in the AliasDictionary or.
            /// </summary>
            /// <param name="x">The first object of type IEntity to compare.</param>
            /// <param name="y">The second object of type IEntity to compare.</param>
            /// <returns>true if the specified objects are equal; otherwise, false.</returns>
            public override bool Equals(IEntity x, IEntity y)
            {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);
                else if (ReferenceEquals(y, null))
                    return ReferenceEquals(x, null);
                else
                    return x.Text == y.Text || x.IsAliasFor(y);
            }

            /// <summary>
            /// Always returns 1 unless the given object is null in which case 0 is returned.
            /// </summary>
            /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
            /// <returns>Always 1 unless the given object is null in which case 0 is returned.</returns>
            public override int GetHashCode(IEntity obj)
            {
                return obj == null ? 0 : 1;
            }


        }
        /// <summary>
        /// An IEquality Comparer implementation using a: Either a known Alias relationship OR a Similarity relationship implies Equality definition.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IEntity interface.</typeparam>
        public class AliasOrSimilarityComparer<T> : EqualityComparer<T>
            where T : IEntity
        {
            /// <summary>
            /// Initializes a new isntance of the AliasOrSimilarityComparer class.
            /// </summary>
            protected internal AliasOrSimilarityComparer()
            {
            }
            /// <summary>
            /// Determines whether the specified IEntity implementors are equal based on wether or not one is a defined alias for the other in the AliasDictionary or, if not,
            /// wether they are considered similar by the associated LexicalLookup logic.
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
                    return x.Text == y.Text || x.IsAliasFor(y) || (x).IsSimilarTo(y);
            }

            /// <summary>
            /// Always returns 1 unless the given object is null in which case 0 is returned.
            /// </summary>
            /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
            /// <returns>Always 1 unless the given object is null in which case 0 is returned.</returns>
            public override int GetHashCode(T obj)
            {
                return obj == null ? 0 : 1;
            }
        }
        //public interface ICustomComparer<in T> : IEqualityComparer<T> where T : ILexical { }
        /// <summary>
        /// An IEquality Comparer implementation using a:
        /// </summary>
        public class CustomComparer<T> : EqualityComparer<T> where T : TLexical
        {
            private Func<T, T, bool> customEquals;
            private Func<T, int> customGetHashCode;
            /// <summary>
            /// Initializes a new instance of the CustomComparer class conforming to the logic of the provided equals function.
            /// </summary>
            /// <param name="equals">The function used test two elements for equality.</param>
            /// <exception cref="ArgumentNullException">Thrown if the provided equality function is null.</exception>
            protected internal CustomComparer(Func<T, T, bool> equals)
            {
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
            protected internal CustomComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
            {
                if (equals == null)
                    throw new ArgumentNullException("equals", "A null equals function was provided.");
                customEquals = equals;
                if (getHashCode == null)
                    throw new ArgumentNullException("getHashCode", "A null getHashCode function was provided.");
                customGetHashCode = getHashCode;
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
                    return customEquals(x, y);
            }
            /// <summary>
            /// Always returns 1 unless the given object is null in which case 0 is returned.
            /// </summary>
            /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
            /// <returns>Always 1 unless the given object is null in which case 0 is returned.</returns>
            public override int GetHashCode(T obj)
            {
                return customGetHashCode(obj);
            }
        }

        #endregion


    }
}
