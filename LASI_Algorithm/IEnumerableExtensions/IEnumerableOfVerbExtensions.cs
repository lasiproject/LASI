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
        /// Returns all instances of Verbs whose subjects match the given Noun under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The noun to match with.</param>
        /// <param name="compare">A comparison function taking two Nouns and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given Noun under the provided comparison.</returns>
        public static IEnumerable<Verb> WithSubject(this IEnumerable<Verb> verbs, Noun verbSubject, Func<Noun, Noun, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as Noun
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given NounPhrase under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The NounPhrase to match with.</param>
        /// <param name="compare">A comparison function taking two NounsPhrase and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given NounPhrase under the provided comparison.</returns>
        public static IEnumerable<Verb> WithSubject(this IEnumerable<Verb> verbs, NounPhrase verbSubject, Func<NounPhrase, NounPhrase, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as NounPhrase
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given Pronoun under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The Pronoun to match with.</param>
        /// <param name="compare">A comparison function taking two Pronouns and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given Pronoun under the provided comparison.</returns>
        public static IEnumerable<Verb> WithSubject(this IEnumerable<Verb> verbs, IEntityReferencer verbSubject, Func<IEntityReferencer, IEntityReferencer, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as Pronoun
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given IEntity instance under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The IEntity instance to match with.</param>
        /// <param name="compare">A comparison function taking two IEntity instances and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given IEntity under the provided comparison.</returns>
        public static IEnumerable<Verb> WithSubject(this IEnumerable<Verb> verbs, IEntity verbSubject, Func<IEntity, IEntity, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as IEntity
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match return true for the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The a function which tests the subjects of each verb in the VerbList.</param>
        /// <param name="compare">A comparison function taking two IActionSubject instances and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given IActionSubject instance under the provided comparison.</returns>
        public static IEnumerable<Verb> WithSubject(this IEnumerable<Verb> verbs, IActionSubject verbSubject, Func<IActionSubject, IActionSubject, bool> compare) {
            return from verb in verbs
                   where compare(verb.BoundSubject, verbSubject)
                   select verb;
        }

        public static IEnumerable<Verb> WhereSubject(this IEnumerable<Verb> verbs, Func<Noun, bool> predicate) {
            return from V in verbs
                   let subject = V.BoundSubject as Noun
                   where subject != null && predicate(subject)
                   select V;
        }
        public static IEnumerable<Verb> WhereSubject(this IEnumerable<Verb> verbs, Func<NounPhrase, bool> predicate) {
            return from V in verbs
                   let subject = V.BoundSubject as NounPhrase
                   where subject != null && predicate(subject)
                   select V;
        }
        public static IEnumerable<Verb> WhereSubject(this IEnumerable<Verb> verbs, Func<IActionSubject, bool> predicate) {
            return from V in verbs
                   where V.BoundSubject != null && predicate(V.BoundSubject)
                   select V;
        }

        public static IEnumerable<TransitiveVerb> GetTransitiveVerbs(this IEnumerable<Verb> verbs) {
            return verbs.OfType<TransitiveVerb>();
        }

    }
}
