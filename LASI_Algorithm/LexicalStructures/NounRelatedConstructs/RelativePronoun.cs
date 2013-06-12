using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Relative Pronoun such as "that", "Which, "What" or "who".
    /// </summary>
    public class WhPronoun : Word, IPronoun
    {
        /// <summary>
        /// Initialiazes a new instance of the WhPronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the WhPronoun.</param>
        public WhPronoun(string text)
            : base(text)
        {
        }

        public IEntity BoundEntity
        {
            get
            {
                return _boundEntity;
            }
        }

        public void BindToIEntity(IEntity target)
        {
            _boundEntity = target;
            _entityKind = BoundEntity.EntityKind;
        }

        public PronounKind PronounKind
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public EntityKind EntityKind
        {
            get
            {
                return _entityKind;
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
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
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
            _describers.Add(adj);
        }

        public IEnumerable<IDescriptor> DescribedBy
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
                _possessed.Add(possession);
            }
            if (IsBound && !BoundEntity.Possessed.Contains(possession)) {
                BoundEntity.AddPossession(possession);
            }
        }

        public IEntity Possesser
        {
            get;
            set;
        }
        public bool IsBound
        {
            get;
            private set;
        }

        private ICollection<IDescriptor> _describers = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();
        private EntityKind _entityKind;
        private IEntity _boundEntity;


    }
}
