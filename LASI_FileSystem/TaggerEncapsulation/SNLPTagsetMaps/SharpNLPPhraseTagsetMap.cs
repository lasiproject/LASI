using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using System.Reflection;
using LASI.Algorithm.AdditionalPhraseTypes;


namespace LASI.FileSystem.TaggerEncapsulation
{
    /// <summary>
    /// Represents a Word Level tagset-to-runtime-type-mapping context which translates between The SharpNLP Tagger's tagset and the classes whose instances provide 
    /// the runtime representations of the tag. 
    /// This class represents the tagset => runtime-type mapping for
    /// the tagset used by SharpNLP, a derrivative of the Penn Tagset.
    /// This class is sealed and thus may not be extended.
    /// If a new tagset is to be implemented, extend the base class, TaggingContext.
    /// <see cref="WordTagsetMap"/>
    ///<see cref="WordMapper"/> 
    /// </summary>    
    /// <example>
    /// Example:
    /// <code>
    /// var phraseMap = new SharpNLPPhraseTagsetMap
    /// var constructorFunction = phraseMap["TAG"];
    /// var runTimePhrase = constructorFunction(itemText);
    /// </code>
    /// </example>
    public sealed class SharpNLPPhraseTagsetMap : PhraseTagsetMap
    {
        #region Fields
        private readonly Dictionary<string, Func<IEnumerable<Word>, Phrase>> typeDictionary = new Dictionary<string, Func<IEnumerable<Word>, Phrase>> {
            
            { "VP", ws => ws.Any(w=> w is Punctuation) ? new SymbolPhrase(ws) as Phrase : new VerbPhrase(ws) as Phrase },
            { "NP", ws => ws.OfType<IEntity>().All(w=>w is IPronoun)?new PronounPhrase(ws) : new NounPhrase(ws) },
            { "PP", ws => new PrepositionalPhrase(ws) },
            { "ADVP", ws => new AdverbPhrase(ws) },
            { "ADJP", ws => new AdjectivePhrase(ws) },
            { "PRT", ws => new ParticlePhrase(ws) },
            { "CONJP", ws => new ConjunctionPhrase(ws) },
            { "S", ws => new SimpleDeclarativeClauseBeginPhrase(ws) },
            { "SINV", ws => new SimpleDeclarativeClauseBeginPhrase(ws) },
            { "SQ", ws => new InterrogativePhrase(ws) },
            { "SBARQ", ws => new InterrogativePhrase(ws) },
            { "SBAR", ws => new SubordinateClauseBeginPhrase(ws) },
            { "LST", ws => new RoughListPhrase(ws) },
            { "INTJ", ws => new InterjectionPhrase(ws) },
            //{ "", ws => { throw new EmptyPhraseTagException(string.Format("the tag for word: {0}\nis empty",ws.Aggregate("", (s,w) => s += " ").Trim())); } },
        };

        #endregion

        #region Properties and Indexers
        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the Phrase class which provides its run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Phrase Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time Phrase type associated with the textual tag.</returns>
        /// <exception cref="UnknownWordTagException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override Func<IEnumerable<Algorithm.Word>, Algorithm.Phrase> this[string tag] {
            get {
                try {
                    try {
                        return typeDictionary[tag];
                    }
                    catch (KeyNotFoundException) {
                        throw new UnknownPhraseTagException(String.Format("The phrase tag {0} is not defined by this Tagset", tag));
                    }
                }
                catch (UnknownPhraseTagException e) {
                    LASI.Utilities.Output.WriteLine("A Phrase with an unknown tag was encounterd.\n{0}\nInstantiating new LASI.Algorithm.UndeterminedPhrase to compensate", e.Message);
                    return ws => new UndeterminedPhrase(ws);
                }
            }
        }
        /// <summary>
        /// Gets the PosTag string corresponding to the runtime System.Type of the Return Type of given function which of type { IEnumerable of LASI.Algorithm.Word => LASI.Algorithm.Phrase }.
        /// </summary>
        /// <param name="phrase">The function which of type { IEnumerable of LASI.Algorithm.Word => LASI.Algorithm.Phrase } for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the runtime System.Type of the Return Type of given function which of type { IEnumerable of LASI.Algorithm.Word => LASI.Algorithm.Phrase }.</returns>
        public override string this[Func<IEnumerable<Algorithm.Word>, Algorithm.Phrase> phraseCreatingFunction] {
            get {
                try {
                    return typeDictionary.First(pair => pair.Value.Method.ReturnType == phraseCreatingFunction.Method.ReturnType).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedPhraseTypeException(string.Format("Phrase constructor\n{0}\nis not mapped by this Tagset.\nFunction Type: {1} => {2}",
                        phraseCreatingFunction,
                        phraseCreatingFunction.Method.GetParameters().Aggregate("", (s, p) => s += p.ParameterType.FullName + ", ").TrimEnd(',', ' '),
                        phraseCreatingFunction.Method.ReturnType.FullName
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
                    return typeDictionary.First(wordCreatorPosTagPair => wordCreatorPosTagPair.Value.Method.ReturnType == phrase.GetType()).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedPhraseTypeException(string.Format("The indexing LASI.Algorithm.Phrase has type {0}, a type which is not mapped by {1}.", phrase.GetType(), this.GetType()));
                }
            }
        }

        public override IReadOnlyDictionary<string, Func<IEnumerable<Word>, Phrase>> TypeDictionary {
            get {
                return typeDictionary;
            }
        }

        #endregion
    }
}
