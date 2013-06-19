
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
            : base(composedWords)
        {
        }

        public EntityKind EntityKind
        {
            get
            {
                return EntityKind.Activitiy;
            }
        }

        public IVerbal DirectObjectOf
        {
            get;
            set;
        }

        public IVerbal IndirectObjectOf
        {
            get;
            set;
        }

        public IVerbal SubjectOf
        {
            get;
            set;
        }

        public void BindPronoun(IPronoun pro)
        {
            if (!_boundPronouns.Contains(pro)) {
                _boundPronouns.Add(pro);
            }
        }

        public IEnumerable<IPronoun> BoundPronouns
        {
            get
            {
                return _boundPronouns;
            }
        }

        public void BindDescriptor(IDescriptor adj)
        {
            if (!_describers.Contains(adj)) {
                adj.Describes = this;
                _describers.Add(adj);
            }
        }

        public IEnumerable<IDescriptor> Descriptors
        {
            get
            {
                return _describers;
            }
        }

        public IEnumerable<IEntity> Possessed
        {
            get
            {
                return _possessed;
            }
        }

        public void AddPossession(IEntity possession)
        {
            if (!_possessed.Contains(possession)) {
                possession.Possesser = this;
                _possessed.Add(possession);
            }
        }

        public IEntity Possesser
        {
            get;
            set;
        }

        public void BindPronoun(Pronoun pro)
        {
            if (!_boundPronouns.Contains(pro)) {
                _boundPronouns.Add(pro);
            }
        }

        #region Fields

        private ICollection<IDescriptor> _describers = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();

        #endregion





    }
}
