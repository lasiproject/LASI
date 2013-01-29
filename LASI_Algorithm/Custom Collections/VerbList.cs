using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class VerbList : IEnumerable<Verb>
    {

        #region Constructors

        public VerbList(IEnumerable<Verb> initialVerbs) {
            items = initialVerbs;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given Noun under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The noun to match with.</param>
        /// <param name="compare">A comparison function taking two Nouns and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given Noun under the provided comparison.</returns>
        public VerbList WithSubject(Noun verbSubject, Func<Noun, Noun, bool> compare) {
            return (VerbList)from v in items
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
        public VerbList WithSubject(NounPhrase verbSubject, Func<NounPhrase, NounPhrase, bool> compare) {
            return (VerbList)from v in items
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
        public VerbList WithSubject(IEntityReferencer verbSubject, Func<IEntityReferencer, IEntityReferencer, bool> compare) {
            return (VerbList)from v in items
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
        public VerbList WithSubject(IEntity verbSubject, Func<IEntity, IEntity, bool> compare) {
            return (VerbList)from v in items
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
        public VerbList WithSubject(IActionSubject verbSubject, Func<IActionSubject, IActionSubject, bool> compare) {
            return (VerbList)from verb in items
                             where compare(verb.BoundSubject, verbSubject)
                             select verb;
        }

        public VerbList WhereSubject(Func<Noun, bool> predicate) {
            return (VerbList)from V in items
                             let subject = V.BoundSubject as Noun
                             where subject != null && predicate(subject)
                             select V;
        }
        public VerbList WhereSubject(Func<NounPhrase, bool> predicate) {
            return (VerbList)from V in items
                             let subject = V.BoundSubject as NounPhrase
                             where subject != null && predicate(subject)
                             select V;
        }
        public VerbList WhereSubject(Func<IActionSubject, bool> predicate) {
            return (VerbList)from V in items where V.BoundSubject != null && predicate(V.BoundSubject) select V;
        }
        public virtual IEnumerator<Verb> GetEnumerator() {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public IEnumerable<TransitiveVerb> TransitiveVerbs {
            get {
                return new TransitiveVerbList(items.OfType<TransitiveVerb>());
            }
        }



        #endregion

        #region Fields

        protected IEnumerable<Verb> items;

        #endregion
    }
}
