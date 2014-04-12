#pragma warning disable 1591
using System;
using E = LASI.Core.IEntity;
using V = LASI.Core.IVerbal;
using C = LASI.Core.IConjunctive;
using D = LASI.Core.IDescriptor;
using R = LASI.Core.IReferencer;
using A = LASI.Core.IAdverbial;
using P = LASI.Core.IPrepositional;
using S = LASI.Core.SymbolPhrase;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    // Alias types to shorten name and thus file size.
    using AE = Action<E>;
    using AV = Action<V>;
    using AC = Action<C>;
    using AD = Action<D>;
    using AR = Action<R>;
    using AA = Action<A>;
    using AP = Action<P>;
    using AS = Action<S>;
    public partial class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>
    {
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<C, Func<A, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<C, Func<V, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<E, Func<A, Func<P, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<E, Func<A, Func<P, Func<C, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<P, Func<E, Func<V, Func<C, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<D, Func<R, Func<A, Func<P, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<P, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<P, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<R, Func<C, Func<D, Func<C, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<R, Func<C, Func<D, Func<P, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<D, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<E, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<R, Func<A, Func<P, Func<E, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<E, Func<D, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<E, Func<P, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<A, Func<E, Func<R, Func<D, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<A, Func<E, Func<R, Func<E, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<E, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<V, Func<C, Func<D, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<C, Func<P, Func<D, Func<P, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<C, Func<P, Func<D, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<E, Func<V, Func<C, Func<D, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<R, Func<P, Func<D, Func<C, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<R, Func<P, Func<D, Func<P, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<E, Func<D, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<E, Func<P, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<A, Func<P, Func<E, Func<V, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<P, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<E, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<R, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<D, Func<C, Func<A, Func<P, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<V, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, AS>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<V, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, AS>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<V, Func<V, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, AS>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, AS>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, AS>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<R, Func<D, Func<R, Func<A, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<D, Func<E, Func<R, Func<A, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<D, Func<E, Func<R, Func<E, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<R, Func<A, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<R, Func<E, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<R, Func<E, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<R, Func<E, Func<C, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<R, Func<E, Func<C, Func<E, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<A, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<E, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<C, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<C, Func<E, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<C, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<C, Func<E, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<C, Func<E, Func<C, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<E, Func<C, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<E, Func<E, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<E, Func<C, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<E, Func<E, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<V, Func<C, Func<D, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<V, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<V, Func<S, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<S, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<S, Func<V, Func<C, Func<D, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<S, Func<D, AR>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<S, Func<P, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<S, Func<D, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<S, Func<P, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<P, Func<S, Func<V, Func<C, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<S, Func<A, Func<P, Func<C, AD>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<S, Func<A, Func<P, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<S, Func<C, Func<P, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<S, Func<C, Func<P, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<A, Func<P, Func<S, Func<V, AC>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<R, Func<A, Func<P, Func<S, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<S, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<A, Func<S, Func<R, Func<S, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<A, Func<S, Func<R, Func<D, AP>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<D, Func<C, Func<A, Func<S, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<S, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<D, Func<S, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<D, Func<R, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, AA>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<R, Func<E, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<R, Func<S, Func<C, Func<A, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<R, Func<S, Func<C, Func<S, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<S, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<S, Func<E, Func<V, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<S, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<S, Func<E, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<S, Func<C, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<S, Func<E, AV>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<S, Func<C, Func<E, Func<C, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<S, Func<C, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<S, Func<E, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<S, Func<C, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<S, Func<E, AE>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
    }
}
#pragma warning restore 1591