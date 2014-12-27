using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    class WhenPredicate<T> : Predicate<T> where T : class, ILexical
    {
        private readonly Func<T, bool> condition;
        public WhenPredicate(Func<bool> conditon) {
            this.condition = t => conditon();
        }
        public WhenPredicate(bool conditon) {
            this.condition = t => conditon;
        }
        public WhenPredicate(Func<T, bool> conditon) {
            this.condition = conditon;
        }
        protected override Func<bool> ToFunc(ILexical element) => () => Satifies(element);
        public override bool Satifies<TLexical>(TLexical element) => element is T && condition(element as T);
    }
}
