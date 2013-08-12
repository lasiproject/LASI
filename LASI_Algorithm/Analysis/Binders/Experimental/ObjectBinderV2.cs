using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Utilities.TypedSwitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis.Binders.Experimental
{
    class ObjectBinderV2
    {
        public void Bind(Sentence sentence) {

            if (!sentence.Phrases.GetVerbPhrases().Any()) { throw new VerblessPhrasalSequenceException(); }

            var objectBindingReleventItems =
                from phrase in sentence.Phrases
                select Match.From<ILexical>(phrase).To<ILexical>()
                    .With<IPrepositional>(p => p)
                    .With<IConjunctive>(p => p)
                    .With<IEntity>(p => p)
                    .With<IVerbal>(p => p)
                    .With<SubordinateClauseBeginPhrase>(p => p)
                    .With<SymbolPhrase>(p => p)
                    .Result as Phrase into result
                where result != null
                select result;

            var bindingActions = ImagineBind(objectBindingReleventItems.SkipWhile(p => !(p is VerbPhrase)));
        }

        private static IEnumerable<Action> ImagineBind(IEnumerable<Phrase> elements) {
            List<Action> results = new List<Action>();
            var targetVPS = new List<VerbPhrase> { elements.First() as VerbPhrase };

            VerbPhrase verbal = null;
            for (var next = elements.First().NextPhrase; next != null; next = next.NextPhrase) {
                verbal = Match.From<Phrase>(next).To<VerbPhrase>()
                         .With<ConjunctionPhrase>(c => c.NextPhrase as VerbPhrase)
                         .With<SymbolPhrase>(s => s.NextPhrase as VerbPhrase)
                         .With<VerbPhrase>(v => v)
                         .Result;
            }
            if (verbal != null)
                results.Add(BindObjects(targetVPS, verbal.NextPhrase));
            return results;

        }

        private static Action BindObjects(List<VerbPhrase> targetVPS, Phrase next) {
            var result = Match.From<Phrase>(next).To<Action>()
                     .With<NounPhrase>((n => () => targetVPS.ForEach(v => v.BindDirectObject(n))))
                     .With<InfinitivePhrase>((i => () => targetVPS.ForEach(v => v.BindDirectObject(i))))
                     .Result;
            return result;
        }
    }
}
