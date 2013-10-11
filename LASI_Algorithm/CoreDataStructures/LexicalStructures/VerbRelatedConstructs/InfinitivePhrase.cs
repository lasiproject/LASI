using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an infinitive verbal phrase, e.g. "to walk".
    /// </summary>
    public class InfinitivePhrase : VerbPhrase, IEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the InfinitivePhrase class.
        /// </summary>
        /// <param name="composed">The words which comprise the InfinitivePhrase.</param>
        public InfinitivePhrase(IEnumerable<Word> composed)
            : base(composed)
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the Activitiy value of the EntityKind enumeration, the kind always associated with an InfinitivePhrase.
        /// </summary>
        public EntityKind EntityKind
        {
            get
            {
                return EntityKind.Activitiy;
            }
        }
        /// <summary>
        ///Gets or sets the IVerbal instance, usually a Verb or VerbPhrase, which the InfinitivePhrase is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf
        {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the InfinitivePhrase is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf
        {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the InfinitivePhrase is the subject of.
        /// </summary>
        public IVerbal SubjectOf
        {
            get;
            set;
        }
        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the InfinitivePhrase.
        /// </summary>
        /// <param name="pro">The referencer which refers to the InfinitivePhrase Instance.</param>
        public void BindPronoun(IReferencer pro)
        {
            _boundPronouns.Add(pro);
            pro.BindAsReference(this);
        }
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the InfinitivePhrase Instance.
        /// </summary>
        public IEnumerable<IReferencer> BoundPronouns
        {
            get
            {
                return _boundPronouns;
            }
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the InfinitivePhrase.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the InfinitivePhrase' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor)
        {
            _descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Gets all of the IDescriptor constructs, generally Adjectives or AdjectivePhrases, which describe the InfinitivePhrase Instance.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors
        {
            get
            {
                return _descriptors;
            }
        }
        /// <summary>
        /// Gets all of the IEntity constructs which the InfinitivePhrase "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessed
        {
            get
            {
                return _possessions;
            }
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the InfinitivePhrase "Owns",
        /// and sets its owner to be the InfinitivePhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            _possessions.Add(possession);
        }
        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the InfinitivePhrase.
        /// </summary>
        public IPossesser Possesser
        {
            get;
            set;
        }

        #endregion

        #region Fields

        ISet<IReferencer> _boundPronouns = new HashSet<IReferencer>();
        ISet<IPossessable> _possessions = new HashSet<IPossessable>();
        ISet<IDescriptor> _descriptors = new HashSet<IDescriptor>();

        #endregion
    }
}
