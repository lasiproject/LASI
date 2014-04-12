#pragma warning disable 1591
using System;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    // Alias types to shorten name and thus file size.
    using E = IEntity;
    using V = IVerbal;
    using C = IConjunctive;
    using D = IDescriptor;
    using R = IReferencer;
    using A = IAdverbial;
    using P = IPrepositional;
    using S = SymbolPhrase;
    public partial class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>
    {
        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<A, Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<E, Func<A, Func<A, Func<D, Func<P, Func<V, Func<C, Func<E, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<P, Func<P, Func<R, Func<E, Func<V, Func<C, Func<D, Func<C, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<P, Func<E, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<C, Func<C, Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<C, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<E, Func<P, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<C, Func<P, Func<P, Func<R, Func<E, Func<V, Func<C, Func<D, Func<R, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<A, Func<D, Func<D, Func<V, Func<R, Func<P, Func<E, Func<A, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<D, Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<A, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<R, Func<R, Func<C, Func<A, Func<P, Func<E, Func<V, Func<E, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<A, Func<V, Func<D, Func<R, Func<C, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<R, Func<R, Func<C, Func<A, Func<P, Func<E, Func<V, Func<C, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<V, Func<V, Func<P, Func<C, Func<D, Func<R, Func<A, Func<R, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<E, Func<V, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<D, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<P, Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<V, Func<P, Func<P, Func<R, Func<E, Func<C, Func<D, Func<V, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<A, Func<A, Func<D, Func<P, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<C, Func<R, Func<R, Func<E, Func<D, Func<A, Func<P, Func<C, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<P, Func<R, Func<R, Func<C, Func<A, Func<E, Func<V, Func<P, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<R, Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<V, Func<V, Func<P, Func<C, Func<D, Func<R, Func<A, Func<P, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<A, Func<A, Func<D, Func<P, Func<E, Func<V, Func<C, Func<D, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<D, Func<V, Func<V, Func<P, Func<C, Func<R, Func<A, Func<D, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<P, Func<A, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<V, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<E, Func<E, Func<P, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<D, Func<E, Func<E, Func<P, Func<R, Func<R, Func<A, Func<D, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<P, Func<R, Func<D, Func<R, Func<A, Func<R, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<V, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<R, Func<E, Func<E, Func<V, Func<C, Func<E, Func<A, Func<R, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<V, Func<C, Func<R, Func<E, Func<A, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<V, Func<C, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<V, Func<C, Func<V, Func<C, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<E, Func<E, Func<V, Func<C, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<E, Func<C, Func<C, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<S, Func<S, Func<A, Func<V, Func<C, Func<D, Func<R, Func<A, Action<S>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<S, Func<C, Func<S, Func<S, Func<A, Func<V, Func<D, Func<R, Func<C, Action<S>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<S, Func<S, Func<A, Func<V, Func<C, Func<D, Func<R, Func<D, Action<S>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<C, Func<P, Func<P, Func<R, Func<S, Func<V, Func<C, Func<D, Func<R, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<V, Func<P, Func<P, Func<R, Func<S, Func<C, Func<D, Func<V, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<P, Func<P, Func<R, Func<S, Func<V, Func<C, Func<D, Func<C, Action<P>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<A, Func<A, Func<D, Func<P, Func<S, Func<V, Func<C, Func<D, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<S, Func<A, Func<A, Func<D, Func<P, Func<V, Func<C, Func<S, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<A, Func<A, Func<D, Func<P, Func<S, Func<V, Func<C, Func<V, Action<A>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<S, Func<R, Func<R, Func<C, Func<A, Func<P, Func<S, Func<V, Func<C, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<P, Func<R, Func<R, Func<C, Func<A, Func<S, Func<V, Func<P, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<R, Func<R, Func<C, Func<A, Func<P, Func<S, Func<V, Func<S, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<P, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<S, Func<V, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<D, Func<A, Func<D, Func<D, Func<V, Func<R, Func<P, Func<S, Func<A, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<C, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<S, Func<P, Action<D>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<S, Func<E, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<C, Func<R, Func<R, Func<E, Func<D, Func<A, Func<S, Func<C, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<S, Func<A, Action<R>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<R, Func<V, Func<V, Func<S, Func<C, Func<D, Func<R, Func<A, Func<S, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<D, Func<V, Func<V, Func<S, Func<C, Func<R, Func<A, Func<D, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<V, Func<V, Func<S, Func<C, Func<D, Func<R, Func<A, Func<R, Action<V>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<S, Func<E, Func<E, Func<P, Func<R, Func<D, Func<R, Func<A, Func<R, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<S, Func<S, Func<V, Func<C, Func<R, Func<E, Func<A, Func<V, Action<S>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<S, Func<R, Func<S, Func<S, Func<V, Func<C, Func<E, Func<A, Func<R, Action<S>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<S, Func<S, Func<V, Func<C, Func<R, Func<E, Func<A, Func<E, Action<S>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<V, Func<S, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<V, Func<S, Func<V, Func<C, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<A, Func<E, Func<E, Func<V, Func<S, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
        public BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<E, Func<S, Func<E, Func<C, Func<C, Action<E>>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(10)), 10); }
    }
}
#pragma warning restore 1591