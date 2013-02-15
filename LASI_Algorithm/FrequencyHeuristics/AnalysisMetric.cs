using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.FrequencyHeuristics
{
    public struct Metric
    {
        public IEnumerable<IEntity> MostSignificantEntities {
            get;
            set;
        }
        public IEnumerable<IAction> MostSignificantActions {
            get;
            set;
        }
    }
}
