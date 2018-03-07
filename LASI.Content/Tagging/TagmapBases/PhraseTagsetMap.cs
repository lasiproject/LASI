using System;
using System.Collections.Generic;
using LASI.Content.Exceptions;
using LASI.Core;

namespace LASI.Content.Tagging
{
    using PhraseFactory = Func<IEnumerable<Word>, Phrase>;

    /// <inheritdoc />
    /// <summary>
    /// Represents a tagset-to-runtime-type-mapping context for Phrase constructs which translates between a Part Of Speech
    /// Tagger's tagset and the classes whose instances provide the runtime representations of the Phrase tag.
    /// This class represents the tagset =&gt; runtime-type mapping for Phrase occurrences.
    /// </summary>
    /// <example>
    /// <code>
    /// var phraseMap = new PhraseTagSetMap();
    /// var phraseFactory = phraseMap["TAG"];
    /// var phrase = phraseFactory(phraseWords);
    /// </code>
    /// </example>
    /// <seealso cref="T:LASI.Content.Tagging.WordTagsetMap" />
    public abstract class PhraseTagsetMap : ITagsetMap<IEnumerable<Word>, Phrase>
    {
        /// <summary>
        /// When overridden in a derived class, Provides POS-Tag indexed access to a constructor
        /// function which can be invoked to create an instance of the Phrase class which provides
        /// its run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Phrase Part Of Speech tag.</param>
        /// <returns>
        /// A function which creates an instance of the run-time Phrase type associated with the
        /// textual tag.
        /// </returns>
        /// <exception cref="UnknownPhraseTagException">
        /// Thrown when the indexing tag string is not defined by the tagset.
        /// </exception>
        public abstract PhraseFactory this[string tag] { get; }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, Gets the PosTag string corresponding to the
        /// System.Type of the given LASI.Algorithm.Phrase.
        /// </summary>
        /// <param name="phrase">
        /// The <see cref="T:LASI.Core.Phrase" /> for which to get the corresponding tag.
        /// </param>
        /// <returns>The PosTag string corresponding to the System.Type of the given <see cref="T:LASI.Core.Phrase" />.</returns>
        public abstract string this[Phrase phrase] { get; }
    }
}