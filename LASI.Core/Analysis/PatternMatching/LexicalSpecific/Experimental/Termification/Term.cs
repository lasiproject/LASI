using System;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.Termification
{
    internal class AppliedTerm<T> where T : ILexical
    {
        public AppliedTerm(MetaTerm<T> applied)
        {
            this.applied = applied;
        }

        private Pattern<TResult> Invoke<TResult>(Pattern<TResult> pattern, Func<T, TResult> f) => pattern.ApplyWhen(applied, f);

        private MetaTerm<T> applied;
    }

    internal class BooleanTerm : Term
    {
        public BooleanTerm(bool boolean)
        {
            Boolean = boolean;
        }

        public override bool Test(ILexical lexical) => Boolean;

        public override TermKind Kind => TermKind.Boolean;

        protected bool Boolean { get; }
    }

    internal class MetaTerm : Term
    {
        public MetaTerm(Func<ILexical, bool> test)
        {
            this.test = test;
        }

        public override bool Test(ILexical lexical) => test(lexical);

        public override TermKind Kind => TermKind.Meta;

        private readonly Func<ILexical, bool> test;
    }

    internal class MetaTerm<T> : MetaTerm where T : ILexical
    {
        public MetaTerm(Func<T, bool> test) : base(lexical => lexical is T && test((T)lexical))
        {
        }

        public static MetaTerm<T> operator &(MetaTerm<T> left, TypeTerm<T> right) => new MetaTerm<T>(lexical => left.Test(lexical) && right.Test(lexical));

        protected override Term Combine(Term other) => new MetaTerm<T>(lexical => lexical is T && other.Test(lexical));
    }

    internal class Pattern<TResult>
    {
        public Pattern(ILexical lexical)
        {
            Lexical = lexical;
        }

        public static TermWithResultType<TResult> operator |(Pattern<TResult> pattern, Term term) => new TermWithResultType<TResult>(term);

        public Pattern<TResult> ApplyIfApplicable<TTerm, T>(TTerm predicate, Func<T, TResult> f) where TTerm : Term where T : ILexical
        {
            Accepted = predicate.Test(Lexical);
            if (Accepted)
            {
                Result = f((T)Lexical);
            }
            return this;
        }

        public Pattern<TResult> ApplyWhen<TTerm, TTypePattern>(TTerm predicate, Func<TTypePattern, TResult> f) where TTerm : Term where TTypePattern : ILexical =>
            ApplyIfApplicable(predicate, f);

        public Pattern<TResult> ApplyWhen(Term predicate, Func<ILexical, TResult> f) => ApplyIfApplicable(predicate, f);

        public Pattern<TResult> ApplyWhen<TTypePattern>(MetaTerm<TTypePattern> predicate, Func<TTypePattern, TResult> f) where TTypePattern : ILexical =>
            ApplyIfApplicable(predicate, f);

        public ILexical Lexical { get; }

        public TResult Result { get; set; }

        protected bool Accepted { get; set; }
    }

    internal abstract class Term
    {
        public static Term operator &(Term left, Term right) => left.Combine(right);

        public abstract bool Test(ILexical lexical);

        protected virtual Pattern<TResult> ApplyWhen<TResult>(Pattern<TResult> pattern, Func<ILexical, TResult> f) => pattern.ApplyWhen(this, f);

        protected virtual Term Combine(Term other) => new MetaTerm(x => Test(x) && other.Test(x));

        public abstract TermKind Kind { get; }
    }

    internal class TermWithResultType<TResult> : Term
    {
        public TermWithResultType(Term term) => this.term = term;

        public TermWithResultType()
        {
        }

        public static TermWithResultType<TResult> operator &(TermWithResultType<TResult> left, Term right) =>
            new TermWithResultType<TResult>(left.Combine(right));

        public override bool Test(ILexical lexical) => throw new NotImplementedException();

        protected override Term Combine(Term other) => base.Combine(other);

        public override TermKind Kind => throw new NotImplementedException();

        private Term term;
    }

    internal class TextualTerm<TResult> : TermWithResultType<TResult>
    {
        public TextualTerm(string text) => Text = text;

        public override bool Test(ILexical lexical) => lexical.Text == Text;


        public override TermKind Kind => TermKind.Textual;

        public static implicit operator TextualTerm<TResult>(string text) => new TextualTerm<TResult>(text);

        public string Text { get; }
    }

    internal class TypeTerm<T> : Term where T : ILexical
    {
        public static MetaTerm<T> operator &(TypeTerm<T> left, Term right) => new MetaTerm<T>(lexical => left.Test(lexical) && right.Test(lexical));

        public static MetaTerm<T> operator &(Term right, TypeTerm<T> left) => new MetaTerm<T>(lexical => left.Test(lexical) && right.Test(lexical));

        public override bool Test(ILexical lexical) => lexical is T;

        protected override Term Combine(Term other) => new MetaTerm<T>(value => other.Test(value) && Test(value));

        //public static MetaTerm<T> operator &(TypeTerm<T> left, Term right) => new MetaTerm<T>(lexical => left.Combine(right).Test(lexical));
        public override TermKind Kind => TermKind.Type;
    }

    internal enum TermKind
    {
        None,
        Value,
        Type,
        Boolean,
        Meta,
        Textual
    }
}
