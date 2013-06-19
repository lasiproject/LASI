using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    public class PronounPhrase : NounPhrase, IPronoun
    {
        private IEntity _boundEntity;
        public PronounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords)
        {
        }


        public override string ToString()
        {
            return base.ToString() + (BoundEntity != null ? " referring to -> " + BoundEntity.Text : "");
        }

        public IEntity BoundEntity
        {
            get
            {
                return _boundEntity;
            }

        }


        public void BindToTarget(IEntity target)
        {
            _boundEntity = target;
            EntityKind = _boundEntity.EntityKind;
        }

        public void BindPronoun(Pronoun pro)
        {
            throw new NotImplementedException();
        }
        public virtual PronounKind PronounKind
        {
            get;
            protected set;
        }


        public bool IsBound
        {
            get;
            private set;
        }
    }
}
