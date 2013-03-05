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
            { "VP", words => new VerbPhrase(words) },
            { "NP", words => new NounPhrase(words) },
            { "PP", words => new PrepositionalPhrase(words) },
            { "ADVP", words => new AdverbPhrase(words) },
            { "ADJP", words => new AdjectivePhrase(words) },
            { "PRT", words => new ParticlePhrase(words) },
            { "CONJP", words => new ConjunctionPhrase(words) },
            { "S", words => new SimpleDeclarativePhrase(words) },
            { "SINV", words => new SimpleDeclarativePhrase(words) },
            { "SQ", words => new InterrogativePhrase(words) },
            { "SBARQ", words => new InterrogativePhrase(words) },
            { "SBAR", words => new PrepositionalPhrase(words) },
            { "LST", words => new RoughListPhrase(words) },

        };

        #endregion

        #region Properties and Indexers

        public override Func<IEnumerable<Algorithm.Word>, Algorithm.Phrase> this[string tag] {
            get {
                try {
                    return typeDictionary[tag];
                }
                catch (KeyNotFoundException) {
                    throw new UnknownPhraseTypeException(String.Format("The phrase tag {0} is not defined by this Tagset", tag));
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
