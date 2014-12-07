//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using LASI.Core;

//namespace LASI.UnitTests.PatternMatching
//{

//    public abstract class Pattern
//    {
//        protected Pattern(ILexical value) { Value = value; }
//        public ILexical Value { get; }
//        public bool Matched { get; protected set; }
//        //protected Func<TCase> CapturedDeferredApplicator { get; set; }
//    }
//    public abstract class Pattern<TValue, TCase> : Pattern where TCase : class, ILexical
//    {

//    }


//    //public class TextExpr : Pattern<string>
//    //{
//    //    public static TextExpr Text(string pattern) { return new TextExpr(pattern); }

//    //    protected TextExpr(, string expr) : base( {

//    //    }
//    //    public static Pattern<string> operator |(TextExpr p, Func<string, ILexical, dynamic> f) { return f(p.Value,  }
//    //}
//    public class Verbal<TResult> : Pattern<TResult, IVerbal>
//    {

//    }
//}

