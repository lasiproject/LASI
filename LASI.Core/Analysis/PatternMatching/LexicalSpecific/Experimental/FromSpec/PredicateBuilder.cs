using System;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    static class PredicateBuilder
    {
        public static ExactTextPredicate<ILexical> Text(string text)
            => new ExactTextPredicate<ILexical>(text);
        public static CaseTypePredicate<T, ILexical> Case<T>() where T : class, ILexical
            => new CaseTypePredicate<T, ILexical>();
        public static WhenPredicate When(bool condition) => new WhenPredicate(condition);
        //public static WhenPredicate When(Func<bool> condition) => new WhenPredicate(condition);
        public static WhenPredicate<ILexical> When(Func<ILexical, bool> condition) => new WhenPredicate<ILexical>(condition);

        public static WhenPredicate<T> When<T>(Func<T, bool> condition) where T : class, ILexical
            => new WhenPredicate<T>(condition);
        public static CaseTypePredicate<IVerbal, ILexical> Verbal => new CaseTypePredicate<IVerbal, ILexical>();
        static void Test(ILexical value)
        {
            var pred = Verbal.Combine(When(true)).Combine(Text("dogged")).Combine(When(true));
            var prec = Verbal
                & When(true)
                & "dogged"
                & When(x => x.Text == "true")
                & "abc"
                & When(x => true);
            var applicator = new Applicator();
            var applicator2 = new Applicator<IVerbal, IEntity>();

            var res = applicator.Apply(pred, value, v => v);
            var rec = applicator.Apply(prec, value, v => v.Subjects);

            //var red = prec | applicator2 | (v => v.AggregateDirectObject);

            var ree = applicator2.SetTarget(value)
                | Verbal & When(value.Text == "true") & Text("")
                | (v => v.AggregateDirectObject)
                | Verbal & Text("dogged")
                | (x => x.AggregateDirectObject);
        }
    }
}
