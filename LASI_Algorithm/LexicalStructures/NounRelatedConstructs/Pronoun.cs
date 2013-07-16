
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a pronoun which gernerally refers back to a previously defined Entity, such as a Noun or NounPhrase.
    /// </summary>
    public abstract class Pronoun : Word, IPronoun
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Pronoun class.
        /// </summary>
        /// <param name="text">The key text content of the pronoun.</param>
        protected Pronoun(string text)
            : base(text) {
            PronounKind = DeterminePronounKind(this);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Binds the Pronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindToEntity(IEntity target) {
            if (_boundEntity != null || !_boundEntity.Any())
                _boundEntity = new EntityGroup(new[] { target });
            else
                _boundEntity = new EntityGroup(_boundEntity.Concat(new[] { target }));
            _entityKind = BoundEntity.EntityKind;
        }
        /// <summary>
        /// Returns a string representation of the Pronoun.
        /// </summary>
        /// <returns>A string representation of the Pronoun.</returns>
        public override string ToString() {

            return Text + (VerboseOutput ? " " + EntityKind : string.Empty);

        }

        /// <summary>
        /// Binds another IPronoun, generally another pronoun but possibly a PronounPhrase, to refer to the Pronoun.
        /// </summary>
        /// <param name="pro">An IPronoun which will be bound to refer to the Pronoun.</param>
        public virtual void BindPronoun(IPronoun pro) {
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the Pronoun.
        /// </summary>
        /// <param name="adjective">The IDescriptor instance which will be added to the Pronoun's descriptors.</param>
        public virtual void BindDescriptor(IDescriptor adjective) {
            _describers.Add(adjective);
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of Pronoun "Owns",
        /// and sets its owner to be the Pronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public virtual void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
            if (IsBound && !BoundEntity.Possessed.Contains(possession)) {
                BoundEntity.AddPossession(possession);
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entity which the Pronoun references.
        /// </summary>
        public virtual IEntityGroup BoundEntity {
            get {
                return _boundEntity;
            }

        }


        /// <summary>
        /// Gets or sets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public IVerbal SubjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public IVerbal DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the Pronoun.
        /// </summary>
        public IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }

        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the Pronoun.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors {
            get {
                return _describers;
            }
        }
        /// <summary>
        /// Gets all of the constructs the Pronoun can be determined to "own".
        /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }


        /// <summary>
        /// Gets or sets the Entity which is inferred to the Pronoun.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }
        /// <summary>
        /// Gets the EntityKind of the Pronoun.
        /// </summary>
        public virtual EntityKind EntityKind {
            get {
                return _entityKind;
            }
        }
        /// <summary>
        /// Gets the PronounKind of the Pronoun.
        /// </summary>
        public PronounKind PronounKind {
            get;
            protected set;
        }
        /// <summary>
        /// Gets a value indicating wether the Pronoun is bound as a reference to some Entity.
        /// </summary>
        public bool IsBound {
            get {
                return BoundEntity != null;
            }
        }

        #endregion


        #region Fields
        private ICollection<IDescriptor> _describers = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();

        private EntityKind _entityKind;
        private IEntityGroup _boundEntity;

        #endregion



        #region Static Methods

        /// <summary>
        /// Determines the PronounKind which corresponds to the Plurarility and Gender of the given pronoun.
        /// </summary>
        /// <param name="pronoun">The pronoun whose gender to is to be checked</param>
        /// <returns>A PronounGenerder enum value representing the gender of the given pronoun.</returns>
        private static PronounKind DeterminePronounKind(Pronoun pronoun) {
            var compareText = pronoun.Text.ToLower();
            return
                males.Contains(compareText) ?
                PronounKind.Male :
                maleReflexives.Contains(compareText) ?
                PronounKind.MaleReflexive :
                females.Contains(compareText) ?
                PronounKind.Female :
                femaleReflexives.Contains(compareText) ?
                PronounKind.FemaleReflexive :
                neutrals.Contains(compareText) ?
                PronounKind.GenderNeurtral :
                neutrals.Contains(compareText) ?
                PronounKind.GenderNeurtralReflexive :
                firstPersonSingulars.Contains(compareText) ?
                PronounKind.FirstPersonSingular :
                firstPersonPluralReflexives.Contains(compareText) ?
                PronounKind.FirstPersonPlural :
                firstPersonPlurals.Contains(compareText) ?
                PronounKind.FirstPersonPlural :
                firstPersonPluralReflexives.Contains(compareText) ?
                PronounKind.FirstPersonPluralReflexive :
                secondPersons.Contains(compareText) ?
                PronounKind.SecondPerson :
                secondPersonSingularReflexives.Contains(compareText) ?
                PronounKind.SecondPersonSingularReflexive :
                secondPersonPluralReflexives.Contains(compareText) ?
                PronounKind.SecondPersonPluralReflexive :
                thirdPersonGenderAmbiguousPlurals.Contains(compareText) ?
                PronounKind.ThirdPersonGenderAmbiguousPlural :
                thirdPersonPluralReflexives.Contains(compareText) ?
                PronounKind.ThirdPersonPluralReflexive :
                PronounKind.Undefined;
        }

        #endregion

        #region Static Fields

        //Common personal Pronouns by gender and plurality 
        private static readonly string[] males = { "he", "him", "his" };
        private static readonly string[] maleReflexives = { "himself", "hisself", };
        private static readonly string[] females = { "she", "her", "hers" };
        private static readonly string[] femaleReflexives = { "herself" };
        private static readonly string[] neutrals = { "it", "itself", "its" };
        private static readonly string[] neutralReflexives = { "itself" };
        private static readonly string[] firstPersonSingulars = { "i", "me", "mine" };
        private static readonly string[] firstPersonSingularReflexives = { "myself" };
        private static readonly string[] firstPersonPlurals = { "we", "us", "ours" };
        private static readonly string[] firstPersonPluralReflexives = { "ourselves" };
        private static readonly string[] secondPersons = { "you", "yours" };
        private static readonly string[] secondPersonSingularReflexives = { "yourself" };
        private static readonly string[] secondPersonPluralReflexives = { "yourselves" };
        private static readonly string[] thirdPersonGenderAmbiguousPlurals = { "them", "they", "theirs" };
        private static readonly string[] thirdPersonPluralReflexives = { "themselves", "theirselves" };

        #endregion


    }
}
