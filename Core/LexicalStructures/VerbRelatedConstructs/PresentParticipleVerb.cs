
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents the present participle form of a verb. As such it may act as verbal, when paired with an auxilary, an entity, or a descriptor.
    /// </summary>
    public class PresentParticipleVerb : Verb, IEntity, IDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the PresentParticiple class.
        /// </summary>
        /// <param name="text">The text content of the Verb.</param>
        public PresentParticipleVerb(string text)
            : base(text, VerbForm.PresentParticiple) {
            EntityKind = EntityKind.Activity;
        }
        #region Methods

        /// <summary>
        /// Binds a referencer, e.g. a Pronoun or PronounPhrase, to refer to the gerund.
        /// </summary>
        /// <param name="referencer">The referencer to bind to the gerund</param>
        public void BindReferencer(IReferencer referencer) {
            referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }


        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the PresentParticiple.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the PresentParticiple' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the PresentParticiple "Owns",
        /// and sets its owner to be the PresentParticiple.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession) {
            possessed.Add(possession);
            possession.Possesser = this;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the PresentParticiple.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return descriptors; } }
        /// <summary>
        /// Gets all of the constructs which the PresentParticiple "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions { get { return possessed; } }
        /// <summary>
        /// Gets the collection of referencers which are bound as referring to the PresentParticiple.
        /// </summary>
        public IEnumerable<IReferencer> Referencers { get { return referencers; } }
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
        public EntityKind EntityKind { get; private set; }

        /// <summary>
        /// Gets or sets the Entity which the PresentParticiple describes.
        /// </summary>
        public IEntity Describes { get; set; }

        #endregion

        #region Fields

        private ISet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        private ISet<IPossessable> possessed = new HashSet<IPossessable>();
        private ISet<IReferencer> referencers = new HashSet<IReferencer>();

        #endregion
    }
}
