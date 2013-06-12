using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Relative Pronoun such as "that", "Which, "What" or "who".
    /// </summary>
    public class RelativePronoun : Word, IPronoun
    {
        /// <summary>
        /// Initialiazes a new instance of the RelativePronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the RelativePronoun.</param>
        public RelativePronoun(string text)
            : base(text) {
            RelativePronounKind = DetermineRelativePronounKind(text);
        }

        #region Methods
        public override string ToString() {
            var result = base.Text;
            result += VerboseOutput ? " " + PronounKind.ToString() : "";
            return result;
        }
        public void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
            if (IsBound && !BoundEntity.Possessed.Contains(possession)) {
                BoundEntity.AddPossession(possession);
            }
        }
        public void BindToIEntity(IEntity target) {
            _boundEntity = target;
            _entityKind = BoundEntity.EntityKind;
        }

        #endregion
        public IEntity BoundEntity {
            get {
                return _boundEntity;
            }
        }


        public PronounKind PronounKind {
            get {
                throw new NotImplementedException();
            }
        }

        public EntityKind EntityKind {
            get {
                return _entityKind;
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
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
        }

        public IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        public void BindDescriptor(IDescriptor adj) {
            _describers.Add(adj);
        }

        public IEnumerable<IDescriptor> DescribedBy {
            get {
                return _describers;
            }
        }

        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }


        public IEntity Possesser {
            get;
            set;
        }
        public bool IsBound {
            get;
            private set;
        }
        public RelativePronounKind RelativePronounKind {
            get;
            private set;
        }
        private ICollection<IDescriptor> _describers = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();
        private EntityKind _entityKind;
        private IEntity _boundEntity;

        private static RelativePronounKind DetermineRelativePronounKind(string text) {
            var checkText = text.ToLower();
            return subjectRolePersonal.Contains(checkText) ?
                RelativePronounKind.SubjectRolePersonal :
                objectRoleEntity.Contains(checkText) ?
                RelativePronounKind.ObjectRoleEntity :
                objectRoleLocationals.Contains(checkText) ?
                RelativePronounKind.ObjectRoleLocational :
                objectRoleTemporals.Contains(checkText) ?
                RelativePronounKind.ObjectRoleTemporal :
                objectRoleExpositories.Contains(checkText) ?
                RelativePronounKind.ObjectRoleExpository :
                RelativePronounKind.Undefined;
        }


        private static readonly string[] subjectRolePersonal = { "who", "that" };
        private static readonly string[] objectRoleEntity = { "whom", "who", "that" };
        private static readonly string[] objectRoleLocationals = { "where" };
        private static readonly string[] objectRoleTemporals = { "when" };
        private static readonly string[] objectRoleExpositories = { "what", "why" };

    }
}
