using LASI.Core;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities;
using System.Threading.Tasks;
using LASI.Core.Interop;

namespace LASI.Core.Binding.Experimental
{
    internal class ObjectBinderV2
    {
        internal void Bind(Sentence sentence) {
            sentence.Match()
                  .WithContinuationMode(ContinuationMode.Recursive)
                  .IgnoreOnce<IAdverbial, IDescriptor>()
                  .BindWhen((IEntity s, IVerbal v, IEntity d) => {
                      v.BindSubject(s);
                      v.BindDirectObject(d);
                  });
            Bind(sentence.Phrases);
        }
        internal void Bind(IEnumerable<Phrase> phrases) {
            if (!phrases.OfVerbPhrase().Any()) { throw new VerblessPhrasalSequenceException(); }

            var releventElements = from phrase in phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   let result = phrase.Match().Yield<Phrase>()
                                           .With((IPrepositional p) => phrase)
                                           .With((IConjunctive p) => phrase)
                                           .With((IEntity p) => phrase)
                                           .With((IVerbal p) => phrase)
                                           .With((SubordinateClauseBeginPhrase p) => phrase)
                                           .With((SymbolPhrase p) => phrase)
                                        .Result()
                                   where result != null
                                   select result;
            var bindingActions = ImagineBindings(releventElements.SkipWhile(p => !(p is VerbPhrase)));
            Phrase last = null;
            foreach (var f in bindingActions) { last = f(); }
            if (last != null) {
                Bind(phrases.PhrasesAfter(last));
            }
        }

        private static IEnumerable<Func<Phrase>> ImagineBindings(IEnumerable<Phrase> elements) {
            var results = new List<Func<Phrase>>();
            var targetVerbPhrases = elements.Select(e =>
                e.Match().Yield<VerbPhrase>()
                    .With((ConjunctionPhrase c) => c.NextPhrase as VerbPhrase)
                    .With((SymbolPhrase s) =>
                        s.NextPhrase.Match().Yield<VerbPhrase>()
                            .With((VerbPhrase v) => v)
                        .Result(s.NextPhrase.NextPhrase as VerbPhrase))
                    .With((VerbPhrase v) => v)
                .Result())
                .Distinct().TakeWhile(v => v != null);
            var next = targetVerbPhrases.LastOrDefault(v => v.NextPhrase != null && v.Sentence == v.NextPhrase.Sentence);
            if (next != null) {
                results.Add(targetVerbPhrases.Last().NextPhrase.Match().Yield<Func<Phrase>>()
                    .With((NounPhrase n) => () => {
                        targetVerbPhrases.ToList().ForEach(v => v.BindDirectObject(n));
                        return n;
                    })
                    .With((InfinitivePhrase i) => () => {
                        targetVerbPhrases.ToList().ForEach(v => v.BindDirectObject(i));
                        return i;
                    })
                    .When(i => i.NextPhrase is IEntity)
                    .Then((PrepositionalPhrase p) => () => {
                        targetVerbPhrases.ToList().ForEach(v => v.BindIndirectObject(p.NextPhrase as IEntity));
                        return p;
                    }).Result());
            }
            return results;
        }


    }
}
