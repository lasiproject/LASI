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
    public class Verb : Word, IVerbal, IAdverbialModifiable, IModalityModifiable, IEquatable<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the Verb class which represents the base tense form of a adverb.
        /// </summary>
        /// <param name="text">The key text content of the adverb.</param>
        /// <param name="tense">The tense of the adverb</param>
        public Verb(string text, VerbTense tense)
            : base(text)
        {
            Tense = tense;
            Arity = VerbalArity.Undetermined;

        }
        #region Methods


        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        /// </summary>
        public virtual void ModifyWith(IAdverbial adv)
        {
            _modifiers.Add(adv);
            adv.Modifies = this;
        }
        //public virtual void ModifyWith(IVerbal verbialModifier) {
        //    _modifiers.Add(verbialModifier);
        //    verbialModifier.Modifies = this;
        //}

        /// <summary>
        /// Binds the Verb to an object via a propisitional construct such as a Prepositon or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prep"></param>
        public virtual void AttachObjectViaPreposition(IPrepositional prep)
        {
            //ObjectOfThePreoposition = this as object == prep.OnLeftSide as object && prep.OnRightSide != null ? prep.OnRightSide : null;
            ObjectViaPreposition = prep.PrepositionalObject;
            PrepositionLinkingTarget = prep;
        }

        /// <summary>
        /// Binds the given Entity as a subject of the Verb instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the Verb as a subject.</param>
        public virtual void BindSubject(IEntity subject)
        {
            if (!_boundSubjects.Contains(subject)) {
                _boundSubjects.Add(subject);
                subject.SubjectOf = this;
            }
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the Verb instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the Verb as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject)
        {
            if (!_boundDirectObjects.Contains(directObject)) {
                _boundDirectObjects.Add(directObject);
                directObject.DirectObjectOf = this;
                if (IsPossessive) {
                    foreach (var s in this.Subjects) {
                        s.AddPossession(directObject);
                    }
                }
            }
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the Verb instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the Verb as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject)
        {
            if (!_boundIndirectObjects.Contains(indirectObject)) {
                _boundIndirectObjects.Add(indirectObject);
                indirectObject.IndirectObjectOf = this;

            }
        }


        public virtual void DetermineIsPossessive()
        {

            var syns = LASI.Algorithm.Thesauri.Thesaurus.Lookup(this);
            if (syns != null && syns.Contains("have")) {
                possessive = true;
            }
        }







        public virtual bool Equals(Verb other)
        {
            return this == other;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the subjects of the Verb.
        /// </summary>
        public IEnumerable<IEntity> Subjects
        {
            get
            {
                return _boundSubjects;
            }
        }


        /// <summary>
        /// Gets or sets the List of IAdverbial modifiers which modify this Verb.
        /// </summary>
        public virtual IEnumerable<IAdverbial> Modifiers
        {
            get
            {
                return _modifiers;
            }
        }

        /// <summary>
        /// Gets or sets the ModalAuxilary adverb which modifies the Verb.
        /// </summary>
        public ModalAuxilary Modality
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the VerbTense of the Verb.
        /// </summary>
        public VerbTense Tense
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the VerbPhrases's object, If the VerbPhrase has an object bound via a Prepositional construct.
        /// </summary>
        public virtual ILexical ObjectViaPreposition
        {
            get;
            protected set;
        }



        /// <summary>
        /// Gets the indirect objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects
        {
            get
            {
                return _boundIndirectObjects;
            }

        }
        /// <summary>
        /// Gets the direct objects of the Verb.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects
        {
            get
            {
                return _boundDirectObjects;
            }
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
        private bool possessive;

        public bool IsPossessive
        {
            get
            {
                return possessive;
            }
            protected set
            {
                possessive = value;
            }
        }
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


        public ILexical ObjectOfThePreoposition
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
