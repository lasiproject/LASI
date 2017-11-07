using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Represents an infinitive verbal phrase, e.g. "to walk".
    /// </summary>
    public class InfinitivePhrase : VerbPhrase, IEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InfinitivePhrase"/> class.
        /// </summary>
        /// <param name="words">The words which comprise the <see cref="InfinitivePhrase"/>.</param>
        public InfinitivePhrase(IEnumerable<Word> words) : base(words) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="InfinitivePhrase"/> class.
        /// </summary>
        /// <param name="first">The first Word of the <see cref="InfinitivePhrase"/>.</param>
        /// <param name="rest">The rest of the Words comprise the <see cref="InfinitivePhrase"/>.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public InfinitivePhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        #endregion

        #region Properties
        /// <summary>
        /// The IVerbal that the <see cref="InfinitivePhrase"/> is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; private set; }

        /// <summary>
        /// The Activity value of the EntityKind enumeration, the kind always associated with an <see cref="InfinitivePhrase"/>.
        /// </summary>
        public EntityKind EntityKind => EntityKind.Activity;

        /// <summary>
        /// The IVerbal, usually a Verb or VerbPhrase, which the <see cref="InfinitivePhrase"/> is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; private set; }

        /// <summary>
        /// The IVerbal that the <see cref="InfinitivePhrase"/> is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; private set; }

        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the <see cref="InfinitivePhrase"/>.
        /// </summary>
        /// <param name="referencer">The referencer which refers to the <see cref="InfinitivePhrase"/>.</param>
        public void BindReferencer(IReferencer referencer)
        {
            referencers = referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }

        /// <summary>
        /// Binds an IDescriptor, generally an <see cref="Adjective"/> or <see cref="AdjectivePhrase"/>, as a descriptor of the <see cref="InfinitivePhrase"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="IDescriptor"/> which will be bound as a descriptor of the <see cref="InfinitivePhrase"/>.</param>
        public void BindDescriptor(IDescriptor descriptor)
        {
            descriptors = descriptors.Add(descriptor);
            descriptor.Describes = this;
        }

        /// <summary>
        /// Binds the <see cref="InfinitivePhrase"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            SubjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="InfinitivePhrase"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsDirectObjectOf(IVerbal verbal)
        {
            DirectObjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="InfinitivePhrase"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }

        /// <summary>
        /// The <see cref="IReferencer"/> instances, generally Pronouns or PronounPhrases, which refer to the <see cref="InfinitivePhrase"/>.
        /// </summary>
        public IEnumerable<IReferencer> Referencers => referencers;

        /// <summary>
        /// The IDescriptor constructs, generally <see cref="Adjective"/>s or <see cref="AdjectivePhrase"/>s, which describe the <see cref="InfinitivePhrase"/>.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors => descriptors;

        /// <summary>
        /// The IEntity constructs which the <see cref="InfinitivePhrase"/> "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;

        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the <see cref="InfinitivePhrase"/> "Owns",
        /// and sets its owner to be the <see cref="InfinitivePhrase"/>.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            possessions = possessions.Add(possession);
            possession.Possesser = this;
        }

        /// <summary>
        /// The <see cref="IPossesser"/>, generally an <see cref="IEntity"/>, which "owns" the <see cref="InfinitivePhrase"/>.
        /// </summary>
        public IPossesser Possesser { get; set; }

        #endregion

        #region Fields

        IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;
        IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;

        #endregion
    }
}
