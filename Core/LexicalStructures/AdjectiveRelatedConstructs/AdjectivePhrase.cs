
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Core
{

    /// <summary>
    /// Represents an adjective phrase which describes an Entity such as a Noun or NounPhrase construct.
    /// </summary>
    public class AdjectivePhrase : Phrase, IDescriptor, IAdverbialModifiable
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
            modifiers.Add(adv);
            adv.Modifies = this;
        }
        private HashSet<IAdverbial> modifiers = new HashSet<IAdverbial>();

        /// <summary>
        /// Gets the collection of Adverbial constructs which modify the AdjectivePhrase.
        /// </summary>
        public virtual IEnumerable<IAdverbial> AdverbialModifiers {
            get {
                return modifiers;
            }
        }
        /// <summary>
        /// Gets the Entity which the AdjectivePhrase describes.
        /// </summary>
        public virtual IEntity Describes {
            get;
            set;
        }



    }
}
