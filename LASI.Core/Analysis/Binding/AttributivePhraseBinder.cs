using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Binding.Experimental;

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
        public static void Bind(Sentence sentence)
        {
            foreach (var cg in FindContiguousNounPhrases(sentence.Phrases))
            {
                ProcessContiguous(cg);
            }
        }

        /// <summary>
        /// Binds the attributively related NounPhrase elements within the given sequence of Phrases.
        /// </summary>
        /// <param name="phrases">The sequence of Phrases to bind within.</param>
        public static void Bind(IEnumerable<Phrase> phrases)
        {
            foreach (var cg in FindContiguousNounPhrases(phrases))
            {
                ProcessContiguous(cg);
            }
        }

        private static void ProcessContiguous(IEnumerable<Phrase> phrases)
        {
            foreach (var prepositional in phrases.OfPrepositionalPhrase())
            {
                ProcessLinkingPrepositionalPhrase(prepositional);
            }
            while (phrases.OfNounPhrase().Count() > 1)
            {
                var npLeft = phrases.OfNounPhrase().First();
                var npRight = phrases.Skip(1).OfNounPhrase().First();
                var leftNpDeterminer = npLeft?.Words?.OfDeterminer()?.FirstOrDefault();
                var rightNpDeterminer = npRight?.Words?.OfDeterminer()?.FirstOrDefault();
                if (leftNpDeterminer != null && rightNpDeterminer != null &&
                    leftNpDeterminer.DeterminerKind == DeterminerKind.Definite &&
                    rightNpDeterminer.DeterminerKind == DeterminerKind.Indefinite)
                {
                    npLeft.InnerAttributive = npRight;
                    npRight.OuterAttributive = npLeft;
                }
                phrases = phrases.SkipWhile(n => n.Previous != npRight);
            }
        }

        private static void ProcessLinkingPrepositionalPhrase(PrepositionalPhrase prepPhrase)
        {
            if (prepPhrase.Previous != null)
            {
                prepPhrase.Previous.BindRightPrepositional(prepPhrase);
            }
            else if (prepPhrase.Next != null)
            {
                prepPhrase.Next.BindLeftPrepositional(prepPhrase);
            }
            prepPhrase.ToTheRightOf = prepPhrase.Next;
            prepPhrase.ToTheLeftOf = prepPhrase.Previous;

            prepPhrase.PrepositionRole = PrepositionRole.DiscriptiveLinker;
        }

        private static IEnumerable<IEnumerable<Phrase>> FindContiguousNounPhrases(IEnumerable<Phrase> phrases)
        {
            var results = new List<IEnumerable<Phrase>>();
            var temp = phrases;
            while (temp.Any())
            {
                bool continueTaking(Phrase p) => p.Match()
                    .Case((NounPhrase n) => true)
                    .Case((PrepositionalPhrase x) => true)
                    .Case(x => x.Words.All(w => w is Punctuator) && x.Next is NounPhrase && x.Previous is NounPhrase)
                    .Result();

                results.Add(temp.TakeWhile(continueTaking));
                temp = temp
                    .SkipWhile(n => !(n is NounPhrase))
                    .Skip(results.Last().Count())
                    .ToList();
            }
            return results.Where(result => result.Count() > 1);
        }
    }
}