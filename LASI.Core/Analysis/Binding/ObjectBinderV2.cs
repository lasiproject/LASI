﻿using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Analysis.Binding.Experimental.SequentialPatterns;
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
            var x = from Utilities.Logger.Mode e in Enum.GetValues(typeof(Utilities.Logger.Mode)) select e;
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
                    .Case((SymbolPhrase s) => s.Next.Match()
                        .Case((VerbPhrase v) => v)
                        .Result(s.Next.Next as VerbPhrase))
                    .Case((VerbPhrase v) => v)
                .Result())
                .Distinct().TakeWhile(v => v != null)
                .ToList();
            var next = targetVerbPhrases.LastOrDefault(v => v.Next != null && v.Sentence == v.Next.Sentence);
            if (next != null)
            {
                results.Add(targetVerbPhrases.Last()
                    .Next.Match()
                    .Case((NounPhrase n) => (Func<Phrase>)(() =>
                    {
                        targetVerbPhrases.ForEach(v => v.BindDirectObject(n));
                        return n;
                    }))
                    .Case((InfinitivePhrase n) => () =>
                    {
                        targetVerbPhrases.ForEach(v => v.BindDirectObject(n));
                        return n;
                    })
                    .When((PrepositionalPhrase i) => i.Next is IEntity)
                    .Then(p => () =>
                    {
                        if (p.Next is IEntity e)
                        {
                            targetVerbPhrases.ForEach(v => v.BindIndirectObject(e));
                        }
                        return p;
                    })
                .Result());
            }
            return results;
        }

    }
}
