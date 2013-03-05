using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.PhraseTypes
{
    class GerundVerbPhrase : VerbPhrase, IEntity
    {
        public GerundVerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }

        public EntityKind EntityKind {
            get {
                return EntityKind.Activitiy;
            }
        }

        public EntityThemeMemberKind ThemeMemberKind {
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

        public IAction SubjectOf {
            get;
            set;
        }

        public void BindPronoun(Pronoun pro) {
            if (!_boundPronouns.Contains(pro)) {
                pro.BoundEntity = this;
                _boundPronouns.Add(pro);
            }
        }

        public IEnumerable<Pronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        public void BindDescriber(IDescriber adj) {
            if (!_describers.Contains(adj)) {
                adj.Describes = this;
                _describers.Add(adj);
            }
        }

        public IEnumerable<IDescriber> DescribedBy {
            get {
                return _describers;
            }
        }

        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }

        public void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                possession.Possesser = this;
                _possessed.Add(possession);
            }
        }

        public IEntity Possesser {
            get;
            set;
        }

        public bool Equals(IEntity other) {
            return this == other;
        }

        #region Fields

        private ICollection<IDescriber> _describers = new List<IDescriber>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<Pronoun> _boundPronouns = new List<Pronoun>();

        #endregion



    }
}
