using System;
using LASI.Content.Exceptions;

namespace LASI.Content.Tagging
{
    /// <summary>
    /// Represents a tagset-to-runtime-type-mapping context for <typeparamref name="TLexical"/> constructs which translates between a Part Of Speech
    /// Tagger's tagset and the types whose instances provide the runtime representations of the said tag.
    /// This <see langword="interface"/> represents the tagset => runtime-type mapping for <typeparamref name="TLexical"/> occurrences.
    /// </summary>
    /// <example>
    /// <code>
    /// var phraseMap = new PhraseTagSetMap();
    /// var phraseFactory = phraseMap["TAG"];
    /// var phrase = phraseFactory(phraseWords);
    /// </code>
    /// </example>
    /// <seealso cref="WordTagsetMap"/>
    public interface ITagsetMap<in TFactoryParameter, TLexical> where TLexical : Core.ILexical
    {
        /// <summary>
        /// Gets the PosTag string corresponding to the <see cref="Type"/> of the given
        /// <typeparamref name="TLexical"/>.
        /// </summary>
        /// <param name="leixcal">The <typeparamref name="TLexical"/> for which to get the corresponding tag.</param>
        /// <returns>
        /// The PosTag string corresponding to the <see cref="Type"/> of the given <typeparamref name="TLexical"/>.
        /// </returns>
        string this[TLexical leixcal] { get; }

        /// <summary>
        /// Provides POS-Tag indexed access to a factory function which can be invoked to create an instance of the class which provides its
        /// run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>
        /// A function which creates an instance of the run-time type associated with the textual tag.
        /// </returns>
        /// <exception cref="UnknownPartOfSpeechException">
        /// Implementors should throw this exception if and only if the indexing string is not a
        /// tag defined by the tagset being provided.
        /// </exception>
        Func<TFactoryParameter, TLexical> this[string tag] { get; }
    }
}