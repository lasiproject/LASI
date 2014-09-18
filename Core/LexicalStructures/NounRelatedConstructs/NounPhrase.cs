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
        /// <param name="words">The words which compose to form the NounPhrase.</param>
        public NounPhrase(IEnumerable<Word> words) : base(words) {
            EntityKind = words.OfEntity()
                .Select(e => e.EntityKind)
                .DefaultIfEmpty()
                .GroupBy(e => e)
                .MaxBy(v => v.Count()).Key;
        }
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the NounPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the NounPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of NounPhrases. 
        /// Thus, its purpose is to simplify test code.</remarks>
        public NounPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        #endregion

        #region Methods

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
            if (!VerboseOutput) {
                return base.ToString();
            }
            var gender = this.GetGender();
            var aliases = this.GetDefinedAliases();
            string empty = string.Empty;
            return base.ToString() + string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                Possessions.Any() ? "\nPossessions: " + Possessions.Format(p => p.Text + '\n') : empty,
                Possesser != null ? "\nPossessed By: " + Possesser.Text : empty,
                OuterAttributive != null ? "\nDefinedby: " + OuterAttributive.Text : empty,
                InnerAttributive != null ? "\nDefines: " + InnerAttributive.Text : empty,
                aliases.Any() ? "\nClassified as: " + aliases.Format() : empty,
                SubjectOf != null ? "\nSubject Of: " + SubjectOf.Text : empty,
                DirectObjectOf != null ? "\nDirect Object Of: " + DirectObjectOf.Text : empty,
                IndirectObjectOf != null ? "\nIndirect Object Of: " + IndirectObjectOf.Text : empty,
                gender.IsDefined() ? "\nPrevailing Gender: " + gender : empty
            );

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
        public NounPhrase OuterAttributive {
            get { return outerAttributive; }
            set { outerAttributive = value != this ? value : null; }
        }
        /// <summary>
        /// Gets or sets another NounPhrase, to the right of current instance, which is functions as an Attributor of current instance.
        /// </summary>
        public NounPhrase InnerAttributive {
            get { return innerAttributive; }
            set { innerAttributive = (value != this ? value : null); }
        }
        /// <summary>
        /// Gets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the NounPhrase.
        /// </summary>
        public EntityKind EntityKind { get; protected set; }


        /// <summary>
        /// Gets all of the constructs which the NounPhrase "owns".
        /// </summary>
        public IEnumerable<IPossessable> Possessions { get { return possessed; } }
        /// <summary>
        /// Gets or sets the Entity which "owns" the NounPhrase.
        /// </summary>
        public IPossesser Possesser {
            get { return possessor; }
            set {
                possessor = value;
                // Bind entity words of the phrase as possessions of possessor.
                if (value != null) {
                    foreach (var entity in Words.OfType<IEntity>()) { value.AddPossession(entity); }
                } else {
                    foreach (var entity in Words.OfType<IEntity>()) { entity.Possesser = value; }
                }
            }
        }

        /// <summary>
        /// Gets or sets the IVerbal instance, generally a Verb or VerbPhrase, which the NounPhrase is the subject of.
        /// </summary>
        public virtual IVerbal SubjectOf {
            get { return subjectOf; }
            set {
                subjectOf = value;
                foreach (var entity in Words.OfType<IEntity>()) {
                    entity.SubjectOf = value;
                }
            }
        }
        /// <summary>
        /// Gets the or sets IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the DIRECT object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf {
            get { return directObjectOf; }
            set {
                directObjectOf = value;
                foreach (var entity in Words.OfType<IEntity>()) {
                    entity.DirectObjectOf = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf {
            get { return indirecObjectOf; }
            set {
                indirecObjectOf = value;
                foreach (var entity in Words.OfType<IEntity>()) {
                    entity.IndirectObjectOf = value;
                }
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
