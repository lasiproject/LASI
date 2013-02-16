using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb phrase which modifies either an IDescriber, such as an Adjective or AdjectivePhrase construct, or an IAction, such as a Verb or VerbPhrase construct.
    /// </summary>
    public class AdverbPhrase : Phrase, IAdverbial
    {
        /// <summary>
        /// Initializes a new instance of the AdverbPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the AdverbPhrase.</param>
        public AdverbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct; such as an Adjective, AdjectivePhrase, Verb, or VerbPhrase; which the AdverPhrase Modifies. 
        /// </summary>
        public virtual IAdverbialModifiable Modiffied {
            get;
            set;
        }
        /// <summary>
        /// Gets the Word which of the phrase. The head word determines the syntactic role of the entire phrase via its intra-phrase associations.
        /// </summary>
        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Determines the head word of the phrase, the word which determines the syntactic role of the entire phrase via its intra-phrase associations.
        /// One determined, the head word can be retrived via the HeadWord Property.
        /// </summary>
        /// <see cref="HeadWord"/>
        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }
    }
}
