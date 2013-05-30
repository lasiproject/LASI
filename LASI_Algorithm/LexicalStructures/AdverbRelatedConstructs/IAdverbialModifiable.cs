using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Adverbial Modifiable elements, including Verbs, VerbPhrases, Adjectives, and AdjectivePhrases, whose meaning can be modified by IAdverbial 
    /// elements such as Adverbs and AdverbPhrases.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IAdverbialModifiable interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IAdverbialModifiable
    {
        void ModifyWith(IAdverbial adv);
        IEnumerable<IAdverbial> Modifiers {
            get;
        }
    }
}
