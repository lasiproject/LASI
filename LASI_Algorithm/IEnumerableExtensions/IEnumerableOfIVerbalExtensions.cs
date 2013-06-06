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
        /// Filters the sequence of Action instances selecting those with at least one bound subject.
        /// </summary>
        /// <param name="actions">The Enumerable of Action instances to filter.</param>
        /// <returns>The subset bound to some subject.</returns>
        public static IEnumerable<ITransitiveVerbal> WithSubject(
            this IEnumerable<ITransitiveVerbal> actions) {

            return from V in actions
                   where V.Subjects.Any(s => s != null)
                   select V;
        }
        /// <summary>
        /// Filters the sequence of actions based returning those who have at least one subject matching the provided subject testing function.
        /// </summary>
        /// <param name="actions">The Enumerable of Action instances to filter.</param>
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
        public static IEnumerable<ITransitiveVerbal> WithSubject(
            this IEnumerable<ITransitiveVerbal> actions,
            Func<IEntity, bool> condition) {

            return from A in actions.WithSubject()
                   where A.Subjects.Any(s => {
                       var p = s as Pronoun;
                       return condition(s) || p != null && condition(p.BoundEntity);
                   })
                   select A;
        }
    }
}
