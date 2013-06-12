using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LASI.Algorithm
{
    public static class IEnumerableOfIVerbalExtensions
    {
        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound subject.
        /// </summary>
        /// <typeparam name="T">Any type which implemenets the IVerbal interface.</typeparam>
        /// <param name="actions">The Enumerable of T instances to filter.</param>
        /// <returns>The subset bound to some subject.</returns>
        public static IEnumerable<T> WithSubject<T>(this IEnumerable<T> actions) where T : IVerbal
        {
            return from V in actions
                   where V.Subjects.Any(s => s != null)
                   select V;
        }
        /// <summary>
        /// Filters the sequence of IVerbal constructs returning those who have at least one subject matching the provided subject testing function.
        /// </summary>
        /// <typeparam name="T">Any type which implemenets the IVerbal interface.</typeparam>
        /// <param name="actions">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="verbalSelector">The function specifying the match verbalSelector. Any function which takes an IEntity and return a bool.</param>
        /// <returns>All actions whose subject match the verbalSelector.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example> Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject(N => N.Text == "banana");
        /// </code>
        /// </example>       
        /// <remarks>This provided function is used to filter the IVerbal constructs based on their subjects.
        /// </remarks>
        public static IEnumerable<T> WithSubject<T>(this IEnumerable<T> actions,
            Func<IEntity, bool> condition) where T : IVerbal
        {

            return from A in actions.WithSubject()
                   where A.Subjects.Any(s =>
                   {
                       var p = s as Pronoun;
                       return condition(s) || p != null && condition(p.BoundEntity);
                   })
                   select A;
        }
        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound direct object.
        /// </summary>
        /// <typeparam name="T">Any type which implemenets the IVerbal interface.</typeparam>
        /// <param name="actions">The Enumerable of Transitive Action instances to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one direct object.</returns>
        public static IEnumerable<T> WithDirectObject<T>(this IEnumerable<T> actions) where T : IVerbal
        {
            return from TA in actions
                   where TA.DirectObjects.Any(o => o != null)
                   select TA;
        }

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one direct object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="T">Any type which implemenets the IVerbal interface.</typeparam>
        /// <param name="actions">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="verbalSelector">The function specifying the match verbalSelector. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one direct object which matches the conidition.</returns>
        public static IEnumerable<T> WithDirectObject<T>(this IEnumerable<T> actions, Func<IEntity, bool> condition) where T : IVerbal
        {
            return from TA in actions.WithDirectObject()
                   where TA.DirectObjects.Any(o =>
                   {
                       var p = o as IPronoun;
                       return condition(o) || p != null && p.BoundEntity != null && condition(p.BoundEntity);
                   })
                   select TA;
        }

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound indirect object.
        /// <param name="actions">The Enumerable of Verb objects to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to an indirect object.</returns>
        public static IEnumerable<T> WithIndirectObject<T>(this IEnumerable<T> actions) where T : IVerbal
        {
            return from TA in actions
                   where TA.IndirectObjects.Any(o => o != null)
                   select TA;
        }

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at k those who have at least one indirect object which matches the provided object testing function
        /// </summary>
        /// <typeparam name="T">Any type which implemenets the IVerbal interface.</typeparam>
        /// <param name="actions">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="verbalSelector">The function specifying the match verbalSelector. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one indirect object which matches the verbalSelector.</returns>
        public static IEnumerable<T> WithIndirectObject<T>(this IEnumerable<T> actions, Func<IEntity, bool> condition) where T : IVerbal
        {
            return from TA in actions.WithIndirectObject()
                   where TA.IndirectObjects.Any(o =>
                   {
                       var p = o as IPronoun;
                       return condition(o) || p != null && p.BoundEntity != null && condition(p.BoundEntity);
                   })
                   select TA;
        }
    }
}
