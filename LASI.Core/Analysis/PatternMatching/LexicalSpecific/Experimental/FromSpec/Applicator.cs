using System;

namespace LASI.Core.Heuristics.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    class Applicator
    {
        public TResult Apply<TCase, TResult>(Predicate<TCase> accumulated, ILexical element, Func<TCase, TResult> apply) where TCase : class, ILexical
        {
            if (accumulated.Satifies(element))
            {
                return apply(element as TCase);
            }
            return default;
        }
    }
    class Applicator<TResult>
    {
        public TResult Apply<TCase>(Predicate<TCase> accumulated, ILexical element, Func<TCase, TResult> apply) where TCase : class, ILexical
        {
            if (accumulated.Satifies(element))
            {
                return apply(element as TCase);
            }
            return default;
        }
    }
    class Applicator<T, TResult> where T : class, ILexical
    {
        private ILexical target;

        public Applicator<T, TResult> SetTarget(ILexical target) { this.target = target; return this; }
        public TResult Apply<TCase>(Predicate<TCase> accumulated, ILexical element, Func<TCase, TResult> apply) where TCase : class, ILexical
        {
            if (accumulated.Satifies(element))
            {
                return apply(element as TCase);
            }
            return default;
        }
        //public static TResult operator |(Applicator<T, TResult> a, Func<T, TResult> f) => f(default(T));
        public static Applicator<T, TResult> operator |(Applicator<T, TResult> a, Func<T, TResult> f)
        {
            f(a.target as T);
            return a;// f(default(T));
        }
        //public static TResult operator |(Applicator<T, TResult> a, Predicate<T> p) => a.Apply(p, default(T), x => default(TResult));
        //public static Applicator<T, TResult> operator |(Predicate<T> p, Applicator<T, TResult> a) => a;
        public static Applicator<T, TResult> operator |(Applicator<T, TResult> a, Predicate<T> p) => p.Satifies(a.target) ? a : new Applicator<T, TResult>();

        public static Applicator<T, TResult> operator >(Applicator<T, TResult> a, T target) => a.SetTarget(target);
        public static Applicator<T, TResult> operator >(Applicator<T, TResult> a, ILexical target) => a.SetTarget(target);
        public static Applicator<T, TResult> operator <(Applicator<T, TResult> a, T target) => a.SetTarget(target);
        public static Applicator<T, TResult> operator <(Applicator<T, TResult> a, ILexical target) => a.SetTarget(target);
    }
}
