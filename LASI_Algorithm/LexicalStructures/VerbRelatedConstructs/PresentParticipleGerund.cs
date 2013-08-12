
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents the present participle/gerund form of a verb. As such takes the behavior of both a verb and an entity.
    /// </summary>
    public class PresentParticipleGerund : Verb, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the PresentPrtcplVerb class.
        /// </summary>
        /// <param name="text">The key text content of the Verb.</param>
        public PresentParticipleGerund(string text)
            : base(text, VerbTense.PresentParticiple) {
            EntityKind = EntityKind.Activitiy;
        }
        #region Methods

        /// <summary>
        /// Binds a Pronoun or PronounPhrase to refer to the gerund.
        /// </summary>
        /// <param name="pro">The Pronoun or PronounPhrase to Bind to the gerund</param>
        public void BindPronoun(IPronoun pro) {

            _boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }


        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the PresentParticipleGerund.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the PresentParticipleGerund' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            _descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the PresentParticipleGerund "Owns",
        /// and sets its owner to be the PresentParticipleGerund.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IEntity possession) {
            _possessed.Add(possession);
            possession.Possesser = this;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the PresentParticipleGerund.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return _descriptors; } }
        /// <summary>
        /// Gets all of the constructs which the PresentParticipleGerund "owns".
        /// </summary>
        public IEnumerable<IEntity> Possessed { get { return _possessed; } }
        /// <summary>
        /// Gets the collection of pronouns which are known to refer to the Gerund.
        /// </summary>
        public IEnumerable<IPronoun> BoundPronouns { get { return _boundPronouns; } }
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
        public IEntity Possesser { get; set; }
        /// <summary>
        /// Gets the Activitiy value of the EntityKind enumeration, the kind always associated with an PresentParticipleGerund.
        /// </summary>
        public EntityKind EntityKind { get; private set; }

        #endregion

        #region Fields

        private ICollection<IDescriptor> _descriptors = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();

        #endregion


    }
}
