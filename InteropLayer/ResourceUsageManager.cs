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
    public static class ResourceUsageManager
    {
        /// <summary>
        /// Sets the overall performance level based on the provided enumeration value, providing for coarse grained adjustments to the CPU and RAM consumption.
        /// </summary>
        /// <param name="mode">The PerformanceLevel value indicating the new performance and resource usage settings to adopt.</param>
        public static void SetPerformanceLevel(Mode mode) {
            Concurrency.SetFromPerformanceMode(mode);
            Memory.SetFromPerformanceMode(mode);
        }
        /// <summary>
        /// Returns a ResourceInfo instance containing the current resource usage percentages for the machine hosting the application.
        /// </summary>
        /// <returns>A ResourceInfo instance containing the current resource usage percentages for the machine hosting the application.</returns>w
        public static ResourceSample GetCurrentUsage() {
            return new ResourceSample {
                MemoryUsage = new System.Diagnostics.PerformanceCounter("Memory", "% Committed Bytes In Use").NextValue(),
                CpuUsage = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total").NextValue(),
                TimeWhenTaken = DateTime.Now
            };
        }
        /// <summary>
        /// Raised when less than the minimum amount of available RAM, in MB, remains.
        /// </summary>
        public static event EventHandler<MemoryThresholdExceededEventArgs> MemoryThresholdExceeded {
            add { Memory.MemoryUsageCritical += value; }
            remove { Memory.MemoryUsageCritical -= value; }
        }

        static ResourceUsageManager() {
            BindDefaultHandlers();
        }

        private static void BindDefaultHandlers() {
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
            /// Normal resource usage indicates a modest allocation and consumption of available system resources.
            /// </summary>
            Normal,
            /// <summary>
            /// High resource usage indicates a conservative allocation and consumption of available system resources.
            /// </summary>
            Low,
        }
    }
    /// <summary>
    /// Represents a resource usage sample.
    /// </summary>
    public struct ResourceSample
    {
        /// <summary>
        /// Gets the current CPU usage % of the machine hosting the application.        
        /// </summary>
        public float CpuUsage { get; internal set; }
        /// <summary>
        /// Gets the current Memory usage % of the machine hosting the application.        
        /// </summary>
        public float MemoryUsage { get; internal set; }
        /// <summary>
        /// Gets the local time of the machine hosting the application when the sample was taken.
        /// </summary> 
        public DateTime TimeWhenTaken { get; internal set; }
    }

}
