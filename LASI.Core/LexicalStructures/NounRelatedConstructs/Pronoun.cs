using LASI.Core.Heuristics;
using LASI.Utilities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LASI.Core
{
    using Kind = PronounKind;
    /// <summary>
    /// Represents a pronoun which generally refers back to a previously defined Entity, such as a Noun or NounPhrase.
    /// </summary>
    public abstract class Pronoun : Word, IReferencer, ISimpleGendered
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Pronoun class.
        /// </summary>
        /// <param name="text">The text content of the pronoun.</param>
        protected Pronoun(string text) : base(text) => PronounKind = DetermineKind(this);

        #endregion

        #region Methods
        /// <summary>
        /// Binds the Pronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target)
        {
            if (RefersTo == null)
            {
                RefersTo = new AggregateEntity(target);
            }
            else
            {
                RefersTo = new AggregateEntity(RefersTo, target);
            }
            EntityKind = RefersTo.EntityKind;
        }
        /// <summary>   
        /// Returns a string representation of the Pronoun.
        /// </summary>
        /// <returns>A string representation of the Pronoun.</returns>
        public override string ToString() => GetType().Name + " \"" + Text + "\"" +
        (
            VerboseOutput ? " " + PronounKind + (
                RefersTo.EmptyIfNull().Any() ?
                " referring to -> " + RefersTo.Text :
                string.Empty
            ) : string.Empty
        );

        /// <summary>
        /// Binds another IReferencer, generally another Pronoun but possibly a PronounPhrase, to refer to the Pronoun.
        /// </summary>
        /// <param name="referencer">An IReferencer which will be bound to refer to the Pronoun.</param>
        public virtual void BindReferencer(IReferencer referencer)
        {
            boundPronouns.Add(referencer);
            referencer.BindAsReferringTo(new AggregateEntity(this, this.RefersTo));
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the Pronoun.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the Pronoun's descriptors.</param>
        public virtual void BindDescriptor(IDescriptor descriptor)
        {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of Pronoun "Owns",
        /// and sets its owner to be the Pronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public virtual void AddPossession(IPossessable possession)
        {
            if (RefersTo != null)
            {
                RefersTo.AddPossession(possession);
            }
            else
            {
                possessions.Add(possession);
                possession.Possessor = this;
            }
        }

        /// <summary>
        /// Binds the <see cref="Pronoun"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            SubjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="Pronoun"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsDirectObjectOf(IVerbal verbal)
        {
            DirectObjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="Pronoun"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the Pronoun.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors => descriptors;
        /// <summary>
        /// Gets all of the constructs the Pronoun can be determined to "own".
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;
        /// <summary>
        /// Gets all of the IReferencer instances, generally Pronouns or PronounPhrases, which refer to the Pronoun.
        /// </summary>
        public IEnumerable<IReferencer> Referencers => boundPronouns;
        /// <summary>
        /// The <see cref="IVerbal"/> which the Pronoun references.
        /// </summary>
        public virtual IAggregateEntity RefersTo { get; private set; }

        /// <summary>
        /// Gets or sets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; private set; }
        /// <summary>
        /// The <see cref="IVerbal"/> instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; private set; }

        /// <summary>
        /// The <see cref="IVerbal"/> instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; private set; }
        /// <summary>
        /// Gets or sets the <see cref="IVerbal"/> which is inferred to the Pronoun.
        /// </summary>
        public IPossessor Possessor { get; set; }
        /// <summary>
        /// The EntityKind of the Pronoun.
        /// </summary>
        public virtual EntityKind EntityKind { get; private set; }
        /// <summary>
        /// The PronounKind of the Pronoun.
        /// </summary>
        public Kind PronounKind { get; protected set; }


        /// <summary>
        /// The gender of the Pronoun.
        /// </summary>
        public virtual Gender Gender => this.IsFemale() ? Gender.Female : this.IsMale() ? Gender.Male : this.IsNeutral() ? Gender.Neutral : Gender.Undetermined;

        #endregion

        #region Fields
        private readonly HashSet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        private readonly HashSet<IPossessable> possessions = new HashSet<IPossessable>();
        private readonly HashSet<IReferencer> boundPronouns = new HashSet<IReferencer>();
        #endregion

        #region Static Members

        #region Static Methods

        /// <summary>
        /// Determines the PronounKind which corresponds to the Plurality and Gender of the given pronoun.
        /// </summary>
        /// <param name="pronoun">The pronoun whose gender to is to be checked</param>
        /// <returns>A PronounGenerder enum value representing the gender of the given pronoun.</returns>
        private static Kind DetermineKind(Pronoun pronoun)
        {
            var text = pronoun.Text.ToLower();
            return
                males.Contains(text)
                ? Kind.Male
                : maleReflexives.Contains(text)
                ? Kind.MaleReflexive
                : females.Contains(text)
                ? Kind.Female : femaleReflexives.Contains(text)
                ? Kind.FemaleReflexive
                : neutrals.Contains(text)
                ? Kind.GenderNeurtral
                : neutralReflexives.Contains(text)
                ? Kind.GenderNeurtralReflexive
                : firstPersonSingulars.Contains(text)
                ? Kind.FirstPersonSingular
                : firstPersonPluralReflexives.Contains(text)
                ? Kind.FirstPersonPlural
                : firstPersonPlurals.Contains(text)
                ? Kind.FirstPersonPlural
                : firstPersonPluralReflexives.Contains(text)
                ? Kind.FirstPersonPluralReflexive
                : secondPersons.Contains(text)
                ? Kind.SecondPerson
                : secondPersonSingularReflexives.Contains(text)
                ? Kind.SecondPersonSingularReflexive
                : secondPersonPluralReflexives.Contains(text)
                ? Kind.SecondPersonPluralReflexive
                : thirdPersonGenderAmbiguousPlurals.Contains(text)
                ? Kind.ThirdPersonGenderAmbiguousPlural
                : thirdPersonPluralReflexives.Contains(text)
                ? Kind.ThirdPersonPluralReflexive
                : Kind.Undefined;
        }
        #endregion

        #region Static Fields

        //Common personal Pronouns by gender and plurality 
        private static readonly ISet<string> males = new HashSet<string> { "he", "him", "his" };
        private static readonly ISet<string> maleReflexives = new HashSet<string> { "himself", "hisself", };
        private static readonly ISet<string> females = new HashSet<string> { "she", "her", "hers" };
        private static readonly ISet<string> femaleReflexives = new HashSet<string> { "herself" };
        private static readonly ISet<string> neutrals = new HashSet<string> { "one", "it", "itself", "its" };
        private static readonly ISet<string> neutralReflexives = new HashSet<string> { "itself" };
        private static readonly ISet<string> firstPersonSingulars = new HashSet<string> { "i", "me", "mine" };
        private static readonly ISet<string> firstPersonSingularReflexives = new HashSet<string> { "myself" };
        private static readonly ISet<string> firstPersonPlurals = new HashSet<string> { "we", "us", "ours" };
        private static readonly ISet<string> firstPersonPluralReflexives = new HashSet<string> { "ourselves" };
        private static readonly ISet<string> secondPersons = new HashSet<string> { "you", "yours" };
        private static readonly ISet<string> secondPersonSingularReflexives = new HashSet<string> { "yourself" };
        private static readonly ISet<string> secondPersonPluralReflexives = new HashSet<string> { "yourselves" };
        private static readonly ISet<string> thirdPersonGenderAmbiguousPlurals = new HashSet<string> { "them", "they", "theirs" };
        private static readonly ISet<string> thirdPersonPluralReflexives = new HashSet<string> { "themselves", "theirselves" };

        #endregion

        #endregion
    }
}
