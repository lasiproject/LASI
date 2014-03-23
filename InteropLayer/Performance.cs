using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Interop
{
    /// <summary>
    /// Controls global performance and resource usage settings.
    /// </summary>
    public static class Performance
    {
        /// <summary>
        /// Sets the overall performance level based on the provided enumeration value, providing for coarse grained adjustments to the CPU and RAM consumption.
        /// </summary>
        /// <param name="mode">The PerformanceLevel value indicating the new performance and resource usage settings to adobt.</param>
        public static void SetPerformanceLevel(Mode mode) {
            Concurrency.SetFromPerformanceMode(mode);
            Memory.SetFromPerformanceMode(mode);
        }


        /// <summary>
        /// Raised when less than the minimum amount of available RAM, in MB, remains.
        /// </summary>
        public static event EventHandler<MemoryThresholdExceededEventArgs> MemoryThresholdExceeded {
            add {
                Memory.MemoryUsageCritical += value;
            }
            remove { Memory.MemoryUsageCritical -= value; }
        }

        static Performance() {
            MemoryThresholdExceeded += (sender, e) => {
                LASI.Core.Heuristics.Lookup.ClearNounCache();
                LASI.Core.Heuristics.Lookup.ClearVerbCache();
                LASI.Core.Heuristics.Lookup.ClearAdjectiveCache();
                LASI.Core.Heuristics.Lookup.ClearAdverbCache();
            };
        }


        /// <summary>
        /// Broadly specifies the various resource usage profiles of the program.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// High resource usage indicates a liberal allocation and consumption of available system resources.
            /// </summary>
            High,
            /// <summary>
            /// Noraml resource usage indicates a modest allocation and consumption of available system resources.
            /// </summary>
            Normal,
            /// <summary>
            /// High resource usage indicates a conservative allocation and consumption of available system resources.
            /// </summary>
            Low,
        }
    }

}
