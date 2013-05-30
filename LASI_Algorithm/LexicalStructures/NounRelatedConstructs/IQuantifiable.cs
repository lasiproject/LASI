using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.SyntacticInterfaces
{
    public interface IQuantifiable
    {
        Quantifier Quantifier {
            get;
            set;
        }
    }
}
