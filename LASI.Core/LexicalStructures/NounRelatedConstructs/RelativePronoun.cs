using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace LASI.Core
{
    using Kind = RelativePronounKind;
    /// <summary>
    /// Represents a Relative Pronoun such as "that", "which, "what" or "who".
    /// </summary>
    public class RelativePronoun : Word, IReferencer, ISubordinator
    {
        /// <summary>
        /// Initializes a new instance of the RelativePronoun class.
        /// </summary>
        /// <param name="text">The text content of the RelativePronoun.</param>
        public RelativePronoun(string text)
            : base(text) => RelativePronounKind = DetermineKind(this);

        #region Methods
        /// <summary>
        /// Binds the RelativePronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target)
        {
            if (RefersTo == null || !RefersTo.Any())
            {
                RefersTo = new AggregateEntity(target);
            }
            else
            {
                RefersTo = new AggregateEntity(RefersTo.Append(target));
            }
            EntityKind = RefersTo.EntityKind;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of IEntity instances the RelativePronoun "Owns",
        /// and sets its owner to be the RelativePronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            if (IsBound)
            {
                RefersTo.AddPossession(possession);
            }
            else
            {
                possessions = possessions.Add(possession);
                possession.Possesser = this;
            }
        }
        /// <summary>
        /// Binds an EntityReferencer, generally a Pronoun or PronounPhrase to refer to the RelativePronoun.
        /// </summary>
        /// <param name="referencer">The EntityReferency to Bind.</param>
        public void BindReferencer(IReferencer referencer)
        {
            referencers = referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the RelativePronoun.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the RelativePronoun's descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor)
        {
            descriptors = descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Returns a string representation of the <see cref="RelativePronoun"/>.
        /// </summary>
        /// <returns>A string representation of the <see cref="RelativePronoun"/>.</returns>
        public override string ToString() => Text + (VerboseOutput ? " " + RelativePronounKind : string.Empty);

        /// <summary>
        /// Binds the <see cref="RelativePronoun"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            SubjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="RelativePronoun"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsDirectObjectOf(IVerbal verbal) => DirectObjectOf = verbal;

        /// <summary>
        /// Binds the <see cref="RelativePronoun"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The IEntity which can be said to "own" the <see cref="RelativePronoun"/>.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Indicates whether or not the IPronoun is bound to an Entity.
        /// </summary>
        public bool IsBound => RefersTo != null && RefersTo.Any();
        /// <summary>
        /// The RelativePronounKind of the RelativePronoun.
        /// </summary>
        public Kind RelativePronounKind { get; }
        /// <summary>
        /// The Entity which the RelativePronoun references.
        /// </summary>
        public IAggregateEntity RefersTo { get; private set; }

        /// <summary>
        ///The EntityKind; Person, Place, Thing, Organization, or Activity; of the <see cref="RelativePronoun"/>.
        /// </summary>
        public EntityKind EntityKind { get; private set; }
        /// <summary>
        /// The IVerbal instance the RelativePronoun is the subject object of.
        /// </summary>
        public IVerbal SubjectOf { get; private set; }
        /// <summary>
        /// The IVerbal instance the RelativePronoun is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; private set; }
        /// <summary>
        /// The IVerbal instance the RelativePronoun is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; private set; }
        /// <summary>
        /// The IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the <see cref="RelativePronoun"/>.
        /// </summary>
        public IEnumerable<IReferencer> Referencers => referencers;
        /// <summary>
        /// The IDescriptors, generally Adjectives or AdjectivePhrases which describe the <see cref="RelativePronoun"/>.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors => descriptors;
        /// <summary>
        /// The IEntity instances which the RelativePronoun can be said to "own".
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;


        /// <summary>
        /// The Lexical construct which is subordinated by the <see cref="RelativePronoun"/>.
        /// </summary>
        public ILexical Subordinates { get; set; }


        #endregion

        IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;
        IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;

        #region Static Members
        static Kind DetermineKind(RelativePronoun relativePronoun)
        {
            var text = relativePronoun.Text.ToLower();
            return subjectRolePersonal.Contains(text)
                ? Kind.SubjectRolePersonal
                : objectRoleEntity.Contains(text)
                ? Kind.ObjectRoleEntity
                : objectRoleLocationals.Contains(text)
                ? Kind.ObjectRoleLocational
                : objectRoleTemporals.Contains(text)
                ? Kind.ObjectRoleTemporal
                : objectRoleExpositories.Contains(text)
                ? Kind.ObjectRoleExpository
                : Kind.Undetermined;
        }

        static readonly HashSet<string> subjectRolePersonal = new HashSet<string> { "who", "that" };
        static readonly HashSet<string> objectRoleLocationals = new HashSet<string> { "where" };
        static readonly HashSet<string> objectRoleEntity = new HashSet<string> { "whom", "which", "who", "that" };
        static readonly HashSet<string> objectRoleTemporals = new HashSet<string> { "when" };
        static readonly HashSet<string> objectRoleExpositories = new HashSet<string> { "what", "why" };
        #endregion
    }
}
