using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
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
        /// <param name="words">The words which comprise the InfinitivePhrase.</param>
        public InfinitivePhrase(IEnumerable<Word> words) : base(words) { }
        /// <summary>
        /// Initializes a new instance of the InfinitivePhrase class.
        /// </summary>
        /// <param name="first">The first Word of the InfinitivePhrase.</param>
        /// <param name="rest">The rest of the Words comprise the InfinitivePhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public InfinitivePhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
        
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Activity value of the EntityKind enumeration, the kind always associated with an InfinitivePhrase.
        /// </summary>
        public EntityKind EntityKind { get { return EntityKind.Activity; } }
        /// <summary>
        ///Gets or sets the IVerbal instance, usually a Verb or VerbPhrase, which the InfinitivePhrase is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; set; }
        /// <summary>
        ///Gets or sets the IVerbal instance the InfinitivePhrase is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; set; }
        /// <summary>
        ///Gets or sets the IVerbal instance the InfinitivePhrase is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; set; }

        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the InfinitivePhrase.
        /// </summary>
        /// <param name="pro">The referencer which refers to the InfinitivePhrase Instance.</param>
        public void BindReferencer(IReferencer pro) {
            boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the InfinitivePhrase Instance.
        /// </summary>
        public IEnumerable<IReferencer> Referencers { get { return boundPronouns; } }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the InfinitivePhrase.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the InfinitivePhrase' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Gets all of the IDescriptor constructs, generally Adjectives or AdjectivePhrases, which describe the InfinitivePhrase Instance.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return descriptors; } }

        /// <summary>
        /// Gets all of the IEntity constructs which the InfinitivePhrase "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions { get { return possessions; } }

        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the InfinitivePhrase "Owns",
        /// and sets its owner to be the InfinitivePhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession) { possessions.Add(possession); }

        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the InfinitivePhrase.
        /// </summary>
        public IPossesser Possesser { get; set; }

        #endregion

        #region Fields

        ISet<IReferencer> boundPronouns = new HashSet<IReferencer>();
        ISet<IPossessable> possessions = new HashSet<IPossessable>();
        ISet<IDescriptor> descriptors = new HashSet<IDescriptor>();

        #endregion
    }
}
