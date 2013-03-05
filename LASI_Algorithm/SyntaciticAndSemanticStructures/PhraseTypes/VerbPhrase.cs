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
        /// Binds the VerbPhrase to an object via a propisitional construct such as a Prepositon or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prep"></param>
        public virtual void AttachObjectViaPreposition(IPrepositional prep) {
            ObjectViaPreposition =
                prep.OnRightSide != null ?
                prep.OnRightSide :
                prep.OnLeftSide;

        }


        public override string ToString(bool verbose) {
            if (verbose) {
                var result = base.ToString() + "\n";
                result += DirectObject != null ? "Direct Object: " + DirectObject.ToString() + "\n" : "";
                result += IndirectObject != null ? "Indirect Object: " + IndirectObject.ToString() + "\n" : "";
                result += BoundSubject != null ? "Subject: " + BoundSubject.ToString() + "\n" : "";
                foreach (var mod in _modifiers) {
                    result += "\n" + mod.ToString();
                }
                return result;
            } else
                return ToString();
        }
        public override XElement Serialize() {
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
        /// Gets the prevailing Tense of the VerbPhrase.
        /// <see cref="VerbTense"/>
        /// </summary>
        public VerbTense Tense {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the Modal w which modifies the VerbPhrase.
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


        public Clause GivenExposition {
            get;
            set;
        }

        #endregion

        #region Fields

        protected IList<IAdverbial> _modifiers = new List<IAdverbial>();

        #endregion




    }
}

