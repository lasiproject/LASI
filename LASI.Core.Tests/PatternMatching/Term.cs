//using System;
//using LASI.Core;
//using LASI.UnitTests.PatternMatching;

//abstract class Term
//{
//    protected Term(Pattern termOf) {
//        TermOf = termOf;
//    }
//    public abstract Pattern MatchWith<T>(T lexical) where T : class, ILexical;
//    public Pattern TermOf { get; }

//}
//abstract class ValueTerm<TValue> : Term
//{
//    protected ValueTerm(Pattern termOf, TValue value) : base(termOf) {
//        expr = value;
//    }
//    public TValue expr { get; }
//    public static Pattern operator |(Pattern left, ValueTerm<TValue> right) {
//        return new ValueTerm(left).MatchWith(left.Value);
//    }
//}
//class TextTerm : ValueTerm<string>
//{
//    public override Term MatchWith() => TermOf.Value.Text == expr;
//}
