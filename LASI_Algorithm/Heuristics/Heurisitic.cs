using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Heuristics
{
    public abstract class Heuristic
    {
        public abstract Metric Analyse();
        public abstract Task<Metric> AnalyseAsync();
        public abstract int MaxResultsPerCategory {
            get;
        }

    }

   
}
