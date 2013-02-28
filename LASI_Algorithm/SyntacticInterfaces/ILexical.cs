using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public interface ILexical
    {
        string Text {
            get;
        }
        Dictionary<Weighting.WeightKind, Weighting.Weight> Weights {
            get;
            set;
        }
    }
}
