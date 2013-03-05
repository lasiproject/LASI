using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
 
    /// <summary>
    /// Represents an adjective phrase which describes an Entity such as a Noun or NounPhrase construct.
    /// </summary>
    public class AdjectivePhrase : Phrase, IDescriber, IAdverbialModifiable
    {  
        /// <summary>
        /// Initializes a new instance of the AdjectivePhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the AdjectivePhrase.</param>
        public AdjectivePhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the AdjectivePhrase.
        /// </summary>
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public virtual void ModifyWith(IAdverbial adv) {
            throw new NotImplementedException();
        }
        private List<IAdverbial> _modifiers = new List<IAdverbial>();
        /// <summary>
        /// Gets or sets the collection of Adverbial constructs which modify the AdjectivePhrase.
        /// </summary>
        public virtual IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }
        /// <summary>
        /// Gets the Entity which the AdjectivePhrase describes.
        /// </summary>
        public virtual IEntity Describes {
            get;
            set;
        }

       

        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
