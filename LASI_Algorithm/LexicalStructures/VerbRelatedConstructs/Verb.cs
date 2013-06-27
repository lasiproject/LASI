using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using LASI.Algorithm.ClauseTypes;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class for all adverb level adverb constructs. An instance of this class represents a adverb in its base tense.
    /// </summary>
    public class Verb : Word, IVerbal, IAdverbialModifiable, IModalityModifiable
    {
        /// <summary>
        /// Initializes a new instance of the Verb class which represents the base tense form of a adverb.
        /// </summary>
        /// <param name="text">The key text content of the adverb.</param>
        /// <param name="tense">The tense of the adverb</param>
        public Verb(string text, VerbTense tense)
            : base(text) {
            Tense = tense;
            Arity = VerbalArity.Undetermined;

        }
        #region Methods


        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        /// </summary>
        public virtual void ModifyWith(IAdverbial adv) {
            _modifiers.Add(adv);
            adv.Modifies = this;
        }


        /// <summary>
        /// Binds the Verb to an object via a propisitional construct such as a Prepositon or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prepositional"></param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional) {
            //ObjectOfThePreoposition = this as object == prepositional.OnLeftSide as object && prepositional.OnRightSide != null ? prepositional.OnRightSide : null;
            PrepositionalToObject = prepositional;
        }

        /// <summary>
        /// Binds the given Entity as a subject of the Verb instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the Verb as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            if (!_subjects.Contains(subject)) {
                _subjects.Add(subject);
                subject.SubjectOf = this;
            }
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the Verb instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the Verb as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            if (!_directObjects.Contains(directObject)) {
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
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the Verb instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the Verb as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            if (!_indirectObjects.Contains(indirectObject)) {
                _indirectObjects.Add(indirectObject);
                indirectObject.IndirectObjectOf = this;
            }
        }


        public virtual bool DetermineIsPossessive() {
            var syns = LASI.Algorithm.Thesauri.Thesaurus.Lookup(this);
            isPossessive = syns.Contains("have");
            return IsPossessive;
        }
        public virtual bool DetermineIsClassifier() {
            var syns = LASI.Algorithm.Thesauri.Thesaurus.Lookup(this);
            isClassifier = syns.Contains("be");
            return IsClassifier;
        }



        #endregion

        #region Properties

        /// <summary>
        /// Gets the subjects of the Verb.
        /// </summary>
        public IEnumerable<IEntity> Subjects {
            get {
                return _subjects;
            }
        }


        /// <summary>
        /// Gets or sets the List of IAdverbial modifiers which modify this Verb.
        /// </summary>
        public virtual IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }

        /// <summary>
        /// Gets or sets the ModalAuxilary adverb which modifies the Verb.
        /// </summary>
        public ModalAuxilary Modality {
            get;
            set;
        }
        /// <summary>
        /// Gets the VerbTense of the Verb.
        /// </summary>
        public VerbTense Tense {
            get;
            protected set;
        }


        /// <summary>
        /// Gets the indirect objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects {
            get {
                return _indirectObjects;
            }

        }
        /// <summary>
        /// Gets the direct objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects {
            get {
                return _directObjects;
            }
        }
        public IPrepositional PrepositionalToObject {
            get;
            protected set;
        }


        public ILexical ObjectOfThePreoposition {
            get {
                return PrepositionalToObject != null ? PrepositionalToObject.BoundObject : null;
            }
        }


        public ILexical GivenExposition {
            get;
            protected set;
        }
        /// <summary>
        /// Gets a value indicating wether or not the Verb has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier {
            get {
                return isClassifier ?? DetermineIsClassifier();
            }
        }
        /// <summary>
        /// Gets a value indicating wether or not the Verb has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive {
            get {
                return isPossessive ?? DetermineIsPossessive();
            }
        }

        public VerbalArity Arity {
            get;
            protected set;
        }
        #endregion


        #region Fields

        private IList<IAdverbial> _modifiers = new List<IAdverbial>();
        private ICollection<IEntity> _subjects = new List<IEntity>();
        private ICollection<IEntity> _directObjects = new List<IEntity>();
        private ICollection<IEntity> _indirectObjects = new List<IEntity>();
        bool? isPossessive;
        bool? isClassifier;

        #endregion

    }
}
