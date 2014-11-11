using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    interface IConflatable<TLexical> where TLexical : ILexical
    {
        IConflatable<TLexical> ConflatedWith {
            get;
            set;
        }
    }
}
