using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class Concurrency
    {
        static Concurrency()
        {
            CurrentMax = GetDefaultParallellMax();
        }

        public static int CurrentMax
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the default maxiumum number of logical CPU cores, based on the executing hardware, the document analysis process is allowed to utilize.
        /// </summary>
        /// <returns>The default maxiumum number of logical CPU cores the document analysis process is allowed to utilize.</returns>
        private static int GetDefaultParallellMax()
        {
            var logicalCPUs = System.Environment.ProcessorCount;
            return logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 2;
        }
        public static void SetResourceUsageMode(ResourceUsageMode mode)
        {
            var logicalCPUs = System.Environment.ProcessorCount;
            CurrentMax = mode == ResourceUsageMode.High ?
                logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 1 :
               mode == ResourceUsageMode.Stamrad ?
                logicalCPUs < 3 ? 2 : logicalCPUs - 2 :
               mode == ResourceUsageMode.Low ?
                logicalCPUs < 4 ? 1 : logicalCPUs - 3 : GetDefaultParallellMax();
        }

    }
}
