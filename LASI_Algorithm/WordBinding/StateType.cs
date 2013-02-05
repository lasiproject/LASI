using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.WordBinding
{
    enum StateType
    : byte
    {
        Initial,
        ExitAllowed,
        ExitRequired,
        Failed
    }
}
