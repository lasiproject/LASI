using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Verb Phrase, a Phrase with the syntactic role of a verb.
    /// </summary>
    public class VerbPhrase : Phrase, ITransitiveAction, IAdverbialModifiable, IModalityModifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the VerbPhrase.</param>
        public VerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
            Tense = VerbTense.Base;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb.
        /// </summary>
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public void ModifyWith(IAdverbial adv) {
            _modifiers.Add(adv);
        }
        /// <summary>
        /// Binds the verb to its object via a propisitional construnct such as a Pronoun or Pronounphrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prep">The prepositional</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prep) {
            ObjectViaPreposition =
                prep.OnRightSide != null ?
                prep.OnRightSide :
                prep.OnLeftSide;

        }
        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        public override System.Xml.Linq.XElement Serialize() {
            throw new NotImplementedException();
        }
        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the subject of the VerbPhrase.
        /// </summary>
        public IEntity BoundSubject {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the direct object of the VerbPhrase.
        /// </summary>
        public virtual IEntity DirectObject {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the indirect object of the VerbPhrase.
        /// </summary>
        public virtual IEntity IndirectObject {
            get;
            set;
        }
        /// <summary>
        /// Gets the collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }

        /// <summary>
        /// Gets or sets the Tense of the VerbPhrase.
        /// <see cref="VerbTense"/>
        /// </summary>
        public VerbTense Tense {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the Modal word which modifies the VerbPhrase.
        /// </summary>
        public Modal Modality {
            get;
            set;
        }
        /// <summary>
        /// Gets the VerbPhrases's object, If the VerbPhrase has an object bound via a Prepositional construct.
        /// </summary>
        public virtual ILexical ObjectViaPreposition {
            get;
            protected set;
        }
        


        #endregion

        #region Fields

        protected IList<IAdverbial> _modifiers = new List<IAdverbial>();

        #endregion


    }
}

