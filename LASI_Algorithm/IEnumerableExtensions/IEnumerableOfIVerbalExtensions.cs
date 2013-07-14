using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IVerbal interface.
    /// </summary>
    /// <see cref="IVerbal"/>
    public static class IEnumerableOfIVerbalExtensions
    {
        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound subject.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of T instances to filter.</param>
        /// <returns>The subset bound to some subject.</returns>
        public static IEnumerable<TVerbal> WithSubject<TVerbal>(this IEnumerable<TVerbal> verbals) where TVerbal : IVerbal {
            return from verbal in verbals
                   where verbal.HasSubject()
                   select verbal;
        }
        /// <summary>
        /// Filters the sequence of IVerbal constructs returning those who have at least one subject matching the provided subject testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">The function specifying the match verbalSelector. Any function which takes an IEntity and return a bool.</param>
        /// <returns>All verbals whose subject match the verbalSelector.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example> Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject(N => N.Text == "banana");
        /// </code>
        /// </example>       
        /// <remarks>This provided function is used to filter the IVerbal constructs based on their subjects.
        /// </remarks>
        public static IEnumerable<TVerbal> WithSubject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition) where TVerbal : IVerbal {
            return from verbal in verbals.WithSubject()
                   where verbal.HasSubject(condition)
                   select verbal;
        }
        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound direct OR indirect object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal instances to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one bound direct OR indirect object.</returns>
        public static IEnumerable<TVerbal> WithDirectOrIndirectObject<TVerbal>(this IEnumerable<TVerbal> verbals) where TVerbal : IVerbal {
            return from verbal in verbals
                   where verbal.HasObject()
                   select verbal;
        }

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one bound direct OR indirect object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">The function specifying the match verbalSelector. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one bound direct OR indirect object which matches the conidition.</returns>
        public static IEnumerable<TVerbal> WithDirectOrIndirectObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition) where TVerbal : IVerbal {
            return from verbal in verbals.WithDirectOrIndirectObject()
                   where verbal.HasObject(condition)
                   select verbal;
        }
        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound direct object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal instances to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one direct object.</returns>
        public static IEnumerable<TVerbal> WithDirectObject<TVerbal>(this IEnumerable<TVerbal> verbals) where TVerbal : IVerbal {
            return from verbal in verbals
                   where verbal.HasDirectObject()
                   select verbal;
        }

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one direct object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">The function specifying the match verbalSelector. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one direct object which matches the conidition.</returns>
        public static IEnumerable<TVerbal> WithDirectObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition) where TVerbal : IVerbal {
            return from verbal in verbals.WithDirectObject()
                   where verbal.HasDirectObject(condition)
                   select verbal;
        }

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound indirect object.
        /// </summary>
        /// <param name="verbals">The Enumerable of Verb objects to filter.</param>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <returns>The subset of IVerbal constructs bound to an indirect object.</returns>
        public static IEnumerable<TVerbal> WithIndirectObject<TVerbal>(this IEnumerable<TVerbal> verbals) where TVerbal : IVerbal {
            return from verbal in verbals
                   where verbal.HasIndirectObject()
                   select verbal;
        }

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one indirect object which matches the provided object testing function
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="condition">The function specifying the criteria an entity bound to the Verbal must match for it to be included. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one indirect object which matches the verbalSelector.</returns>
        public static IEnumerable<TVerbal> WithIndirectObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition) where TVerbal : IVerbal {
            return from verbal in verbals.WithIndirectObject()
                   where verbal.HasIndirectObject(condition)
                   select verbal;
        }

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one subject, direct object, or indirect object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of Verb objects to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one subject, direct object, or indirect object.</returns>
        public static IEnumerable<TVerbal> WithSubjectOrObject<TVerbal>(this IEnumerable<TVerbal> verbals) where TVerbal : IVerbal {
            return from verbal in verbals
                   where verbal.HasSubject() || verbal.HasObject()
                   select verbal;
        }
        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one subject, direct object, or indirect object that matches the provided IEntity testing function
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implemenets the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="condition">The function specifying the criteria an entity bound to the Verbal must match for it to be included. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one subject, direct object, or indirect object which matches the provided condition.</returns>
        public static IEnumerable<TVerbal> WithSubjectOrObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition) where TVerbal : IVerbal {
            return from verbal in verbals
                   where verbal.HasSubject(condition) || verbal.HasObject(condition)
                   select verbal;
        }

    }
}
