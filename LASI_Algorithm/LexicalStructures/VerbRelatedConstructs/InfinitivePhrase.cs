using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an infinitive verbal phrase, e.g. "to walk".
    /// </summary>
    public class InfinitivePhrase : VerbPhrase, IEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the InfinitivePhrase class.
        /// </summary>
        /// <param name="composed"></param>
        public InfinitivePhrase(IEnumerable<Word> composed)
            : base(composed) {
        }

        #endregion

        #region Properties

        public EntityKind Kind {
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

        #endregion

        #region Fields

        ISet<IPronoun> _boundPronouns = new HashSet<IPronoun>();
        ISet<IEntity> _possessions = new HashSet<IEntity>();
        ISet<IDescriptor> _descriptors = new HashSet<IDescriptor>();

        #endregion
    }
}
