using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Represents an adverb phrase which modifies either an IDescriptor, such as an Adjective or AdjectivePhrase construct, or an IVerbal,
    /// such as a Verb or VerbPhrase construct.
    /// </summary>
    public class AdverbPhrase : Phrase, IAdverbial
    {
        /// <summary>
        /// Initializes entity new instance of the AdverbPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the AdverbPhrase.</param>
        public AdverbPhrase(IEnumerable<Word> words) : base(words) { }

        /// <summary>
        /// Initializes a new instance of the AdverbPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the AdverbPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the AdverbPhrase.</param>
        /// <remarks>
        /// This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. Thus, its purpose
        /// is to simplify test code.
        /// </remarks>
        public AdverbPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        /// <summary>
        /// Returns a string representation of the <see cref="AdverbPhrase"/>.
        /// </summary>
        /// <returns>A string representation of the <see cref="AdverbPhrase"/>.</returns>
        public override string ToString() => base.ToString() + (VerboseOutput && Modifies != null ? $"\nModifies: {Modifies}" : string.Empty);

        /// <summary>
        /// Gets the IVerbal the AdverbPhrase modifies.
        /// </summary>
        public IVerbal AttributedTo { get; }

        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct; such as an Adjective, AdjectivePhrase, Verb, or VerbPhrase; which the
        /// AdverPhrase Modifies.
        /// </summary>
        public virtual IAdverbialModifiable Modifies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the IDescriptor the AdverbPhrase modifies.
        /// </summary>
        IDescriptor IAttributive<IDescriptor>.AttributedTo { get; }
    }
}
