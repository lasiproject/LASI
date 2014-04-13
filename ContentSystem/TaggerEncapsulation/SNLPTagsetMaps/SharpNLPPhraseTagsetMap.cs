using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.ContentSystem.TaggerEncapsulation
{
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
    /// var constructorFunction = phraseMap["TAG"];
    /// var runTimePhrase = constructorFunction(itemText);
    /// </code>
    /// </example>    
    /// <see cref="WordTagsetMap"/>
    /// <see cref="WordFactory"/> 
    sealed class SharpNLPPhraseTagsetMap : PhraseTagsetMap
    {
        #region Fields
        private static readonly IReadOnlyDictionary<string, PhraseCreator> map = new Dictionary<string, PhraseCreator>
        {
            ["VP"] = ws => ws.Any(w => w is Punctuator) ? new SymbolPhrase(ws) : ws.TakeWhile(w => !(w is IVerbal)).FirstOrDefault(w => w is ToLinker) != null ? new InfinitivePhrase(ws) : new VerbPhrase(ws) as Phrase,
            ["NP"] = ws => ws.OfType<IEntity>().Any() && ws.All(w => w is Pronoun) ? new PronounPhrase(ws) : ws.All(w => w is Adverb) ? new AdverbPhrase(ws) : new NounPhrase(ws) as Phrase,
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
            [""] = ws => { throw new EmptyPhraseTagException(string.Join(" ", ws.Select(w => w.Text))); },
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
                }
                catch (KeyNotFoundException) {
                    throw new UnknownPhraseTagException(posTag);
                }
            }
        }
        /// <summary>
        /// Gets the PosTag string corresponding to the runtime System.Type of the Return Type of given function of type { IEnumerable of LASI.Algorithm.Word => LASI.Algorithm.Phrase }.
        /// </summary>
        /// <param name="phraseCreatingFunc">The function of type { IEnumerable of LASI.Algorithm.Word => LASI.Algorithm.Phrase } for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the runtime System.Type of the Return Type of the given function of type { IEnumerable of LASI.Algorithm.Word => LASI.Algorithm.Phrase }.</returns>
        public override string this[PhraseCreator phraseCreatingFunc] {
            get {
                try {
                    return map.First(pair => pair.Value.Method.ReturnType == phraseCreatingFunc.Method.ReturnType).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedPhraseTypeException(string.Format("Phrase constructor\n{0}\nis not mapped by this Tagset.\nFunction Type: {1} => {2}",
                        phraseCreatingFunc,
                        string.Join(", ", from param in phraseCreatingFunc.Method.GetParameters()
                                          select param.ParameterType.FullName),
                        phraseCreatingFunc.Method.ReturnType.FullName
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given LASI.Algorithm.Phrase.
        /// </summary>
        /// <param name="phrase">The LASI.Algorithm.Phrase for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Phrase.</returns>
        public override string this[Phrase phrase] {
            get {
                try {
                    return map.First(funcPosTagPair => funcPosTagPair.Value.Method.ReturnType == phrase.GetType()).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedPhraseTypeException(string.Format("The indexing LASI.Algorithm.Phrase has type {0}, a type which is not mapped by {1}.", phrase.GetType(), this.GetType()));
                }
            }
        }
        /// <summary>
        /// Gets the IDictionary which contains the mappings between literal tags and Phrase Instantiating functions.
        /// </summary>
        protected override IReadOnlyDictionary<string, PhraseCreator> TypeDictionary {
            get {
                return map;
            }
        }

        #endregion
    }
}
