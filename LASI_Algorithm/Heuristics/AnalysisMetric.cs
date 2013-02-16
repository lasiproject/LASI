using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.Heuristics
{
    public struct Metric
    {
        public IEnumerable<CountedEntityResult> MostSignificantEntities {
            get;
            set;
        }
        public IEnumerable<CountedActionResult> MostSignificantActions {
            get;
            set;
        }
    }
    public struct CountedEntityResult
    {
        public int Count {
            get;
            set;
        }
        public IEntity Entity {
            get;
            set;
        }
    }
    public struct CountedActionResult
    {
        public int Count {
            get;
            set;
        }
        public IAction Action {
            get;
            set;
        }
    }
}
