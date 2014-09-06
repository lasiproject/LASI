
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents the present participle/gerund form of a verb. As such takes the behavior of both a verb and an entity.
    /// </summary>
    public class PresentParticipleGerund : Verb, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the PresentPrtcplVerb class.
        /// </summary>
        /// <param name="text">The text content of the Verb.</param>
        public PresentParticipleGerund(string text)
            : base(text, VerbForm.PresentParticiple) {
            EntityKind = EntityKind.Activity;
        }
        #region Methods

        /// <summary>
        /// Binds an IReferencer, e.g. a Pronoun or PronounPhrase, to refer to the gerund.
        /// </summary>
        /// <param name="pro">The Pronoun or PronounPhrase to Bind to the gerund</param>
        public void BindReferencer(IReferencer pro) {

            boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }


        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the PresentParticipleGerund.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the PresentParticipleGerund' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the PresentParticipleGerund "Owns",
        /// and sets its owner to be the PresentParticipleGerund.
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
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the PresentParticipleGerund.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return descriptors; } }
        /// <summary>
        /// Gets all of the constructs which the PresentParticipleGerund "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions { get { return possessed; } }
        /// <summary>
        /// Gets the collection of pronouns which are known to refer to the Gerund.
        /// </summary>
        public IEnumerable<IReferencer> Referencers { get { return boundPronouns; } }
        /// <summary>
        /// The Verb construct which the Gerund is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; set; }
        /// <summary>
        /// The Verb construct which the gerund is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; set; }
        /// <summary>
        /// The Verb construct which the gerund is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; set; }

        /// <summary>
        /// Gets or sets the Entity which "owns" the PresentParticipleGerund.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Gets the Activity value of the EntityKind enumeration, the kind always associated with an PresentParticipleGerund.
        /// </summary>
        public EntityKind EntityKind { get; private set; }

        #endregion

        #region Fields

        private ICollection<IDescriptor> descriptors = new List<IDescriptor>();
        private ICollection<IPossessable> possessed = new List<IPossessable>();
        private ICollection<IReferencer> boundPronouns = new List<IReferencer>();

        #endregion
    }
}
