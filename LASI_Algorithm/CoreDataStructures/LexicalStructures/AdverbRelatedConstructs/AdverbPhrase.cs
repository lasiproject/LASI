
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb phrase which modifies either an IDescriptor, such as an Adjective or AdjectivePhrase construct, or an IVerbal, such as a Verb or VerbPhrase construct.
    /// </summary>
    public class AdverbPhrase : Phrase, IAdverbial
    {
        /// <summary>
        /// Initializes entity new instance of the AdverbPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the AdverbPhrase.</param>
        public AdverbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct; such as an Adjective, AdjectivePhrase, Verb, or VerbPhrase; which the AdverPhrase Modifies. 
        /// </summary>
        public virtual IAdverbialModifiable Modifies {
            get;
            set;
        }


    }
}
