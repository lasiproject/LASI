using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// <para>Defines the role requirements for Adverbial Modifiable elements, including Verbs, 
    /// VerbPhrases, Adjectives, and AdjectivePhrases, whose meaning can be modified by IAdverbial 
    /// elements such as Adverbs and AdverbPhrases.</para>
    /// <para>Along with the other interfaces in the Syntactic Interfaces Library, the IAdverbialModifiable interface provides
    /// for cross-axial generalization over lexical types.
    /// </para>
    /// </summary>
    public interface IAdverbialModifiable : ILexical, IAttributable<IAdverbialModifiable, IAdverbial>
    {
        /// <summary>
        /// Attaches an IAdverbial as a modifier of the IAdverbialModifiable.
        /// </summary>
        /// <param name="modifier">The modifier to attach.</param>
        void ModifyWith(IAdverbial modifier);
        /// <summary>
        /// Gets the sequence of IAdverbial constructs which modify the IAdverbialModifiable.
        /// </summary>
        IEnumerable<IAdverbial> AdverbialModifiers { get; }
    }
}
