using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a verb phrase.
    /// </summary>
    public class VerbPhrase : Phrase, IAction, IAdverbialModifiable, IModalityModifiable
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

        public virtual void AttachObjectViaPreposition(IPrepositional prep) {
            throw new NotImplementedException();
        }


        public override void DetermineHeadWord() {
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
        /// Gets or sets the List of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }

        /// <summary>
        /// Gets or sets the tense of the VerbPhrase.
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

        public virtual ILexical ObjectViaPreposition {
            get;
            protected set;
        }
        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }


        #endregion

        #region Fields

        protected IList<IAdverbial> _modifiers = new List<IAdverbial>();

        #endregion
    }
}

