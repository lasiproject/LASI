using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Utilities.PatternMatching;
using LASI.Utilities.AlgebraicDecomposition;
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
                from phrase in phrases
                select Match
                    .From<ILexical>(phrase).To<ILexical>()
                        .With<IPrepositional>(p => p)
                        .With<IConjunctive>(p => p)
                        .With<IEntity>(p => p)
                        .With<IVerbal>(p => p)
                        .With<SubordinateClauseBeginPhrase>(p => p)
                        .With<SymbolPhrase>(p => p)
                        .Result as Phrase into result
                where result != null
                select result;
            var bindingActions = ImagineBindings(objectBindingReleventItems.SkipWhile(p => !(p is VerbPhrase)));
        }

        private static IEnumerable<Action> ImagineBindings(IEnumerable<Phrase> elements) {
            List<Action> results = new List<Action>();
            var targetVPS = elements
                .Select(e => Match
                    .From<Phrase>(e).To<VerbPhrase>()
                        .With<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase)
                        .With<SymbolPhrase>(s => Match
                            .From(s.NextPhrase).To<VerbPhrase>()
                            .With<VerbPhrase>(v => v)
                            .With<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase)
                            .Result)
                        .With<VerbPhrase>(v => v)
                        .Result)
                .Distinct()
                .TakeWhile(v => v != null);
            var next = targetVPS.LastOrDefault(v => v.NextPhrase != null && v.Sentence == v.NextPhrase.Sentence);
            if (next != null) {
                results.Add(BindObjects(targetVPS, targetVPS.Last().NextPhrase));
            }
            return results;
        }

        private static Action BindObjects(IEnumerable<IVerbal> targets, Phrase next) {
            var result = Match
                    .From<Phrase>(next).To<Action>()
                        .With<NounPhrase>(n => () => targets.ToList().ForEach(v => v.BindDirectObject(n)))
                        .With<InfinitivePhrase>(i => () => targets.ToList().ForEach(v => v.BindDirectObject(i)))
                        .Result;
            return result;
        }
    }
}
