using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LASI.Utilities;
using LASI.Core.ComparativeHeuristics;

namespace LASI.Core
{
    /// <summary>
    /// Represents a noun phrase such as "The Pinko-Commy Conspiracy".
    /// Note that noun componentPhrases are the constructs which wrap both nouns and pronouns at the phrase level.
    /// </summary>
    public class NounPhrase : Phrase, IEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="composed">The words which compose to form the NounPhrase.</param>
        public NounPhrase(IEnumerable<Word> composed)
            : base(composed) {
            determineEntityType();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Current,  somewhat sloppy determination of the Noun, person, place, thing etc, of nounphrase by 
        /// selecting the most common Noun between its nouns and from its bound pronouns 
        /// </summary>
        protected void determineEntityType() {

            var kindsOfNouns = from N in Words.OfType<IEntity>()
                               group N by N.EntityKind into KindGroup
                               orderby KindGroup.Count()
                               select KindGroup.Key;
            /*
             * I'm not sure why this is causing my program to crash.
             * But when I comment it out my program works.
             * - Scott
             */

            EntityKind = kindsOfNouns.FirstOrDefault();
        }


        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the NounPhrase.
        /// </summary>
        /// <param name="pro">The referencer which refers to the NounPhrase Instance.</param>
        public virtual void BindPronoun(IReferencer pro) {
            _boundPronouns.Add(pro);
            pro.BindAsReference(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the NounPhrase.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the NounPhrase' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            _descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the NounPhrase "Owns",
        /// and sets its owner to be the NounPhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession) {
            _possessed.Add(possession);
            possession.Possesser = this;
        }
        /// <summary>
        /// Returns a string representation of the NounPhrase.
        /// </summary>
        /// <returns>A string representation of the NounPhrase.</returns>
        public override string ToString() {
            var result = base.ToString();
            if (Phrase.VerboseOutput) {
                result += Possessed.Any() ? "\nPossessions: " + Possessed.Format(p => p.Text + '\n') : string.Empty;
                result += Possesser != null ? "\nOwned By: " + Possesser.Text : string.Empty;
                result += InnerAttributive != null ? "\nDefines: " + InnerAttributive.Text : string.Empty;
                result += OuterAttributive != null ? "\nDefines: " + OuterAttributive.Text : string.Empty;
                result += (AliasLookup.GetDefinedAliases(this).Any() ? "\nClassified as: " + AliasLookup.GetDefinedAliases(this).Format() : string.Empty);
                result += SubjectOf != null ? "\nSubject Of: " + SubjectOf.Text : string.Empty;
                result += DirectObjectOf != null ? "\nDirect Object Of: " + DirectObjectOf.Text : string.Empty;
                result += IndirectObjectOf != null ? "\nIndirect Object Of: " + IndirectObjectOf.Text : string.Empty;
                var gender = this.GetGender();
                result += gender.IsDefined() ? "\nPrevailing Gender: " + this.GetGender() : string.Empty;
            }
            return result;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase.
        /// </summary>
        public IEnumerable<IReferencer> Referees { get { return _boundPronouns; } }

        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return _descriptors; } }
        /// <summary>
        /// Gets or sets another NounPhrase, to the left of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase OuterAttributive { get; set; }
        /// <summary>
        /// Gets or sets another NounPhrase, to the right of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase InnerAttributive { get; set; }
        /// <summary>
        /// Gets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the NounPhrase.
        /// </summary>
        public EntityKind EntityKind { get; protected set; }


        /// <summary>
        /// Gets all of the constructs which the NounPhrase "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessed {
            get { return _possessed; }
        }
        /// <summary>
        /// Gets or sets the Entity which "owns" the NounPhrase.
        /// </summary>
        public IPossesser Possesser {
            get {
                return _possessor;
            }
            set {
                _possessor = value;
                if (value != null) {
                    foreach (var item in this.Words.OfType<IEntity>()) {
                        value.AddPossession(item);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the IVerbal instance, generally a Verb or VerbPhrase, which the NounPhrase is the subject of.
        /// </summary>
        public virtual IVerbal SubjectOf {
            get {
                return _subjectOf;
            }
            set {
                _subjectOf = value;
                foreach (var N in Words.OfType<IEntity>())
                    N.SubjectOf = _subjectOf;
            }
        }
        /// <summary>
        /// Gets the or sets IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the DIRECT object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf {
            get {
                return _direcObjectOf;
            }
            set {
                _direcObjectOf = value;
                foreach (var N in Words.OfType<IEntity>())
                    N.DirectObjectOf = _direcObjectOf;
            }
        }

        /// <summary>
        /// Gets or sets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf {
            get {
                return _indirecObjectOf;
            }
            set {
                _indirecObjectOf = value;
                foreach (var N in Words.OfType<IEntity>())
                    N.IndirectObjectOf = IndirectObjectOf;
            }
        }
        #endregion

        #region Fields

        private HashSet<IDescriptor> _descriptors = new HashSet<IDescriptor>();
        private HashSet<IPossessable> _possessed = new HashSet<IPossessable>();
        private HashSet<IReferencer> _boundPronouns = new HashSet<IReferencer>();
        private IPossesser _possessor;
        private IVerbal _direcObjectOf;
        private IVerbal _indirecObjectOf;
        private IVerbal _subjectOf;

        #endregion
    }
}
