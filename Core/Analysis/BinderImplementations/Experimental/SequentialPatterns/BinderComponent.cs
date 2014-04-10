using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    using E = LASI.Core.IEntity;
    using V = LASI.Core.IVerbal;
    using C = LASI.Core.IConjunctive;
    using D = LASI.Core.IDescriptor;
    using R = LASI.Core.IReferencer;
    using A = LASI.Core.IAdverbial;
    using P = LASI.Core.IPrepositional;

    public class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>
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
    }
}
