using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Provides the base class for all word level verb constructs. An instance of this class represents a verb in its base tense.
    /// </summary>
    public class Verb : Word, IVerbal, IAdverbialModifiable, IModalityModifiable
    {
        private StringComparer caseIgnoringComp = StringComparer.OrdinalIgnoreCase;
        /// <summary>
        /// Initializes a new instance of the Verb class which represents the base tense form of a verb.
        /// </summary>
        /// <param name="text">The key text content of the verb.</param>
        /// <param name="form">The tense of the verb</param>
        public Verb(string text, VerbForm form)
            : base(text) {
            VerbForm = form;
        }
        #region Methods


        /// <summary>
        /// Attaches an IAdverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb
        /// <param name="adv">The IAdverbial construct by which to modify the AdjectivePhrase.</param>
        /// </summary>
        public virtual void ModifyWith(IAdverbial adv) {
            modifiers.Add(adv);
            adv.Modifies = this;
        }


        /// <summary>
        /// Binds the Verb to an object via a prepositional construct such as a Preposition or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prepositional"></param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional) {
            ObjectOfThePreoposition = prepositional.BoundObject;
            PrepositionalToObject = prepositional;
        }

        /// <summary>
        /// Binds the given Entity as a subject of the Verb instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the Verb as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            subjects.Add(subject);
            subject.SubjectOf = this;

        }

        /// <summary>
        /// Binds the given Entity as a direct object of the Verb instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the Verb as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            directObjects.Add(directObject);
            directObject.DirectObjectOf = this;
            if (IsPossessive) {
                foreach (var subject in this.Subjects) {
                    subject.AddPossession(directObject);
                }
            } else if (IsClassifier) {
                foreach (var subject in this.Subjects) {
                    LASI.Core.Heuristics.AliasLookup.DefineAlias(subject, directObject);
                }
            }

        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the Verb instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the Verb as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            indirectObjects.Add(indirectObject);
            indirectObject.IndirectObjectOf = this;
        }

        /// <summary>
        /// Determines if the Verb implies a possession relationship. E.g. in the sentence 
        /// "They have a lot of ideas." the Verb "have" asserts a possessor possessee relationship between "They" and "a lot of ideas".
        /// </summary>
        /// <returns>True if the Verb is a possessive relationship specifier; otherwise, false.</returns>
        protected virtual bool DetermineIsPossessive() {
            var syns = LASI.Core.Heuristics.Lookup.GetSynonyms(this);
            return syns.Contains("have", caseIgnoringComp);
        }
        /// <summary>
        /// Determines if the Verb acts as a classifier. E.g. in the sentence "Rodents are prey animals." the Verb "are" acts as a classification tool because it states that rodents are a subset of prey animals.
        /// </summary>
        /// <returns>True if the Verb is a classifier; otherwise, false.</returns>
        protected virtual bool DetermineIsClassifier() {
            var syns = LASI.Core.Heuristics.Lookup.GetSynonyms(this);
            return !IsPossessive && Modality == null && AdverbialModifiers.None() && syns.Contains("is", caseIgnoringComp);
        }


        /// <summary>
        /// Return a value indicating if the Verb has any subjects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any Subjects bound to it; otherwise, false.</returns>
        public bool HasSubject() {
            return subjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any subjects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any subjects bound to it which match the given predicate function; otherwise, false.</returns>
        public bool HasSubject(Func<IEntity, bool> predicate) {
            return HasBoundEntitiyImpl(Subjects, predicate);
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it; otherwise, false.</returns>
        public bool HasDirectObject() {
            return DirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it which match the given predicate function; otherwise, false.</returns>
        public bool HasDirectObject(Func<IEntity, bool> predicate) {
            return HasBoundEntitiyImpl(DirectObjects, predicate);
        }
        /// <summary>
        /// Return a value indicating if the Verb has any indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it; otherwise, false.</returns>
        public bool HasIndirectObject() {
            return IndirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any indirect objects bound to it which match the given predicate function; otherwise, false.</returns>
        public bool HasIndirectObject(Func<IEntity, bool> predicate) {
            return HasBoundEntitiyImpl(IndirectObjects, predicate);
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct OR indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct OR indirect objects bound to it; otherwise, false.</returns>
        public bool HasObject() {
            return HasDirectObject() || HasIndirectObject();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct OR indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any direct OR indirect objects bound to it which match the given predicate function; otherwise, false.</returns>
        public bool HasObject(Func<IEntity, bool> predicate) {
            return HasDirectObject(predicate) || HasIndirectObject(predicate);
        }

        /// <summary>
        /// Gets a value indicating if the Verb has at least one subject, direct object, or indirect object.
        /// </summary>
        /// <returns>True if the Verb has at least one subject, direct object, or indirect object; otherwise, false.</returns>
        public bool HasSubjectOrObject() {
            return HasObject() || HasSubject();
        }
        /// <summary>
        /// Gets a value indicating if the Verb has at least one subject, direct object, or indirect object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each associated subject, direct object, or indirect object..</param>
        /// <returns>True if the Verb has at least one subject, direct object, or indirect object  matching the provided predicate; otherwise, false.</returns>
        public bool HasSubjectOrObject(Func<IEntity, bool> predicate) {
            return HasObject(predicate) || HasSubject(predicate);
        }
        private static bool HasBoundEntitiyImpl(IEnumerable<IEntity> entities, Func<IEntity, bool> predicate) {
            return entities.Any(predicate) || entities.OfType<IReferencer>().Any(p => p.RefersTo != null && predicate(p.RefersTo));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the Verb's subjects.
        /// </summary>
        public IAggregateEntity AggregateSubject { get { return new AggregateEntity(subjects); } }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the Verb's direct objects.
        /// </summary>
        public IAggregateEntity AggregateDirectObject { get { return new AggregateEntity(directObjects); } }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the Verb's indirect objects.
        /// </summary>
        public IAggregateEntity AggregateIndirectObject { get { return new AggregateEntity(indirectObjects); } }
        /// <summary>
        /// Gets the subjects of the Verb.
        /// </summary> 
        public IEnumerable<IEntity> Subjects { get { return subjects; } }
        /// <summary>
        /// Gets the indirect objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects { get { return indirectObjects; } }
        /// <summary>
        /// Gets the direct objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects { get { return directObjects; } }

        /// <summary>
        /// Gets or the collection of IAdverbial modifiers which modify the Verb.
        /// </summary>
        public virtual IEnumerable<IAdverbial> AdverbialModifiers { get { return modifiers; } }
        /// <summary>
        /// Gets or sets the ModalAuxilary word which modifies the Verb.
        /// </summary>
        public ModalAuxilary Modality { get; set; }
        /// <summary>
        /// Gets the VerbTense of the Verb.
        /// </summary>
        public VerbForm VerbForm { get; protected set; }
        /// <summary>
        /// Gets the object of the Verb's preposition. This can be any ILexical construct including a word, phrase, or clause.
        /// </summary>
        public ILexical ObjectOfThePreoposition { get; protected set; }
        /// <summary>
        /// Gets the IPrepositional object which links the Verb to the ObjectOfThePreoposition.
        /// </summary>
        public IPrepositional PrepositionalToObject { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether or not the Verb has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier {
            get {
                classifier = classifier ?? DetermineIsClassifier();
                return classifier.Value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether or not the Verb has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive {
            get {
                possessive = possessive ?? DetermineIsPossessive();
                return possessive.Value;
            }
        }
        #endregion

        #region Fields

        private IList<IAdverbial> modifiers = new List<IAdverbial>();
        private HashSet<IEntity> subjects = new HashSet<IEntity>();
        private HashSet<IEntity> directObjects = new HashSet<IEntity>();
        private HashSet<IEntity> indirectObjects = new HashSet<IEntity>();
        bool? possessive = null;
        bool? classifier = null;

        #endregion
    }
}
