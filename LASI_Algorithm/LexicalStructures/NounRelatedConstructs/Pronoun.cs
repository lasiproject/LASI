using LASI.Algorithm.Patternization;
using LASI.Algorithm.Lookup;
using LASI.Utilities;
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
    public abstract class Pronoun : Word, IPronoun, IGendered
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Pronoun class.
        /// </summary>
        /// <param name="text">The key text content of the pronoun.</param>
        protected Pronoun(string text)
            : base(text) {
            PronounKind = DetermineKind(this);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Binds the Pronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target) {
            if (RefersTo == null) {
                RefersTo = new AggregateEntity(new[] { target });
            } else {
                RefersTo = new AggregateEntity(RefersTo.Append(target));
            }
            EntityKind = RefersTo.EntityKind;
        }
        /// <summary>   
        /// Returns a string representation of the Pronoun.
        /// </summary>
        /// <returns>A string representation of the Pronoun.</returns>
        public override string ToString() {
            return Type.Name + " \"" + Text + "\"" + (VerboseOutput ? " " + PronounKind + (RefersTo != null ? " referring to -> " + RefersTo.Text : string.Empty) : string.Empty);

        }

        /// <summary>
        /// Binds another IPronoun, generally another pronoun but possibly a PronounPhrase, to refer to the Pronoun.
        /// </summary>
        /// <param name="pro">An IPronoun which will be bound to refer to the Pronoun.</param>
        public virtual void BindPronoun(IPronoun pro) {
            _boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the Pronoun.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the Pronoun's descriptors.</param>
        public virtual void BindDescriptor(IDescriptor descriptor) {
            _descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of Pronoun "Owns",
        /// and sets its owner to be the Pronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public virtual void AddPossession(IPossessable possession) {
            if (RefersTo != null) {
                RefersTo.AddPossession(possession);
            } else {
                _possessed.Add(possession);
                possession.Possesser = this;
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the Pronoun.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return _descriptors; } }
        /// <summary>
        /// Gets all of the constructs the Pronoun can be determined to "own".
        /// </summary>
        public IEnumerable<IPossessable> Possessed { get { return _possessed; } }

        /// <summary>
        /// Gets or sets the Entity which the Pronoun references.
        /// </summary>
        public virtual IAggregateEntity RefersTo { get; private set; }

        /// <summary>
        /// Gets or sets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; set; }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; set; }
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the Pronoun.
        /// </summary>
        public IEnumerable<IPronoun> BoundPronouns { get { return _boundPronouns; } }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; set; }
        /// <summary>
        /// Gets or sets the Entity which is inferred to the Pronoun.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Gets the EntityKind of the Pronoun.
        /// </summary>
        public virtual EntityKind EntityKind { get; private set; }
        /// <summary>
        /// Gets the PronounKind of the Pronoun.
        /// </summary>
        public PronounKind PronounKind { get; protected set; }


        /// <summary>
        /// Gets the gender of the Pronoun.
        /// </summary>
        public virtual Gender Gender {
            get {
                return
                    this.IsFemale() ? Gender.Female :
                    this.IsMale() ? Gender.Male :
                    this.IsNeutral() ? Gender.Neutral :
                    Gender.Undetermined;
            }
        }

        #endregion

        #region Fields
        private HashSet<IDescriptor> _descriptors = new HashSet<IDescriptor>();
        private HashSet<IPossessable> _possessed = new HashSet<IPossessable>();
        private HashSet<IPronoun> _boundPronouns = new HashSet<IPronoun>();
        #endregion

        #region Static Members

        #region Static Methods

        /// <summary>
        /// Determines the PronounKind which corresponds to the Plurarility and Gender of the given pronoun.
        /// </summary>
        /// <param name="pronoun">The pronoun whose gender to is to be checked</param>
        /// <returns>A PronounGenerder enum value representing the gender of the given pronoun.</returns>
        private static PronounKind DetermineKind(Pronoun pronoun) {
            var text = pronoun.Text.ToLower();
            return
                males.Contains(text) ? PronounKind.Male :
                maleReflexives.Contains(text) ? PronounKind.MaleReflexive :
                females.Contains(text) ? PronounKind.Female :
                femaleReflexives.Contains(text) ? PronounKind.FemaleReflexive :
                neutrals.Contains(text) ? PronounKind.GenderNeurtral :
                neutralReflexives.Contains(text) ? PronounKind.GenderNeurtralReflexive :
                firstPersonSingulars.Contains(text) ? PronounKind.FirstPersonSingular :
                firstPersonPluralReflexives.Contains(text) ? PronounKind.FirstPersonPlural :
                firstPersonPlurals.Contains(text) ? PronounKind.FirstPersonPlural :
                firstPersonPluralReflexives.Contains(text) ? PronounKind.FirstPersonPluralReflexive :
                secondPersons.Contains(text) ? PronounKind.SecondPerson :
                secondPersonSingularReflexives.Contains(text) ? PronounKind.SecondPersonSingularReflexive :
                secondPersonPluralReflexives.Contains(text) ? PronounKind.SecondPersonPluralReflexive :
                thirdPersonGenderAmbiguousPlurals.Contains(text) ? PronounKind.ThirdPersonGenderAmbiguousPlural :
                thirdPersonPluralReflexives.Contains(text) ? PronounKind.ThirdPersonPluralReflexive :
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

        #endregion
    }
}
