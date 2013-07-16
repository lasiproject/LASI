using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Centrailizes management and control of the concurrency level of concurrent operations.
    /// </summary>
    public static class Concurrency
    {
        static Concurrency() {
            CurrentMax = GetDefaultParallelMax();
        }
        /// <summary>
        /// Gets the maximum allowed Concurrency level for Parallel operations.
        /// </summary>
        public static int CurrentMax {
            get;
            private set;
        }
        /// <summary>
        /// Gets the default maxiumum number of logical CPU cores, based on the executing hardware, the document analysis process is allowed to utilize.
        /// </summary>
        /// <returns>The default maxiumum number of logical CPU cores the document analysis process is allowed to utilize.</returns>
        private static int GetDefaultParallelMax() {
            var logicalCPUs = System.Environment.ProcessorCount;
            return logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 2;
        }
        /// <summary>
        /// Sets the maximum allowed Concurrency level based on the supplied ResourceUsageMode.
        /// </summary>
        /// <param name="mode">The ResourceUsageMode value from which to determine concurrency settings.</param>
        public static void SetConcurrencyLevelByResourceUsageMode(ResourceUsageMode mode) {
            var logicalCPUs = System.Environment.ProcessorCount;
            CurrentMax = mode == ResourceUsageMode.High ?
                logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 1 :
               mode == ResourceUsageMode.Normal ?
                logicalCPUs < 3 ? 2 : logicalCPUs - 2 :
               mode == ResourceUsageMode.Low ?
                logicalCPUs < 4 ? 1 : logicalCPUs - 3 : GetDefaultParallelMax();
        }

    }
}
