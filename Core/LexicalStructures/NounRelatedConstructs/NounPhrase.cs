using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LASI.Utilities;
using LASI.Core.Heuristics;
using System.Math;

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
            DetermineEntityKind();
        }
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the NounPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the NounPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of NounPhrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public NounPhrase(Word first, params Word[] rest) : this(rest.AsEnumerable().Prepend(first)) { }

        #endregion

        #region Methods

        /// <summary>
        /// Current,  somewhat sloppy determination of the Noun, person, place, thing etc, of NounPhrase by 
        /// selecting the most common Noun between its nouns and from its bound pronouns 
        /// </summary>
        private void DetermineEntityKind() {
            EntityKind = Words.OfEntity()
                .Select(e => e.EntityKind).DefaultIfEmpty()
                .GroupBy(e => e)
                .MaxBy(v => v.Count()).Key;
        }

        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the NounPhrase.
        /// </summary>
        /// <param name="referee">The referencer which refers to the NounPhrase Instance.</param>
        public virtual void BindReferencer(IReferencer referee) {
            boundReferences.Add(referee);
            referee.BindAsReferringTo(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the NounPhrase.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the NounPhrase' descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the NounPhrase "Owns",
        /// and sets its owner to be the NounPhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession) {
            possessed.Add(possession);
            possession.Possesser = this;
        }
        /// <summary>
        /// Returns a string representation of the NounPhrase.
        /// </summary>
        /// <returns>A string representation of the NounPhrase.</returns>
        public override string ToString() {
            var result = base.ToString();

            if (Phrase.VerboseOutput) {
                var gender = this.GetGender();
                var aliases = this.GetDefinedAliases();
                result += string.Format("{0}{2}{3}{4}{5}{6}{7}{8}",
                        Possessed.Any() ? "\nPossessions: " + Possessed.Format(p => p.Text + '\n') : string.Empty,
                        Possesser != null ? "\nPossessed By: " + Possesser.Text : string.Empty,
                        OuterAttributive != null ? "\nDefinedby: " + OuterAttributive.Text : string.Empty,
                        InnerAttributive != null ? "\nDefines: " + InnerAttributive.Text : string.Empty,
                        aliases.Any() ? "\nClassified as: " + aliases.Format() : string.Empty,
                        SubjectOf != null ? "\nSubject Of: " + SubjectOf.Text : string.Empty,
                        DirectObjectOf != null ? "\nDirect Object Of: " + DirectObjectOf.Text : string.Empty,
                        IndirectObjectOf != null ? "\nIndirect Object Of: " + IndirectObjectOf.Text : string.Empty,
                        gender.IsDefined() ? "\nPrevailing Gender: " + gender : string.Empty
                    );
                //result += string.Empty;
                //result += InnerAttributive != null ? "\nDefines: " + InnerAttributive.Text : string.Empty;
                //result += OuterAttributive != null ? "\nDefinedby: " + OuterAttributive.Text : string.Empty;
                //result += (AliasLookup.GetDefinedAliases(this).Any() ? "\nClassified as: " + AliasLookup.GetDefinedAliases(this).Format() : string.Empty);
                //result += SubjectOf != null ? "\nSubject Of: " + SubjectOf.Text : string.Empty;
                //result += DirectObjectOf != null ? "\nDirect Object Of: " + DirectObjectOf.Text : string.Empty;
                //result += IndirectObjectOf != null ? "\nIndirect Object Of: " + IndirectObjectOf.Text : string.Empty;
                //var gender = this.GetGender();
                //result += gender.IsDefined() ? "\nPrevailing Gender: " + this.GetGender() : string.Empty;
            }
            return result;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets all of the IReferencer instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase.
        /// </summary>
        public IEnumerable<IReferencer> Referencers { get { return boundReferences; } }
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return descriptors; } }
        /// <summary>
        /// Gets or sets another NounPhrase, to the left of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase OuterAttributive { get { return outerAttributive; } set { outerAttributive = (value != this ? value : null); } }
        /// <summary>
        /// Gets or sets another NounPhrase, to the right of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase InnerAttributive { get { return innerAttributive; } set { innerAttributive = (value != this ? value : null); } }
        /// <summary>
        /// Gets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the NounPhrase.
        /// </summary>
        public EntityKind EntityKind { get; protected set; }


        /// <summary>
        /// Gets all of the constructs which the NounPhrase "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessed {
            get { return possessed; }
        }
        /// <summary>
        /// Gets or sets the Entity which "owns" the NounPhrase.
        /// </summary>
        public IPossesser Possesser {
            get {
                return possessor;
            }
            set {
                possessor = value;
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
                return subjectOf;
            }
            set {
                subjectOf = value;
                foreach (var N in Words.OfType<IEntity>())
                    N.SubjectOf = subjectOf;
            }
        }
        /// <summary>
        /// Gets the or sets IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the DIRECT object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf {
            get {
                return directObjectOf;
            }
            set {
                directObjectOf = value;
                foreach (var N in Words.OfType<IEntity>())
                    N.DirectObjectOf = directObjectOf;
            }
        }

        /// <summary>
        /// Gets or sets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf {
            get {
                return indirecObjectOf;
            }
            set {
                indirecObjectOf = value;
                foreach (var N in Words.OfType<IEntity>())
                    N.IndirectObjectOf = IndirectObjectOf;
            }
        }
        #endregion

        #region Fields

        private HashSet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        private HashSet<IPossessable> possessed = new HashSet<IPossessable>();
        private HashSet<IReferencer> boundReferences = new HashSet<IReferencer>();
        private IPossesser possessor;
        private IVerbal directObjectOf;
        private IVerbal indirecObjectOf;
        private IVerbal subjectOf;
        private NounPhrase innerAttributive;
        private NounPhrase outerAttributive;

        #endregion
    }
}
