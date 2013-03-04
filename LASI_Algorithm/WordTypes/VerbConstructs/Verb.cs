using LASI.Algorithm.Weighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class for all word level verb constructs. An instance of this class represents a verb in its base tense.
    /// </summary>
    public class Verb : Word, ITransitiveAction, IAdverbialModifiable, IModalityModifiable, IEquatable<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the Verb class which represents the base tense form of a verb.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public Verb(string text, VerbTense tense)
            : base(text) {
            Modifiers = new List<IAdverbial>();
            Tense = tense;
        }
        #region Methods


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
        /// Binds an 
        /// </summary>
        /// <param name="prep"></param>
        public virtual void AttachObjectViaPreposition(IPrepositional prep) {
            ObjectViaPreposition = this as object == prep.OnLeftSide as object && prep.OnRightSide != null ? prep.OnRightSide : null;
        }
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }




        public override XElement Serialize() {
            throw new NotImplementedException();
        }









        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public virtual bool Equals(Verb other) {
            return this == other;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the subject of the Verb
        /// </summary>
        public virtual IEntity BoundSubject {
            get;
            set;
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
        /// <summary>
        /// Gets the VerbTense of the Verb.
        /// </summary>
        public VerbTense Tense {
            get;
            protected set;
        }
        public virtual ILexical ObjectViaPreposition {
            get;
            protected set;
        }



        /// <summary>
        /// Gets or sets the indirect object of the TransitiveVerb.
        /// </summary>
        public virtual IEntity IndirectObject {
            get;
            set;
        }

        #endregion






        #region Fields
        protected IList<IAdverbial> _modifiers = new List<IAdverbial>();
        #endregion




        public IEntity DirectObject {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }


    }
}
