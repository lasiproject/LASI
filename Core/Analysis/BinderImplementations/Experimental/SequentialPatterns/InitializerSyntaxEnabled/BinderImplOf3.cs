#pragma warning disable 1591
using System;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns.InitializerSyntaxEnabled
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
    public partial class BinderComponent :
        IBinderComponent<E, V, C, D, R, A, P>,
        IBinderComponent<P, E, V, C, D, R, A>,
        IBinderComponent<A, P, E, V, C, D, R>,
        IBinderComponent<R, A, P, E, V, C, D>,
        IBinderComponent<D, R, A, P, E, V, C>,
        IBinderComponent<R, D, C, A, P, E, V>,
        IBinderComponent<V, C, D, R, A, P, E>,
        IBinderComponent<E, R, D, R, A, P, E>,
        IBinderComponent<E, C, R, E, A, V, E>,
        IBinderComponent<E, C, E, V, C, V, A>,
        IBinderComponent<E, E, C, E, C, E, V>,
        IBinderComponent<S, V, C, D, R, A, P>,
        IBinderComponent<P, S, V, C, D, R, A>,
        IBinderComponent<A, P, S, V, C, D, R>,
        IBinderComponent<R, A, P, S, V, C, D>,
        IBinderComponent<D, R, A, P, S, V, C>,
        IBinderComponent<R, D, C, A, S, E, V>,
        IBinderComponent<V, C, D, R, A, S, E>,
        IBinderComponent<E, R, D, R, A, P, S>,
        IBinderComponent<S, C, R, E, A, V, E>,
        IBinderComponent<E, S, E, V, C, V, A>,
        IBinderComponent<E, S, C, E, C, E, V>,
        System.Collections.IEnumerable

    {

        public BinderComponent Add(Func<A, Func<D, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<A, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<C, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<C, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<C, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<D, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<D, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<P, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<P, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<P, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<R, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<R, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<C, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<D, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<D, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<E, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<E, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<P, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<P, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<P, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<R, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<R, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<V, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<V, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<C, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<C, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<D, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<E, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<E, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<E, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<R, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<R, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<R, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<R, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<V, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<V, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<A, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<A, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<E, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<P, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<P, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<R, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<R, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<R, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<V, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<V, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<V, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<A, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<A, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<D, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<D, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<D, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<E, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<E, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<E, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<P, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<R, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<R, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<A, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<A, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<A, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<C, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<C, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<D, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<D, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<D, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<D, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<R, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<V, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }

        public BinderComponent Add(Func<A, Func<D, Action<S>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<A, Func<E, Func<P, Action<D>>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<R, Func<V, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<V, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<A, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<A, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<A, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<C, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<C, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<C, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<E, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<E, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<P, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<P, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<V, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<R, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<E, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<E, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<R, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<C, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<C, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<C, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<C, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<E, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<E, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<E, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<C, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<C, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<C, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<E, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<V, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<V, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<V, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<V, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<S, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<S, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<S, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<S, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<S, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<S, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<S, Action<P>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<S, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<S, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<P, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<V, Func<S, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<S, Action<V>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<P, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<P, Func<A, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<P, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<A, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<A, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<P, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<D, Func<R, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<R, Action<D>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<R, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<R, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<D, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<D, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<D, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<D, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<C, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<C, Action<A>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<A, Func<C, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<C, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<S, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<S, Action<S>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<R, Func<S, Action<R>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<S, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<E, Func<S, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<C, Func<S, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<E, Action<E>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Add(Func<S, Func<E, Action<C>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
    }
}

#pragma warning restore 1591