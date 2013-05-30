using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.SyntacticInterfaces
{
    interface IConflatable
    {
        IConflatable ConflatedWith {
            get;
            set;
        }
    }
}
