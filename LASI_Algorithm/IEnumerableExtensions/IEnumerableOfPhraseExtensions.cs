using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfPhraseExtensions
    {
        public static IEnumerable<NounPhrase> GetNounPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<NounPhrase>();
        }
        public static IEnumerable<VerbPhrase> GetVerbPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<VerbPhrase>();
        }
        public static IEnumerable<PronounPhrase> GetPronounPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<PronounPhrase>();
        }
        public static IEnumerable<PrepositionalPhrase> GetPrepositionalPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<PrepositionalPhrase>();
        }
        public static IEnumerable<AdverbPhrase> GetAdverbPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdverbPhrase>();
        }
        public static IEnumerable<ConjunctionPhrase> GetConjunctionPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<ConjunctionPhrase>();
        }
        public static IEnumerable<RoughListPhrase> GetListPhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<RoughListPhrase>();
        }
        public static IEnumerable<AdjectivePhrase> GetAdjectivePhrases(this IEnumerable<Phrase> phrases) {
            return phrases.OfType<AdjectivePhrase>();
        }
    }
}