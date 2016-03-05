using LASI.Core;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Configuration;

namespace LASI.Core.Analysis.Binding
{
    internal class ObjectBinderV2
    {
        internal void Bind(Sentence sentence)
        {
            sentence.Match()
                  .WithContinuationMode(ContinuationMode.Recursive)
                  .IgnoreOnce<IAdverbial, IDescriptor>()
                  .Case((IEntity s, IVerbal v, IEntity d) =>
                  {
                      v.BindSubject(s);
                      v.BindDirectObject(d);
                  })
                  .Case((IEntity subject,
                        IVerbal verb,
                        IEntity direct,
                        IPrepositional prepToDirectObject,
                        IEntity indirect) =>
                    {
                        verb.BindSubject(subject);
                        verb.BindDirectObject(direct);
                        verb.BindIndirectObject(indirect);
                    });
            Bind(sentence.Phrases);
        }
        internal void Bind(IEnumerable<Phrase> phrases)
        {
            if (!phrases.OfVerbPhrase().Any())
            {
                throw new VerblessPhrasalSequenceException(phrases);
            }

            var releventElements = from phrase in phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   let result = phrase.Match()
                                           .Case((IPrepositional p) => phrase)
                                           .Case((IConjunctive p) => phrase)
                                           .Case((IEntity p) => phrase)
                                           .Case((IVerbal p) => phrase)
                                           .Case((SubordinateClauseBeginPhrase p) => phrase)
                                           .Case((SymbolPhrase p) => phrase)
                                        .Result()
                                   where result != null
                                   select result;
            var bindingActions = ImagineBindings(releventElements.SkipWhile(p => !(p is VerbPhrase)));
            Phrase last = null;
            foreach (var action in bindingActions)
            {
                last = action();
            }
            if (last != null)
            {
                Bind(phrases.PhrasesAfter(last));
            }
        }

        private static IEnumerable<Func<Phrase>> ImagineBindings(IEnumerable<Phrase> elements)
        {
            var results = new List<Func<Phrase>>();
            var targetVerbPhrases = elements.Select(e => e.Match()
                    .Case((ConjunctionPhrase c) => c.Next as VerbPhrase)
                    .Case((SymbolPhrase s) => s.NextPhrase.Match()
                            .Case((VerbPhrase v) => v).Result(s.NextPhrase.NextPhrase as VerbPhrase))
                    .Case((VerbPhrase v) => v)
                .Result())
                .Distinct().TakeWhile(v => v != null);
            var next = targetVerbPhrases.LastOrDefault(v => v.NextPhrase != null && v.Sentence == v.NextPhrase.Sentence);
            if (next != null)
            {
                results.Add(targetVerbPhrases.Last()
                    .NextPhrase.Match().Yield<Func<Phrase>>()
                    .Case((NounPhrase n) => () =>
                    {
                        targetVerbPhrases.ToList().ForEach(v => v.BindDirectObject(n));
                        return n;
                    })
                    .Case((InfinitivePhrase i) => () =>
                    {
                        targetVerbPhrases.ToList().ForEach(v => v.BindDirectObject(i));
                        return i;
                    })
                    .When(i => i.NextPhrase is IEntity)
                    .Then((PrepositionalPhrase p) => () =>
                    {
                        targetVerbPhrases.ToList().ForEach(v => v.BindIndirectObject(p.NextPhrase as IEntity));
                        return p;
                    })
                .Result());
            }
            return results;
        }


    }
}
