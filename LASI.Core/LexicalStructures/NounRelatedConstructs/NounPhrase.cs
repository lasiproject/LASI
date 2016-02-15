using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.Core.Heuristics;
using LASI.Core.LexicalStructures;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Represents a noun phrase such as "The Pinko-Commy Conspiracy".
    /// Note that noun componentPhrases are the constructs which wrap both nouns and pronouns at the phrase level.
    /// </summary>
    public class NounPhrase : Phrase, IEntity, IRoleCompositeLexical<Word, Noun>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the NounPhrase.</param>
        public NounPhrase(IEnumerable<Word> words) : base(words)
        {
            EntityKind = words
                .OfEntity()
                .Select(e => e.EntityKind)
                .DefaultIfEmpty()
                .GroupBy(kind => kind)
                .MaxBy(group => group.Count()).Key;
        }
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the NounPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the NounPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of NounPhrases. 
        /// Thus, its purpose is to simplify test code.</remarks>
        internal NounPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        #endregion

        #region Methods

        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the NounPhrase.
        /// </summary>
        /// <param name="referencer">The referencer which refers to the NounPhrase Instance.</param>
        public virtual void BindReferencer(IReferencer referencer)
        {
            referencers = referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the NounPhrase.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the NounPhrase' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor)
        {
            descriptors = descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the NounPhrase "Owns",
        /// and sets its owner to be the NounPhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            possessions = possessions.Add(possession);
            possession.Possesser = this.ToOption();
        }
        /// <summary>
        /// Returns a string representation of the NounPhrase.
        /// </summary>
        /// <returns>A string representation of the NounPhrase.</returns>

        /// <summary>
        /// Binds the <see cref="NounPhrase"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            subjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="NounPhrase"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsDirectObjectOf(IVerbal verbal)
        {
            DirectObjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="NounPhrase"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }

        public override string ToString()
        {
            if (!VerboseOutput)
            {
                return base.ToString();
            }
            var gender = this.GetGender();
            var aliases = this.GetDefinedAliases();
            var empty = string.Empty;
            return base.ToString() + string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                Possessions.Any() ? "\nPossessions: " + Possessions.Format(p => p.Text + '\n') : empty,
                Possesser.Match().Case((ILexical c) => $"\nPossessed By: {c.Text}").Result(empty),
                OuterAttributive != null ? "\nDefinedby: " + OuterAttributive.Text : empty,
                InnerAttributive != null ? "\nDefines: " + InnerAttributive.Text : empty,
                aliases.Any() ? "\nClassified as: " + aliases.Format() : empty,
                SubjectOf != null ? "\nSubject Of: " + SubjectOf.Text : empty,
                DirectObjectOf != null ? "\nDirect Object Of: " + DirectObjectOf.Text : empty,
                IndirectObjectOf != null ? "\nIndirect Object Of: " + IndirectObjectOf.Text : empty,
                gender.IsDetermined() ? "\nPrevailing Gender: " + gender : empty
            );

        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets all of the IReferencer instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase.
        /// </summary>
        public IEnumerable<IReferencer> Referencers => referencers;
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors => descriptors;

        /// <summary>
        /// Gets all of the constructs which the NounPhrase "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;

        /// <summary>
        /// Gets or sets another NounPhrase, to the left of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase OuterAttributive
        {
            get { return outerAttributive; }
            set { outerAttributive = value != this ? value : null; }
        }
        /// <summary>
        /// Gets or sets another NounPhrase, to the right of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase InnerAttributive
        {
            get { return innerAttributive; }
            set { innerAttributive = value != this ? value : null; }
        }

        /// <summary>
        /// Gets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the NounPhrase.
        /// </summary>
        public EntityKind EntityKind { get; protected set; }

        /// <summary>
        /// Gets or sets the Entity which "owns" the NounPhrase.
        /// </summary>
        public IOption<IPossesser> Possesser
        {
            get { return possessor; }
            set
            {
                possessor = value;
                // Bind entity words of the phrase as possessions of possessor.
                if (value != null)
                {
                    foreach (var entity in Words.OfEntity())
                    {
                        value.Match().Case((IEntity e) => e.AddPossession(entity));
                    }
                }
                else
                {
                    foreach (var entity in Words.OfEntity())
                    {
                        entity.Possesser = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="IVerbal"/> instance, generally a Verb or VerbPhrase, which the NounPhrase is the subject of.
        /// </summary>
        public virtual IVerbal SubjectOf
        {
            get { return subjectOf; }
            private set
            {
                subjectOf = value;
                foreach (var entity in Words.OfEntity())
                {
                    entity.BindAsSubjectOf(value);
                }
            }
        }
        /// <summary>
        /// Gets the <see cref="IVerbal"/> instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the DIRECT object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf
        {
            get { return directObjectOf; }
            private set
            {
                directObjectOf = value;
                foreach (var entity in Words.OfEntity())
                {
                    entity.BindAsDirectObjectOf(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IVerbal"/> instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf
        {
            get { return indirectObjectOf; }
            private set
            {
                indirectObjectOf = value;
                foreach (var entity in Words.OfEntity())
                {
                    entity.BindAsDirectObjectOf(value);
                }
            }
        }

        public virtual IEnumerable<Noun> RoleComponents => Words.OfNoun();
        #endregion

        #region Fields
        private IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;
        private IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        private IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;
        private IOption<IPossesser> possessor = Option.None<IPossesser>();
        private IVerbal directObjectOf;
        private IVerbal indirectObjectOf;
        private IVerbal subjectOf;
        private NounPhrase innerAttributive;
        private NounPhrase outerAttributive;

        #endregion
    }
}
