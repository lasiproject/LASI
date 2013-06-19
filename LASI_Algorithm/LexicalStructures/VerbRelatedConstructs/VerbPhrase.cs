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

            Tense = composedWords.GetVerbs().Any() ? (from v in composedWords.GetVerbs()
                                                      group v.Tense by v.Tense into tenseGroup
                                                      orderby tenseGroup.Count()
                                                      select tenseGroup).First().Key : VerbTense.Base;
            Arity = VerbalArity.Undetermined;
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
            // if (!DirectObjects.Contains(prepositional.PrepositionalObject) && !IndirectObjects.Contains(prepositional.PrepositionalObject)) {
            ObjectOfThePreoposition =
                prepositional.OnRightSide != null ?
                prepositional.OnRightSide :
                prepositional.OnLeftSide;
            PrepositionalToObject = prepositional;


        }
        /// <summary>
        /// Binds the given Entity as a subject of the VerbPhrase instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the VerbPhrase as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            if (!_subjects.Contains(subject)) {
                _subjects.Add(subject);
                subject.SubjectOf = this;
            }
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the VerbPhrase instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the VerbPhrase as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            if (!_directObjects.Contains(directObject)) {
                _directObjects.Add(directObject);
                directObject.DirectObjectOf = this;
                if (IsPossessive) {
                    foreach (var subject in this.Subjects) {
                        subject.AddPossession(directObject);
                    }
                } else if (IsClassifier) {
                    foreach (var subject in this.Subjects) {
                        AliasDictionary.DefineAlias(subject, directObject);
                    }
                }
            }
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the VerbPhrase instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the VerbPhrase as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            if (!_indirectObjects.Contains(indirectObject)) {
                _indirectObjects.Add(indirectObject);
                indirectObject.IndirectObjectOf = this;
            }
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
            } else
                return base.ToString();
        }



        public virtual bool DetermineIsPossessive() {
            isPossessive = Words.GetVerbs().Any() && Words.GetVerbs().Last().IsPossessive;
            return isPossessive ?? false;
        }
        public virtual bool DetermineIsClassifier() {
            isClassifier = Words.GetVerbs().Any() && Words.GetVerbs().Last().IsClassifier;
            return isClassifier ?? false;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion


        #region Properties

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
        /// Gets the VerbPhrases'subject object, If the VerbPhrase has an object bound via a Prepositional construct.
        /// </summary>
        public ILexical ObjectOfThePreoposition {
            get;
            protected set;
        }
        public IPrepositional PrepositionalToObject {
            get;
            protected set;
        }

        private bool? isPossessive;

        public bool IsPossessive {
            get {
                return isPossessive ?? DetermineIsPossessive();
            }
        }
        private bool? isClassifier;

        public bool IsClassifier {
            get {
                return isClassifier ?? DetermineIsClassifier();
            }
        }
        public ILexical GivenExposition {
            get;
            protected set;
        }

        #endregion

        #region Fields

        private IList<IAdverbial> _modifiers = new List<IAdverbial>();
        private ICollection<IEntity> _subjects = new List<IEntity>();
        private ICollection<IEntity> _directObjects = new List<IEntity>();
        private ICollection<IEntity> _indirectObjects = new List<IEntity>();


        #endregion

        #region Operators

        #endregion


        public VerbalArity Arity {
            get;
            protected set;
        }


    }
}

