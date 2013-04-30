using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class Concurrency
    {
        static Concurrency() {

            CurrentMax = GetDefaultParallellMax();
        }

        public static int CurrentMax {
            get;
            set;
        }
        private static int GetDefaultParallellMax() {
            var logicalCPUs = System.Environment.ProcessorCount;
            return logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 2;
        }

    }
}
