using System;
using System.Collections.Generic;

namespace LASI.Algorithm
{
    public class NounPhrase : Phrase, IEntity
    {
        public NounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {

        }

        /// <summary>
        /// Binds a Pronoun or PronounPhrase as a reference to the NounPhrase Instance.
        /// </summary>
        /// <param name="pro">The referencer which refers to the NounPhrase Instance.</param>
        public virtual void BindPronoun(IEntityReferencer pro) {
            IndirectReferences.Add(pro);
        }

        public virtual void BindAdjective(IDescriber adj) {
            DescribedBy.Add(adj);
        }

        /// <summary>
        /// Gets the ITransitiveAction instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the object of.
        /// </summary>
        public virtual ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets the ITransitiveAction instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual ITransitiveAction IndirectObjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the NounPhrase is the subject of.
        /// </summary>
        public virtual IIntransitiveAction SubjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase Instance.
        /// </summary>
        public virtual ICollection<IEntityReferencer> IndirectReferences {
            get {
                return _indirectReferences;
            }
        }

        /// <summary>
        /// Gets all of the IDescriber constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase Instance.
        /// </summary>
        public virtual ICollection<IDescriber> DescribedBy {
            get {
                return _describedBy;
            }
        }


        /// <summary>
        /// Gets all of the IPossessable constructs which the Entity "owns".
        /// </summary>
        public ICollection<IEntity> Possessed {
            get {
                throw new NotImplementedException();
            }
        }

        public bool Equals(IEntity other) {
            return this == other as NounPhrase;
        }

        public void BindDescriber(IDescriber adj) {
            throw new NotImplementedException();
        }
        private ICollection<IDescriber> _describedBy = new List<IDescriber>();
        private ICollection<IPossessable> _possessed = new List<IPossessable>();
        private ICollection<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();


        public IEntity Possesser {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }









    }
}


