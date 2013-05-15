using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class PronounPhrase : NounPhrase, IPronounBindable, LASI.Algorithm.LexicalStructures.NounRelatedConstructs.IPronoun
    {
        private IEntity _boundEntity;
        public PronounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }


        public override string ToString() {
            return base.ToString() + (BoundEntity != null ? " referring to -> " + BoundEntity.Text : "");
        }

        public IEntity BoundEntity {
            get {
                return _boundEntity;
            }
            private set {
                _boundEntity = value;
                EntityKind = value.EntityKind;
            }
        }


        public void BindToIEntity(IEntity target) {
            BoundEntity = target;
        }

        public void BindPronoun(Pronoun pro) {
            throw new NotImplementedException();
        }
        public virtual PronounKind PronounKind {
            get;
            protected set;
        }
    }
}
