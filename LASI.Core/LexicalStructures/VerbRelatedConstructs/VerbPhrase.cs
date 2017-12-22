using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.Core.Heuristics;
using LASI.Core.LexicalStructures;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Represents a Verb Phrase, a Phrase with the syntactic role of a verb.
    /// </summary>
    public class VerbPhrase : Phrase, IVerbal, IAdverbialModifiable, IModalityModifiable, IRoleCompositeLexical<Word, Verb>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the VerbPhrase.</param>
        public VerbPhrase(IEnumerable<Word> words) : base(words)
        {
            var constituentVerbForms = from verb in Words.OfVerb()
                                       let typeName = verb.GetType().Name
                                       group typeName by typeName into byTypeName
                                       select new
                                       {
                                           Name = byTypeName.Key,
                                           Count = byTypeName.Count()
                                       };

            PrevailingForm = constituentVerbForms
                .DefaultIfEmpty(new { Name = "Undetermined", Count = 1 })
                .MaxBy(form => form.Count).Name;

            modifiers = modifiers.Union(words.OfAdverb());
        }

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the VerbPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the VerbPhrase.</param>
        /// <remarks>
        /// This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. Thus, its purpose
        /// is to simplify test code.
        /// </remarks>
        public VerbPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        #endregion Constructors
        /// <summary>
        /// Gets or the collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> AttributedBy => AdverbialModifiers;

        #region Methods

        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb.
        /// </summary>
        /// <param name="modifier">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public void ModifyWith(IAdverbial modifier)
        {
            modifiers = modifiers.Add(modifier);
            modifier.Modifies = this;
        }

        /// <summary>
        /// <para>Binds the VerbPhrase to an object via a prepositional construct such as a Preposition or PrepositionalPhrase.</para>
        /// <para>Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"</para>
        /// </summary>
        /// <param name="prepositional">The IPrepositional construct through which the Object is associated.</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional)
        {
            ObjectOfThePreposition = prepositional.BoundObject;
            PrepositionalToObject = prepositional;
        }

        /// <summary>
        /// Binds the given Entity as a subject of the VerbPhrase instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the VerbPhrase as a subject.</param>
        public virtual void BindSubject(IEntity subject)
        {
            if (subject != null)
            {
                subjects = subjects.Add(subject);
                subject.BindAsSubjectOf(this);
                if (PostpositiveDescriptor != null)
                {
                    subject.BindDescriptor(postpositiveDescriptor);
                }
                foreach (var verb in Words.OfVerb())
                {
                    verb.BindSubject(subject);
                }
            }
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the VerbPhrase instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the VerbPhrase as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject)
        {
            if (directObject != null)
            {
                directObjects = directObjects.Add(directObject);
                directObject.BindAsDirectObjectOf(this);
                foreach (var verb in Words.OfVerb())
                {
                    verb.BindDirectObject(directObject);
                }
                if (IsPossessive)
                {
                    foreach (var subject in Subjects)
                    {
                        subject.AddPossession(directObject);
                    }
                }
                else if (IsClassifier)
                {
                    foreach (var subject in Subjects)
                    {
                        AliasLookup.DefineAlias(subject, directObject);
                    }
                }
            }
        }

        /// <summary>
        /// Binds the given Entity as an indirect object of the VerbPhrase instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the VerbPhrase as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject)
        {
            if (indirectObject != null)
            {
                indirectObjects = indirectObjects.Add(indirectObject);
                indirectObject.BindAsIndirectObjectOf(this);
                foreach (var v in Words.OfVerb()) { v.BindIndirectObject(indirectObject); }
            }
        }

        /// <summary>
        /// Returns a string representation of the VerbPhrase.
        /// </summary>
        /// <returns>A string representation of the VerbPhrase.</returns>
        public override string ToString()
        {
            var empty = string.Empty;
            return !VerboseOutput ?
                base.ToString() :
                string.Join("\n", base.ToString(),
                    Subjects.Any() ? $"Subjects: {Subjects.Format(s => s.Text)}" : empty,
                    SubjectComplement != null ? $"\nAttached Subject Complement{SubjectComplement.Text}" : empty,
                    DirectObjects.Any() ? $"Direct Objects: {DirectObjects.Format(o => o.Text)}" : empty,
                    IndirectObjects.Any() ? $"Indirect Objects: {IndirectObjects.Format(o => o.Text)}" : empty,
                    ObjectOfThePreposition != null ? $"Via Preposition Object: {ObjectOfThePreposition.Text}" : empty,
                    Modality != null ? $"Modality: {Modality.Text}" : empty,
                    AdverbialModifiers.Any() ? $"Modifiers: {AdverbialModifiers.Format(m => m.Text)}" : empty,
                    $"\nPossessive: [{(IsPossessive ? "Yes" : "No")}]",
                    $"\nClassifier: [{(IsClassifier ? "Yes" : "No")}]",
                    $"\nPrevailing Form: [{PrevailingForm.SpaceByCase().RemoveSubstrings("Verb").Trim()}]"
                );
        }

        /// <summary>
        /// Determines if the VerbPhrase implies a possession relationship. E.g. in the sentence "They certainly have a lot of ideas." the
        /// VerbPhrase "certainly have" asserts a possessor possessee relationship between "They" and "a lot of ideas".
        /// </summary>
        /// <returns><c>true</c> if the VerbPhrase is a possessive relationship specifier; otherwise, <c>false</c>.</returns>
        private bool DetermineIsPossessive() => Words.OfVerb().DefaultIfEmpty().Last()?.IsPossessive ?? false;

        /// <summary>
        /// Determines if the VerbPhrase acts as a classifier. E.g. in the sentence "Rodents are definitely prey animals." the VerbPhrase
        /// "are definitely" acts as a classifier because it states that rodents are a subset of prey animals.
        /// </summary>
        /// <returns><c>true</c> if the VerbPhrase is a classifier; otherwise, <c>false</c>.</returns>
        private bool DetermineIsClassifier() => Words.OfVerb().DefaultIfEmpty().Last()?.IsClassifier ?? false;

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's subjects.
        /// </summary>
        public IAggregateEntity AggregateSubject => subjects.ToAggregate();

        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's direct objects.
        /// </summary>
        public IAggregateEntity AggregateDirectObject => directObjects.ToAggregate();

        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's indirect objects.
        /// </summary>
        public IAggregateEntity AggregateIndirectObject => indirectObjects.ToAggregate();

        /// <summary>
        /// The collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> AdverbialModifiers => modifiers;

        /// <summary>
        /// Gets or sets the IDescriptor which modifies, by way of the Verbal, its Subject.
        /// </summary>
        public IDescriptor PostpositiveDescriptor
        {
            get => postpositiveDescriptor;
            set
            {
                postpositiveDescriptor = value;
                foreach (var described in Subjects) { described.BindDescriptor(value); }
            }
        }
        public virtual IEnumerable<Verb> RoleComponents => Words.OfVerb();

        /// <summary>
        /// Gets or sets the ModalAuxilary word which modifies the VerbPhrase.
        /// </summary>
        public ModalAuxilary Modality { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the VerbPhrase has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive => isPossessive ?? (isPossessive = DetermineIsPossessive()) ?? false;

        /// <summary>
        /// Gets a value indicating whether or not the VerbPhrase has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier => isClassifier ?? (isClassifier = DetermineIsClassifier()) ?? false;

        /// <summary>
        /// The subjects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> Subjects => subjects;

        /// <summary>
        /// The direct objects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> DirectObjects => directObjects;

        /// <summary>
        /// The indirect objects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> IndirectObjects => indirectObjects;

        /// <summary>
        /// Gets the VerbPhrases's object, If the VerbPhrase has an object bound via a Prepositional. This can be any ILexical construct
        /// including a word, phrase, or clause.
        /// </summary>
        public ILexical ObjectOfThePreposition { get; protected set; }

        /// <summary>
        /// The IPrepositional object which links the VerbPhrase to the ObjectOfThePreoposition.
        /// </summary>
        public IPrepositional PrepositionalToObject { get; protected set; }
        /// <summary>Gets all of the Direct and Indirect objects of the VerbPhrase.</summary>
        public IEnumerable<IEntity> DirectAndIndirectObjects => DirectObjects.Concat(IndirectObjects);
        /// <summary>
        /// The string representation of the <see cref="VerbPhrase"/>'s prevalent form.
        /// </summary>
        public string PrevailingForm { get; }
        /// <summary>
        /// Gets or sets the subject complement of the <see cref="VerbPhrase"/>.
        /// </summary>
        public ILexical SubjectComplement { get; set; }

        #endregion Properties

        #region Fields

        private IImmutableSet<IAdverbial> modifiers = ImmutableHashSet<IAdverbial>.Empty;
        private IImmutableSet<IEntity> subjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> directObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> indirectObjects = ImmutableHashSet<IEntity>.Empty;

        private IDescriptor postpositiveDescriptor;

        private bool? isClassifier;
        private bool? isPossessive;

        #endregion Fields
    }
}