using LASI.Algorithm.FundamentalSyntacticInterfaces;
using LASI.Algorithm.LexicalStructures;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a pronoun which gernerally refers back to a previously defined Entity, such as a Noun or NounPhrase.
    /// </summary>
    public abstract class Pronoun : Word, IPronounBindable, IPronoun
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Pronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the pronoun.</param>
        protected Pronoun(string text)
            : base(text) {
        }

        #endregion

        #region Methods

        public override string Text {
            get {
                var result = base.Text;
                result += VerboseOutput ? " " + PronounKind.ToString() : "";
                return result;
            }
            protected set {
                base.Text = value;
            }
        }

        public virtual void BindPronoun(IPronoun pro) {
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
        }


        public virtual bool Equals(IEntity other) {
            return other as object == this as object;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entity which the Pronoun references.
        /// </summary>
        public virtual IEntity BoundEntity {
            get {
                return _boundEntity;
            }
            set {
                _boundEntity = value;
                _entityKind = BoundEntity.EntityKind;
            }
        }
        /// <summary>
        /// Gets the ITransitiveVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public virtual ITransitiveVerbal DirectObjectOf {
            get;
            set;
        }

        public virtual IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }

        }
        /// <summary>
        /// Gets the ITransitiveVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public virtual ITransitiveVerbal IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public virtual ITransitiveVerbal SubjectOf {
            get;
            set;
        }
        public virtual void BindDescriber(IDescriber adj) {
            _describers.Add(adj);
        }

        public virtual IEnumerable<IDescriber> DescribedBy {
            get {
                return _describers;
            }
        }

        public virtual IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }


        public virtual void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
            if (IsBound && !BoundEntity.Possessed.Contains(possession)) {
                BoundEntity.AddPossession(possession);
            }
        }

        public virtual IEntity Possesser {
            get;
            set;
        }
        public virtual EntityKind EntityKind {
            get {
                return _entityKind;
            }
        }
        public virtual PronounKind PronounKind {
            get;
            protected set;
        }
        public EntityThemeMemberKind ThemeMemberKind {
            get;
            set;
        }

        #endregion


        #region Fields
        private ICollection<IDescriber> _describers = new List<IDescriber>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();
        protected bool IsBound {
            get {
                return BoundEntity != null;
            }
        }
        private EntityKind _entityKind;
        private IEntity _boundEntity;

        #endregion






        public void BindToIEntity(IEntity target) {
            _boundEntity = target;
        }


        IEnumerable<IPronoun> IPronounBindable.BoundPronouns {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
