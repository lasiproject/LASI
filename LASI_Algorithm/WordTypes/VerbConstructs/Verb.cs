using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class for all word level verb constructs. An instance of this class represents a verb in its base tense.
    /// </summary>
    public class Verb : Word, IAction, IAdverbialModifiable, IModalityModifiable, IEquatable<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the Verb class which represents the base tense form of a verb.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public Verb(string text, VerbTense tense = VerbTense.Base)
            : base(text) {
            Modifiers = new List<IAdverbial>();
            Tense = tense;
        }

        /// <summary>
        /// Gets the subject of the Verb
        /// </summary>
        public virtual IEntity BoundSubject {
            get;
            set;
        }
        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb.
        /// </summary>
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        /// </summary>
        /// <param name="adv"></param>
        public virtual void ModifyWith(IAdverbial adv) {
            _modifiers.Add(adv);
            adv.Modiffied = this;
        }
        /// <summary>
        /// Gets or sets the List of IAdverbial modifiers which modify this Verb.
        /// </summary>
        public virtual IEnumerable<IAdverbial> Modifiers {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Modal word which modifies the Verb.
        /// </summary>
        public Modal Modality {
            get;
            set;
        }
        public VerbTense Tense {
            get;
            protected set;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public bool Equals(Verb other) {
            return this == other;
        }
        protected IList<IAdverbial> _modifiers = new List<IAdverbial>();


        #region Operators


        /// <summary>
        /// Overloads the equality comparasison operator for the Verb class such that two instances of Verb compare equal if and only if:
        /// they have the same text content, the same tense, and are both either transitive or  intransitive.
        /// </summary>
        /// <param name="A">The Verb on the left of hand side of the comparison operator.</param>
        /// <param name="B">The Verb on the right of hand side of the comparison operator.</param>
        /// <returns>True if the Verb instances meet the equality conditions outlined above.</returns>
        //public static bool operator ==(Verb A, Verb B) {
        //    if (A == null || B == null)
        //        return A == null && B == null;
        //    return A.Text == B.Text && A.Tense == B.Tense && A as TransitiveVerb == B as TransitiveVerb;
        //}
        /// <summary>
        /// Overloads the inequality comparasison operator for the Verb class such that two instances of Verb compare unequal if:
        /// they have different text conent, and or of different tenses, and or one is transitive and the other intransitive.
        /// </summary>
        /// <param name="A">The Verb on the left of hand side of the comparison operator.</param>
        /// <param name="B">The Verb on the right of hand side of the comparison operator.</param>ssss
        /// <returns>True if the Verb instances erb meet the equality conditions outlined above.</returns>
        //public static bool operator !=(Verb A, Verb B) {
        //    return !(A == B);
        //}

        #endregion
    }
}
