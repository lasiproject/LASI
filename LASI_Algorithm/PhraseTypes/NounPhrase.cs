using System;
using System.Collections.Generic;
using System.Linq;

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
            _indirectReferences.Add(pro);
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
        public virtual IAction SubjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase Instance.
        /// </summary>
        public virtual IEnumerable<IEntityReferencer> IndirectReferences {
            get {
                return _indirectReferences;
            }
        }

        /// <summary>
        /// Gets all of the IDescriber constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase Instance.
        /// </summary>
        public virtual IEnumerable<IDescriber> DescribedBy {
            get {
                return _describedBy;
            }
        }


        /// <summary>
        /// Gets all of the IPossessable constructs which the Entity "owns".
        /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }
        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the Noun.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }
        public bool Equals(IEntity other) {
            return this == other as NounPhrase;
        }

        public void BindDescriber(IDescriber adj) {
            if (!_describedBy.Contains(adj))
                _describedBy.Add(adj);
        }
        private IList<IDescriber> _describedBy = new List<IDescriber>();
        private IList<IEntity> _possessed = new List<IEntity>();
        private IList<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();












        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }


        public void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
        }
    }
}


