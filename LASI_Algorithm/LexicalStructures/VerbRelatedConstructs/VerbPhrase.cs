using LASI.Algorithm.ClauseTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Verb Phrase, a Phrase with the syntactic role of a adverb.
    /// </summary>
    public class VerbPhrase : Phrase, IVerbal, IAdverbialModifiable, IModalityModifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the VerbPhrase.</param>
        public VerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {

            Tense = composedWords.GetVerbs().Any() ?
                (from v in composedWords.GetVerbs()
                 group v.Tense by v.Tense into tenseGroup
                 orderby tenseGroup.Count()
                 select tenseGroup).First().Key : VerbTense.Base;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb.
        /// </summary>
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public void ModifyWith(IAdverbial adv) {
            _modifiers.Add(adv);
            adv.Modifies = this;
        }
        /// <summary>
        /// Binds the VerbPhrase to an object via a propisitional construct such as a Prepositon or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prepositional">The IPrepositional construct through which the Object is associated.</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional) {
            ObjectOfThePreoposition = prepositional.BoundObject;
            PrepositionalToObject = prepositional;
        }
        /// <summary>
        /// Binds the given Entity as a subject of the VerbPhrase instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the VerbPhrase as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            _subjects.Add(subject);
            subject.SubjectOf = this;
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the VerbPhrase instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the VerbPhrase as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            _directObjects.Add(directObject);
            directObject.DirectObjectOf = this;
            if (IsPossessive) {
                foreach (var subject in this.Subjects) {
                    subject.AddPossession(directObject);
                }
            }
            else if (IsClassifier) {
                foreach (var subject in this.Subjects) {
                    AliasDictionary.DefineAlias(subject, directObject);
                }
            }
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the VerbPhrase instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the VerbPhrase as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            _indirectObjects.Add(indirectObject);
            indirectObject.IndirectObjectOf = this;
        }


        public override string ToString() {
            if (Phrase.VerboseOutput) {

                var result = base.ToString();
                foreach (var s in Subjects) {
                    result += s != null ? "\n\tSubject: " + s.ToString() : "";
                }
                foreach (var d in DirectObjects) {
                    result += d != null ? "\n\tDirect Object:" + d.ToString() : "";
                }
                foreach (var i in IndirectObjects) {
                    result += i != null ? "\n\tIndirect Object: " + i.ToString() : "";
                }
                if (ObjectOfThePreoposition != null) {
                    result += "\n\tVia Preposition Object" + ObjectOfThePreoposition.ToString();
                }
                foreach (var mod in _modifiers) {
                    result += _modifiers.Count > 0 ? "\n\tModifier: " + mod.ToString() : "";

                }
                return result;
            }
            else
                return base.ToString();
        }



        protected virtual bool DetermineIsPossessive() {
            isPossessive = Words.GetVerbs().Any() && Words.GetVerbs().Last().IsPossessive;
            return isPossessive ?? false;
        }
        protected virtual bool DetermineIsClassifier() {
            isClassifier = Words.GetVerbs().Any() && Words.GetVerbs().Last().IsClassifier;
            return isClassifier ?? false;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }


        /// <summary>
        /// Return a value indicating if the Verb has any subjects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any Subjects bound to it, false otherwise.</returns>
        public bool HasSubject() {
            return _subjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any subjects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any subjects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasSubject(Func<IEntity, bool> predicate) {
            return Subjects.Any(predicate) || Subjects.OfType<IPronoun>().Any(p => predicate(p.BoundEntity));
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it, false otherwise.</returns>
        public bool HasDirectObject() {
            return DirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasDirectObject(Func<IEntity, bool> predicate) {
            return DirectObjects.Any(predicate) || DirectObjects.OfType<IPronoun>().Any(p => predicate(p.BoundEntity));
        }
        /// <summary>
        /// Return a value indicating if the Verb has any indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it, false otherwise.</returns>
        public bool HasIndirectObject() {
            return IndirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any indirect objects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasIndirectObject(Func<IEntity, bool> predicate) {
            return IndirectObjects.Any(predicate) || IndirectObjects.OfType<IPronoun>().Any(p => predicate(p.BoundEntity));
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct OR indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct OR indirect objects bound to it, false otherwise.</returns>
        public bool HasObject() {
            return HasDirectObject() || HasIndirectObject();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct OR indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any direct OR indirect objects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasObject(Func<IEntity, bool> predicate) {
            return HasDirectObject(predicate) || HasIndirectObject(predicate);
        }



        #endregion


        #region Properties

        /// <summary>
        /// Gets the collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }
        public IDescriptor AdjectivalModifier {
            get;
            set;
        }
        /// <summary>
        /// Gets the prevailing Tense of the VerbPhrase.
        /// <see cref="VerbTense"/>
        /// </summary>
        public VerbTense Tense {
            get;
            protected set;
        }
        /// <summary>
        /// Gets or sets the ModalAuxilary adverb which modifies the VerbPhrase.
        /// </summary>
        public ModalAuxilary Modality {
            get;
            set;
        }
        /// <summary>
        /// Gets a value indicating wether or not the VerbPhrase has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive {
            get {
                return isPossessive ?? DetermineIsPossessive();
            }
        }

        /// <summary>
        /// Gets a value indicating wether or not the VerbPhrase has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier {
            get {
                return isClassifier ?? DetermineIsClassifier();
            }
        }

        /// <summary>
        /// Gets the subjects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> Subjects {
            get {
                return _subjects;
            }
        }
        /// <summary>
        /// Gets the direct objects of the VerbPhrase.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects {
            get {
                return _directObjects;
            }
        }
        /// <summary>
        /// Gets the indirect objects of the VerbPhrase.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects {
            get {
                return _indirectObjects;
            }
        }

        /// <summary>
        /// Gets the VerbPhrases'subject object, If the VerbPhrase has an object bound via a Prepositional. This can be any ILexical construct including a word, phrase, or clause.
        /// </summary>
        public ILexical ObjectOfThePreoposition {
            get;
            protected set;
        }
        public IPrepositional PrepositionalToObject {
            get;
            protected set;
        }


        #endregion

        #region Fields

        private IList<IAdverbial> _modifiers = new List<IAdverbial>();
        private HashSet<IEntity> _subjects = new HashSet<IEntity>();
        private HashSet<IEntity> _directObjects = new HashSet<IEntity>();
        private HashSet<IEntity> _indirectObjects = new HashSet<IEntity>();
        private bool? isClassifier;
        private bool? isPossessive;
        #endregion


    }
}

