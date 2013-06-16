
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
namespace LASI.FileSystem
{
    /// <summary>
    /// Represents a tagset-to-runtime-PointerKind-mapping context which translates between The SharpNLP Tagger'subject tagset and the classes whose instances provide 
    /// the runtime representations of the tag. 
    /// This class represents the tagset => runtime-PointerKind mapping for
    /// the tagset used by SharpNLP, a derrivative of the Penn Tagset.
    /// This class is sealed and thus may not be extended.
    /// If a new tagset is to be implemented, extend the base class, TaggingContext.
    /// <see cref="WordTagsetMap"/>
    ///<see cref="WordMapper"/>
    /// <example><code>
    /// var constructorFunction = myContext["TAG"];
    /// var runtimeWord = constructorFunction(itemText);
    /// </code>
    /// </example>
    /// </summary>
    public sealed class SharpNLPWordTagsetMap : WordTagsetMap
    {
        #region Fields

        private readonly Dictionary<string, Func<string, Word>> typeDictionary = new Dictionary<string, Func<string, Word>> {
            { "", t => { throw new EmptyTagException(String.Format("the tag for word: {0}\nis empty",t)); } },  
            { "CC", t => new Conjunction(t) }, //Coordinating conjunction
            { ",", t => new Conjunction(t) }, //Coordinating conjunction
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
            //WH-adverb mappings
            { "WDT", t => new Determiner(t) }, //Wh-leftNPDeterminer
            { "WP", t => new RelativePronoun(t) }, //Wh-pronoun
            { "WP$", t => new RelativePossessivePronoun(t) }, //isPossessive wh-pronoun
            { "WRB", t => new Adverb(t) }, //Wh-adverb
            //Additional mappings
            { "RP", t => new Particle(t) }, //Particle
            { "SYM", t => new Symbol(t) }, //Symbol
            { "TO", t=> new ToLinker() }, //'To'
            { "UH", t => new Interjection(t) }, //Interjection
        };

        #endregion

        #region Properties and Indexers
        /// <summary>
        /// Gets the Read Only Dictionary which represents the mapping between Part Of Speech tags and the cunstructors which instantiate their run-time representations.
        /// </summary>
        public override IReadOnlyDictionary<string, Func<string, Word>> TypeDictionary {
            get {
                return typeDictionary;
            }
        }

        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the class which provides its run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time PointerKind associated with the textual tag.</returns>
        /// <exception cref="UnknownPOSException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override Func<string, Word> this[string tag] {
            get {
                try {
                    return typeDictionary[tag];
                }
                catch (KeyNotFoundException) {
                    throw new UnknownPOSException(String.Format("The tag {0} is not defined by this Tagset", tag));
                }
            }
        }

        public override string this[Func<string, Word> mappedConstructor] {
            get {
                try {
                    return typeDictionary.First(pair => pair.Value == mappedConstructor).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedWordConstructorException(String.Format("Word constructor\n{0}\nis not mapped by this Tagset for", mappedConstructor));
                }
            }
        }
        #endregion
    }
}
