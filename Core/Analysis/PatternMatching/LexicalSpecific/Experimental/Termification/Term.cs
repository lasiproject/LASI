using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.Termification
{
    class Pattern<TResult>
    {
        public Pattern(ILexical lexical) { Lexical = lexical; }
        public ILexical Lexical { get; }
        public TResult Result { get; protected set; }
        protected bool Accepted { get; set; }

        public Pattern<TResult> ApplyIfApplicable<TTerm, T>(TTerm predicate, Func<T, TResult> f) where TTerm : Term where T : ILexical {
            Accepted = predicate.Test(Lexical);
            if (Accepted) {
                Result = f((T)Lexical);
            }
            return this;
        }

        //public Pattern<TResult> ApplyWhen(Term predicate, Func<ILexical, TResult> f) {
        //    return ApplyIfApplicable(predicate, f);
        //}

        public Pattern<TResult> ApplyWhen<TTerm, TTypePattern>(TTerm predicate, Func<TTypePattern, TResult> f) where TTerm : Term where TTypePattern : ILexical {
            return ApplyIfApplicable(predicate, f);
        }
        public Pattern<TResult> ApplyWhen<TTypePattern>(MetaTerm<TTypePattern> predicate, Func<TTypePattern, TResult> f) where TTypePattern : ILexical {
            return ApplyIfApplicable(predicate, f);
        }
        public static TermWithResultType<TResult> operator |(Pattern<TResult> pattern, Term term) {
            return new TermWithResultType<TResult>(term);
        }

    }
    enum TermKind
    {
        None,
        Value,
        Type,
        Boolean,
        Meta,
        Textual
    }
    abstract class Term
    {
        public abstract TermKind Kind { get; }
        public abstract bool Test(ILexical lexical);
        protected virtual Term Combine(Term other) => new MetaTerm(x => this.Test(x) && other.Test(x));
        protected virtual Pattern<TResult> ApplyWhen<TResult>(Pattern<TResult> pattern, Func<ILexical, TResult> f) => pattern.ApplyWhen(this, f);
        public static Term operator &(Term left, Term right) => left.Combine(right);
    }
    class TermWithResultType<TResult> : Term
    {
        private Term term;
         
        public TermWithResultType(Term term) {
            this.term = term;
        }

        public override TermKind Kind {
            get {
                throw new NotImplementedException();
            }
        }
        protected override Term Combine(Term other) {
            return base.Combine(other);
        }
        public override bool Test(ILexical lexical) {
            throw new NotImplementedException();
        }
        public static TermWithResultType<TResult> operator &(TermWithResultType<TResult> left, Term right) => new TermWithResultType<TResult>(left.Combine(right));

    }

    class MetaTerm : Term
    {
        private readonly Func<ILexical, bool> test;
        public MetaTerm(Func<ILexical, bool> test) { this.test = test; }
        public override TermKind Kind => TermKind.Meta;
        public override bool Test(ILexical lexical) => test(lexical);
    }
    class MetaTerm<T> : MetaTerm where T : ILexical
    {
        public static MetaTerm<T> operator &(MetaTerm<T> left, TypeTerm<T> right) => new MetaTerm<T>(lexical => left.Test(lexical) && right.Test(lexical));

        protected override Term Combine(Term other) {
            return new MetaTerm<T>(lexical => lexical is T && other.Test(lexical));
        }
        public MetaTerm(Func<T, bool> test) : base(lexical => lexical is T && test((T)lexical)) { }
    }
    class AppliedTerm<T> where T : ILexical
    {
        private MetaTerm<T> applied;

        public AppliedTerm(MetaTerm<T> applied) {
            this.applied = applied;
        }
        Pattern<TResult> Invoke<TResult>(Pattern<TResult> pattern, Func<T, TResult> f) => pattern.ApplyWhen(applied, f);
    }
    class TypeTerm<T> : Term where T : ILexical
    {
        public static MetaTerm<T> operator &(TypeTerm<T> left, Term right) => new MetaTerm<T>(lexical => left.Test(lexical) && right.Test(lexical));
        public static MetaTerm<T> operator &(Term right, TypeTerm<T> left) => new MetaTerm<T>(lexical => left.Test(lexical) && right.Test(lexical));

        //public static MetaTerm<T> operator &(TypeTerm<T> left, Term right) => new MetaTerm<T>(lexical => left.Combine(right).Test(lexical));
        public override TermKind Kind => TermKind.Type;

        protected override Term Combine(Term other) {
            return new MetaTerm<T>(value => other.Test(value) && this.Test(value));
        }
        public override bool Test(ILexical lexical) => lexical is T;

    }
    class TextualTerm<TResult> : TermWithResultType<TResult>
    {
        public TextualTerm(Term term) : base(term) { }
        public string Text { get; }
        //public TextualTerm(string text) : base(term) { Text = text; }
        public override TermKind Kind => TermKind.Textual;
        public override bool Test(ILexical lexical) => lexical.Text == Text;
    }
    class BooleanTerm : Term
    {
        protected bool Boolean { get; }
        public BooleanTerm(bool boolean) { this.Boolean = boolean; }
        public override TermKind Kind => TermKind.Boolean;

        public override bool Test(ILexical lexical) => Boolean;
    }

}

