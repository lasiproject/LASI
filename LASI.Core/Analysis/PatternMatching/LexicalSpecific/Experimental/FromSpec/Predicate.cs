using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    abstract class Predicate<T> where T : class, ILexical
    {
        /// <summary>
        /// Returns a value indicating if the given <see cref="ILexical"/> instance satisfies the <see cref="Predicate{T}"/>.
        /// </summary>
        /// <param name="element"> The <see cref="ILexical"/> instance to test.</param>
        /// <returns>
        /// <c>true</c> if the given element satisfies the predicate; otherwise <c>false</c>.
        /// </returns>
        public abstract bool Satifies<TLexical>(TLexical element) where TLexical : ILexical;

        protected virtual Func<bool> ToFunc(ILexical element) => () => Satifies(element);

        protected static Predicate<T> LiftOver(ILexical element, params Predicate<T>[] predicates) => new LiftedPredicate<T>(e => predicates.All(f => f.ToFunc(e)()));
        /// <summary>
        /// Combines the <see cref="Predicate{T}"/> with another <see cref="Predicate{T}"/> 
        /// yielding a new <see cref="Predicate{T}"/> stipulating the conditions of both.
        /// </summary>
        /// <typeparam name="TOther">The Type stipulation of the other Predicate</typeparam>
        /// <param name="other">The <see cref="Predicate{T}"/> to combine with the current instance. </param>
        /// <returns> A new <see cref="Predicate{T}"/> stipulating the conditions of both. </returns>
        public virtual Predicate<TOther> Combine<TOther>(Predicate<TOther> other) where TOther : class, ILexical => new LiftedPredicate<TOther>(e => this.ToFunc(e)() && other.ToFunc(e)());

        //public virtual Predicate<TCase> Combine<TCase>(CaseTypePredicate<TCase, T> other) where TCase : class, T, ILexical {
        //    return new LiftedPredicate<TCase>(e => this.ToFunc(e)() && other.ToFunc(e)());
        //}
        public static Predicate<T> operator &(Predicate<T> left, WhenPredicate right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));
        public static Predicate<T> operator &(Predicate<T> left, WhenPredicate<T> right) => left.Combine(right);
        public static Predicate<T> operator &(WhenPredicate<T> left, Predicate<T> right) => left.Combine(right);

        public static Predicate<T> operator &(Predicate<T> left, WhenPredicate<ILexical> right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));
        public static Predicate<T> operator &(WhenPredicate<ILexical> left, Predicate<T> right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));

        public static Predicate<T> operator &(ExactTextPredicate<ILexical> left, Predicate<T> right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));
        public static Predicate<T> operator &(Predicate<T> left, ExactTextPredicate<ILexical> right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));

        public static Predicate<T> operator &(Predicate<T> left, Predicate<T> right) => left.Combine(right);

        private class LiftedPredicate<TOther> : Predicate<TOther> where TOther : class, ILexical
        {
            private readonly Func<TOther, bool> requirement;
            public LiftedPredicate(Func<TOther, bool> requirement) { this.requirement = requirement; }
            public override bool Satifies<TLexical>(TLexical element) => element is TOther && requirement(element as TOther);
        }
    }
}
