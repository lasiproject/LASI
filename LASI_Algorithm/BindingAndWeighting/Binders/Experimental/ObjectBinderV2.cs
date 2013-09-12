using LASI.Algorithm.Binding;
using LASI.Algorithm.Patternization;
using LASI.Algorithm.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities.FunctionExtensions;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis.Binders.Experimental
{
    class ObjectBinderV2
    {
        public void Bind(Sentence sentence) { Bind(sentence.Phrases); }
        public void Bind(IEnumerable<Phrase> phrases) {
            if (phrases.GetVerbPhrases().None()) { throw new VerblessPhrasalSequenceException(); }

            var releventElements =
                from phrase in phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                select phrase.Match().Yield<ILexical>()
                        .Case<IPrepositional>(p => p)
                        .Case<IConjunctive>(p => p)
                        .Case<IEntity>(p => p)
                        .Case<IVerbal>(p => p)
                        .Case<SubordinateClauseBeginPhrase>(p => p)
                        .Case<SymbolPhrase>(p => p)
                        .Result() as Phrase into result
                where result != null
                select result;
            var bindingActions = ImagineBindings(releventElements.SkipWhile(p => !(p is VerbPhrase)));
            Phrase last = null;
            foreach (var f in bindingActions) { last = f(); }
            if (last != null) {
                Bind(phrases.GetPhrasesAfter(last));
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
                            .When<ConjunctionPhrase>(c => c.NextPhrase is VerbPhrase)
                            .Then<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase)
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
