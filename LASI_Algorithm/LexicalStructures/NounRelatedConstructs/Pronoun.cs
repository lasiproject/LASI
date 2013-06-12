
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="text">The literal text content of the pronoun.</param>
        protected Pronoun(string text)
            : base(text) {
        }

        #endregion

        #region Methods

        public override string ToString() {
            var result = base.Text;
            result += VerboseOutput ? " " + PronounKind.ToString() : "";
            return result;
        }


        public virtual void BindPronoun(IPronoun pro) {
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
        }




        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entity which the Pronoun references.
        /// </summary>
        public virtual IEntity BoundEntity {
            get {
                return _boundEntity;
            }

        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf {
            get;
            set;
        }

        public virtual IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }

        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public virtual IVerbal SubjectOf {
            get;
            set;
        }
        public virtual void BindDescriptor(IDescriptor adj) {
            _describers.Add(adj);
        }

        public virtual IEnumerable<IDescriptor> DescribedBy {
            get {
                return _describers;
            }
        }

        public virtual IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }


        public virtual void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
            if (IsBound && !BoundEntity.Possessed.Contains(possession)) {
                BoundEntity.AddPossession(possession);
            }
        }

        public virtual IEntity Possesser {
            get;
            set;
        }
        public virtual EntityKind EntityKind {
            get {
                return _entityKind;
            }
        }
        public virtual PronounKind PronounKind {
            get;
            protected set;
        }


        #endregion


        #region Fields
        private ICollection<IDescriptor> _describers = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();
        public bool IsBound {
            get {
                return BoundEntity != null;
            }
        }
        private EntityKind _entityKind;
        private IEntity _boundEntity;

        #endregion






        public void BindToIEntity(IEntity target) {
            _boundEntity = target;
            _entityKind = BoundEntity.EntityKind;
        }

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
                PronounKind.ThirdPersonGenderAmbiguousPlurals :
                thirdPersonPluralReflexives.Contains(compareText) ?
                PronounKind.ThirdPersonPluraralReflexive :
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
