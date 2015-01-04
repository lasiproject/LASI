
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents the present participle form of a verb. As such it may act as verbal, when paired with an auxilary, an entity, or a descriptor.
    /// </summary>
    public class PresentParticiple : Verb, IEntity, IDescriptor 
    {
        #region Constuctors
        /// <summary>
        /// Initializes a new instance of the PresentParticiple class.
        /// </summary>
        /// <param name="text">The text content of the Verb.</param>
        public PresentParticiple(string text)
            : base(text, VerbForm.PresentParticiple) {
            EntityKind = EntityKind.Activity;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Binds a referencer, e.g. a Pronoun or PronounPhrase, to refer to the gerund.
        /// </summary>
        /// <param name="referencer">The referencer to bind to the gerund</param>
        public void BindReferencer(IReferencer referencer) {
            referencers = referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the PresentParticiple.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the PresentParticiple' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors = descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the PresentParticiple "Owns",
        /// and sets its owner to be the PresentParticiple.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possessable">The possession to add.</param>
        public void AddPossession(IPossessable possessable) {
            possessions = possessions.Add(possessable);
            possessable.Possesser = this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets all of the IDescriptor constructs, generally Adjectives or AdjectivePhrases, which describe the PresentParticiple.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors => descriptors;
        /// <summary>
        /// Gets all of the constructs which the PresentParticiple "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;
        /// <summary>
        /// Gets the collection of referencers which are bound as referring to the PresentParticiple.
        /// </summary>
        public IEnumerable<IReferencer> Referencers => referencers;
        /// <summary>
        /// The Verb construct which the PresentParticiple is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; set; }
        /// <summary>
        /// The Verb construct which the PresentParticiple is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; set; }
        /// <summary>
        /// The Verb construct which the PresentParticiple is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; set; }
        /// <summary>
        /// Gets or sets the Entity which "owns" the PresentParticiple.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Gets the Activity value of the EntityKind enumeration, the kind always associated with an PresentParticiple.
        /// </summary>
        public EntityKind EntityKind { get; }
        /// <summary>
        /// Gets or sets the Entity which the PresentParticiple describes.
        /// </summary>
        public IEntity Describes { get; set; }
        public IEntity AttributedTo => Describes;

        #endregion

        #region Fields
        private IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;
        private IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        private IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;
        #endregion
    }
}
