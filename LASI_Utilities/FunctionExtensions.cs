using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public static class FunctionExtensions
    {
        public static Func<U, T> Compose<T, U, R>(this Func<R, T> f, Func<U, R> g) {
            return t => f(g(t));
        }
        public static Func<T, T> Compose<T>(this Func<T, T> func, params Func<T, T>[] fs) {
            Func<T, T> result = func;
            foreach (var f in fs.Skip(1))
                result = f.Compose(result);
            return result;
        }



    }
}
