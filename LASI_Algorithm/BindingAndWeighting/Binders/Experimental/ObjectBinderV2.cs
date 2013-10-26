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
                select phrase.Match().Yield<Phrase>()
                        .Case<IPrepositional>(phrase)
                        .Case<IConjunctive>(phrase)
                        .Case<IEntity>(phrase)
                        .Case<IVerbal>(phrase)
                        .Case<SubordinateClauseBeginPhrase>(phrase)
                        .Case<SymbolPhrase>(phrase)
                        .Result() into result
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
                    .Case<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase)
                    .Case<SymbolPhrase>(s =>
                        s.NextPhrase.Match().Yield<VerbPhrase>()
                            .Case<VerbPhrase>(v => v)
                            .When(n => n is VerbPhrase)
                            .Then<Phrase>(n => n.NextPhrase as VerbPhrase)
                            .Result())
                    .Case<VerbPhrase>(v => v)
                .Result()).Distinct().TakeWhile(v => v != null);
            var next = targetVPS.LastOrDefault(v => v.NextPhrase != null && v.Sentence == v.NextPhrase.Sentence);
            if (next != null) {
                results.Add(targetVPS.Last().NextPhrase.Match().Yield<Func<Phrase>>()
                        .Case<NounPhrase>(n => () => {
                            targetVPS.ToList().ForEach(v => v.BindDirectObject(n));
                            return n;
                        })
                        .Case<InfinitivePhrase>(i => () => {
                            targetVPS.ToList().ForEach(v => v.BindDirectObject(i));
                            return i;
                        })
                        .When<Phrase>(i => i.NextPhrase is IEntity)
                        .Then<PrepositionalPhrase>(p => () => {
                            targetVPS.ToList().ForEach(v => v.BindIndirectObject(p.NextPhrase as IEntity));
                            return p;
                        })
                        .Result());
            }
            return results;
        }


    }
}
