using LASI.Algorithm.FundamentalSyntacticInterfaces;
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
    public abstract class Pronoun : Word, IEntity, IPronounBindable
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


        public virtual void BindPronoun(Pronoun pro) {
            if (!_boundPronouns.Contains(pro))
                _boundPronouns.Add(pro);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public virtual bool Equals(IEntity other) {
            return (Pronoun) other == this;
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
        /// Gets the ITransitiveVerbial instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public virtual ITransitiveVerbial DirectObjectOf {
            get;
            set;
        }

        public virtual IEnumerable<Pronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }

        }
        /// <summary>
        /// Gets the ITransitiveVerbial instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public virtual ITransitiveVerbial IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public virtual ITransitiveVerbial SubjectOf {
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
        public EntityThemeMemberKind ThemeMemberKind {
            get;
            set;
        }

        #endregion

        #region Operators

        /// <summary>
        /// This Pronoun specialized implementation of the Equality Operator returns True if and only if its operands refer to the same Entity instance and are composed of the same text.
        /// </summary>
        /// <param name="A">The Pronoun on the Left hand side of the operator.</param>
        /// <param name="B">The Pronoun on the Left hand side of the operator.</param>
        /// <returns>True if the Pronouns are equal and False otherwise.</returns>
        public static bool operator ==(Pronoun A, Pronoun B) {
            return A.Text == B.Text && (
            A.BoundEntity.Equals(B.BoundEntity) || (A.BoundEntity == null) && B.BoundEntity == null);
        }
        /// <summary>
        /// This Pronoun specialized implementation of the Inquality Operator returns True if its operands refer to different entities and or are composed of different text.
        /// </summary>
        /// <param name="A">The Pronoun on the Left hand side of the operator.</param>
        /// <param name="B">The Pronoun on the Left hand side of the operator.</param>
        /// <returns>True if the Pronouns are not equal and False otherwise.</returns>
        public static bool operator !=(Pronoun A, Pronoun B) {
            return !(A == B);
        }
        #endregion

        #region Fields
        private ICollection<IDescriber> _describers = new List<IDescriber>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<Pronoun> _boundPronouns = new List<Pronoun>();
        protected bool IsBound {
            get {
                return BoundEntity != null;
            }
        }
        private EntityKind _entityKind;
        private IEntity _boundEntity;

        #endregion




    }
}
