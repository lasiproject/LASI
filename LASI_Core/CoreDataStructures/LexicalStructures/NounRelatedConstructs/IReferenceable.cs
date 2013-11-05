using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Entities; generally Nouns, Nounphrases; which can be indirectly, and implicitely referred to by Pronouns, thus allowing their semantic influence to persist 
    /// for long stretches during which they may not be longer directly mentioned
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IPronounBindable interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IReferenceable
    {
        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the IPronounBindable.
        /// </summary>
        /// <param name="pro">The IPronoun which will be bound to refer to the IPronounBindable.</param>
        void BindPronoun(IReferencer pro);
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the IPronounBindable.
        /// </summary>
        IEnumerable<IReferencer> BoundPronouns {
            get;
        }
    }
}
