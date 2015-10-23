using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.LexicalStructures.Structural
{
    interface ILinkedUnitLexical<out TLexical> : IUnitLexical
    {
        TLexical Previous { get; }
        TLexical Next { get; }
    }
}
