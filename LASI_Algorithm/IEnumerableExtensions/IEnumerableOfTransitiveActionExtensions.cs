using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.SyntacticInterfaces;

namespace LASI.Algorithm
{
    public static class IEnumerableOfTransitiveActionExtensions
    {
        /// <summary>
        /// Filters the sequence of Transitive Action instances selecting those with at least one bound direct object.
        /// </summary>
        /// <param name="actions">The Enumerable of Transitive Action instances to filter.</param>
        /// <returns>The subset of actions bound to at least one direct object.</returns>
        public static IEnumerable<ITransitiveVerbal> WithDirectObject(this IEnumerable<ITransitiveVerbal> actions) {
            return from TA in actions
                   where TA.DirectObjects.Count(o => o != null) > 0
                   select TA;
        }

        /// <summary>
        /// Filters a collection of Transitive Action instances, returning those who have at least one direct object matching the provided object testing function.
        /// </summary>
        /// <param name="actions">The Enumerable of Transitive Action instances to filter.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of actions bound to at least one direct object which matches the conidition.</returns>
        public static IEnumerable<ITransitiveVerbal> WithDirectObject(this IEnumerable<ITransitiveVerbal> actions, Func<IEntity, bool> condition) {
            return from TA in actions.WithDirectObject()
                   where TA.DirectObjects.Count(o => {
                       var p = o as Pronoun;
                       return condition(o) || p != null && condition(p.BoundEntity);
                   }) > 0
                   select TA;
        }

        /// <summary>
        /// Filters the sequence of Transitive Action instances selecting those with at least one bound indirect object.
        /// <param name="actions">The Enumerable of Verb objects to filter.</param>
        /// <returns>The subset bound to an indirect object.</returns>
        public static IEnumerable<ITransitiveVerbal> WithIndirectObject(this IEnumerable<ITransitiveVerbal> actions) {
            return from TA in actions
                   where TA.IndirectObjects.Count(o => o != null) > 0
                   select TA;
        }

        /// <summary>
        /// Filters a collection of Transitive Actions, returning those who have at k those who have at least one indirect object which matches the provided object testing function
        /// </summary>
        /// <param name="actions">The Enumerable of Transitive Action instances to filter.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset of actuibs bound to at least one indirect object which matches the condition.</returns>
        public static IEnumerable<ITransitiveVerbal> WithIndirectObject(this IEnumerable<ITransitiveVerbal> actions, Func<IEntity, bool> condition) {
            return from TA in actions.WithIndirectObject()
                   where TA.IndirectObjects.Count(o => {
                       var p = o as Pronoun;
                       return condition(o) || p != null && condition(p.BoundEntity);
                   }) > 0
                   select TA;
        }
    }
}
