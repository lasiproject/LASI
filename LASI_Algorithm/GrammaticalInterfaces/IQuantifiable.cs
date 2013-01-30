using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public interface IQuantifiable
    {
        Quantifier Quantifier {
            get;
            set;
        }
    }
}
