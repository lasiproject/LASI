using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.TermBased
{
    abstract class Token
    {
        public abstract bool AppliesTo<TCase>(TCase value);
    }
    abstract class LexicalToken<TLexical> : Token where TLexical : class, ILexical
    {
        public override bool AppliesTo<TCase>(TCase value) => value is TLexical;
    }
    abstract class ValueToken<TValue> : Token
    {
        private readonly TValue value;

        protected ValueToken(TValue value) {
            this.value = value;
        }
        public override bool AppliesTo<TCase>(TCase value) => this.value.Equals(value);
    }
}
