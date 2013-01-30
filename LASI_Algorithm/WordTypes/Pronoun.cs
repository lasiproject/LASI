using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a pronoun which gernerally refers to a previously defined Entity, such as a Noun or NounPhrase.
    /// </summary>
    public class Pronoun : Word, IEntityReferencer, IActionObject, IActionSubject, IReferenciable
    {
        /// <summary>
        /// Initializes a new instance of the Pronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the pronoun.</param>
        public Pronoun(string text)
            : base(text) {
            var list = new WordList();

        }


        private ICollection<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();



        #region Methods


        public virtual void BindPronoun(IEntityReferencer pro) {
            throw new NotImplementedException();
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
        /// Gets or sets the Entity which the Pronoun references.
        /// </summary>
        public IEntity BoundEntity {
            get;
            set;
        }
        /// <summary>
        /// Gets the ITransitiveAction instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the object of.
        /// </summary>
        public virtual ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        public virtual ICollection<IEntityReferencer> IndirectReferences {
            get {
                return _indirectReferences;
            }

        }
        /// <summary>
        /// Gets the ITransitiveAction instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the Pronoun is the INDIRECT object of.
        /// </summary>
        public virtual ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the ISubjectTaker instance, generally a Verb or VerbPhrase, which the Pronoun is the subject of.
        /// </summary>
        public virtual IIntransitiveAction SubjectOf {
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
            return A == B && (
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



    }
}
