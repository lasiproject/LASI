
using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.Content.TaggerEncapsulation
{
    using LASI.Utilities;
    using PhraseCreator = System.Func<IEnumerable<LASI.Core.Word>, LASI.Core.Phrase>;
    /// <summary>
    /// Represents a Word Level tagset-to-runtime-type-mapping context which translates
    /// between The SharpNLP Tagger's tagset and the classes whose instances provide 
    /// the runtime representations of the tag. 
    /// This class represents the tagset => runtime-type mapping for
    /// the tagset used by SharpNLP, a derrivative of the Penn Tagset.
    /// This class is sealed and thus may not be extended.
    /// If a new tagset is to be implemented, extend the base class, TaggingContext.
    /// </summary>    
    /// <example>
    /// <code>
    /// var phraseMap = new SharpNLPPhraseTagsetMap();
    /// var createPhrase = phraseMap["TAG"];
    /// var phrase = createPhrase(phraseWords);
    /// </code>
    /// </example>    
    /// <see cref="WordTagsetMap"/>
    /// <see cref="WordFactory"/> 
    sealed class SharpNLPPhraseTagsetMap : PhraseTagsetMap
    {
        #region Fields
        private static readonly IReadOnlyDictionary<string, PhraseCreator> map = new Dictionary<string, PhraseCreator>
        {
            ["VP"] = ws => ws.OfPunctuator().Any() ? new SymbolPhrase(ws) : ws.TakeWhile(w => !(w is IVerbal)).OfToLinker().Any() ? new InfinitivePhrase(ws) : new VerbPhrase(ws) as Phrase,
            ["NP"] = ws => ws.OfEntity().Any() && ws.All(w => w is Pronoun) ? new PronounPhrase(ws) : ws.All(w => w is Adverb) ? new AdverbPhrase(ws) : new NounPhrase(ws) as Phrase,
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
            [""] = ws => { throw new EmptyPhraseTagException(ws.Format(w => w.Text)); },
        };

        #endregion

        #region Properties and Indexers
        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the Phrase class which provides its run-time representation.
        /// </summary>
        /// <param name="posTag">The textual representation of a Phrase Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time Phrase type associated with the textual tag.</returns>
        /// <exception cref="UnknownPhraseTagException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override PhraseCreator this[string posTag] {
            get {
                try {
                    return map[posTag];
                } catch (KeyNotFoundException) {
                    throw new UnknownPhraseTagException(posTag);
                }
            }
        }

        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given <see cref="Phrase"/>.
        /// </summary>
        /// <param name="phrase">The <see cref="Phrase"/> for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Phrase.</returns>
        public override string this[Phrase phrase] {
            get {
                try {
                    return map.First(funcPosTagPair => funcPosTagPair.Value.Method.ReturnType == phrase.GetType()).Key;
                } catch (InvalidOperationException) {
                    throw new UnmappedPhraseTypeException(string.Format("The indexing {0} has type {1}, a type which is not mapped by {2}.",
                        typeof(Phrase),
                        phrase.GetType(),
                        this.GetType()));
                }
            }
        }
        #endregion
    }
}
