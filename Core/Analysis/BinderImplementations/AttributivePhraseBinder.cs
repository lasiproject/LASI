using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Binding
{
    /// <summary>
    /// Binds the attributively related NounPhrase elements within a source together.
    /// </summary>
    static class AttributivePhraseBinder
    {
        /// <summary>
        /// Binds the attributively related NounPhrase elements within the given sentence.
        /// </summary>
        /// <param name="sentence">The sentence to bind within.</param>
        public static void Bind(Sentence sentence) {
            foreach (var cg in FindContiguousNounPhrases(sentence.Phrases)) {
                ProcessContiguous(cg);
            }
        }
        /// <summary>
        /// Binds the attributively related NounPhrase elements within the given sequence of Phrases.
        /// </summary>
        /// <param name="phrases">The sequence of Phrases to bind within.</param>
        public static void Bind(IEnumerable<Phrase> phrases) {
            foreach (var cg in FindContiguousNounPhrases(phrases)) {
                ProcessContiguous(cg);
            }
        }

        private static void ProcessContiguous(IEnumerable<Phrase> phrases) {
            foreach (var prepPhrase in phrases.OfPrepositionalPhrase()) {
                ProcessLinkingPrepositionalPhrase(prepPhrase);
            }
            while (phrases.Count(n => n is NounPhrase) > 1) {
                var npLeft = phrases.First(n => n is NounPhrase) as NounPhrase;
                var npRight = phrases.Skip(1).First(n => n is NounPhrase) as NounPhrase;
                var leftNPDeterminer = npLeft != null ? npLeft.Words.OfDeterminer().FirstOrDefault() : null;
                var rightNpDeterminer = npRight != null ? npLeft.Words.OfDeterminer().FirstOrDefault() : null;
                if ((leftNPDeterminer != null && rightNpDeterminer != null) && leftNPDeterminer.DeterminerKind == DeterminerKind.Definite && rightNpDeterminer.DeterminerKind == DeterminerKind.Indefinite) {
                    npLeft.InnerAttributive = npRight;
                    npRight.OuterAttributive = npLeft;
                }
                phrases = phrases.SkipWhile(n => n.PreviousPhrase != npRight);
            }
        }

        private static void ProcessLinkingPrepositionalPhrase(PrepositionalPhrase prepPhrase) {
            if (prepPhrase.PreviousPhrase != null) {
                prepPhrase.PreviousPhrase.PrepositionOnRight = prepPhrase;
            }
            if (prepPhrase.NextPhrase != null) {
                prepPhrase.NextPhrase.PrepositionOnLeft = prepPhrase;
            }
            prepPhrase.ToTheRightOf = prepPhrase.NextPhrase;
            prepPhrase.ToTheLeftOf = prepPhrase.PreviousPhrase;

            prepPhrase.Role = PrepositionRole.DiscriptiveLinker;
        }

        private static IEnumerable<IEnumerable<Phrase>> FindContiguousNounPhrases(IEnumerable<Phrase> phrases) {
            var result = new List<IEnumerable<Phrase>>();
            var temp = phrases;
            while (temp.Any()) {

                result.Add(temp.TakeWhile(n => n is NounPhrase || 
                    ((n is PrepositionalPhrase ||
                        n.Words.Count(w => w is Punctuator) == n.Words.Count()) &&
                        n.NextPhrase is NounPhrase &&
                        n.PreviousPhrase is NounPhrase)));
                temp = temp.SkipWhile(n => !(n is NounPhrase)).Skip(result.Last().Count()).ToList();
            }
            return from r in result
                   where r.Count() > 1
                   select r;
        }

    }

}