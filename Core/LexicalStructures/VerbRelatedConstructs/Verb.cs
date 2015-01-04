using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Provides the base class for all word level verbal constructs. An instance of this class represents a verb in its base tense.
    /// </summary>
    public abstract class Verb : Word, IVerbal, IAdverbialModifiable, IModalityModifiable
    {
        public IEnumerable<IAdverbial> AttributedBy { get; }
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Verb class which represents the base tense form of a verb.
        /// </summary>
        /// <param name="text">The text content of the verb.</param>
        /// <param name="form">The tense of the verb</param>
        protected Verb(string text, VerbForm form)
            : base(text) {
            VerbForm = form;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Attaches an IAdverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb <param name="modifier">The
        /// IAdverbial construct by which to modify the AdjectivePhrase.</param>
        /// </summary>
        public virtual void ModifyWith(IAdverbial modifier) {
            modifiers = modifiers.Add(modifier);
            modifier.ModifiedBy = this;
        }

        /// <summary>
        /// Binds the Verb to an object via a prepositional construct such as a Preposition or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to".
        /// </summary>
        /// <param name="prepositional">The prepositional which links the verb and its prepositional object.</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional) {
            ObjectOfThePreposition = prepositional.BoundObject;
            PrepositionalToObject = prepositional;
        }

        /// <summary>
        /// Binds the given Entity as a subject of the Verb instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the Verb as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            subjects = subjects.Add(subject);
            subject.SubjectOf = this;
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the Verb instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the Verb as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            directObjects = directObjects.Add(directObject);
            directObject.DirectObjectOf = this;
            if (IsPossessive) {
                foreach (var subject in subjects) {
                    subject.AddPossession(directObject);
                }
            } else if (IsClassifier) {
                foreach (var subject in subjects) {
                    AliasLookup.DefineAlias(subject, directObject);
                }
            }
        }

        /// <summary>
        /// Binds the given Entity as an indirect object of the Verb instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the Verb as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            indirectObjects = indirectObjects.Add(indirectObject);
            indirectObject.IndirectObjectOf = this;
        }

        /// <summary>
        /// Determines if the Verb implies a possession relationship. E.g. in the sentence "They have a lot of ideas." the Verb "have"
        /// asserts a possessor possessee relationship between "They" and "a lot of ideas".
        /// </summary>
        /// <returns><c>true</c> if the Verb is a possessive relationship specifier; otherwise, <c>false</c>.</returns>
        protected virtual bool DetermineIsPossessive() => this.GetSynonyms().Contains("have", ignoreCase);

        /// <summary>
        /// Determines if the Verb acts as a classifier. E.g. in the sentence "Rodents are prey animals." the Verb "are" acts as a
        /// classification tool because it states that rodents are a subset of prey animals.
        /// </summary>
        /// <returns><c>true</c> if the Verb is a classifier; otherwise, <c>false</c>.</returns>
        protected virtual bool DetermineIsClassifier() {
            return !IsPossessive &&
                Modality == null &&
                AdverbialModifiers.None() &&
                this.GetSynonyms().Contains("be", ignoreCase);
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the Verb's subjects.
        /// </summary>
        public IAggregateEntity AggregateSubject => subjects.ToAggregate();

        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the Verb's direct objects.
        /// </summary>
        public IAggregateEntity AggregateDirectObject => directObjects.ToAggregate();

        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the Verb's indirect objects.
        /// </summary>
        public IAggregateEntity AggregateIndirectObject => indirectObjects.ToAggregate();

        /// <summary>
        /// Gets the subjects of the Verb.
        /// </summary>
        public IEnumerable<IEntity> Subjects => subjects;

        /// <summary>
        /// Gets the indirect objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects => indirectObjects;

        /// <summary>
        /// Gets the direct objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects => directObjects;

        /// <summary>
        /// Gets or the collection of IAdverbial modifiers which modify the Verb.
        /// </summary>
        public virtual IEnumerable<IAdverbial> AdverbialModifiers => modifiers;

        /// <summary>
        /// Gets or sets the ModalAuxilary word which modifies the Verb.
        /// </summary>
        public ModalAuxilary Modality { get; set; }

        /// <summary>
        /// Gets the VerbTense of the Verb.
        /// </summary>
        public VerbForm VerbForm { get; }

        /// <summary>
        /// Gets the object of the Verb's preposition. This can be any ILexical construct including a word, phrase, or clause.
        /// </summary>
        public ILexical ObjectOfThePreposition { get; protected set; }

        /// <summary>
        /// Gets the IPrepositional object which links the Verb to the ObjectOfThePreoposition.
        /// </summary>
        public IPrepositional PrepositionalToObject { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether or not the Verb has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier => (classifier = classifier ?? DetermineIsClassifier()) ?? false;

        /// <summary>
        /// Gets a value indicating whether or not the Verb has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive => (possessive = possessive ?? DetermineIsPossessive()) ?? false;

        #endregion Properties

        #region Fields

        private IImmutableSet<IAdverbial> modifiers = ImmutableHashSet<IAdverbial>.Empty;
        private IImmutableSet<IEntity> subjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> directObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> indirectObjects = ImmutableHashSet<IEntity>.Empty;
        private bool? possessive;
        private bool? classifier;

        private StringComparer ignoreCase = StringComparer.OrdinalIgnoreCase;

        #endregion Fields
    }
}