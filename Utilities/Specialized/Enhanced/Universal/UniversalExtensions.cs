using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Specialized.Option;

namespace LASI.Utilities.Specialized.Enhanced.Universal
{
    public static class UniversalExtensions
    {
        /// <summary>
        /// Lifts the given value into an enumerable. Returns a singleton sequence containing the
        /// single value unless it is <c>null</c> in which case an empty sequence will be produced.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the value to lift and the return type of the resulting enumerable.
        /// </typeparam>
        /// <param name="value">The value to lift into an enumerable.</param>
        /// <returns>
        /// A singleton sequence containing the single value unless it is <c>null</c> in which
        /// case an empty sequence will be produced.
        /// </returns>
        public static IEnumerable<T> Lift<T>(this T value)
        {
            yield return value;
        }
    }
}