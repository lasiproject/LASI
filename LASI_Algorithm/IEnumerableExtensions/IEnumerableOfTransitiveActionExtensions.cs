using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfTransitiveActionExtensions
    {
        /// <summary>
        /// Filters the sequence of actions selecting those with bound, not-null, direct objects.
        /// </summary>
        /// <param name="actions">The Enumerable of Verb objects to filter.</param>
        /// <returns>The subset bound to a direct object.</returns>
        public static IEnumerable<ITransitiveAction> WithDirectObject(this IEnumerable<ITransitiveAction> transitiveVerbPhrases) {
            return from TA in transitiveVerbPhrases
                   where TA.DirectObject != null
                   select TA;
        }

        /// <summary>
        /// Filters a collection of Transitive Actions, returning those whose direct objects match the provided object testing function
        /// </summary>
        /// <param name="transitives">A collection of elements which implement the ITransitiveAction interface.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset bound to direct objects matching the condition.</returns>
        public static IEnumerable<ITransitiveAction> WithDirectObject(this IEnumerable<ITransitiveAction> transitives, Func<IEntity, bool> condition) {
            return from TA in transitives.WithDirectObject()
                   let P = TA.DirectObject as Pronoun
                   where condition(TA.DirectObject) || P != null && condition(P.BoundEntity)
                   select TA;
        }

        /// <summary>
        /// Filters the sequence of actions selecting those with bound, not-null, indirect objects.
        /// </summary>
        /// <param name="actions">The Enumerable of Verb objects to filter.</param>
        /// <returns>The subset bound to an indirect object.</returns>
        public static IEnumerable<ITransitiveAction> WithIndirectObject(this IEnumerable<ITransitiveAction> transitives) {
            return from TA in transitives
                   where TA.IndirectObject != null
                   select TA;
        }

        /// <summary>
        /// Filters a collection of Transitive Actions, returning those whose indirect objects match the provided object testing function
        /// </summary>
        /// <param name="transitives">A collection of elements which implement the ITransitiveAction interface.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes an IEntity and return a bool is compatible.</param>
        /// <returns>The subset bound to direct objects matching the condition.</returns>
        public static IEnumerable<ITransitiveAction> WithIndirectObject(this IEnumerable<ITransitiveAction> transitives, Func<IEntity, bool> condition) {
            return from TA in transitives.WithIndirectObject()
                   let P = TA.IndirectObject as Pronoun
                   where condition(TA.IndirectObject) || P != null && condition(P.BoundEntity)
                   select TA;
        }
    }
}
