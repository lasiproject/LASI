using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Adverbial elements, generally Adverbs or or AdvebPhrases, which may modify the meaning of IAdverbialModifiable elememts such as Verbs, VerbPhrases, Adjectives, and AdjectivePhrases.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IAdverbial interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IAdverbial : ILexical
    {
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct the IAdverbial modfies.
        /// </summary>
        IAdverbialModifiable Modifies {
            get;
            set;
        }
    }
}
