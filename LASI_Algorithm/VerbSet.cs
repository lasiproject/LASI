using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class VerbSet : IEnumerable<Verb>
    {

        #region Constructors

        public VerbSet(IEnumerable<Verb> initialVerbs) {
            verbs = initialVerbs;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given Noun under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The noun to match with.</param>
        /// <param name="compare">A comparison function taking two Nouns and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given Noun under the provided comparison.</returns>
        public IEnumerable<Verb> GetVerbsBySubject(Noun verbSubject, Func<Noun, Noun, bool> compare) {
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
        public IEnumerable<Verb> GetVerbsBySubject(NounPhrase verbSubject, Func<NounPhrase, NounPhrase, bool> compare) {
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
        public IEnumerable<Verb> GetVerbsBySubject(Pronoun verbSubject, Func<Pronoun, Pronoun, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as Pronoun
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given PronounPhrase under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The PronounPhrase to match with.</param>
        /// <param name="compare">A comparison function taking two PronounsPhrase and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given PronounPhrase under the provided comparison.</returns>
        public IEnumerable<Verb> GetVerbsBySubject(PronounPhrase verbSubject, Func<PronounPhrase, PronounPhrase, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as PronounPhrase
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given IEntity instance under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The IEntity instance to match with.</param>
        /// <param name="compare">A comparison function taking two IEntity instances and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given IEntity under the provided comparison.</returns>
        public IEnumerable<Verb> GetVerbsBySubject(IEntity verbSubject, Func<IEntity, IEntity, bool> compare) {
            return from v in verbs
                   let subj = v.BoundSubject as IEntity
                   where subj != null && compare(subj, verbSubject)
                   select v;
        }
        /// <summary>
        /// Returns all instances of Verbs whose subjects match the given IActionSubject instance under the provided comparison function.
        /// </summary>
        /// <param name="verbSubject">The IActionSubject instnace to match with.</param>
        /// <param name="compare">A comparison function taking two IActionSubject instances and returning a bool value indicating whether or not they match.</param>
        /// <returns>All verbs whose subjects compared true against the given IActionSubject instance under the provided comparison.</returns>
        public IEnumerable<Verb> GetVerbsBySubject(IActionSubject verbSubject, Func<IActionSubject, IActionSubject, bool> compare) {
            return from verb in verbs
                   where compare(verb.BoundSubject, verbSubject)
                   select verb;
        }



        public virtual IEnumerator<Verb> GetEnumerator() {
            return verbs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
        #endregion


        #region Properties
        public TransitiveVerbSet TransitiveVerbs {
            get {
                return new TransitiveVerbSet(verbs.OfType<TransitiveVerb>());
            }
        }



        #endregion

        #region Fields

        private IEnumerable<Verb> verbs;

        #endregion
    }
}
