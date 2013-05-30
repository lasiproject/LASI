using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.SyntacticInterfaces
{
    public interface IQuantifier
    {
        IQuantifiable Quantifies {
            get;
            set;
        }

    }
}
