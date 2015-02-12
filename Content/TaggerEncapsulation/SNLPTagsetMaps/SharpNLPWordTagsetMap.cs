using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Heuristics;

namespace LASI.Content.TaggerEncapsulation
{
    using WordCreator = Func<string, Word>;
    /// <summary>
    /// Represents a Word Level tagset-to-runtime-type-mapping context which translates between The SharpNLP Tagger's tagset and the classes whose instances provide 
    /// the runtime representations of the tag. 
    /// This class represents the tagset => runtime-type mapping for
    /// the tagset used by SharpNLP, a derrivative of the Penn Tagset.
    /// This class is sealed and thus may not be extended.
    /// If a new tagset is to be implemented, extend the base class, TaggingContext.
    /// </summary>    
    /// <example>
    /// <code>
    /// var wordTagsetMap = new SharpNLPWordTagsetMap();
    /// var createWord = wordMap["TAG"];
    /// var word = createWord(wordText);
    /// </code>
    /// </example>    
    /// <see cref="WordTagsetMap"/>
    /// <see cref="WordFactory"/> 
    sealed class SharpNLPWordTagsetMap : WordTagsetMap
    {
        private static readonly IReadOnlyDictionary<string, WordCreator> map = new Dictionary<string, WordCreator>
        {
            //Punctation Mappings
            ["."] = t => SentenceEnding.Period, //. sentence ending
            ["!"] = t => SentenceEnding.ExclamationPoint, //! sentence ending
            ["?"] = t => SentenceEnding.QuestionMark, //? sentence ending
            [","] = t => new Punctuator(t),//Comma punctuation
            [";"] = t => new Punctuator(t), //Semicolon punctuation
            [":"] = t => new Punctuator(t), //Colon punctuation 
            ["``"] = t => new SingleQuote(), //Single quote * should be remapped
            ["''"] = t => new DoubleQuote(), //Double Quotation Mark punctuation
            ["LS"] = t => new Punctuator(t), //List item marker
            ["-LRB-"] = t => new Punctuator(t), //Left Brackets
            ["-RRB-"] = t => new Punctuator(t),  //Right Bracket
            //Determiner mappings
            ["CD"] = t => new Quantifier(t),//Cardinal number
            ["DT"] = t => new Determiner(t),//Determiner
            ["EX"] = t => new Existential(t), //Existential 'there'
            ["FW"] = t => new ForeignWord(t), //Foreign word
            ["IN"] = t => new Preposition(t), //Preposition or subordinating conjunction
            ["CC"] = t => new Conjunction(t), //Coordinating conjunction
            //Adjective mappings
            ["JJ"] = t => new Adjective(t), //Adjective
            ["JJR"] = t => new ComparativeAdjective(t), //Adjective, comparative
            ["JJS"] = t => new SuperlativeAdjective(t), //Adjective, superlative
            ["MD"] = t => new ModalAuxilary(t), //ModalAuxilary
            //Noun mappings
            ["NN"] = t => new CommonSingularNoun(t), //Noun, singular or mass
            ["NNS"] = t => new CommonPluralNoun(t), //Noun, plural
            ["NNP"] = t => Lexicon.IsCommon(t) ? new CommonSingularNoun(t) : new ProperSingularNoun(t) as Noun, //Proper noun, singular
            ["NNPS"] = t => Lexicon.IsCommon(t) ? new CommonPluralNoun(t) : new ProperPluralNoun(t) as Noun, //Proper noun, plural
            //Pronoun mappings
            ["PDT"] = t => new PreDeterminer(t), //Predeterminer
            ["POS"] = t => new PossessiveEnding(t), //Possessive ending
            ["PRP"] = t => new PersonalPronoun(t), //Personal pronoun
            ["PRP$"] = t => new PossessivePronoun(t), //Possessive pronoun
            //Adverb mappings
            ["RB"] = t => new Adverb(t), //Adverb
            ["RBR"] = t => new ComparativeAdverb(t), //Adverb }, comparative
            ["RBS"] = t => new SuperlativeAdverb(t), //Adverb }, superlative
            //Verb mappings
            ["VB"] = t => new BaseVerb(t), //Verb }, base form
            ["VBD"] = t => new PastTenseVerb(t), //Verb }, past tense
            ["VBG"] = t => new PresentParticiple(t), //Verb }, gerund or present participle
            ["VBN"] = t => new PastParticiple(t), //Verb }, past participle
            ["VBP"] = t => new SingularPresentVerb(t), //Verb }, non-3rd person singular present
            ["VBZ"] = t => new ThirdPersonSingularPresentVerb(t), //Verb, 3rd person singular present
            //WH-word mappings
            ["WDT"] = t => new Determiner(t), //Wh-Determiner
            ["WP"] = t => new RelativePronoun(t), //Wh-Pronoun
            ["WP$"] = t => new RelativePossessivePronoun(t), //Possessive wh-pronoun
            ["WRB"] = t => new Adverb(t), //Wh-word
            //Additional mappings
            ["RP"] = t => new Particle(t), //Particle
            ["SYM"] = t => new Symbol(t), //Symbol
            ["TO"] = t => new ToLinker(t), //'To'
            ["UH"] = t => new Interjection(t), //Interjection
            //Empty POS Tag, resulting function will throw EmptyTagException on invocation.
            [""] = t => { throw new EmptyWordTagException(t); }
        };

        /// <summary>
        /// Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the class which provides its run-time representation.
        /// </summary>
        /// <param name="posTag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>A function which creates an instance of the run-time type associated with the textual tag.</returns>
        /// <exception cref="UnknownWordTagException">Thrown when the indexing tag string is not defined by the tagset.</exception>
        public override WordCreator this[string posTag]
        {
            get
            {
                try { return map[posTag]; }
                catch (KeyNotFoundException)
                {
                    throw new UnknownWordTagException(posTag);
                }
            }
        }

        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given <see cref="Word"/>.
        /// </summary>
        /// <param name="word">The <see cref="Word"/> for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given <see cref="Word"/>.</returns>
        public override string this[Word word]
        {
            get
            {
                try
                {
                    return map.First(funcPosTagPair => funcPosTagPair.Value.Method.ReturnType == word.GetType()).Key;
                }
                catch (InvalidOperationException)
                {
                    throw new UnmappedWordTypeException(string.Format("The indexing {0} has type {1}, a type which is not mapped by {2}.",
                        typeof(Word),
                        word.GetType(),
                        this.GetType()
                        )
                    );
                }
            }
        }
    }
}
