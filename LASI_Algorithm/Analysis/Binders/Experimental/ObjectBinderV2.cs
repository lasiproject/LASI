using LASI.Algorithm.Binding;
using LASI.Algorithm.Patternization;
using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis.Binders.Experimental
{
    class ObjectBinderV2
    {
        public void Bind(Sentence sentence) { Bind(sentence.Phrases); }
        public void Bind(IEnumerable<Phrase> phrases) {
            if (!phrases.GetVerbPhrases().Any()) { throw new VerblessPhrasalSequenceException(); }

            var objectBindingReleventItems =
                from phrase in phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                select
                    phrase.Match().To<ILexical>()
                        .With<IPrepositional>(p => p)
                        .With<IConjunctive>(p => p)
                        .With<IEntity>(p => p)
                        .With<IVerbal>(p => p)
                        .With<SubordinateClauseBeginPhrase>(p => p)
                        .With<SymbolPhrase>(p => p) as ILexical into result
                where result != null
                select result;
            var bindingActions = ImagineBindings(objectBindingReleventItems.SkipWhile(p => !(p is VerbPhrase)));
            Phrase last = null;
            foreach (var f in bindingActions) { last = f(); }
            if (last != null) {
                Bind(phrases.GetPhrasesAfter(last));
            }
        }

        private static IEnumerable<Func<Phrase>> ImagineBindings(IEnumerable<ILexical> elements) {
            var results = new List<Func<Phrase>>();
            var targetVPS = elements.Select(e =>
                e.Match().To<VerbPhrase>()
                    .With<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase)
                    .With<SymbolPhrase>(s =>
                        s.NextPhrase.Match().To<VerbPhrase>()
                            .With<VerbPhrase>(v => v)
                            .With<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase))
                    .With<VerbPhrase>(v => v)
                .Result())
            .Distinct()
            .TakeWhile(v => v != null);
            var next = targetVPS.LastOrDefault(v => v.NextPhrase != null && v.Sentence == v.NextPhrase.Sentence);
            if (next != null) {
                results.Add(targetVPS.Last().NextPhrase.Match().To<Func<Phrase>>()
                        .With<NounPhrase>(n => () => {
                            targetVPS.ToList().ForEach(v => v.BindDirectObject(n));
                            return n;
                        })
                        .With<InfinitivePhrase>(i => () => {
                            targetVPS.ToList().ForEach(v => v.BindDirectObject(i));
                            return i;
                        })
                        .When(i => i.NextPhrase as IEntity != null)
                        .With<PrepositionalPhrase>(i => () => {
                            targetVPS.ToList().ForEach(v => v.BindIndirectObject(i.NextPhrase as IEntity));
                            return i;
                        }));
            }
            return results;
        }


    }
}
