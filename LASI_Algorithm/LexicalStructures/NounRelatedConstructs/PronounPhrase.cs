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
            : base(composedWords) {
            if (composedWords.GetPronouns().Any(p => p.IsBound)) {
                _boundEntity = composedWords.GetPronouns().Last().BoundEntity;
                IsBound = true;
            }
        }


        public override string ToString() {
            return base.ToString() + (BoundEntity != null ? " referring to -> " + BoundEntity.Text : "");
        }

        public IEntity BoundEntity {
            get {
                _boundEntity = _boundEntity ?? (Words.GetPronouns().Any(p => p.IsBound) ? Words.GetPronouns().Last().BoundEntity : null);
                IsBound = _boundEntity != null;
                return _boundEntity;
            }

        }


        public void BindToEntity(IEntity target) {
            _boundEntity = target;
            EntityKind = _boundEntity.EntityKind;
            IsBound = true;
        }

        public void BindPronoun(Pronoun pro) {
            base.BindPronoun(pro);
        }
        public virtual PronounKind PronounKind {
            get;
            protected set;
        }


        public bool IsBound {
            get;
            private set;
        }
        internal static PronounPhrase PromoteNounPhraseToPronounPhrase(ref NounPhrase contextualPronoun) {
            contextualPronoun = new PronounPhrase(contextualPronoun.Words) {
                SubjectOf = contextualPronoun.SubjectOf,
                DirectObjectOf = contextualPronoun.DirectObjectOf,
                IndirectObjectOf = contextualPronoun.IndirectObjectOf,
                ID = contextualPronoun.ID,
                EntityKind = contextualPronoun.EntityKind,
                Document = contextualPronoun.Document,
                Sentence = contextualPronoun.Sentence,
                BoundPronouns = contextualPronoun.BoundPronouns,
                InnerAttributive = contextualPronoun.InnerAttributive,
                OuterAttributive = contextualPronoun.OuterAttributive,
                Possesser = contextualPronoun.Possesser,
                Possessed = contextualPronoun.Possessed,
                PrepositionOnLeft = contextualPronoun.PrepositionOnLeft,
                PrepositionOnRight = contextualPronoun.PrepositionOnRight,
                Weight = contextualPronoun.Weight,
                MetaWeight = contextualPronoun.MetaWeight,
                Descriptors = contextualPronoun.Descriptors,
                PreviousPhrase = contextualPronoun.PreviousPhrase,
                NextPhrase = contextualPronoun.NextPhrase,
                BoundNoun = contextualPronoun.BoundNoun,
                BoundNounPhrase = contextualPronoun.BoundNounPhrase,
            };
            return contextualPronoun as PronounPhrase;
        }
    }
}
