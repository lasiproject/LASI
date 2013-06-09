
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
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

        public IVerbal DirectObjectOf {
            get;
            set;
        }

        public IVerbal IndirectObjectOf {
            get;
            set;
        }

        public IVerbal SubjectOf {
            get;
            set;
        }

        public void BindPronoun(IPronoun pro) {
            if (!_boundPronouns.Contains(pro)) {
                _boundPronouns.Add(pro);
            }
        }

        public IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        public void BindDescriber(IDescriber adj) {
            if (!_describers.Contains(adj)) {
                adj.Described = this;
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
            return this == other as GerundVerbPhrase;
        }

        #region Fields

        private ICollection<IDescriber> _describers = new List<IDescriber>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();

        #endregion




        public void BindPronoun(Pronoun pro) {
            throw new NotImplementedException();
        }
    }
}
