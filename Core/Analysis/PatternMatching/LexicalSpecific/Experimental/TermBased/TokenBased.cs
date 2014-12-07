using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.TermBased
{
    abstract class Token<TValue>
    {
        public abstract bool AppliesTo<TCase>(TCase value);
    }
    abstract class LexicalToken<TLexical> : Token<TLexical> where TLexical : class, ILexical
    {
        public override bool AppliesTo<TCase>(TCase value) => value is TLexical
    }
}
