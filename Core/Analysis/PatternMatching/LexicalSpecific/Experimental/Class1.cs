using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental
{
    using System.Collections;
    static class Class1
    {
        static void Test(ILexical l) {
            var x = l | new Match<float>
            {
                Referencer = delegate { return 1; },
                Entity = e => 0f,
                Verbal = v => (float)v.Weight,
                Default = delegate { return float.Parse("1"); }
            };
        }
    }


    internal class Match<TResult>
    {

        public delegate TResult TypePattern<in TPattern>(TPattern l) where TPattern : ILexical;

        public static TResult operator |(ILexical lexical, Match<TResult> match) {
            return match.Result;
        }
        public TypePattern<NounPhrase> NounPhrase { private get; set; }
        public TypePattern<CommonNoun> CommonNoun { private get; set; }
        public TypePattern<IReferencer> Referencer { private get; set; }
        public TypePattern<IEntity> Entity { private get; set; }
        public TypePattern<IVerbal> Verbal { private get; set; }
        public TypePattern<IAdverbial> Adverbial { private get; set; }
        public TypePattern<IDescriptor> Descriptor { private get; set; }
        public TypePattern<IConjunctive> Conjunctive { private get; set; }
        public TypePattern<IPrepositional> Prepositional { private get; set; }
        public TypePattern<ISubordinator> Subordinator { private get; set; }
        public TypePattern<ISimpleGendered> SimpleGendered { private get; set; }
        public TypePattern<IDeterminable> Determinable { private get; set; }
        public TypePattern<IPossesser> Possesser { private get; set; }
        public TypePattern<IPossessable> Possessable { private get; set; }
        public TypePattern<IAdverbialModifiable> AdverbialModifiable { private get; set; }
        public TypePattern<IAggregateEntity> AggregateEntity { private get; set; }
        public TypePattern<IAggregateVerbal> AggregateVerbal { private get; set; }

        public Func<ILexical, TResult> Default { private get; set; }

        public TResult Result { get; private set; }


    }


}
