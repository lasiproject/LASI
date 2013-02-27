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
        /// Filters the sequence of verbs selecting those with bound, not-null, subjects.
        /// </summary>
        /// <param name="verbs">The Enumerable of Verb objects to filter.</param>
        /// <returns></returns>
        public static IEnumerable<IAction> WithSubject(this IEnumerable<IAction> verbs) {
            return from V in verbs
                   where V.BoundSubject != null
                   select V;
        }
        /// <summary>
        /// Filters the sequence of verbs based returning those whose subjects match the provided subject testing function.
        /// </summary>
        /// <param name="verbs">The Enumerable of Verb objects to filter.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes a Noun and return a bool.</param>
        /// <returns>All verbs whose subject match the condition.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example> Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject((Noun N)=>N.Text == "banana");
        /// </code>
        /// </example>       
        /// <remarks>This provided function is used to filter the verbs based on their subjects.
        /// </remarks>
        public static IEnumerable<IAction> WithSubject(this IEnumerable<IAction> verbs, Func<Noun, bool> condition) {
            return from V in verbs
                   let subject = V.BoundSubject as Noun
                   where subject != null && condition(subject)
                   select V;
        }
        /// <summary>
        /// Filters the sequence of verbs based returning those whose subjects match the provided subject testing function.
        /// </summary>
        /// <param name="verbs">The Enumerable of Verb objects to filter.</param>
        /// <param name="condition">The function specifying the match condition. Any function which takes a NounPhrase and return a bool.</param>
        /// <returns>All verbs whose subject match the condition.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example> Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject((NounPhrase NP)=>NP.Words.Count() >=3);
        /// </code>
        /// </example>       
        /// <remarks>This provided function is used to filter the verbs based on their subjects.
        /// </remarks>
        public static IEnumerable<IAction> WithSubject(this IEnumerable<IAction> verbs, Func<NounPhrase, bool> condition) {
            return from V in verbs
                   let subject = V.BoundSubject as NounPhrase
                   where subject != null && condition(subject)
                   select V;
        }

    }
}
