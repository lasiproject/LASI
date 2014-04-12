﻿#pragma warning disable 1591
using System;
using System.Collections.Generic;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    // Alias types to shorten name and thus file size.
    using L = LASI.Core.ILexical;
    interface IBinderComponent<E, V, C, D, R, A, P>
       where E : class, L
       where V : class, L
       where C : class, L
       where D : class, L
       where R : class, L
       where A : class, L
       where P : class, L
    {

        BinderComponent Bind(IEnumerable<Phrase> stream);

        BinderComponent Match3(Func<E, Func<V, Action<E>>> p);
        BinderComponent Match3(Func<E, Func<V, Action<R>>> p);
        BinderComponent Match3(Func<R, Func<V, Action<E>>> p);
        BinderComponent Match3(Func<R, Func<V, Action<R>>> p);
        BinderComponent Match3(Func<E, Func<V, Action<C>>> p);
        BinderComponent Match3(Func<E, Func<E, Action<E>>> p);
        BinderComponent Match3(Func<V, Func<E, Action<C>>> p);
        BinderComponent Match3(Func<C, Func<V, Action<C>>> p);
        BinderComponent Match3(Func<C, Func<E, Action<E>>> p);
        BinderComponent Match3(Func<C, Func<E, Action<C>>> p);
        BinderComponent Match3(Func<R, Func<V, Action<C>>> p);
        BinderComponent Match4(Func<E, Func<V, Func<C, Action<D>>>> p);
        BinderComponent Match4(Func<E, Func<E, Func<R, Action<E>>>> p);
        BinderComponent Match4(Func<E, Func<V, Func<E, Action<C>>>> p);
        BinderComponent Match4(Func<E, Func<V, Func<C, Action<E>>>> p);
        BinderComponent Match4(Func<E, Func<V, Func<E, Action<E>>>> p);
        BinderComponent Match4(Func<E, Func<E, Func<V, Action<E>>>> p);
        BinderComponent Match4(Func<E, Func<V, Func<E, Action<D>>>> p);
        BinderComponent Match4(Func<V, Func<V, Func<C, Action<E>>>> p);
        BinderComponent Match4(Func<E, Func<V, Func<R, Action<V>>>> p);
        BinderComponent Match4(Func<E, Func<E, Func<R, Action<V>>>> p);
        BinderComponent Match4(Func<E, Func<E, Func<R, Action<P>>>> p);
        BinderComponent Match4(Func<V, Func<V, Func<R, Action<V>>>> p);
        BinderComponent Match4(Func<E, Func<D, Func<R, Action<V>>>> p);
        BinderComponent Match4(Func<E, Func<E, Func<D, Action<E>>>> p);
        BinderComponent Match4(Func<V, Func<D, Func<R, Action<V>>>> p);
        BinderComponent Match4(Func<R, Func<R, Func<D, Action<R>>>> p);
        BinderComponent Match5(Func<E, Func<V, Func<C, Func<D, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<V, Func<E, Func<C, Action<R>>>>> p);
        BinderComponent Match5(Func<E, Func<V, Func<E, Func<C, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<V, Func<R, Func<C, Action<R>>>>> p);

        BinderComponent Match5(Func<E, Func<V, Func<E, Func<C, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<R, Func<E, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<C, Func<D, Action<R>>>>> p);
        BinderComponent Match5(Func<E, Func<R, Func<E, Func<C, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<R, Func<D, Action<R>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<E, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<D, Func<C, Action<R>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<V, Func<C, Action<V>>>>> p);
        BinderComponent Match5(Func<E, Func<V, Func<E, Func<E, Action<V>>>>> p);
        BinderComponent Match5(Func<E, Func<V, Func<E, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<D, Func<V, Func<C, Action<R>>>>> p);
        BinderComponent Match5(Func<E, Func<C, Func<E, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<V, Func<E, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<D, Func<V, Func<R, Action<A>>>>> p);
        BinderComponent Match5(Func<E, Func<D, Func<V, Func<E, Action<D>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<V, Func<P, Action<P>>>>> p);
        BinderComponent Match5(Func<V, Func<V, Func<C, Func<D, Action<R>>>>> p);
        BinderComponent Match5(Func<E, Func<R, Func<D, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<C, Func<R, Action<E>>>>> p);
        BinderComponent Match5(Func<V, Func<R, Func<D, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<V, Func<D, Func<E, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<E, Func<C, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<D, Func<P, Func<E, Action<V>>>>> p);
        BinderComponent Match5(Func<E, Func<D, Func<P, Func<V, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<A, Func<V, Func<E, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<A, Func<V, Func<D, Action<E>>>>> p);
        BinderComponent Match5(Func<E, Func<A, Func<V, Func<E, Action<D>>>>> p);
        BinderComponent Match5(Func<R, Func<R, Func<V, Func<R, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<D, Func<P, Func<V, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<A, Func<V, Func<R, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<R, Func<A, Func<V, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<A, Func<V, Func<D, Action<R>>>>> p);
        BinderComponent Match5(Func<R, Func<A, Func<V, Func<R, Action<D>>>>> p);
        BinderComponent Match6(Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>> p);
        BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<R, Action<A>>>>>> p);
        BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<E, Action<D>>>>>> p);
        BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<R, Action<D>>>>>> p);
        BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<E, Action<D>>>>>> p);

        BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<E, Func<C, Action<E>>>>>>> p);
        BinderComponent Match7(Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>> p);
        BinderComponent Match7(Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>> p);
        BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>> p);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<D, Action<E>>>>>>> p);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<D, Action<R>>>>>>> p);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>> p);
        BinderComponent Match7(Func<R, Func<C, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>> p);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>> p);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<E>>>>>>> p);
        BinderComponent Match7(Func<R, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>> p);
        BinderComponent Match7(Func<E, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>> p);
        BinderComponent Match7(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Action<A>>>>>>> p);
        BinderComponent Match7(Func<E, Func<C, Func<V, Func<D, Func<R, Func<A, Action<P>>>>>>> p);
        BinderComponent Match7(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Action<R>>>>>>> p);
        BinderComponent Match7(Func<E, Func<C, Func<V, Func<D, Func<R, Func<A, Action<R>>>>>>> p);

        BinderComponent Match8(Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> p);
        BinderComponent Match8(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> p);
        BinderComponent Match8(Func<E, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> p);
        BinderComponent Match8(Func<E, Func<V, Func<E, Func<C, Func<D, Func<E, Func<C, Action<E>>>>>>>> p);
        BinderComponent Match8(Func<E, Func<V, Func<V, Func<D, Func<C, Func<E, Func<V, Action<E>>>>>>>> p);
        BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>> p);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<D, Func<R, Action<P>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<D, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<E, Func<E, Action<P>>>>>>>>> p);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<E, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<E, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<R, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<E, Func<R, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p);
        BinderComponent Match10(Func<D, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<A, Action<E>>>>>>>>>> p);
        BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<A, Func<V, Func<D, Func<R, Func<C, Action<E>>>>>>>>>> p);
        BinderComponent Match10(Func<P, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<D, Action<E>>>>>>>>>> p);
    }
}

#pragma warning restore 1591