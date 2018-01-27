﻿using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Utilities;

namespace LASI.Content.Tagging
{
    using PhraseFactory = Func<IEnumerable<Word>, Phrase>;

    /// <summary>
    /// Represents a Word Level tagset-to-runtime-type-mapping context which translates between The SharpNLP Tagger's tagset and the classes whose instances provide the runtime representations of the
    /// tag. This class represents the tagset =&gt; runtime-type mapping for the tagset used by SharpNLP, a derivative of the Penn Tagset. This class is sealed and thus may not be extended. If a new
    /// tagset is to be implemented, extend the base class, TaggingContext.
    /// </summary>
    /// <example>
    /// <code>
    /// var phraseMap = new SharpNLPPhraseTagsetMap();
    /// var phraseFactory = phraseMap["TAG"];
    /// var phrase = phraseFactory(phraseWords);
    /// </code>
    /// </example>
    /// <seealso cref="WordTagsetMap"/>
    /// <seealso cref="WordFactory"/>
    internal sealed class SharpNLPPhraseTagsetMap : PhraseTagsetMap
    {
        private static readonly IReadOnlyDictionary<string, PhraseFactory> map = new Dictionary<string, PhraseFactory>
        {
            ["VP"] = ws => ws.OfPunctuator().Any()
                ? new SymbolPhrase(ws)
                : ws.TakeWhile(w => !(w is IVerbal)).OfToLinker().Any()
                ? new InfinitivePhrase(ws)
                : new VerbPhrase(ws) as Phrase,
            ["NP"] = ws => ws.OfEntity().Any() && ws.All(w => w is Pronoun)
                ? new PronounPhrase(ws)
                : ws.All(w => w is Adverb)
                ? new AdverbPhrase(ws)
                : new NounPhrase(ws) as Phrase,
            ["PP"] = ws => new PrepositionalPhrase(ws),
            ["ADVP"] = ws => new AdverbPhrase(ws),
            ["ADJP"] = ws => new AdjectivePhrase(ws),
            ["PRT"] = ws => new ParticlePhrase(ws),
            ["CONJP"] = ws => new ConjunctionPhrase(ws),
            ["S"] = ws => new SimpleDeclarativeClauseBeginPhrase(ws),
            ["SINV"] = ws => new SimpleDeclarativeClauseBeginPhrase(ws),
            ["SQ"] = ws => new InterrogativePhrase(ws),
            ["SBARQ"] = ws => new InterrogativePhrase(ws),
            ["SBAR"] = ws => new SubordinateClauseBeginPhrase(ws),
            ["LST"] = ws => new RoughListPhrase(ws),
            ["INTJ"] = ws => new InterjectionPhrase(ws),
            [""] = ws => throw new EmptyPhraseTagException(ws.Format(w => w.Text)),
        };

        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the Phrase class which provides its run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Phrase Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time Phrase type associated with the textual tag.</returns>
        /// <exception cref="UnknownPhraseTagException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override PhraseFactory this[string tag]
        {
            get
            {
                PhraseFactory result;
                if (!map.TryGetValue(tag, out result))
                {
                    throw new UnknownPhraseTagException(tag);
                }
                return result;
            }
        }

        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given <see cref="Phrase"/>.
        /// </summary>
        /// <param name="phrase">The <see cref="Phrase"/> for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Phrase.</returns>
        public override string this[Phrase phrase]
        {
            get
            {
                try
                {
                    return map.First(funcPosTagPair => funcPosTagPair.Value.Method.ReturnType == phrase.GetType()).Key;
                }
                catch (InvalidOperationException)
                {
                    throw new UnmappedPhraseTypeException(phrase.GetType(), typeof(SharpNLPPhraseTagsetMap));
                }
            }
        }
    }
}