using LASI.Algorithm.ClauseTypes;
using LASI.Algorithm.SyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Verb Phrase, a Phrase with the syntactic role of a verb.
    /// </summary>
    public class VerbPhrase : Phrase, ITransitiveVerbal, IAdverbialModifiable, IModalityModifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the VerbPhrase.</param>
        public VerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords)
        {

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
        public void ModifyWith(IAdverbial adv)
        {
            _modifiers.Add(adv);
            adv.Modified = this;
        }
        /// <summary>
        /// Binds the VerbPhrase to an object via a propisitional construct such as a Prepositon or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prep">The IPrepositional construct through which the Object is associated.</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prep)
        {
            // if (!DirectObjects.Contains(prep.PrepositionalObject) && !IndirectObjects.Contains(prep.PrepositionalObject)) {
            ObjectViaPreposition =
                prep.OnRightSide != null ?
                prep.OnRightSide :
                prep.OnLeftSide;
            PrepositionLinkingTarget = prep;


        }
        /// <summary>
        /// Binds the given Entity as a subject of the VerbPhrase instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the VerbPhrase as a subject.</param>
        public virtual void BindSubject(IEntity subject)
        {
            if (!_boundSubjects.Contains(subject)) {
                _boundSubjects.Add(subject);
                subject.SubjectOf = this;
            }
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the VerbPhrase instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the VerbPhrase as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject)
        {
            if (!_boundDirectObjects.Contains(directObject)) {
                _boundDirectObjects.Add(directObject);
                directObject.DirectObjectOf = this;
                if (Possessive) {
                    foreach (var s in this.BoundSubjects) {
                        s.AddPossession(directObject);
                    }
                }
            }
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the VerbPhrase instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the VerbPhrase as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject)
        {
            if (ObjectViaPreposition != indirectObject && !_boundIndirectObjects.Contains(indirectObject)) {
                _boundIndirectObjects.Add(indirectObject);
                indirectObject.IndirectObjectOf = this;
            }
        }


        public override string ToString()
        {
            if (Phrase.VerboseOutput) {

                var result = base.ToString();
                foreach (var s in BoundSubjects) {
                    result += s != null ? "\n\tSubject: " + s.ToString() : "";
                }
                foreach (var d in DirectObjects) {
                    result += d != null ? "\n\tDirect Object:" + d.ToString() : "";
                }
                foreach (var i in IndirectObjects) {
                    result += i != null ? "\n\tIndirect Object: " + i.ToString() : "";
                }
                if (ObjectViaPreposition != null) {
                    result += "\n\tVia Preposition Object" + ObjectViaPreposition.ToString();
                }
                foreach (var mod in _modifiers) {
                    result += _modifiers.Count > 0 ? "\n\tModifier: " + mod.ToString() : "";

                }
                return result;
            } else
                return base.ToString();
        }



        public virtual void DetermineIsPossessive()
        {
            if (Words.GetVerbs().Any() && Words.GetVerbs().Last().IsPossessive == true) {
                Possessive = true;
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the subjects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> BoundSubjects
        {
            get
            {
                return _boundSubjects;
            }
        }

        /// <summary>
        /// Gets the direct objects of the VerbPhrase.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects
        {
            get
            {
                return _boundDirectObjects;
            }
        }


        /// <summary>
        /// Gets the indirect objects of the VerbPhrase.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects
        {
            get
            {
                return _boundIndirectObjects;
            }
        }
        /// <summary>
        /// Gets the collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> Modifiers
        {
            get
            {
                return _modifiers;
            }
        }

        public IDescriber AdjectivalModifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the prevailing Tense of the VerbPhrase.
        /// <see cref="VerbTense"/>
        /// </summary>
        public VerbTense Tense
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the ModalAuxilary verb which modifies the VerbPhrase.
        /// </summary>
        public ModalAuxilary Modality
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the VerbPhrases's object, If the VerbPhrase has an object bound via a Prepositional construct.
        /// </summary>
        public virtual ILexical ObjectViaPreposition
        {
            get;
            protected set;
        }


        public bool Possessive
        {
            get;
            protected set;
        }
        public ILexical GivenExposition
        {
            get;
            protected set;
        }

        #endregion

        #region Fields

        private IList<IAdverbial> _modifiers = new List<IAdverbial>();
        private ICollection<IEntity> _boundSubjects = new List<IEntity>();
        private ICollection<IEntity> _boundDirectObjects = new List<IEntity>();
        private ICollection<IEntity> _boundIndirectObjects = new List<IEntity>();


        #endregion

        #region Operators

        #endregion


        public VerbalArity Arity
        {
            get;
            protected set;
        }


        public IPrepositional PrepositionLinkingTarget
        {
            get;
            set;
        }
    }
}

