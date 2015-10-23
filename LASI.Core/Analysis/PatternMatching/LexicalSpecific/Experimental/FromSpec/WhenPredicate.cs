using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    class WhenPredicate : Predicate<ILexical>
    {
        private readonly Lazy<bool> condition;

        public WhenPredicate(bool condition) { this.condition = new Lazy<bool>(() => condition); }
        public WhenPredicate(Func<bool> condition) { this.condition = new Lazy<bool>(() => condition()); }
        public override bool Satifies<TLexical>(TLexical element) => condition.Value;
        protected override Func<bool> ToFunc(ILexical element) => () => condition.Value;
    }
}
