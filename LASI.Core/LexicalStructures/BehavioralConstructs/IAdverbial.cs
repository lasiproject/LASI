﻿namespace LASI.Core
{
    /// <summary>
    /// <para>Defines the role requirements for Adverbial elements, generally Adverbs or AdvebPhrases,</para>
    /// <para>which may modify the meaning of IAdverbialModifiable elements such as Verbs, VerbPhrases, Adjectives, and AdjectivePhrases.</para>
    /// <para>Along with the other interfaces in the Syntactic Interfaces Library, the IAdverbial interface provides for cross-axial generalization over lexical types.</para>
    /// </summary>
    public interface IAdverbial : ILexical, IAttributive<IVerbal>, IAttributive<IDescriptor>
    {
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct the IAdverbial modifies.
        /// </summary>
        IAdverbialModifiable Modifies { get; set; }
    }
}
