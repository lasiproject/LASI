using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
namespace LASI.ContentSystem.TaggerEncapsulation
{
    using WordCreator = Func<string, Word>;
    /// <summary>
    /// Represents a Word Level tagset-to-runtime-type-mapping context which translates between The SharpNLP Tagger's tagset and the classes whose instances provide 
    /// the runtime representations of the tag. 
    /// This class represents the tagset => runtime-type mapping for
    /// the tagset used by SharpNLP, a derrivative of the Penn Tagset.
    /// This class is sealed and thus may not be extended.
    /// If a new tagset is to be implemented, extend the base class, TaggingContext.
    /// <see cref="WordTagsetMap"/>
    ///<see cref="WordFactory"/> 
    /// </summary>    
    /// <example>
    /// Example:
    /// <code>
    /// var wordMap = new SharpNLPWordTagsetMap();
    /// var constructorFunction = wordMap["TAG"];
    /// var runTimeWord = constructorFunction(itemText);
    /// </code>
    /// </example>
    sealed class SharpNLPWordTagsetMap : WordTagsetMap
    {
        #region Fields

        private static readonly IReadOnlyDictionary<string, WordCreator> map = new Dictionary<string, WordCreator> {
            //Punctation Mappings
            { ",", t => new Punctuator(t) }, //Comma punctuation
            { ";", t => new Punctuator(t) }, //Semicolon punctuation
            { ":", t => new Punctuator(t) }, //Colon punctuation 
            { ".", t => t == "." || t == "!" || t == "?" ? new SentenceEnding(t[0]) : new UnknownWord(t) as Word }, //. sentence ending
            { "!", t => t == "." || t == "!" || t == "?" ? new SentenceEnding(t[0]) : new UnknownWord(t) as Word }, //! sentence ending
            { "?", t => t == "." || t == "!" || t == "?" ? new SentenceEnding(t[0]) : new UnknownWord(t) as Word }, //? sentence ending
            { "``", t => new DoubleQuote() }, //Single quote * should be remapped
            { "''", t => new DoubleQuote() }, //Double Quotation Mark punctuation
            { "LS", t => new Punctuator(t) }, //List item marker
            { "-LRB-", t => new Punctuator(t) }, //Left Brackets
            { "-RRB-", t => new Punctuator(t) },  //Right Bracket
            //Determinism mappings
            { "CD", t => new Quantifier(t) }, //Cardinal number
            { "DT", t => new Determiner(t) }, //Determiner
            { "EX", t => new Existential(t) }, //Existential 'there'
            { "FW", t => new ForeignWord(t) }, //Foreign word
            { "IN", t => new Preposition(t) }, //Preposition or subordinating conjunction
            { "CC", t => new Conjunction(t) }, //Coordinating conjunction
            //Adjective mappings
            { "JJ", t => new Adjective(t) }, //Adjective
            { "JJR", t => new ComparativeAdjective(t) }, //Adjective, comparative
            { "JJS", t => new SuperlativeAdjective(t) }, //Adjective, superlative
            { "MD", t => new ModalAuxilary(t) }, //ModalAuxilary
            //Noun mappings
            { "NN", t => new CommonSingularNoun(t) }, //Noun, singular or mass
            { "NNS", t => new CommonPluralNoun(t) }, //Noun, plural
            { "NNP", t => Lookup.ScrabbleDictionary.Contains(t.ToLower())? new CommonSingularNoun(t): new ProperSingularNoun(t) as Noun }, //Proper noun, singular
            { "NNPS", t => Lookup.ScrabbleDictionary.Contains(t.ToLower())? new CommonPluralNoun(t): new ProperPluralNoun(t) as Noun }, //Proper noun, plural
            //Pronoun mappings
            { "PDT", t => new PreDeterminer(t) }, //Predeterminer
            { "POS", t => new PossessiveEnding(t) }, //Possessive ending
            { "PRP", t => new PersonalPronoun(t) }, //Personal pronoun
            { "PRP$", t => new PossessivePronoun(t) }, //Possessive pronoun
            //Adverb mappings
            { "RB", t => new Adverb(t) }, //Adverb
            { "RBR", t => new ComparativeAdverb(t) }, //Adverb, comparative
            { "RBS", t => new SuperlativeAdverb(t) }, //Adverb, superlative
            //Verb mappings
            { "VB", t => new Verb(t, VerbForm.Base) }, //Verb, base form
            { "VBD", t => new PastTenseVerb(t) }, //Verb, past tense
            { "VBG", t => new PresentParticipleGerund (t) }, //Verb, gerund or present participle
            { "VBN", t => new PastParticipleVerb(t) }, //Verb, past participle
            { "VBP", t => new Verb(t, VerbForm.SingularPresent) }, //Verb, non-3rd person singular present
            { "VBZ", t => new Verb(t, VerbForm.ThirdPersonSingularPresent) }, //Verb, 3rd person singular present
            //WH-word mappings
            { "WDT", t => new Determiner(t) }, //Wh-Determiner
            { "WP", t => new RelativePronoun(t) }, //Wh-Pronoun
            { "WP$", t => new RelativePossessivePronoun(t) }, //Possessive wh-pronoun
            { "WRB", t => new Adverb(t) }, //Wh-word
            //Additional mappings
            { "RP", t => new Particle(t) }, //Particle
            { "SYM", t => new Symbol(t) }, //Symbol
            { "TO", t=> new ToLinker() }, //'To'
            { "UH", t => new Interjection(t) }, //Interjection
            //Empty POS Tag, resulting function will throw EmptyTagException on invocation.
            { "", t => { throw new EmptyWordTagException(t); } }, 

        };

        #endregion

        #region Properties and Indexers
        /// <summary>
        /// Gets the Read Only Dictionary which represents the mapping between Part Of Speech tags and the cunstructors which instantiate their run-time representations.
        /// </summary>
        protected override IReadOnlyDictionary<string, WordCreator> TypeDictionary {
            get {
                return map;
            }
        }
        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the class which provides its run-time representation.
        /// </summary>
        /// <param name="posTag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time type associated with the textual tag.</returns>
        /// <exception cref="UnknownWordTagException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override WordCreator this[string posTag] {
            get {
                try {
                    return map[posTag];
                }
                catch (KeyNotFoundException) {
                    throw new UnknownWordTagException(posTag);
                }
            }
        }
        /// <summary>
        /// Gets the PosTag string corresponding to the runtime System.Type of the Return Type of given function of type { System.string => LASI.Algorithm.Word }.
        /// </summary>
        /// <param name="wordCreator">The function of type { System.string => LASI.Algorithm.Word } for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the runtime System.Type of the Return Type of given function of type { System.string => LASI.Algorithm.Word }.</returns>
        public override string this[WordCreator wordCreator] {
            get {
                try {
                    return map.First(pair => pair.Value.Method.ReturnType == wordCreator.Method.ReturnType).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedWordTypeException(string.Format("Word constructor\n{0}\nis not mapped by this Tagset.\nFunction Type: {1} => {2}",
                        wordCreator, string.Join(", ", from param in wordCreator.Method.GetParameters() select param.ParameterType.FullName,
                        wordCreator.Method.ReturnType.FullName)));
                }
            }
        }
        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.
        /// </summary>
        /// <param name="word">The LASI.Algorithm.Word for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.</returns>
        public override string this[Word word] {
            get {
                try {
                    return map.First(funcPosTagPair => funcPosTagPair.Value.Method.ReturnType == word.GetType()).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedWordTypeException(string.Format("The indexing LASI.Algorithm.Word has type {0}, a type which is not mapped by {1}.",
                        word.GetType(),
                        this.GetType()
                        )
                    );
                }
            }
        }
        #endregion
    }
}
