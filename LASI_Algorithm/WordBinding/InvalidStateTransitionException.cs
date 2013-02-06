using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.WordBinding
{
    class InvalidStateTransitionException : Exception
    {
        public InvalidStateTransitionException(string p)
            : base(p) {
        }
    }
}
