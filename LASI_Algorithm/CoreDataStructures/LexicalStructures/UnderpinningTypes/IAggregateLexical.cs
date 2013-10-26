using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines the requirements for a Lexical type which is simultaneously a single element and a composition of other Lexical elements.
    /// </summary>
    /// <typeparam name="TLexical">The type of the Lexical elements of which the IAggregateLexical implementation is composed. 
    /// This can be any Type which implements the ILexical interface.
    /// This type parameter is covariant. That is,
    /// you can use either the type you specified or any type that is more derived.
    /// </typeparam>
    public interface IAggregateLexical<out TLexical> : ILexical, IEnumerable<TLexical> where TLexical : ILexical
    {
    }
}
