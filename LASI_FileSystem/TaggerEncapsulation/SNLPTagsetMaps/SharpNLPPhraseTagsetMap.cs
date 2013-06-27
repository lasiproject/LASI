using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;


namespace LASI.FileSystem.TaggerEncapsulation
{
    public sealed class SharpNLPPhraseTagsetMap : PhraseTagsetMap
    {
        #region Fields
        private readonly Dictionary<string, Func<IEnumerable<Word>, Phrase>> typeDictionary = new Dictionary<string, Func<IEnumerable<Word>, Phrase>> {
            { "VP", words => words.Any(w=> w is Punctuation) ? 
                new PunctuatorPhrase(words) as Phrase:
                new VerbPhrase(words) as Phrase
                },
            { "NP", words => 
                words.GetPronouns().Count()!=words.Count()?
                new NounPhrase(words):
                new PronounPhrase(words) },
            { "PP", words => new PrepositionalPhrase(words) },
            { "ADVP", words => new AdverbPhrase(words) },
            { "ADJP", words => new AdjectivePhrase(words) },
            { "PRT", words => new ParticlePhrase(words) },
            { "CONJP", words => new ConjunctionPhrase(words) },
            { "S", words => new SimpleDeclarativePhrase(words) },
            { "SINV", words => new SimpleDeclarativePhrase(words) },
            { "SQ", words => new InterrogativePhrase(words) },
            { "SBARQ", words => new InterrogativePhrase(words) },
            { "SBAR", words => new SubordinateClauseBeginPhrase(words) },
            { "LST", words => new RoughListPhrase(words) },

        };

        #endregion

        #region Properties and Indexers

        public override Func<IEnumerable<Algorithm.Word>, Algorithm.Phrase> this[string tag] {
            get {
                try {
                    try {
                        return typeDictionary[tag];
                    }
                    catch (KeyNotFoundException) {
                        throw new UnknownPhraseTypeException(String.Format("The phrase tag {0} is not defined by this Tagset", tag));
                    }
                }
                catch (UnknownPhraseTypeException) {
                    return (w => new UndeterminedPhrase(w));
                }
            }
        }


        public override string this[Func<IEnumerable<Algorithm.Word>, Algorithm.Phrase> mappedConstructor] {
            get {
                try {
                    return typeDictionary.First(pair => pair.Value == mappedConstructor).Key;
                }
                catch (InvalidOperationException) {
                    throw new UnmappedPhraseConstructorException(String.Format("Phrase constructor\n{0}\nis not mapped by this Tagset for", mappedConstructor));
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
