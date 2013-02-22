using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfVerbExtensions
    {

        /// <summary>
        /// Filters the sequence of verbs based returning those whose subjects match the provided subject testing function.
        /// </summary>
        /// <param name="verbs">The Enumerable of Verb objects to filter</param>
        /// <param name="subjectMatcher">Any function which takes a Noun and return a bool.</param>
        /// <returns>All verbs for which the subjectMatcher function returns true.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example> Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WhereSubject((Noun noun)=>noun.Text == "banana");
        /// </code>
        /// </example>       
        /// <remarks>This provided function is used to filter the verbs based on their subjects.
        /// </remarks>
        public static IEnumerable<Verb> WhereSubject(this IEnumerable<Verb> verbs, Func<Noun, bool> subjectMatcher) {
            return from V in verbs
                   let subject = V.BoundSubject as Noun
                   where subject != null && subjectMatcher(subject)
                   select V;
        }
        public static IEnumerable<Verb> WhereSubject(this IEnumerable<Verb> verbs, Func<NounPhrase, bool> subjectMatcher) {
            return from V in verbs
                   let subject = V.BoundSubject as NounPhrase
                   where subject != null && subjectMatcher(subject)
                   select V;
        }
        public static IEnumerable<Verb> WhereSubject(this IEnumerable<Verb> verbs, Func<IEntity, bool> subjectMatcher) {
            return from V in verbs
                   where V.BoundSubject != null && subjectMatcher(V.BoundSubject)
                   select V;
        }
        public static IEnumerable<TransitiveVerb> GetTransitiveVerbs(this IEnumerable<Verb> verbs) {
            return verbs.OfType<TransitiveVerb>();
        }

    }
}
