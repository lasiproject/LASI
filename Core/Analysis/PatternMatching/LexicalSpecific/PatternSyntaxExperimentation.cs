using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Matcher;

namespace LASI.Core.Analysis.PatternMatching
{
    public delegate Pattern<TResult> match<TResult>();
    static class PatternSyntaxExperimentation<TLexical> where TLexical : class, ILexical
    {
        public static void Test(TLexical element) {
            //var pattern = new Pattern<double>() {
            //    (IReferencer r) => r.RefersTo.Weight,
            //    (IEntity e) => e.Weight,
            //    (IVerbal v) => v.Subjects.Average(s=>s.Weight),
            //};
            //var result = element > pattern;
        }
        public static void Test1(TLexical element) {
            var p = from i in Enumerable.Range(0, 10)
                    select element.Match().Yield<double?>()
                            | ((IReferencer r) => r.RefersTo.Weight)
                            | ((IEntity e) => e.Weight)
                            | ((IVerbal v) => v.Subjects.Average(s => s.Weight))
                            | (() => null);


        }
    }
}
