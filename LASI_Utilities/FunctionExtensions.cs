using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public static class FunctionExtensions
    {
        /// <summary>
        /// Composes two functions which map to and from a single type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f">The first function, must take an argument of type T and return an argument of type T</param>
        /// <param name="g">The second function, must take an argument of type T and return an argument of type T</param>
        /// <returns>A function which when invoked produces the result of calling g on the result of calling f. 
        /// </returns>
        /// <example></example>for f(x) = 1/x and g(x) = 2x, g(f(x) = 2/x, so g.Compose(f)(3) = 2/3
        public static Func<T, T> Compose<T>(this Func<T, T> f, Func<T, T> g) {
            return t => f(g(t));
        }
        public static Func<T, T> Compose<T>(params Func<T, T>[] fs) {
            Func<T, T> result = fs.First();
            foreach (var f in fs.Skip(1))
                result = f.Compose(result);
            return result;
        }
    }
}
