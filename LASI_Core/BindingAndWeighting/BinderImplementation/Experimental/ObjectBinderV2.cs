using LASI.Core.Binding;
using LASI.Core.Patternization;
using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities;
using System.Threading.Tasks;

namespace LASI.Core.Binding.Experimental
{
    class ObjectBinderV2
    {
        public void Bind(Sentence sentence) { Bind(sentence.Phrases); }
        public void Bind(IEnumerable<Phrase> phrases) {
            if (phrases.OfVerbPhrase().None()) { throw new VerblessPhrasalSequenceException(); }

            var releventElements =
                from phrase in phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                let result = phrase.Match().Yield<Phrase>()
                        ._<IPrepositional>(phrase)
                        ._<IConjunctive>(phrase)
                        ._<IEntity>(phrase)
                        ._<IVerbal>(phrase)
                        ._<SubordinateClauseBeginPhrase>(phrase)
                        ._<SymbolPhrase>(phrase)
                        .Result()

                where result != null
                select result;
            var bindingActions = ImagineBindings(releventElements.SkipWhile(p => !(p is VerbPhrase)));
            Phrase last = null;
            foreach (var f in bindingActions) { last = f(); }
            if (last != null) {
                Bind(phrases.PhrasesFollowing(last));
            }
        }

        private static IEnumerable<Func<Phrase>> ImagineBindings(IEnumerable<Phrase> elements) {
            var results = new List<Func<Phrase>>();
            var targetVPS = elements.Select(e =>
                e.Match().Yield<VerbPhrase>() 
                    ._<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase) 
                    ._<SymbolPhrase>(s =>
                        s.NextPhrase.Match().Yield<VerbPhrase>() 
                            ._<VerbPhrase>(v => v)
                            .When(n => n is VerbPhrase)
                            .Then<Phrase>(n => n.NextPhrase as VerbPhrase) 
                         .Result()) 
                    ._<VerbPhrase>(v => v)
                .Result())
                .Distinct().TakeWhile(v => v != null);
            var next = targetVPS.LastOrDefault(v => v.NextPhrase != null && v.Sentence == v.NextPhrase.Sentence);
            if (next != null) {
                results.Add(targetVPS.Last().NextPhrase.Match().Yield<Func<Phrase>>() 
                    ._<NounPhrase>(n => () => {
                        targetVPS.ToList().ForEach(v => v.BindDirectObject(n));
                        return n;
                    })
                    ._<InfinitivePhrase>(i => () => {
                        targetVPS.ToList().ForEach(v => v.BindDirectObject(i));
                        return i;
                    }) 
                    .When<Phrase>(i => i.NextPhrase is IEntity) 
                    .Then<PrepositionalPhrase>(p => () => {
                        targetVPS.ToList().ForEach(v => v.BindIndirectObject(p.NextPhrase as IEntity));
                        return p;
                    }).Result());
            }
            return results;
        }


    }
}
