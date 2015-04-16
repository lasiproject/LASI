
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Represents an adverb phrase which modifies either an IDescriptor, such as an Adjective or AdjectivePhrase construct, or an IVerbal, such as a Verb or VerbPhrase construct.
    /// </summary>
    public class AdverbPhrase : Phrase, IAdverbial
    {
        /// <summary>
        /// Gets the collection of Adverbial constructs which modify the AdverbPhrase
        /// </summary>
        public IVerbal AttributedTo { get; }
        /// <summary>
        /// Gets the Verbal the AdverbPhrase modifies.
        /// </summary>
        IDescriptor IAttributive<IDescriptor>.AttributedTo { get; }
        /// <summary>
        /// Initializes entity new instance of the AdverbPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the AdverbPhrase.</param>
        public AdverbPhrase(IEnumerable<Word> words)
            : base(words)
        {
        }
        /// <summary>
        /// Initializes a new instance of the AdverbPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the AdverbPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the AdverbPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public AdverbPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct; such as an Adjective, AdjectivePhrase, Verb, or VerbPhrase; which the AdverPhrase Modifies. 
        /// </summary>
        public virtual IAdverbialModifiable Modifies
        {
            get;
            set;
        }

        public override string ToString() =>
            base.ToString() + (VerboseOutput && Modifies != null ? $"\nModifies: {Modifies}" : string.Empty);
    }
}
