using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using System.Reflection;
namespace LASI.FileSystem.TaggerEncapsulation
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
    ///<see cref="WordMapper"/> 
    /// </summary>    
    /// <example>
    /// Example:
    /// <code>
    /// var wordMap = new SharpNLPWordTagsetMap
    /// var constructorFunction = wordMap["TAG"];
    /// var runTimeWord = constructorFunction(itemText);
    /// </code>
    /// </example>
    public sealed class SharpNLPWordTagsetMap : WordTagsetMap
    {
        #region Fields

        private static readonly IReadOnlyDictionary<string, WordCreator> typeDictionary = new Dictionary<string, WordCreator> {
            
            { "CC", t => new Conjunction(t) }, //Coordinating conjunction
            { ",", t => new Punctuation(t) }, //Comma punctuation
            { ";", t => new Punctuation(t) }, //Semicolon punctuation
            { ":", t => new Punctuation(t) }, //Colon punctuation
            { "CD", t => new Quantifier(t) }, //Cardinal number
            { "DT", t => new Determiner(t) }, //Determiner
            { "EX", t => new Existential(t) }, //Existential 'there'
            { "FW", t => new ForeignWord(t) }, //Foreign wd
            { "IN", t => new Preposition(t) }, //Preposition or subordinating conjunction
            //Adjective mappings
            { "JJ", t => new Adjective(t) }, //Adjective
            { "JJR", t => new ComparativeAdjective(t) }, //Adjective, comparative
            { "JJS", t => new SuperlativeAdjective(t) }, //Adjective, superlative
            { "LS", t => new Punctuation(t) }, //List item marker
            { "-LRB-", t => new Punctuation(t) }, //Left Bracket
            { "-RRB-", t => new Punctuation(t) },  //Right Bracket
            { "''", t => new Punctuation(t) }, //Single quote * should be remapped
            { "MD", t => new ModalAuxilary(t) }, //ModalAuxilary
            //Noun mappings
            { "NN", t => new GenericSingularNoun(t) }, //Noun, singular or mass
            { "NNS", t => new GenericPluralNoun(t) }, //Noun, plural
            { "NNP", t => new ProperSingularNoun(t) }, //Proper noun, singular
            { "NNPS", t => new ProperPluralNoun(t) }, //Proper noun, plural
            //Pronoun mappings
            { "PDT", t => new PreDeterminer(t) }, //Predeterminer
            { "POS", t => new PossessiveEnding(t) }, //isPossessive ending
            { "PRP", t => new PersonalPronoun(t) }, //Personal pronoun
            { "PRP$", t => new PossessivePronoun(t) }, //isPossessive pronoun
            //Adverb mappings
            { "RB", t => new Adverb(t) }, //Adverb
            { "RBR", t => new ComparativeAdverb(t) }, //Adverb, comparative
            { "RBS", t => new SuperlativeAdverb(t) }, //Adverb, superlative
            //Verb mappings
            { "VB", t => new Verb(t,VerbTense.Base) }, //Verb, base form
            { "VBD", t => new Verb(t,VerbTense.Past) }, //Verb, past tense
            { "VBG", t => new Verb(t,VerbTense.PresentParticiple) }, //Verb, gerund or present participle
            { "VBN", t => new Verb(t,VerbTense.PastParticiple) }, //Verb, past participle
            { "VBP", t => new Verb(t,VerbTense.SingularPresent) }, //Verb, non-3rd person singular present
            { "VBZ", t => new Verb(t,VerbTense.ThirdPersonSingularPresent) }, //Verb, 3rd person singular present
            //WH-word mappings
            { "WDT", t => new Determiner(t) }, //Wh-leftNPDeterminer
            { "WP", t => new RelativePronoun(t) }, //Wh-pronoun
            { "WP$", t => new RelativePossessivePronoun(t) }, //isPossessive wh-pronoun
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
                return typeDictionary;
            }
        }
        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the class which provides its run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time type associated with the textual tag.</returns>
        /// <exception cref="UnknownWordTagException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override WordCreator this[string tag] {
            get {
                try {
                    return typeDictionary[tag];
                }
                catch (KeyNotFoundException) {
                    throw new UnknownWordTagException(String.Format("The tag {0} is not defined by this Tagset", tag));
                }
            }
        }
        /// <summary>
        /// Gets the PosTag string corresponding to the runtime System.Type of the Return Type of given function which of type { System.string => LASI.Algorithm.Word }.
        /// </summary>
        /// <param name="phrase">The function which of type { System.string => LASI.Algorithm.Word } for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the runtime System.Type of the Return Type of given function which of type { System.string => LASI.Algorithm.Word }.</returns>
        public override string this[WordCreator wordCreatingFunction] {
            get {
                try {
                    return typeDictionary.First(pair => pair.Value.Method.ReturnType == wordCreatingFunction.Method.ReturnType).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedWordTypeException(string.Format("Word constructor\n{0}\nis not mapped by this Tagset.\nFunction Type: {1} => {2}",
                        wordCreatingFunction,
                        wordCreatingFunction.Method.GetParameters().Aggregate("", (s, p) => s += p.ParameterType.FullName + ", ").TrimEnd(',', ' '),
                        wordCreatingFunction.Method.ReturnType.FullName
                        )
                    );
                }
            }
        }
        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.
        /// </summary>
        /// <param name="phrase">The LASI.Algorithm.Word for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.</returns>
        public override string this[Word word] {
            get {
                try { return typeDictionary.First(phraseCreatorPosTagPair => phraseCreatorPosTagPair.Value.Method.ReturnType == word.GetType()).Key; }
                catch (InvalidOperationException) {
                    throw new UnmappedWordTypeException(string.Format("The indexing LASI.Algorithm.Word has type {0}, a type which is not mapped by {1}.", word.GetType(), this.GetType()));
                }
            }
        }
        #endregion
    }
}
