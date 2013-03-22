using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb entity which modifies either an IDescriber, such as an Adjective or AdjectivePhrase construct, or an IVerbial, such as a Verb or VerbPhrase construct.
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
        public virtual IVerbial Modiffied {
            get;
            set;
        }

      
    }
}
