using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Provides the base class, shared properties, and shared behaviors for all noun words.
    /// </summary>
    public abstract class Noun : Word, IEntity, IDeterminable, IQuantifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Noun class.
        /// </summary>
        /// <param name="text">The text content of the Noun.</param>
        protected Noun(string text)
            : base(text) {
            EntityKind = EntityKind.Undefined;
            //EstablishKind();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Binds the given Determiner to The Noun.
        /// </summary>
        /// <param name="determiner">The Determiner which to bind.</param>
        public virtual void BindDeterminer(Determiner determiner) {
            determiner.Determines = this;
            Determiner = determiner;
        }

        /// <summary>
        /// Binds an EntityReferencer, generally a Pronoun or PronounPhrase to refer to the Noun.
        /// </summary>
        /// <param name="referencer">The EntityReferency to Bind.</param>
        public virtual void BindReferencer(IReferencer referencer) {
            referencers = referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }

        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the Noun.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the Noun's descriptors.</param>
        public virtual void BindDescriptor(IDescriptor descriptor) {
            descriptors = descriptors.Add(descriptor);
            descriptor.Describes = this;
        }

        /// <summary>
        /// Adds an IPossessable construct, such as a person place or thing, to the collection of IEntity instances the Noun "Owns", and
        /// sets its owner to be the Noun. If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public virtual void AddPossession(IPossessable possession) {
            possessions = possessions.Add(possession);
            possession.Possesser = this;
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets or sets the IVerbal instance the Noun is the subject of.
        /// </summary>
        public virtual IVerbal SubjectOf { get; set; }

        /// <summary>
        /// Gets or sets the ITRansitiveAction instance, usually a Verb or VerbPhrase, which the Noun is the direct object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf { get; set; }

        /// <summary>
        /// Gets or sets the IVerbal instance the Noun is the indirect object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf { get; set; }

        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the Noun Instance.
        /// </summary>
        public virtual IEnumerable<IReferencer> Referencers => referencers;

        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the Noun Instance.
        /// </summary>
        public virtual IEnumerable<IDescriptor> Descriptors => descriptors;

        /// <summary>
        /// Gets all of the IEntity constructs which the Noun "owns".
        /// </summary>
        public virtual IEnumerable<IPossessable> Possessions => possessions;

        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the Noun.
        /// </summary>
        public IPossesser Possesser {
            get { return possessor is IWeakPossessor ? (possessor as IWeakPossessor).ProxyFor ?? possessor : possessor; }
            set { possessor = value; }
        }

        /// <summary>
        /// Gets or sets the EntityKind; Person, Place, Thing, Organization, or Activity; of the Noun.
        /// </summary>
        public EntityKind EntityKind { get; protected set; }

        /// <summary>
        /// Gets the single Determiner which modifies the noun.
        /// </summary>
        public Determiner Determiner { get; protected set; }

        /// <summary>
        /// Gets or sets the single Noun which directly, in terms of reading order, specifies the current Noun instance. For example,
        /// consider the noun phrase "Felis Catus", the taxonomic nomenclature of the common domestic cat by its genus and species. While
        /// both "Felis" and "Catus" are individual nouns, the first implicitly specifies the second. Catus is the species of the genus
        /// Felis, but Felis also contains the species "Silvestris", commonly called a wildcat.
        /// </summary>
        public Noun PrecedingAdjunctNoun { get; set; }

        /// <summary>
        /// Gets or sets the single Noun which this Noun directly, in terms of reading order, specifies.
        /// </summary>
        public Noun FollowingAdjunctNoun { get; set; }

        /// <summary>
        /// Gets or sets the IQunatifier which specifies the number of units of the Noun which are referred to in this occurrence. e.g.
        /// "[18] Pinkos"
        /// </summary>
        public virtual IQuantifier QuantifiedBy {
            get {
                return quantity;
            }
            set {
                if (quantity != null) {
                    quantity.QuantifiedBy = value;
                    value.Quantifies = quantity;
                } else {
                    quantity = value;
                    value.Quantifies = this;
                }
            }
        }

        #endregion Properties

        #region Fields

        private IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;
        private IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        private IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;
        private IQuantifier quantity;
        private IPossesser possessor;

        #endregion Fields
    }
}