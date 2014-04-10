using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    using E = IEntity;
    using V = IVerbal;
    using C = IConjunctive;
    using D = IDescriptor;
    using R = IReferencer;
    using A = IAdverbial;
    using P = IPrepositional;
    using S = SymbolPhrase;
    public class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>
    {
        public BinderComponent Bind(IEnumerable<LASI.Core.ILexical> stream) { return this; }
        public BinderComponent Match3(Func<A, Func<A, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<C, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<C, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<C, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<D, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<D, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<P, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<P, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<P, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<R, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<A, Func<R, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<C, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<D, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<D, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<E, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<E, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<P, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<P, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<P, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<R, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<R, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<V, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<C, Func<V, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<C, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<C, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<D, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<E, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<E, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<E, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<R, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<R, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<R, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<R, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<V, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<D, Func<V, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<A, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<A, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<E, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<P, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<P, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<R, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<R, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<R, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<V, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<V, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<E, Func<V, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<A, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<A, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<D, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<D, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<D, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<E, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<E, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<E, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<P, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<R, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<P, Func<R, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<A, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<A, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<A, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<C, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<C, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<D, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<D, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<D, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<D, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<R, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<V, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<V, Action<E>>> pattern) { return this; }
        public BinderComponent Match3(Func<R, Func<V, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<A, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<A, Action<R>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<A, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<C, Action<A>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<C, Action<D>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<C, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<E, Action<C>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<E, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<P, Action<P>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<P, Action<V>>> pattern) { return this; }
        public BinderComponent Match3(Func<V, Func<V, Action<V>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<C, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<C, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<C, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<P, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<P, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<R, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<A, Func<V, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<E, Func<V, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<P, Func<A, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<P, Func<A, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<P, Func<A, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<P, Func<C, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<P, Func<E, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<P, Func<E, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<A, Func<V, Func<C, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<A, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<A, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<D, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<D, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<P, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<P, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<C, Func<V, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<D, Func<C, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<D, Func<C, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<D, Func<C, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<D, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<D, Func<R, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<D, Func<R, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<C, Func<R, Func<A, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<A, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<C, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<C, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<E, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<E, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<E, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<R, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<D, Func<R, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<P, Func<E, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<R, Func<A, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<R, Func<A, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<R, Func<D, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<R, Func<D, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<R, Func<D, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<D, Func<R, Func<E, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<C, Func<D, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<D, Func<R, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<D, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<P, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<R, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<R, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<R, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<V, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<E, Func<V, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<V, Func<C, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<V, Func<C, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<V, Func<E, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<V, Func<E, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<V, Func<E, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<E, Func<V, Func<R, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<C, Func<D, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<E, Func<D, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<E, Func<P, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<E, Func<P, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<E, Func<P, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<E, Func<V, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<E, Func<V, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<A, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<C, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<D, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<D, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<D, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<E, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<P, Func<E, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<P, Func<V, Func<C, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<P, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<P, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<R, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<R, Action<P>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<R, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<A, Func<V, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<D, Func<C, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<D, Func<C, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<D, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<D, Func<R, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<D, Func<R, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<D, Func<R, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<E, Func<V, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<P, Func<E, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<A, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<A, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<D, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<E, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<P, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<P, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<P, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<V, Action<A>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<V, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<R, Func<R, Func<V, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<C, Func<A, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<C, Func<D, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<C, Func<D, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<C, Func<V, Action<D>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<C, Func<V, Action<R>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<C, Func<V, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<D, Func<R, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<R, Func<A, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<A, Action<C>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<A, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<A, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<C, Action<E>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<C, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<E, Action<V>>>> pattern) { return this; }
        public BinderComponent Match4(Func<V, Func<V, Func<R, Action<V>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<A, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<C, Func<A, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<C, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<E, Func<C, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<E, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<E, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<A, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<C, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<A, Func<V, Func<E, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<C, Func<A, Func<E, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<C, Func<A, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<C, Func<V, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<D, Func<P, Func<A, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<D, Func<P, Func<A, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<D, Func<P, Func<V, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<E, Func<A, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<A, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<A, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<C, Func<A, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<C, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<C, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<E, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<P, Func<V, Func<A, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<R, Func<E, Func<C, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<V, Func<E, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<V, Func<P, Func<A, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<V, Func<P, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<V, Func<P, Func<E, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<V, Func<R, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<A, Func<V, Func<R, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<A, Func<D, Func<C, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<A, Func<D, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<A, Func<D, Func<R, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<A, Func<R, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<A, Func<V, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<A, Func<V, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<A, Func<R, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<C, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<D, Func<C, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<D, Func<P, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<D, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<D, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<D, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<P, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<P, Func<C, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<R, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<R, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<C, Func<R, Func<P, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<A, Func<C, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<C, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<C, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<P, Func<C, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<P, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<P, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<D, Func<R, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<E, Func<D, Func<A, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<E, Func<D, Func<C, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<E, Func<D, Func<C, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<P, Func<A, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<P, Func<C, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<P, Func<C, Func<R, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<R, Func<C, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<C, Func<V, Func<R, Func<P, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<A, Func<D, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<C, Func<A, Func<E, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<A, Func<E, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<A, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<A, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<C, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<D, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<E, Func<D, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<E, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<P, Func<A, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<R, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<R, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<R, Func<C, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<R, Func<D, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<D, Func<R, Func<E, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<E, Func<D, Func<A, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<E, Func<D, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<E, Func<P, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<A, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<A, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<C, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<C, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<R, Func<A, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<R, Func<D, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<P, Func<R, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<A, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<D, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<D, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<E, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<E, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<E, Func<D, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<R, Func<P, Func<D, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<V, Func<R, Func<D, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<V, Func<R, Func<D, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<D, Func<V, Func<R, Func<P, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<A, Func<V, Func<D, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<A, Func<V, Func<E, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<A, Func<V, Func<E, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<D, Func<C, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<D, Func<P, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<D, Func<P, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<D, Func<V, Func<C, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<D, Func<V, Func<E, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<D, Func<V, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<R, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<D, Func<C, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<E, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<E, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<P, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<R, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<P, Func<C, Func<R, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<R, Func<D, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<C, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<C, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<D, Func<E, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<E, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<E, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<R, Func<E, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<R, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<E, Func<V, Func<R, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<A, Func<V, Func<D, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<C, Func<A, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<C, Func<A, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<C, Func<E, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<C, Func<E, Func<P, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<C, Func<E, Func<V, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<C, Func<V, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<D, Func<C, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<D, Func<P, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<D, Func<P, Func<V, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<C, Func<P, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<D, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<D, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<D, Func<P, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<P, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<P, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<E, Func<V, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<C, Func<V, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<D, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<D, Func<P, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<E, Func<A, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<E, Func<D, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<E, Func<P, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<E, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<E, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<P, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<V, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<V, Func<D, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<P, Func<V, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<R, Func<E, Func<C, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<R, Func<E, Func<P, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<R, Func<E, Func<P, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<P, Func<V, Func<P, Func<E, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<D, Func<C, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<D, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<D, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<E, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<P, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<R, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<R, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<V, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<V, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<A, Func<V, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<C, Func<A, Func<E, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<C, Func<A, Func<R, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<C, Func<A, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<C, Func<R, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<D, Func<A, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<D, Func<C, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<D, Func<P, Func<V, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<D, Func<R, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<D, Func<R, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<A, Func<P, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<A, Func<R, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<A, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<D, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<D, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<D, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<E, Func<P, Func<R, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<P, Func<A, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<P, Func<R, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<P, Func<R, Func<C, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<P, Func<R, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<C, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<D, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<P, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<P, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<V, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<C, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<C, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<C, Func<P, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<D, Func<C, Action<D>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<D, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<D, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<E, Func<P, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<P, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<P, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<P, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<P, Func<V, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<R, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<R, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<V, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<R, Func<V, Func<R, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<V, Func<E, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<V, Func<R, Func<A, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<R, Func<V, Func<R, Func<P, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<A, Func<R, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<A, Func<V, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<A, Func<V, Func<D, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<A, Func<E, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<A, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<A, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<D, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<R, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<V, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<C, Func<V, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<D, Func<V, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<E, Func<D, Func<A, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<P, Func<C, Func<R, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<P, Func<C, Func<V, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<P, Func<C, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<R, Func<C, Func<A, Action<P>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<R, Func<C, Func<D, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<R, Func<C, Func<V, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<R, Func<D, Func<V, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<R, Func<E, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<R, Func<E, Func<V, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<A, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<A, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<C, Func<A, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<C, Func<D, Action<C>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<C, Func<D, Action<R>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<C, Func<E, Action<E>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<C, Func<V, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<D, Func<A, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<D, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<D, Func<R, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<R, Func<D, Action<A>>>>> pattern) { return this; }
        public BinderComponent Match5(Func<V, Func<V, Func<V, Func<C, Action<V>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<C, Func<A, Action<R>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<C, Func<V, Action<R>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<A, Func<E, Func<A, Func<P, Func<A, Action<V>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<A, Func<E, Func<A, Func<P, Func<C, Action<D>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<P, Func<A, Action<V>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<P, Func<C, Action<V>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<C, Func<R, Func<C, Func<D, Func<C, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<C, Func<R, Func<C, Func<D, Func<P, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<D, Action<P>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<E, Action<V>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<E, Func<D, Action<C>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<E, Func<P, Action<C>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<E, Func<A, Func<E, Func<R, Func<D, Action<P>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<E, Func<A, Func<E, Func<R, Func<E, Action<P>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<E, Action<D>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<R, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<C, Func<P, Func<D, Func<P, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<C, Func<P, Func<D, Func<R, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<R, Func<P, Func<D, Func<C, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<R, Func<P, Func<D, Func<P, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<E, Func<D, Action<R>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<E, Func<P, Action<C>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<P, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<R, Action<A>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<E, Action<D>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<R, Action<D>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<D, Func<C, Func<A, Func<P, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<V, Action<C>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, Action<P>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<V, Action<R>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, Action<E>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<R, Func<D, Func<C, Func<A, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<A, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<A, Func<P, Func<C, Func<A, Func<E, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<C, Func<A, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<C, Func<V, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<C, Func<V, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<C, Func<V, Func<R, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<D, Func<V, Func<C, Func<V, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<E, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<E, Func<P, Func<V, Func<C, Func<D, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<E, Func<P, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<P, Func<A, Func<E, Func<V, Func<C, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<P, Func<A, Func<E, Func<V, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<A, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<C, Func<D, Func<P, Func<C, Func<R, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<C, Func<E, Func<D, Func<C, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<D, Func<C, Func<R, Func<A, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<D, Func<C, Func<R, Func<A, Func<P, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<E, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<P, Func<A, Func<V, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<P, Func<A, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<P, Func<C, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<R, Func<C, Func<D, Func<C, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<R, Func<D, Func<A, Func<P, Func<E, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<C, Func<R, Func<D, Func<A, Func<P, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<A, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<A, Func<R, Func<P, Func<E, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<A, Func<R, Func<P, Func<E, Func<V, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<D, Func<R, Func<E, Func<D, Func<A, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<D, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<R, Func<D, Func<A, Func<P, Func<E, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<R, Func<D, Func<A, Func<P, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<E, Func<D, Func<A, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<E, Func<D, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<E, Func<P, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<E, Func<P, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<E, Func<P, Func<C, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<D, Func<V, Func<P, Func<E, Func<P, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<A, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<A, Func<E, Func<R, Func<D, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<A, Func<E, Func<R, Func<D, Func<P, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<A, Func<E, Func<R, Func<D, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<A, Func<E, Func<R, Func<E, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<A, Func<E, Func<R, Func<E, Func<C, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<C, Func<V, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<C, Func<V, Func<D, Func<R, Func<A, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<E, Func<V, Func<R, Func<E, Func<C, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<C, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<C, Func<P, Func<D, Func<P, Func<V, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<C, Func<P, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<C, Func<P, Func<D, Func<R, Func<A, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<C, Func<P, Func<D, Func<R, Func<V, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<C, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<E, Func<P, Func<V, Func<C, Func<D, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<E, Func<P, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<P, Func<E, Func<D, Func<P, Func<V, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<P, Func<R, Func<E, Func<P, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<R, Func<C, Func<D, Func<C, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<R, Func<P, Func<D, Func<C, Func<A, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<R, Func<P, Func<D, Func<C, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<R, Func<P, Func<D, Func<C, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<R, Func<P, Func<D, Func<P, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<R, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<V, Func<E, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<V, Func<E, Func<C, Func<D, Func<R, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<P, Func<V, Func<P, Func<E, Func<P, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<A, Func<R, Func<P, Func<E, Func<V, Action<C>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<A, Func<R, Func<P, Func<E, Func<V, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<D, Func<A, Func<P, Func<E, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<D, Func<A, Func<P, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<D, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<D, Func<C, Func<A, Func<P, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<D, Func<R, Func<C, Func<A, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<D, Func<R, Func<C, Func<A, Func<P, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<P, Func<A, Func<E, Func<V, Func<C, Action<D>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<P, Func<A, Func<E, Func<V, Func<C, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<P, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<R, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<R, Func<R, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<C, Func<V, Func<D, Func<R, Func<A, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<C, Func<V, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<D, Func<C, Func<R, Func<A, Func<P, Action<A>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<D, Func<C, Func<R, Func<A, Func<P, Action<E>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<D, Func<V, Func<C, Func<V, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<P, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<R, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<R, Func<E, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<R, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<V, Func<D, Action<R>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<V, Func<C, Func<A, Func<V, Func<D, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<V, Func<V, Func<P, Func<C, Func<V, Func<E, Action<V>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<A, Func<P, Func<A, Func<E, Func<V, Func<A, Func<E, Action<A>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<A, Func<P, Func<A, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<A, Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<A, Func<P, Func<P, Func<V, Func<E, Func<A, Func<P, Action<A>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<C, Func<D, Func<C, Func<R, Func<A, Func<C, Func<R, Action<C>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<C, Func<D, Func<C, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<C, Func<D, Func<D, Func<A, Func<R, Func<C, Func<D, Action<C>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<C, Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<D, Func<R, Func<D, Func<A, Func<P, Func<D, Func<A, Action<D>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<D, Func<R, Func<D, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<D, Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<D, Func<R, Func<R, Func<P, Func<A, Func<D, Func<R, Action<D>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<E, Func<V, Func<E, Func<C, Func<D, Func<E, Func<C, Action<E>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<E, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<E, Func<V, Func<V, Func<D, Func<C, Func<E, Func<V, Action<E>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<P, Func<E, Func<E, Func<C, Func<V, Func<P, Func<E, Action<P>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<P, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<P, Func<E, Func<P, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<P, Func<E, Func<P, Func<V, Func<C, Func<P, Func<V, Action<P>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<A, Func<A, Func<E, Func<P, Func<R, Func<A, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<A, Func<R, Func<P, Func<E, Func<R, Func<P, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<A, Func<R, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<D, Func<D, Func<A, Func<C, Func<R, Func<D, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<D, Func<D, Func<C, Func<A, Func<P, Func<E, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<D, Func<R, Func<C, Func<A, Func<P, Func<E, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<D, Func<R, Func<C, Func<A, Func<R, Func<C, Action<R>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<R, Func<R, Func<D, Func<C, Func<A, Func<P, Func<E, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<V, Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<V, Func<C, Func<C, Func<R, Func<D, Func<V, Func<C, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<V, Func<C, Func<V, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<V, Func<C, Func<V, Func<D, Func<R, Func<V, Func<D, Action<V>>>>>>>> pattern) { return this; }
        public BinderComponent Match8(Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<A, Func<E, Func<D, Func<A, Func<P, Func<A, Func<A, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<A, Func<E, Func<D, Func<A, Func<P, Func<V, Func<C, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<A, Func<E, Func<D, Func<E, Func<P, Func<A, Func<A, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<A, Func<E, Func<D, Func<E, Func<P, Func<A, Func<V, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<R, Func<A, Func<C, Func<C, Func<P, Func<V, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<R, Func<A, Func<C, Func<D, Func<C, Func<V, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<R, Func<A, Func<C, Func<D, Func<C, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<R, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<P, Func<D, Func<R, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<P, Func<P, Func<P, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<P, Func<P, Func<R, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<R, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<R, Func<A, Func<C, Func<D, Func<C, Func<V, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<R, Func<A, Func<C, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<R, Func<A, Func<P, Func<D, Func<C, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<R, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<R, Func<A, Func<P, Func<P, Func<C, Func<V, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<P, Func<R, Func<A, Func<P, Func<P, Func<P, Func<V, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<P, Func<D, Func<R, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<P, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<R, Func<R, Func<P, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<C, Func<R, Func<E, Func<C, Func<D, Func<A, Func<P, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<C, Func<R, Func<E, Func<C, Func<D, Func<C, Func<C, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<C, Func<R, Func<E, Func<R, Func<D, Func<C, Func<A, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<C, Func<R, Func<E, Func<R, Func<D, Func<C, Func<C, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<D, Func<D, Func<D, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<D, Func<D, Func<P, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<D, Func<E, Func<D, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<D, Func<E, Func<P, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<P, Func<E, Func<D, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<D, Func<E, Func<D, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<D, Func<E, Func<P, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<D, Func<E, Func<P, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<P, Func<P, Func<D, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<D, Func<A, Func<V, Func<A, Func<R, Func<D, Func<D, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<D, Func<A, Func<V, Func<A, Func<R, Func<D, Func<P, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<D, Func<A, Func<V, Func<D, Func<R, Func<D, Func<D, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<D, Func<A, Func<V, Func<D, Func<R, Func<P, Func<E, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<E, Func<R, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<E, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<R, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<D, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<E, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<D, Func<R, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<E, Func<E, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<R, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<R, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<R, Func<P, Func<E, Func<R, Func<R, Func<V, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<R, Func<P, Func<E, Func<V, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<R, Func<P, Func<E, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<R, Func<P, Func<E, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<R, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<V, Func<V, Func<R, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<V, Func<P, Func<E, Func<V, Func<V, Func<V, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<D, Func<D, Func<E, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<E, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<E, Func<R, Func<D, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<E, Func<R, Func<E, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<D, Func<R, Func<E, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<E, Func<E, Func<D, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<E, Func<E, Func<E, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<E, Func<R, Func<D, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<E, Func<A, Func<P, Func<E, Func<R, Func<E, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<P, Func<E, Func<C, Func<D, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<P, Func<E, Func<P, Func<P, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<V, Func<E, Func<P, Func<C, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<V, Func<E, Func<P, Func<P, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<A, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<V, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<C, Func<V, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<V, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<C, Func<E, Func<C, Func<D, Func<R, Func<A, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<C, Func<E, Func<C, Func<D, Func<R, Func<R, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<C, Func<E, Func<R, Func<D, Func<A, Func<P, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<C, Func<E, Func<R, Func<D, Func<R, Func<R, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<P, Func<C, Func<P, Func<A, Func<R, Func<E, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<P, Func<C, Func<P, Func<A, Func<R, Func<R, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<P, Func<C, Func<R, Func<A, Func<E, Func<V, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<P, Func<C, Func<R, Func<A, Func<R, Func<R, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<R, Func<R, Func<D, Func<C, Func<A, Func<P, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<V, Func<D, Func<R, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<V, Func<D, Func<R, Func<A, Func<C, Func<V, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<V, Func<D, Func<R, Func<A, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<V, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<V, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<V, Func<D, Func<R, Func<V, Func<V, Func<A, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<A, Func<E, Func<V, Func<A, Func<A, Func<C, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<A, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<A, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<A, Func<E, Func<V, Func<C, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<A, Func<E, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<A, Func<E, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<A, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<A, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<C, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<P, Func<D, Func<C, Func<V, Func<R, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<P, Func<D, Func<C, Func<V, Func<V, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<P, Func<V, Func<C, Func<R, Func<A, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<P, Func<V, Func<C, Func<V, Func<V, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<P, Func<R, Func<E, Func<P, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<D, Func<R, Func<E, Func<P, Func<A, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<D, Func<R, Func<E, Func<P, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<D, Func<R, Func<E, Func<D, Func<A, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<A, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<D, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<P, Func<E, Func<R, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<P, Func<E, Func<V, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<P, Func<E, Func<V, Func<C, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<E, Func<P, Func<E, Func<V, Func<C, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<D, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<E, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<E, Func<V, Func<R, Func<D, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<E, Func<V, Func<R, Func<E, Func<C, Action<E>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<R, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<P, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<P, Func<E, Func<D, Func<R, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<P, Func<E, Func<D, Func<P, Func<V, Action<P>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<V, Func<P, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<C, Func<V, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<A, Func<A, Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<A, Func<E, Func<A, Func<A, Func<D, Func<P, Func<V, Func<C, Func<E, Action<A>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<A, Func<P, Func<P, Func<R, Func<E, Func<V, Func<C, Func<D, Func<C, Action<P>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<A, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<P, Func<E, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<C, Func<C, Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<C, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<E, Func<P, Action<D>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<C, Func<P, Func<P, Func<R, Func<E, Func<V, Func<C, Func<D, Func<R, Action<P>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<D, Func<A, Func<D, Func<D, Func<V, Func<R, Func<P, Func<E, Func<A, Action<D>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<D, Func<D, Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<D, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<A, Action<E>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<D, Func<R, Func<R, Func<C, Func<A, Func<P, Func<E, Func<V, Func<E, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<A, Func<V, Func<D, Func<R, Func<C, Action<E>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<E, Func<R, Func<R, Func<C, Func<A, Func<P, Func<E, Func<V, Func<C, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<E, Func<V, Func<V, Func<P, Func<C, Func<D, Func<R, Func<A, Func<R, Action<V>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<P, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<E, Func<V, Action<D>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<P, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<D, Action<E>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<P, Func<P, Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<P, Func<V, Func<P, Func<P, Func<R, Func<E, Func<C, Func<D, Func<V, Action<P>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<R, Func<A, Func<A, Func<D, Func<P, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<R, Func<C, Func<R, Func<R, Func<E, Func<D, Func<A, Func<P, Func<C, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<R, Func<P, Func<R, Func<R, Func<C, Func<A, Func<E, Func<V, Func<P, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<R, Func<R, Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<R, Func<V, Func<V, Func<P, Func<C, Func<D, Func<R, Func<A, Func<P, Action<V>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<V, Func<A, Func<A, Func<D, Func<P, Func<E, Func<V, Func<C, Func<D, Action<A>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<V, Func<D, Func<V, Func<V, Func<P, Func<C, Func<R, Func<A, Func<D, Action<V>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<V, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<P, Func<A, Action<R>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match10(Func<V, Func<V, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>>> pattern) { return this; }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<E, Func<C, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<P, Func<V, Func<P, Func<E, Func<P, Func<V, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<E, Func<A, Func<P, Func<A, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<R, Func<P, Func<R, Func<A, Func<R, Func<P, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<D, Func<A, Func<D, Func<R, Func<D, Func<A, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<D, Func<R, Func<C, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<V, Func<D, Func<V, Func<C, Func<V, Func<D, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<V, Func<E, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<E, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<V, Func<R, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<D, Func<E, Func<P, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<P, Func<E, Func<P, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<D, Func<E, Func<D, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<P, Func<E, Func<P, Func<V, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<P, Func<A, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<P, Func<A, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<P, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<P, Func<A, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<V, Func<A, Func<R, Func<P, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<A, Func<R, Func<P, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<V, Func<A, Func<V, Func<P, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<A, Func<R, Func<P, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<D, Func<A, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<D, Func<R, Func<D, Func<A, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<A, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<D, Func<R, Func<D, Func<A, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<P, Func<D, Func<R, Func<C, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<D, Func<R, Func<C, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<P, Func<D, Func<P, Func<C, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<D, Func<R, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<C, Func<V, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<V, Func<C, Func<V, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<C, Func<A, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<V, Func<C, Func<V, Func<D, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<A, Func<R, Action<E>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<R, Func<E, Action<D>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<D, Func<E, Action<E>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<A, Func<R, Action<D>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<R, Func<D, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<A, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<R, Func<E, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<R, Func<D, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<R, Func<E, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<R, Func<E, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<R, Func<R, Func<D, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<R, Func<A, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<A, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<D, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<R, Func<E, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<R, Func<A, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<D, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<A, Func<E, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<D, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<A, Func<E, Func<D, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<A, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<E, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<E, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<R, Func<D, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<D, Func<E, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<R, Func<A, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<R, Func<E, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<R, Func<D, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<A, Func<R, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<D, Func<A, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<R, Func<A, Func<R, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<R, Func<R, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<D, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<P, Func<R, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<P, Func<R, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<P, Func<R, Func<E, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<A, Func<R, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<R, Func<E, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<P, Func<R, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<P, Func<R, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<P, Func<R, Func<A, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<R, Func<D, Func<R, Func<A, Action<P>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<D, Func<E, Func<R, Func<A, Action<P>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<D, Func<E, Func<R, Func<E, Action<R>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<R, Func<A, Action<R>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<R, Func<E, Action<R>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<D, Func<E, Func<R, Func<E, Func<D, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<D, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<E, Func<R, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<E, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<A, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<D, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<A, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<A, Func<P, Func<R, Func<A, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<P, Func<R, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<E, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<D, Func<R, Func<R, Func<A, Func<P, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<E, Func<D, Func<R, Func<A, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<D, Func<R, Func<R, Func<A, Func<P, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<R, Func<E, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<R, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<R, Func<E, Func<D, Func<R, Func<E, Func<D, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<R, Func<R, Func<R, Func<D, Func<E, Func<R, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<R, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<D, Func<P, Func<D, Func<R, Func<E, Func<R, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<D, Func<P, Func<D, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<A, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<E, Func<E, Func<A, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<E, Func<P, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<R, Func<E, Func<E, Func<P, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<D, Func<E, Func<E, Func<P, Func<R, Func<R, Func<A, Func<D, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<P, Func<R, Func<D, Func<R, Func<A, Func<R, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<E, Func<C, Action<E>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<E, Func<C, Action<A>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<A, Func<C, Action<E>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<E, Func<C, Action<R>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<C, Func<E, Action<R>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<R, Func<E, Action<E>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<R, Func<E, Action<R>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<A, Func<C, Action<R>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<C, Func<R, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<C, Func<E, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<C, Func<E, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<C, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<C, Func<C, Func<R, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<C, Func<A, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<A, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<E, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<C, Func<E, Func<A, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<A, Func<A, Func<E, Action<A>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<R, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<C, Func<E, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<C, Func<A, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<A, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<A, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<R, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<A, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<R, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<A, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<A, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<C, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<E, Func<E, Func<C, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<V, Func<C, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<A, Func<V, Func<C, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<V, Func<C, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<A, Func<V, Func<C, Func<A, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<C, Func<R, Func<E, Func<A, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<R, Func<E, Func<C, Func<A, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<R, Func<E, Func<C, Func<E, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<A, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<E, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<E, Func<C, Func<E, Func<R, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<C, Func<R, Func<E, Func<A, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<E, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<R, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<A, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<A, Func<A, Func<V, Func<C, Func<A, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<V, Func<C, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<R, Func<E, Func<A, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<C, Func<E, Func<A, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<R, Func<E, Func<A, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<R, Func<C, Func<E, Func<A, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<E, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<E, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<E, Func<R, Func<E, Func<E, Func<R, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<C, Func<E, Func<R, Func<E, Func<C, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<V, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<V, Func<R, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<V, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<E, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<E, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<C, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<A, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<E, Func<E, Func<E, Func<A, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<E, Func<A, Func<C, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<R, Func<E, Func<A, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<E, Func<V, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<V, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<R, Func<E, Func<E, Func<V, Func<C, Func<E, Func<A, Func<R, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<V, Func<C, Func<R, Func<E, Func<A, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<E, Func<C, Action<C>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<C, Func<C, Action<E>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<C, Func<E, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<C, Func<C, Func<E, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<C, Func<C, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<C, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<C, Action<A>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<C, Func<C, Func<C, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<V, Func<C, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<C, Func<V, Func<C, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<E, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<C, Func<C, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<C, Func<E, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<E, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<V, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<V, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<C, Func<V, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<A, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<A, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<V, Func<C, Func<V, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<C, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<V, Func<A, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<V, Func<C, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<V, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<V, Func<C, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<V, Func<C, Func<C, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<C, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<C, Func<E, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<C, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<C, Func<E, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<C, Func<E, Func<V, Func<C, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<C, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<C, Func<E, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<C, Func<C, Func<A, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<E, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<C, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<C, Func<C, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<V, Func<C, Func<C, Func<A, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<V, Func<C, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<E, Func<V, Func<C, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<C, Func<V, Func<C, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<E, Func<V, Func<C, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<C, Func<V, Func<C, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<E, Func<C, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<E, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<C, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<E, Func<E, Func<V, Func<E, Func<E, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<C, Func<C, Func<V, Func<E, Func<E, Func<C, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<C, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<V, Func<C, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<C, Func<C, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<E, Func<C, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<C, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<E, Func<E, Func<C, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<E, Func<V, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<V, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<V, Func<C, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<V, Func<C, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<V, Func<C, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<V, Func<C, Func<V, Func<C, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<A, Func<E, Func<E, Func<V, Func<C, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match3(Func<E, Func<E, Action<C>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<E, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<E, Func<E, Func<C, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match4(Func<C, Func<C, Func<E, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<E, Func<E, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<E, Func<C, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<V, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<C, Func<E, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<E, Func<V, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<E, Func<E, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match5(Func<C, Func<E, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<E, Func<C, Func<E, Func<C, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<E, Func<C, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<E, Func<E, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<E, Func<C, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<E, Func<E, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<E, Func<E, Func<C, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<E, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<E, Func<E, Func<E, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<E, Func<C, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<E, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<E, Func<C, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<C, Func<C, Func<E, Func<E, Func<C, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<E, Func<C, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<E, Func<C, Func<E, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<E, Func<E, Func<C, Func<E, Func<E, Func<C, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match8(Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<C, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<C, Func<E, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<E, Func<E, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<C, Func<E, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<C, Func<C, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<C, Func<C, Func<C, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<C, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<E, Func<C, Func<C, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<V, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<V, Action<R>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<R, Func<V, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<V, Action<C>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<S, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<V, Func<S, Action<C>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<C, Func<S, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<C, Func<S, Action<C>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<V, Func<C, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<R, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<V, Func<S, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<V, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<V, Func<S, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<V, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<V, Func<S, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<V, Func<V, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<V, Func<R, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<R, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<R, Action<P>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<D, Func<R, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<D, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<C, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<V, Func<S, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<S, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<R, Func<S, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<R, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<R, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<S, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<D, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<V, Func<C, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<S, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<S, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<V, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<S, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<V, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<V, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<V, Func<S, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<V, Func<P, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<R, Func<D, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<R, Func<D, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<D, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<P, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<P, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<A, Func<V, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<A, Func<V, Func<D, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<A, Func<V, Func<S, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<V, Func<R, Action<A>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<V, Func<S, Action<D>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<S, Action<D>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<V, Func<S, Func<C, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<V, Func<S, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<S, Func<D, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<S, Func<D, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<S, Func<V, Func<S, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<S, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<S, Func<A, Func<V, Func<S, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<V, Func<S, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<V, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<V, Func<S, Func<C, Func<D, Func<R, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<V, Func<D, Func<R, Func<A, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<S, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<V, Func<S, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<V, Func<S, Func<C, Func<D, Func<S, Func<C, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<V, Func<V, Func<D, Func<C, Func<S, Func<V, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<S, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<C, Func<A, Func<S, Func<V, Func<D, Func<R, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<S, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<C, Func<A, Func<C, Func<V, Func<S, Func<D, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<C, Func<A, Func<S, Func<V, Func<S, Func<S, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<C, Func<A, Func<C, Func<V, Func<S, Func<S, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<S, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<S, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<S, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<S, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<S, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<R, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<S, Func<C, Func<D, Func<S, Func<S, Func<R, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<S, Func<C, Func<D, Func<R, Func<V, Func<S, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<S, Func<C, Func<D, Func<R, Func<V, Func<S, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<S, Func<A, Func<V, Func<S, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<S, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<S, Func<P, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<D, Func<S, Func<S, Func<A, Func<V, Func<C, Func<D, Func<R, Func<A, Action<S>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<S, Func<C, Func<S, Func<S, Func<A, Func<V, Func<D, Func<R, Func<C, Action<S>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<P, Func<S, Func<S, Func<A, Func<V, Func<C, Func<D, Func<R, Func<D, Action<S>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<P, Func<S, Action<P>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<P, Func<S, Action<D>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<D, Func<S, Action<P>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<D, Func<S, Action<D>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<P, Func<S, Action<V>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<P, Action<V>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<V, Func<S, Action<V>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<D, Func<S, Action<V>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<S, Func<V, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<S, Func<P, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<S, Func<V, Action<P>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<S, Func<P, Action<P>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<P, Func<S, Action<P>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<S, Func<P, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<V, Action<P>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<S, Func<D, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<P, Func<D, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<C, Func<D, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<D, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<S, Func<V, Func<C, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<S, Func<P, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<S, Func<P, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<S, Func<D, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<S, Func<P, Func<V, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<D, Func<P, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<P, Func<P, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<P, Func<S, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<S, Func<P, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<S, Func<P, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<C, Func<S, Func<V, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<V, Func<P, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<P, Func<S, Func<P, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<C, Func<S, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<C, Func<S, Func<P, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<P, Func<S, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<V, Func<C, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<D, Func<C, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<C, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<S, Func<C, Func<P, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<P, Func<V, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<C, Func<A, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<C, Func<A, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<R, Func<S, Func<P, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<R, Func<S, Func<C, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<R, Func<S, Func<P, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<S, Func<D, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<C, Func<A, Func<S, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<R, Func<S, Func<D, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<R, Func<S, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<R, Func<S, Func<C, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<R, Func<S, Func<D, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<S, Func<D, Action<R>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<S, Func<P, Action<C>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<S, Func<D, Action<C>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<S, Func<P, Action<C>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<V, Func<P, Func<S, Func<P, Func<V, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<V, Func<P, Func<S, Func<P, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<S, Func<P, Func<C, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<S, Func<P, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<S, Func<D, Func<A, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<V, Func<P, Func<S, Func<P, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<S, Func<P, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<V, Func<D, Func<S, Func<D, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<D, Func<R, Func<S, Func<D, Func<A, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<P, Func<R, Func<S, Func<P, Func<A, Action<P>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<S, Func<P, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<V, Func<S, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<S, Func<P, Func<V, Func<C, Func<D, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<P, Func<V, Func<S, Func<C, Func<D, Func<R, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<P, Func<P, Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<P, Func<S, Func<P, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<P, Func<S, Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<P, Func<S, Func<P, Func<V, Func<C, Func<P, Func<V, Action<P>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<P, Func<S, Func<S, Func<C, Func<V, Func<P, Func<S, Action<P>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<P, Func<P, Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<P, Func<S, Func<C, Func<D, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<V, Func<S, Func<P, Func<C, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<P, Func<S, Func<P, Func<P, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<P, Func<V, Func<R, Func<V, Func<S, Func<P, Func<P, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<D, Func<S, Func<D, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<D, Func<S, Func<D, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<P, Func<S, Func<D, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<D, Func<S, Func<P, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<D, Func<S, Func<P, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<D, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<P, Func<V, Func<C, Func<D, Func<S, Func<P, Func<A, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<P, Func<R, Func<S, Func<P, Func<A, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<D, Func<R, Func<S, Func<P, Func<A, Action<P>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<D, Func<R, Func<S, Func<P, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<C, Func<V, Func<C, Func<D, Func<R, Func<S, Func<D, Func<A, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<C, Func<P, Func<P, Func<R, Func<S, Func<V, Func<C, Func<D, Func<R, Action<P>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<P, Func<V, Func<P, Func<P, Func<R, Func<S, Func<C, Func<D, Func<V, Action<P>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<A, Func<P, Func<P, Func<R, Func<S, Func<V, Func<C, Func<D, Func<C, Action<P>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<A, Func<P, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<P, Func<A, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<P, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<A, Action<A>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<A, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<C, Func<P, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<A, Func<P, Func<S, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<A, Func<P, Func<A, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<A, Func<P, Func<S, Action<A>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<P, Func<P, Func<S, Action<A>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<P, Func<S, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<P, Func<A, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<P, Func<A, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<P, Func<C, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<P, Func<A, Func<S, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<A, Func<S, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<C, Func<A, Func<S, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<A, Func<V, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<V, Func<P, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<S, Func<A, Func<P, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<P, Func<P, Func<S, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<A, Func<S, Func<C, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<A, Func<S, Func<P, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<A, Func<S, Func<A, Func<P, Func<C, Action<D>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<A, Func<S, Func<A, Func<P, Func<A, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<C, Func<S, Func<C, Func<P, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<C, Func<S, Func<C, Func<P, Func<A, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<S, Func<A, Func<P, Func<A, Func<S, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<S, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<S, Func<C, Func<P, Func<A, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<S, Func<C, Func<P, Func<A, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<S, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<S, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<S, Func<C, Func<P, Func<A, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<S, Func<C, Func<P, Func<C, Func<R, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<P, Func<A, Func<S, Func<V, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<S, Func<P, Func<V, Func<C, Func<D, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<P, Func<A, Func<S, Func<V, Func<C, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<S, Func<P, Func<V, Func<C, Func<D, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<A, Func<A, Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<A, Func<P, Func<A, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<A, Func<P, Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<A, Func<P, Func<A, Func<S, Func<V, Func<A, Func<S, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<A, Func<P, Func<P, Func<V, Func<S, Func<A, Func<P, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<A, Func<A, Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<A, Func<S, Func<D, Func<A, Func<P, Func<V, Func<C, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<A, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<A, Func<S, Func<D, Func<S, Func<P, Func<A, Func<V, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<A, Func<S, Func<D, Func<A, Func<P, Func<A, Func<A, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<A, Func<S, Func<D, Func<S, Func<P, Func<A, Func<A, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<A, Func<S, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<A, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<A, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<C, Func<C, Func<A, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<C, Func<C, Func<C, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<A, Func<S, Func<V, Func<A, Func<A, Func<C, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<A, Func<S, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<A, Func<S, Func<V, Func<C, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<S, Func<V, Func<A, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<S, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<S, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<S, Func<V, Func<C, Func<D, Func<P, Func<C, Func<R, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<V, Func<A, Func<A, Func<D, Func<P, Func<S, Func<V, Func<C, Func<D, Action<A>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<A, Func<S, Func<A, Func<A, Func<D, Func<P, Func<V, Func<C, Func<S, Action<A>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<R, Func<A, Func<A, Func<D, Func<P, Func<S, Func<V, Func<C, Func<V, Action<A>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<A, Func<P, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<A, Func<R, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<S, Func<V, Action<A>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<R, Func<S, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<A, Func<S, Func<V, Action<A>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<V, Func<V, Func<S, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<A, Func<P, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<P, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<V, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<S, Func<P, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<A, Func<P, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<A, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<A, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<A, Func<P, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<V, Func<S, Func<A, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<V, Func<S, Func<A, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<A, Func<S, Func<R, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<D, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<D, Func<A, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<C, Func<A, Func<S, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<C, Func<A, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<S, Func<D, Func<A, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<C, Func<A, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<C, Func<A, Func<V, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, Action<S>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, Action<S>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, Action<S>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<R, Func<S, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<P, Func<V, Func<A, Func<R, Func<S, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<A, Func<R, Func<P, Func<S, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<P, Func<A, Func<S, Func<V, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<A, Func<R, Func<P, Func<S, Func<V, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<P, Func<A, Func<S, Func<V, Func<C, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<R, Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<A, Func<R, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<A, Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<A, Func<R, Func<P, Func<S, Func<R, Func<P, Action<R>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<A, Func<A, Func<S, Func<P, Func<R, Func<A, Action<R>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<R, Func<R, Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<R, Func<P, Func<C, Func<R, Func<A, Func<S, Func<V, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<R, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<R, Func<P, Func<C, Func<P, Func<A, Func<R, Func<S, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<R, Func<P, Func<S, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<R, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<R, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<V, Func<V, Func<R, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<V, Func<V, Func<V, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<R, Func<P, Func<S, Func<R, Func<R, Func<V, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<R, Func<P, Func<S, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<V, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<R, Func<P, Func<S, Func<V, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<P, Func<S, Func<R, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<P, Func<S, Func<V, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<P, Func<S, Func<V, Func<C, Func<A, Func<R, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<P, Func<S, Func<V, Func<C, Func<A, Func<V, Func<D, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<S, Func<R, Func<R, Func<C, Func<A, Func<P, Func<S, Func<V, Func<C, Action<R>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<R, Func<P, Func<R, Func<R, Func<C, Func<A, Func<S, Func<V, Func<P, Action<R>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<D, Func<R, Func<R, Func<C, Func<A, Func<P, Func<S, Func<V, Func<S, Action<R>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<D, Func<R, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<R, Action<D>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<R, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<R, Action<A>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<D, Func<D, Func<S, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<D, Func<R, Func<S, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<D, Func<D, Func<S, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<D, Func<D, Func<S, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<D, Func<P, Func<S, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<P, Func<S, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<P, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<R, Func<A, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<R, Func<D, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<R, Func<D, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<R, Func<S, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<S, Func<D, Func<R, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<A, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<S, Func<D, Func<A, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<S, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<P, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<P, Func<R, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<P, Func<R, Func<S, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<S, Func<P, Func<R, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<A, Func<S, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<P, Func<R, Action<D>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<R, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<P, Func<C, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<R, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<V, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<R, Func<P, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<R, Func<S, Action<P>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<D, Func<R, Func<A, Func<P, Func<S, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<S, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<A, Func<S, Func<R, Func<S, Action<P>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<A, Func<S, Func<R, Func<D, Action<P>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<D, Func<R, Func<A, Func<P, Func<S, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<A, Func<S, Func<R, Func<D, Func<P, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<A, Func<S, Func<R, Func<D, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<A, Func<S, Func<R, Func<S, Func<C, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<A, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<A, Func<S, Func<R, Func<D, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<A, Func<S, Func<R, Func<S, Func<C, Action<D>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<S, Func<V, Func<R, Func<S, Func<C, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<R, Func<D, Func<A, Func<P, Func<S, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<A, Func<R, Func<P, Func<S, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<R, Func<D, Func<A, Func<P, Func<S, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<D, Func<A, Func<R, Func<P, Func<S, Func<V, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<D, Func<D, Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<D, Func<R, Func<D, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<D, Func<R, Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<D, Func<D, Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<D, Func<D, Func<A, Func<V, Func<D, Func<R, Func<P, Func<S, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<S, Func<R, Func<S, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<S, Func<R, Func<S, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<D, Func<R, Func<S, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<S, Func<R, Func<D, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<S, Func<S, Func<D, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<S, Func<S, Func<S, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<D, Func<D, Func<S, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<S, Func<R, Func<D, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<S, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<D, Func<A, Func<P, Func<S, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<S, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<S, Func<V, Func<R, Func<D, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<P, Func<A, Func<P, Func<S, Func<V, Func<R, Func<S, Func<C, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<P, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<S, Func<V, Action<D>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<D, Func<A, Func<D, Func<D, Func<V, Func<R, Func<P, Func<S, Func<A, Action<D>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<C, Func<D, Func<D, Func<V, Func<R, Func<A, Func<P, Func<S, Func<P, Action<D>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<R, Func<D, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<D, Action<R>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<D, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<D, Action<C>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<D, Func<S, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<R, Func<S, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<R, Func<S, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<R, Func<A, Func<S, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<D, Func<A, Func<S, Action<D>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<A, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<D, Func<C, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<R, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<D, Func<R, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<D, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<R, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<C, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<R, Func<C, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<S, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<A, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<A, Func<D, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<A, Func<D, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<D, Func<C, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<S, Func<A, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<R, Func<R, Func<C, Func<S, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<D, Func<S, Func<A, Func<D, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<D, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<D, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<E, Func<D, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<D, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<D, Func<S, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<R, Func<D, Func<C, Func<A, Func<S, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<S, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<D, Func<S, Action<A>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<D, Func<R, Action<A>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<R, Func<D, Func<C, Func<A, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<D, Func<C, Func<A, Func<S, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<D, Func<R, Func<A, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<D, Func<R, Func<A, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<D, Func<S, Func<V, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<D, Func<R, Func<V, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<D, Func<S, Func<V, Action<R>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<S, Func<E, Func<D, Func<S, Func<V, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<D, Func<R, Func<C, Func<A, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<D, Func<A, Func<S, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<D, Func<R, Func<C, Func<A, Func<S, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<R, Func<C, Func<D, Func<A, Func<S, Func<E, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<R, Func<D, Func<C, Func<A, Func<S, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<D, Func<R, Func<C, Func<A, Func<S, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<R, Func<D, Func<D, Func<C, Func<A, Func<S, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<R, Func<R, Func<D, Func<C, Func<A, Func<S, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<R, Func<C, Func<E, Func<R, Func<D, Func<A, Func<S, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<S, Func<D, Func<S, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<S, Func<D, Func<S, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<R, Func<D, Func<S, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<S, Func<D, Func<R, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<S, Func<S, Func<R, Func<V, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<S, Func<S, Func<S, Func<V, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<R, Func<R, Func<S, Func<V, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<S, Func<D, Func<R, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<S, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<R, Func<C, Func<A, Func<S, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<S, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<S, Func<E, Func<D, Func<R, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<A, Func<C, Func<A, Func<S, Func<E, Func<D, Func<S, Func<V, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<A, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<S, Func<E, Action<R>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<R, Func<C, Func<R, Func<R, Func<E, Func<D, Func<A, Func<S, Func<C, Action<R>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<V, Func<R, Func<R, Func<E, Func<D, Func<C, Func<A, Func<S, Func<A, Action<R>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<R, Func<C, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<S, Func<C, Func<V, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<S, Func<C, Func<R, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<V, Func<S, Func<C, Func<V, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<S, Func<C, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<S, Func<C, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<S, Func<C, Func<A, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, Action<S>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, Action<S>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<C, Func<D, Func<R, Func<A, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<A, Func<S, Func<C, Func<A, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<V, Func<S, Func<C, Func<V, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<C, Func<V, Func<D, Func<R, Func<A, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<D, Func<C, Func<R, Func<A, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<V, Func<D, Func<C, Func<R, Func<A, Func<S, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<S, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<V, Func<C, Func<V, Func<D, Func<R, Func<A, Func<S, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<V, Func<C, Func<C, Func<D, Func<R, Func<A, Func<S, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<S, Func<V, Func<C, Func<R, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<S, Func<D, Func<C, Func<V, Func<R, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<S, Func<V, Func<C, Func<V, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<V, Func<D, Func<S, Func<D, Func<C, Func<V, Func<V, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<V, Func<S, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<S, Func<C, Func<V, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<S, Func<C, Func<V, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<S, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<R, Func<V, Func<V, Func<S, Func<C, Func<D, Func<R, Func<A, Func<S, Action<V>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<V, Func<D, Func<V, Func<V, Func<S, Func<C, Func<R, Func<A, Func<D, Action<V>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<E, Func<V, Func<V, Func<S, Func<C, Func<D, Func<R, Func<A, Func<R, Action<V>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<E, Func<A, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<R, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<R, Func<S, Func<E, Action<R>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<R, Func<S, Func<R, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<R, Func<S, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<D, Func<E, Func<R, Func<E, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<A, Func<S, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<D, Func<E, Func<R, Func<E, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<E, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<D, Func<A, Func<R, Func<A, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<A, Func<P, Func<R, Func<A, Func<S, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<P, Func<R, Func<E, Func<S, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<D, Func<R, Func<R, Func<A, Func<P, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<R, Func<E, Func<D, Func<R, Func<A, Func<P, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<R, Func<R, Func<D, Func<R, Func<A, Func<P, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<R, Func<A, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<E, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<A, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<A, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<A, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<E, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<E, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<A, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<E, Func<E, Func<A, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<E, Func<P, Func<R, Func<E, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<A, Func<S, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<S, Func<E, Func<E, Func<P, Func<R, Func<D, Func<R, Func<A, Func<R, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<C, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<C, Action<A>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<A, Func<C, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<C, Action<R>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<C, Func<S, Action<R>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<R, Func<S, Action<S>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<R, Func<S, Action<R>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<R, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<S, Action<R>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<R, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<S, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<S, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<C, Func<C, Func<R, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<C, Func<A, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<A, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<A, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<E, Func<A, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<E, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<R, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<A, Func<C, Func<S, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<S, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<S, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<A, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<R, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<A, Func<S, Func<R, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<A, Func<E, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<E, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<R, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<S, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<C, Func<R, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<C, Func<A, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<C, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<A, Func<E, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<R, Func<A, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<A, Func<E, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<E, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<R, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<E, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<E, Func<E, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<C, Func<S, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<C, Func<E, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<V, Func<C, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<C, Func<R, Func<E, Func<A, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<R, Func<S, Func<C, Func<A, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<S, Func<R, Func<S, Func<C, Func<S, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<S, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<R, Func<S, Func<C, Func<S, Func<R, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<S, Func<C, Func<R, Func<E, Func<A, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<R, Func<S, Func<C, Func<S, Func<E, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<S, Func<E, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<S, Func<E, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<R, Func<S, Func<C, Func<S, Func<E, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<A, Func<R, Func<A, Func<C, Func<A, Func<E, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<S, Func<V, Func<C, Func<S, Func<E, Action<S>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<R, Func<E, Func<A, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<R, Func<C, Func<E, Func<A, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<C, Func<S, Func<R, Func<E, Func<A, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<S, Func<R, Func<C, Func<E, Func<A, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<S, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<C, Func<S, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<C, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<C, Func<S, Func<R, Func<E, Func<S, Func<R, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<S, Func<C, Func<C, Func<E, Func<R, Func<S, Func<C, Action<S>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<S, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<R, Func<V, Func<S, Func<C, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<S, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<R, Func<V, Func<R, Func<C, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<R, Func<V, Func<S, Func<C, Func<S, Func<S, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<S, Func<S, Func<R, Func<V, Func<R, Func<C, Func<S, Func<S, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<S, Func<R, Func<E, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<S, Func<C, Func<A, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<C, Func<S, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<S, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<A, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<S, Func<R, Func<E, Func<S, Func<S, Func<A, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<S, Func<R, Func<E, Func<A, Func<C, Func<S, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<A, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<S, Func<R, Func<E, Func<A, Func<C, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<S, Func<V, Func<C, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<S, Func<E, Action<S>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<S, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<E, Func<S, Func<S, Func<V, Func<C, Func<R, Func<E, Func<A, Func<V, Action<S>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<S, Func<R, Func<S, Func<S, Func<V, Func<C, Func<E, Func<A, Func<R, Action<S>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<E, Func<S, Func<S, Func<V, Func<C, Func<R, Func<E, Func<A, Func<E, Action<S>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<E, Func<S, Action<E>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<E, Func<S, Action<C>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<C, Func<S, Action<E>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<E, Action<E>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<S, Func<E, Action<V>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<S, Func<E, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<E, Func<S, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<E, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<S, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<E, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<V, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<S, Func<E, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<S, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<C, Func<E, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<E, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<E, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<E, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<S, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<S, Func<C, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<S, Func<E, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<A, Action<A>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<E, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<C, Func<V, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<V, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<V, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<A, Func<E, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<A, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<S, Func<E, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<V, Func<S, Func<V, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<C, Func<S, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<V, Func<A, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<V, Func<S, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<C, Func<V, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<V, Func<S, Func<V, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<V, Func<S, Func<C, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<E, Func<S, Func<E, Func<V, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<S, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<S, Func<E, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<S, Func<C, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<S, Func<E, Action<V>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<S, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<S, Func<E, Func<V, Func<C, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<S, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<S, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<S, Func<C, Func<A, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<E, Func<E, Func<S, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<S, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<E, Func<C, Func<S, Func<C, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<V, Func<S, Func<C, Func<A, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<V, Func<S, Func<E, Func<A, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<S, Func<E, Func<E, Func<V, Func<C, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<S, Func<V, Func<C, Func<V, Action<A>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<S, Func<E, Func<E, Func<V, Func<C, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<S, Func<V, Func<C, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<E, Func<S, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<E, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<S, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<E, Func<E, Func<V, Func<E, Func<E, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<S, Func<V, Func<E, Func<E, Func<S, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<S, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<V, Func<C, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<E, Action<A>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<S, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<C, Func<S, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<E, Func<S, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<C, Func<S, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<C, Func<S, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<C, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<E, Func<V, Func<C, Func<S, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<E, Func<V, Func<S, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<V, Func<S, Func<E, Func<A, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<V, Func<S, Func<E, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<V, Func<E, Func<V, Func<C, Func<V, Func<S, Func<C, Func<A, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<V, Func<S, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<V, Func<S, Func<V, Func<C, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<A, Func<E, Func<E, Func<V, Func<S, Func<E, Func<V, Func<C, Func<V, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match3(Func<S, Func<E, Action<C>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<S, Func<C, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<E, Func<S, Func<E, Action<C>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<S, Func<C, Action<E>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match4(Func<S, Func<E, Func<C, Action<S>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<S, Func<E, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<S, Func<C, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<S, Func<E, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<C, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<S, Func<V, Action<V>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<S, Func<C, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<S, Func<C, Func<E, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<C, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<E, Action<S>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<E, Func<E, Func<V, Func<S, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<E, Func<V, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<E, Func<S, Func<C, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<C, Func<E, Func<S, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<E, Func<S, Func<E, Action<C>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match5(Func<C, Func<E, Func<S, Func<C, Action<E>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<E, Func<S, Func<C, Func<E, Func<C, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<S, Func<C, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<S, Func<E, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<S, Func<C, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<S, Func<E, Action<E>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<S, Func<E, Func<C, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<C, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<S, Func<E, Func<E, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<S, Func<E, Func<E, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<S, Func<C, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<S, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<C, Func<S, Func<C, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<C, Func<C, Func<E, Func<S, Func<C, Func<V, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<S, Func<E, Func<C, Func<E, Func<C, Action<E>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<C, Func<S, Func<E, Func<C, Func<E, Action<V>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<S, Func<E, Func<C, Func<E, Func<C, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match7(Func<E, Func<C, Func<S, Func<E, Func<C, Func<E, Action<C>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<E, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<S, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<E, Func<C, Func<E, Func<E, Func<C, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match8(Func<E, Func<S, Func<S, Func<E, Func<C, Func<E, Func<S, Action<E>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<C, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<E, Func<E, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<E, Action<V>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<C, Func<S, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<E, Func<S, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<C, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<C, Func<E, Func<S, Func<E, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match9(Func<E, Func<C, Func<E, Func<C, Func<E, Func<S, Func<C, Func<V, Action<C>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<E, Func<E, Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<E, Func<S, Func<E, Func<C, Func<C, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }

        public BinderComponent Match10(Func<V, Func<E, Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<E>>>>>>>>>> pattern) {
            throw new NotImplementedException();
        }
    }
}
