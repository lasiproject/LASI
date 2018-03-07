using System;
using LASI.Content.Exceptions;
using LASI.Core;

namespace LASI.Content.Tagging
{
    using WordFactory = Func<string, Word>;

    /// <summary>
    /// Represents a tagset-to-runtime-type-mapping context which translates between a Part Of Speech
    /// Tagger's tagset and the classes whose instances provide their runtime representations of the tag.
    /// This class represents the tagset => runtime-type mapping for word occurrences.
    /// </summary>
    /// <example>
    /// <code>
    /// var wordMap = new WordTagSetMap();
    /// var wordFactory = map["TAG"];
    /// var word = wordFactory(wordText);
    /// </code>
    /// </example>
    /// <seealso cref="WordTagsetMap"/>
    /// <seealso cref="PhraseTagsetMap"/>
    /// <seealso cref="Content.WordFactory"/>
    public abstract class WordTagsetMap : ITagsetMap<string, Word>
    {
        /// <summary>
        /// When overridden in a derived class, Provides POS-Tag indexed access to a constructor
        /// function which can be invoked to create an instance of the class which provides its
        /// run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>
        /// A function which creates an instance of the run-time type associated with the textual tag.
        /// </returns>
        /// <exception cref="UnknownWordTagException">
        /// Implementors should throw this exception if and only if the indexing string is not a
        /// tag defined by the tagset being provided.
        /// </exception> 
        public abstract WordFactory this[string tag] { get; }

        /// <summary>Gets the PosTag string corresponding to the System.Type of the given <see cref="Word" />.</summary>
        /// <param name="word">The LASI.Algorithm.Word for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.</returns>
        public abstract string this[Word word] { get; }
    }
}