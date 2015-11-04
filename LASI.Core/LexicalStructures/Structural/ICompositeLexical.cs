using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.LexicalStructures
{
    /// <summary>
    /// Represents a single, unit viewable lexical element which is comprised of a collections of lexical components.
    /// </summary>
    /// <typeparam name="TLexical">The type of the components.</typeparam>
    public interface ICompositeLexical<out TLexical> : ILexical, IUnitLexical where TLexical : ILexical
    {
        /// <summary>Gets the components which comprise the ICompositeLexical&lt;<typeparamref name="TLexical"/>&gt;.</summary>
        IEnumerable<TLexical> Components { get; }
    }
}
