using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;
using LASI.Utilities;

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
        /// <param name="text">The key text content of the RelativePronoun.</param>
        public RelativePronoun(string text)
            : base(text) {
            RelativePronounKind = DetermineRelativePronounKind(text);
        }

        #region Methods
        /// <summary>
        /// Returns a string representation of the RelativePronoun.
        /// </summary>
        /// <returns>A string representation of the RelativePronoun.</returns>
        public override string ToString() {
            var result = base.Text;
            result += VerboseOutput ? " " + RelativePronounKind.ToString() : "";
            return result;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of IEntity instances the RelativePronoun "Owns",
        /// and sets its owner to be the RelativePronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
            if (IsBound && !BoundEntity.Possessed.Contains(possession)) {
                BoundEntity.AddPossession(possession);
            }
        }
        /// <summary>
        /// Binds the RelativePronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindToEntity(IEntity target) {
            if (_boundEntity != null || !_boundEntity.Any())
                _boundEntity = new EntityGroup(new[] { target });
            else
                _boundEntity = new EntityGroup(_boundEntity.Concat(new[] { target }));
            _entityKind = BoundEntity.EntityKind;
        }

        #endregion
        /// <summary>
        /// Gets the Entity which the RelativePronoun references.
        /// </summary>
        public IEntityGroup BoundEntity {
            get {
                return _boundEntity;
            }
        }

        /// <summary>
        /// Gets or sets the EntityKind; Person, Place, Thing, Organization, or Activity;  of the Noun.
        /// </summary>
        public EntityKind EntityKind {
            get {
                return _entityKind;
            }
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the RelativePronoun is the subject object of.
        /// </summary>
        public IVerbal SubjectOf {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the RelativePronoun is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the RelativePronoun is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf {
            get;
            set;
        }


        /// <summary>
        /// Binds an EntityReferencer, generall a Pronoun or PronounPhrase to refer to the RelativePronoun.
        /// </summary>
        /// <param name="pro">The EntityReferency to Bind.</param>
        public void BindPronoun(IPronoun pro) {
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
        }
        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the RelativePronoun Instance.
        /// </summary>
        public IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the RelativePronoun.
        /// </summary>
        /// <param name="adjective">The IDescriptor instance which will be added to the RelativePronoun's descriptors.</param>
        public void BindDescriptor(IDescriptor adjective) {
            _describers.Add(adjective);
        }
        /// <summary>
        /// Gets the collection of IDescriptors, generally Adjectives or AdjectivePhrases which describe the RelativePronoun.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors {
            get {
                return _describers;
            }
        }
        /// <summary>
        /// Gets the collection of IEntity instances which the RelativePronoun can be said to "own".
        /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }

        /// <summary>
        /// Gets the IEntity which can be said to "own" the RelativePronoun.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }
        /// <summary>
        /// Indicates wether or not the IPronoun is bound to an Entity.
        /// </summary>
        public bool IsBound {
            get;
            private set;
        }
        /// <summary>
        /// Gets the RelativePronounKind of the RelativePronoun.
        /// </summary>
        public RelativePronounKind RelativePronounKind {
            get;
            private set;
        }
        private ICollection<IDescriptor> _describers = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();
        private EntityKind _entityKind;
        private IEntityGroup _boundEntity;

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
                RelativePronounKind.UNDEFINED;
        }


        private static readonly string[] subjectRolePersonal = { "who", "that" };
        private static readonly string[] objectRoleEntity = { "whom", "who", "that" };
        private static readonly string[] objectRoleLocationals = { "where" };
        private static readonly string[] objectRoleTemporals = { "when" };
        private static readonly string[] objectRoleExpositories = { "what", "why" };






    }
}
