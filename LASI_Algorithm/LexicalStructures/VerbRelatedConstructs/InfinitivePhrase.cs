using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    public class InfinitivePhrase : VerbPhrase, IEntity
    {
        public InfinitivePhrase(IEnumerable<Word> composed)
            : base(composed) {
        }

        public EntityKind EntityKind {
            get {
                return Algorithm.EntityKind.Activitiy;
            }
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
            _boundPronouns.Add(pro);
            pro.BindToEntity(this);
        }

        public IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        public void BindDescriptor(IDescriptor adj) {
            _descriptors.Add(adj);
            adj.Describes = this;
        }

        public IEnumerable<IDescriptor> Descriptors {
            get {
                return _descriptors;
            }
        }

        public IEnumerable<IEntity> Possessed {
            get {
                return _possessions;
            }
        }
        public void AddPossession(IEntity possession) {
            _possessions.Add(possession);
        }

        public IEntity Possesser {
            get;
            set;
        }

        #region Fields

        ISet<IPronoun> _boundPronouns = new HashSet<IPronoun>();
        ISet<IEntity> _possessions = new HashSet<IEntity>();
        ISet<IDescriptor> _descriptors = new HashSet<IDescriptor>();

        #endregion
    }
}
