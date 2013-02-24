using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PronounPhrase : Phrase, IEntityReferencer, IActionObject, IActionSubject, IReferenciable, IEntity
    {

        public PronounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        public void BindPronoun(IEntityReferencer pro) {
            pro.BoundEntity = this;
        }

        public IEnumerable<IEntityReferencer> IndirectReferences {
            get {
                return _indirectReferences;
            }
        }

        public IAction SubjectOf {
            get;
            set;
        }

        public ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        public ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
        public IEntity BoundEntity {
            get {
                return _boundEntity;
            }
            set {
                _boundEntity = value;
                this.EntityType = BoundEntity.EntityType;
            }
        }
        private IEnumerable<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();
        private IEntity _boundEntity;

        public void BindDescriber(IDescriber adj) {
            throw new NotImplementedException();
        }

        public IEnumerable<IDescriber> DescribedBy {
            get {
                throw new NotImplementedException();
            }
        }

        public IEntity Possesser {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public bool Equals(IEntity other) {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        public IEnumerable<IEntity> Possessed {
            get {
                throw new NotImplementedException();
            }
        }

        public EntityType EntityType {
            get;
            protected set;
        }
    }
}
