using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfIActionExtensions
    {
        /// <summary>
        /// Filters the sequence of actions selecting those with bound, not-null, subjects.
        /// </summary>
        /// <param name="actions">The Enumerable of Verb objects to filter.</param>
        /// <returns>The subset bound to a subject.</returns>
        public static IEnumerable<ITransitiveAction> WithSubject(this IEnumerable<ITransitiveAction> verbs) {
            return from V in verbs
                   where V.BoundSubject != null
                   select V;
        }
        /// <summary>
        /// Filters the sequence of actions based returning those whose subjects match the provided subject testing function.
        /// </summary>
        /// <param name="actions">The Enumerable of Verb objects to filter.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes an IEntity and return a bool.</param>
        /// <returns>All actions whose subject match the condition.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example> Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject(N => N.Text == "banana");
        /// </code>
        /// </example>       
        /// <remarks>This provided function is used to filter the actions based on their subjects.
        /// </remarks>
        public static IEnumerable<ITransitiveAction> WithSubject(this IEnumerable<ITransitiveAction> actions, Func<IEntity, bool> condition) {
            return from A in actions.WithSubject()
                   where condition(A.BoundSubject)
                   select A;
        }
    }
}
