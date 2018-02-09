using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics.PatternMatching.LexicalSpecific.Experimental.Termification
{
    static class Tests
    {
        static void Test1<TLexical>(TLexical lexical) where TLexical : ILexical
        {
            var pattern = new Pattern<double>(lexical);
            var textual = new TextualTerm<double>("cat");
            var typeTerm = new TypeTerm<IEntity>();
            var boolTerm = new BooleanTerm(true);
            //var metaTerm = textual & boolTerm & typeTerm;
            var appliedWhen = pattern.ApplyWhen(textual, x => x.Weight);
            var result = appliedWhen.Result;
        }
    }
}
