using System.Collections.Generic;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Entities; generally Nouns, Nounphrases; which can be indirectly, and implicitely referred to by Pronouns, thus allowing their semantic influence to persist 
    /// for long stretches during which they may not be longer directly mentioned
    /// Along with the rhs interfaces in the Syntactic Interfaces Library, the IPronounBindable interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IPronounBindable
    {
        void BindPronoun(LASI.Algorithm.LexicalStructures.NounRelatedConstructs.IPronoun pro);

        IEnumerable<LASI.Algorithm.LexicalStructures.NounRelatedConstructs.IPronoun> BoundPronouns {
            get;
        }
    }
}
