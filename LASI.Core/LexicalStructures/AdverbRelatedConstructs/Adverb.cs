using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Represents an adverb which can be bound as a modifier to either a verb construct or an adjective construct.
    /// </summary>
    public class Adverb : Word, IAdverbial, IAdverbialModifiable
    {
        /// <summary>
        /// Initializes a new instance of the Adverb class.
        /// </summary>
        /// <param name="text">The text content of the adverb.</param>
        public Adverb(string text) : base(text) { }

        /// <summary>
        /// Attaches an IAdverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the current Adverb
        /// <param name="modifier">The IAdverbial construct by which to modify the current Adverb.</param>
        /// </summary>
        public void ModifyWith(IAdverbial modifier)
        {
            adverbialModifiers.Add(modifier);
            modifier.Modifies = this;
        }

        /// <summary>
        /// The List of IAdverbial modifiers which modify the Adverb.
        /// </summary>
        public IEnumerable<IAdverbial> AdverbialModifiers => adverbialModifiers;

        /// <summary>
        /// Gets the collection of Adverbial constructs which modify the Adverb
        /// </summary>
        public IEnumerable<IAdverbial> AttributedBy => AdverbialModifiers;
        /// <summary>
        /// The Verbal the Adverb modifies.
        /// </summary>
        public IVerbal AttributedTo => Modifies as IVerbal;
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct; such as an Adjective, AdjectivePhrase, Verb, or VerbPhrase; which the Adverb Modifies. 
        /// </summary>
        public IAdverbialModifiable Modifies { get; set; }

        IDescriptor IAttributive<IDescriptor>.AttributedTo => Modifies as IDescriptor;
        private HashSet<IAdverbial> adverbialModifiers = new HashSet<IAdverbial>();
    }
}
