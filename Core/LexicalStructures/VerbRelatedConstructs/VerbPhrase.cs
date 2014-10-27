using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LASI.Utilities;
using LASI.Core.Heuristics;
using LASI.Core.PatternMatching;

namespace LASI.Core
{
    /// <summary>
    /// Represents a Verb Phrase, a Phrase with the syntactic role of a verb.
    /// </summary>
    public class VerbPhrase : Phrase, IVerbal, IAdverbialModifiable, IModalityModifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the VerbPhrase.</param>
        public VerbPhrase(IEnumerable<Word> words)
            : base(words) {

            Tense = (from v in words.OfVerb()
                     group v.VerbForm by v.VerbForm into byTense
                     select new { Count = byTense.Count(), byTense.Key } into tenseCount
                     orderby tenseCount.Count
                     select tenseCount.Key).FirstOrDefault();
        }
        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the VerbPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the VerbPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public VerbPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }


        #endregion

        #region Methods

        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb.
        /// </summary>
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public void ModifyWith(IAdverbial adv) {
            modifiers.Add(adv);
            adv.Modifies = this;
        }
        /// <summary>
        /// <para> Binds the VerbPhrase to an object via a prepositional construct such as a Preposition or PrepositionalPhrase. </para>
        /// <para> Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to" </para>
        /// </summary>
        /// <param name="prepositional">The IPrepositional construct through which the Object is associated.</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional) {
            ObjectOfThePreposition = prepositional.BoundObject;
            PrepositionalToObject = prepositional;
        }
        /// <summary>
        /// Binds the given Entity as a subject of the VerbPhrase instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the VerbPhrase as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            if (subject != null) {
                subjects.Add(subject);
                subject.SubjectOf = this;
                if (PostpositiveDescriptor != null) { subject.BindDescriptor(postpositiveDescriptor); }
                foreach (var v in this.Words.OfVerb()) { v.BindSubject(subject); }
            }
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the VerbPhrase instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the VerbPhrase as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            if (directObject != null) {
                directObjects.Add(directObject);
                directObject.DirectObjectOf = this;
                foreach (var v in this.Words.OfVerb()) { v.BindDirectObject(directObject); }
                if (IsPossessive) {
                    foreach (var subject in this.Subjects) {
                        subject.AddPossession(directObject);
                    }
                } else if (IsClassifier) {
                    foreach (var subject in this.Subjects) {
                        AliasLookup.DefineAlias(subject, directObject);
                    }
                }
            }
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the VerbPhrase instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the VerbPhrase as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            if (indirectObject != null) {
                indirectObjects.Add(indirectObject);
                indirectObject.IndirectObjectOf = this;
                foreach (var v in Words.OfVerb()) { v.BindIndirectObject(indirectObject); }
            }
        }

        /// <summary>
        /// Returns a string representation of the VerbPhrase.
        /// </summary>
        /// <returns>A string representation of the VerbPhrase.</returns>
        public override string ToString() {
            var result = base.ToString();
            if (Phrase.VerboseOutput) {
                result += Subjects.Any() ? "\nSubjects: " + Subjects.Format(s => s.Text + ", ") : string.Empty;
                result += DirectObjects.Any() ? "\nDirect Objects: " + DirectObjects.Format(s => s.Text + ", ") : string.Empty;
                result += IndirectObjects.Any() ? "\nIndirect Objects: " + IndirectObjects.Format(s => s.Text + ", ") : string.Empty;
                result += ObjectOfThePreposition != null ? "\nVia Preposition Object: " + ObjectOfThePreposition.Text : string.Empty;
                result += Modality != null ? "\nModal Aux: " + Modality.Text : string.Empty;
                result += AdverbialModifiers.Any() ? "\nModifiers: " + AdverbialModifiers.Format(s => s.Text + ", ") : string.Empty;
                result += string.Format("\nPossessive Indicator: [{0}]\nCategorizatizer: [{1}]\nPrevailing Tense: [{2}]", IsPossessive, IsClassifier, Tense);
            }
            return result;
        }
        /// <summary>
        /// Determines if the VerbPhrase implies a possession relationship. E.g. in the sentence 
        /// "They certainly have a lot of ideas." the VerbPhrase "certainly have" asserts a possessor possessee relationship between "They" and "a lot of ideas".
        /// </summary>
        /// <returns>True if the VerbPhrase is a possessive relationship specifier; otherwise, false.</returns>
        protected virtual bool DetermineIsPossessive() {
            return Words.OfVerb().Any() && Words.OfVerb().Last().IsPossessive;
        }
        /// <summary>
        /// Determines if the VerbPhrase acts as a classifier. E.g. in the sentence "Rodents are definitely prey animals." 
        /// the VerbPhrase "are definitely" acts as a classification tool because it states that rodents are a subset of prey animals.
        /// </summary>
        /// <returns>True if the VerbPhrase is a classifier; otherwise, false.</returns>
        protected virtual bool DetermineIsClassifier() {
            return !IsPossessive && Modality == null && AdverbialModifiers.None() && Words.OfVerb().Any() && Words.OfVerb().All(v => v.IsClassifier);
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's subjects.
        /// </summary>
        public IAggregateEntity AggregateSubject { get { return new AggregateEntity(subjects); } }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's direct objects.
        /// </summary>
        public IAggregateEntity AggregateDirectObject { get { return new AggregateEntity(directObjects); } }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's indirect objects.
        /// </summary>
        public IAggregateEntity AggregateIndirectObject { get { return new AggregateEntity(indirectObjects); } }
        /// <summary>
        /// Gets the collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> AdverbialModifiers {
            get {
                return modifiers;
            }
        }
        /// <summary>
        /// Gets or sets the IDescriptor which modifies, by way of the Verbal, its Subject.
        /// </summary>
        public IDescriptor PostpositiveDescriptor {
            get { return postpositiveDescriptor; }
            set { postpositiveDescriptor = value; foreach (var described in Subjects) described.BindDescriptor(value); }
        }
        /// <summary>
        /// Gets the prevailing Tense of the VerbPhrase.
        /// <see cref="VerbForm"/>
        /// </summary>
        public VerbForm Tense { get; protected set; }
        /// <summary>
        /// Gets or sets the ModalAuxilary word which modifies the VerbPhrase.
        /// </summary>
        public ModalAuxilary Modality { get; set; }
        /// <summary>
        /// Gets a value indicating whether or not the VerbPhrase has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive {
            get {
                isPossessive = isPossessive ?? DetermineIsPossessive();
                return isPossessive ?? false;
            }
        }
        /// <summary>
        /// Gets a value indicating whether or not the VerbPhrase has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier {
            get {
                isClassifier = isClassifier ?? DetermineIsClassifier();
                return isClassifier ?? false;
            }
        }
        /// <summary>
        /// Gets the subjects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> Subjects { get { return subjects; } }
        /// <summary>
        /// Gets the direct objects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> DirectObjects { get { return directObjects; } }
        /// <summary>
        /// Gets the indirect objects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> IndirectObjects { get { return indirectObjects; } }
        /// <summary>
        /// Gets the VerbPhrases's object, If the VerbPhrase has an object bound via a Prepositional. This can be any ILexical construct including a word, phrase, or clause.
        /// </summary>
        public ILexical ObjectOfThePreposition { get; protected set; }
        /// <summary>
        /// Gets the IPrepositional object which links the VerbPhrase to the ObjectOfThePreoposition.
        /// </summary>
        public IPrepositional PrepositionalToObject { get; protected set; }

        #endregion

        #region Fields

        private ISet<IAdverbial> modifiers = new HashSet<IAdverbial>();
        private ISet<IEntity> subjects = new HashSet<IEntity>();
        private ISet<IEntity> directObjects = new HashSet<IEntity>();
        private ISet<IEntity> indirectObjects = new HashSet<IEntity>();
        private bool? isClassifier;
        private bool? isPossessive;
        private IDescriptor postpositiveDescriptor;
        #endregion
    }
}

