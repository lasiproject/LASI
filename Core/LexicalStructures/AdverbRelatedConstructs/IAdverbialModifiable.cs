using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para>Defines the role reqirements for Adverbial Modifiable elements, including Verbs, VerbPhrases, Adjectives, and AdjectivePhrases, whose meaning can be modified by IAdverbial 
    /// elements such as Adverbs and AdverbPhrases.
    /// <para>Along with the other interfaces in the Syntactic Interfaces Library, the IAdverbialModifiable interface provides for generalization and abstraction over word and Phrase types
    /// </summary>
    public interface IAdverbialModifiable
    {
        /// <summary>
        /// Attaches an IAdverbial as a modifier of the IAdverbialModifiable.
        /// </summary>
        /// <param name="adv">The modifier to attach.</param>
        void ModifyWith(IAdverbial adv);
        /// <summary>
        /// Gets the sequence of IAdverbial constructs which modify the IAdverbialModifiable.
        /// </summary>
        IEnumerable<IAdverbial> Modifiers {
            get;
        }
    }
}
