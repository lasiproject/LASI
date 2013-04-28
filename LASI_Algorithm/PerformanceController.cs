using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class PerformanceController
    {
        static PerformanceController() {
            MaxParallellism = 3;
        }

        public static int MaxParallellism {
            get;
            set;
        }
    }
}
