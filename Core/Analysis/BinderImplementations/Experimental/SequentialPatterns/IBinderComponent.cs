using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
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
        BinderComponent Bind(IEnumerable<L> stream);

        BinderComponent Match3(Func<E, Func<V, Action<E>>> pattern);
        BinderComponent Match3(Func<E, Func<V, Action<R>>> pattern);
        BinderComponent Match3(Func<R, Func<V, Action<E>>> pattern);
        BinderComponent Match3(Func<R, Func<V, Action<R>>> pattern);
        BinderComponent Match3(Func<E, Func<V, Action<C>>> pattern);
        BinderComponent Match3(Func<E, Func<E, Action<E>>> pattern);
        BinderComponent Match3(Func<V, Func<E, Action<C>>> pattern);
        BinderComponent Match3(Func<C, Func<V, Action<C>>> pattern);
        BinderComponent Match3(Func<C, Func<E, Action<E>>> pattern);
        BinderComponent Match3(Func<C, Func<E, Action<C>>> pattern);
        BinderComponent Match3(Func<R, Func<V, Action<C>>> pattern);
        BinderComponent Match4(Func<E, Func<V, Func<C, Action<D>>>> pattern);
        BinderComponent Match4(Func<E, Func<E, Func<R, Action<E>>>> pattern);
        BinderComponent Match4(Func<E, Func<V, Func<E, Action<C>>>> pattern);
        BinderComponent Match4(Func<E, Func<V, Func<C, Action<E>>>> pattern);
        BinderComponent Match4(Func<E, Func<V, Func<E, Action<E>>>> pattern);
        BinderComponent Match4(Func<E, Func<E, Func<V, Action<E>>>> pattern);
        BinderComponent Match4(Func<E, Func<V, Func<E, Action<D>>>> pattern);
        BinderComponent Match4(Func<V, Func<V, Func<C, Action<E>>>> pattern);
        BinderComponent Match4(Func<E, Func<V, Func<R, Action<V>>>> pattern);
        BinderComponent Match4(Func<E, Func<E, Func<R, Action<V>>>> pattern);
        BinderComponent Match4(Func<E, Func<E, Func<R, Action<P>>>> pattern);
        BinderComponent Match4(Func<V, Func<V, Func<R, Action<V>>>> pattern);
        BinderComponent Match4(Func<E, Func<D, Func<R, Action<V>>>> pattern);
        BinderComponent Match4(Func<E, Func<E, Func<D, Action<E>>>> pattern);
        BinderComponent Match4(Func<V, Func<D, Func<R, Action<V>>>> pattern);
        BinderComponent Match4(Func<R, Func<R, Func<D, Action<R>>>> pattern);
        BinderComponent Match5(Func<E, Func<V, Func<C, Func<D, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<V, Func<E, Func<C, Action<R>>>>> pattern);
        BinderComponent Match5(Func<E, Func<V, Func<E, Func<C, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<V, Func<R, Func<C, Action<R>>>>> pattern);

        BinderComponent Match5(Func<E, Func<V, Func<E, Func<C, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<R, Func<E, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<C, Func<D, Action<R>>>>> pattern);
        BinderComponent Match5(Func<E, Func<R, Func<E, Func<C, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<R, Func<D, Action<R>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<E, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<D, Func<C, Action<R>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<V, Func<C, Action<V>>>>> pattern);
        BinderComponent Match5(Func<E, Func<V, Func<E, Func<E, Action<V>>>>> pattern);
        BinderComponent Match5(Func<E, Func<V, Func<E, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<D, Func<V, Func<C, Action<R>>>>> pattern);
        BinderComponent Match5(Func<E, Func<C, Func<E, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<V, Func<E, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<D, Func<V, Func<R, Action<A>>>>> pattern);
        BinderComponent Match5(Func<E, Func<D, Func<V, Func<E, Action<D>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<V, Func<P, Action<P>>>>> pattern);
        BinderComponent Match5(Func<V, Func<V, Func<C, Func<D, Action<R>>>>> pattern);
        BinderComponent Match5(Func<E, Func<R, Func<D, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<C, Func<R, Action<E>>>>> pattern);
        BinderComponent Match5(Func<V, Func<R, Func<D, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<V, Func<D, Func<E, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<E, Func<C, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<D, Func<P, Func<E, Action<V>>>>> pattern);
        BinderComponent Match5(Func<E, Func<D, Func<P, Func<V, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<A, Func<V, Func<E, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<A, Func<V, Func<D, Action<E>>>>> pattern);
        BinderComponent Match5(Func<E, Func<A, Func<V, Func<E, Action<D>>>>> pattern);
        BinderComponent Match5(Func<R, Func<R, Func<V, Func<R, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<D, Func<P, Func<V, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<A, Func<V, Func<R, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<R, Func<A, Func<V, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<A, Func<V, Func<D, Action<R>>>>> pattern);
        BinderComponent Match5(Func<R, Func<A, Func<V, Func<R, Action<D>>>>> pattern);
        BinderComponent Match6(Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>> pattern);
        BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<R, Action<A>>>>>> pattern);
        BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<E, Action<D>>>>>> pattern);
        BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<R, Action<D>>>>>> pattern);
        BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<E, Action<D>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<E, Func<C, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<C, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<D, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<D, Action<R>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<C, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<C, Func<R, Func<V, Func<R, Func<P, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<R, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Action<A>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<C, Func<V, Func<D, Func<R, Func<A, Action<P>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Action<R>>>>>>> pattern);
        BinderComponent Match7(Func<E, Func<C, Func<V, Func<D, Func<R, Func<A, Action<R>>>>>>> pattern);
        BinderComponent Match8(Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern);
        BinderComponent Match8(Func<E, Func<V, Func<E, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern);
        BinderComponent Match8(Func<E, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>> pattern);
        BinderComponent Match8(Func<E, Func<V, Func<E, Func<C, Func<D, Func<E, Func<C, Action<E>>>>>>>> pattern);
        BinderComponent Match8(Func<E, Func<V, Func<V, Func<D, Func<C, Func<E, Func<V, Action<E>>>>>>>> pattern);
        BinderComponent Match9(Func<E, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>> pattern);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<D, Func<R, Action<P>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<D, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<E, Func<E, Action<P>>>>>>>>> pattern);
        BinderComponent Match9(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<E, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<E, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<R, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<E, Func<E, Func<R, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match9(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>>>> pattern);
        BinderComponent Match10(Func<D, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<A, Action<E>>>>>>>>>> pattern);
        BinderComponent Match10(Func<E, Func<C, Func<E, Func<E, Func<A, Func<V, Func<D, Func<R, Func<C, Action<E>>>>>>>>>> pattern);
        BinderComponent Match10(Func<P, Func<E, Func<E, Func<A, Func<V, Func<C, Func<D, Func<R, Func<D, Action<E>>>>>>>>>> pattern);
    }
}
