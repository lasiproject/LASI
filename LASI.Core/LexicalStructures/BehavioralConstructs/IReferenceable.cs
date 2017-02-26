using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Entities; generally Nouns, NounPhrases; 
    /// which can be indirectly, and implicitly referred to by Pronouns, thus allowing their semantic influence to persist 
    /// for long stretches during which they may not be longer directly mentioned
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IReferenceable interface provides for cross-axial generalization over lexical types.
    /// </summary>
    public interface IReferenceable
    {
        /// <summary>
        /// Binds an IReferencer, generally a Pronoun or PronounPhrase, as a reference to the <see cref="IReferenceable"/>.
        /// </summary>
        /// <param name="referencer">The <see cref="IReferencer"/> which will be bound to refer to the <see cref="IReferenceable"/>.</param>
        void BindReferencer(IReferencer referencer);
        /// <summary>
        /// Gets all of the IReferencer instances, generally Pronouns or PronounPhrases, which refer to the IReferenceable.
        /// </summary>
        IEnumerable<IReferencer> Referencers { get; }
    }
}
