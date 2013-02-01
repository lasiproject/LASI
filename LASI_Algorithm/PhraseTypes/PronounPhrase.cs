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

        public ICollection<IEntityReferencer> IndirectReferences {
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
            get;
            set;
        }
        private ICollection<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();


        public void BindDescriber(IDescriber adj) {
            throw new NotImplementedException();
        }

        public ICollection<IDescriber> DescribedBy {
            get {
                throw new NotImplementedException();
            }
        }

        public ICollection<IPossessable> Possessed {
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

        ICollection<IEntity> IPossesser.Possessed {
            get {
                throw new NotImplementedException();
            }
        }

        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
