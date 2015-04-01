using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.LexicalStructures.Structural
{
    public interface ICompositeLexical<out TLexical> : ILexical, IUnitLexical where TLexical : ILexical
    {
        IEnumerable<TLexical> Components { get; }
    }
 
}
