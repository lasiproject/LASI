using System;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    class CaseTypePredicate<TX, T> : Predicate<T> where T : class, ILexical where TX : class, T
    {
        private readonly Func<T, bool> additionalRequirement;

        public CaseTypePredicate() => additionalRequirement = x => true;

        public CaseTypePredicate(Predicate<TX> inner) => additionalRequirement = x => inner.Satifies(x);

        public CaseTypePredicate(Func<T, bool> inner) => additionalRequirement = x => inner(x);

        /// <summary>
        /// Returns a value indicating if the given <see cref="ILexical"/> element conforms to the Type <typeparamref name="T"/> represented by the <see cref="CaseTypePredicate{TX,T}"/>.
        /// </summary>
        /// <param name="element">The <typeparam name="TLexical"/> instance to test against the Type pattern.</param>
        /// <returns><c>true</c> if the given element satisfies the predicate; otherwise <c>false</c>.</returns>
        public override bool Satifies<TLexical>(TLexical element) => element is T;

        //public static Predicate<TX> operator &(CaseTypePredicate<TX, T> left, Predicate<T> right) => new CaseTypePredicate<TX, TX>(x => left.Combine(right).Satifies(x));
        public static Predicate<TX> operator &(CaseTypePredicate<TX, T> left, ExactTextPredicate<TX> right) => new WhenPredicate<TX>(x => left.Combine(right).Satifies(x));

        public static Predicate<TX> operator &(CaseTypePredicate<TX, T> left, WhenPredicate<T> right) => new WhenPredicate<TX>(x => left.Satifies(x) && right.Satifies(x));

        public static Predicate<TX> operator &(CaseTypePredicate<TX, T> left, WhenPredicate right) => new WhenPredicate<TX>(x => left.Satifies(x) && right.Satifies(x));

        public static Predicate<TX> operator &(WhenPredicate left, CaseTypePredicate<TX, T> right) => new WhenPredicate<TX>(x => left.Satifies(x) && right.Satifies(x));

        public static Predicate<TX> operator &(CaseTypePredicate<TX, T> left, ExactTextPredicate<T> right) => new WhenPredicate<TX>(x => left.Combine(right).Satifies(x));

        public static Predicate<TX> operator &(CaseTypePredicate<TX, T> left, Predicate<T> right) => new WhenPredicate<TX>(x => left.Combine(right).Satifies(x));

        public static Predicate<TX> operator &(ExactTextPredicate<TX> left, CaseTypePredicate<TX, T> right) => new WhenPredicate<TX>(x => left.Combine(right).Satifies(x));
    }
}