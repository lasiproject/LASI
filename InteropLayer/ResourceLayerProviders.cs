using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop
{
    using Mode = ResourceUsageManager.Mode;
    using MemoryHandler = EventHandler<MemoryThresholdExceededEventArgs>;
    /// <summary>
    /// Centrailizes management and control of the concurrency level of concurrent operations.
    /// </summary>
    public static class Concurrency
    {
        /// <summary>
        /// Sets the maximum allowed Concurrency level based on the supplied ResourceUsageMode.
        /// </summary>
        /// <param name="mode">The ResourceUsageMode value from which to determine concurrency settings.</param>
        public static void SetFromPerformanceMode(Mode mode) {
            var logicalCPUs = System.Environment.ProcessorCount;
            Max = mode == Mode.High ?
                logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 1 :
               mode == Mode.Normal ?
                logicalCPUs < 3 ? 2 : logicalCPUs - 2 :
               mode == Mode.Low ?
                logicalCPUs < 4 ? 1 : logicalCPUs - 3 : GetDefaultParallelMax();
        }
        /// <summary>
        /// Gets the default maxiumum number of logical CPU cores, based on the executing hardware, the document analysis process is allowed to utilize.
        /// </summary>
        /// <returns>The default maxiumum number of logical CPU cores the document analysis process is allowed to utilize.</returns>
        private static int GetDefaultParallelMax() {
            var logicalCPUs = System.Environment.ProcessorCount;
            return logicalCPUs < 3 ? logicalCPUs : logicalCPUs - 1;
        }
        /// <summary>
        /// Gets the maximum allowed Concurrency level for Parallel operations.
        /// </summary>
        public static int Max { get; private set; }

        static Concurrency() {
            Max = GetDefaultParallelMax();
            // This is critical for LASI.Core to obey the same concurrency contstraints as its client assemblies without a circular dependency
            LASI.Core.Interop.Configuation.ConfigureConcurrency(() => Max);
        }

    }
    /// <summary>
    /// Centrailizes management and control of the memory (RAM) consumed by lookup caches.
    /// </summary>
    public static class Memory
    {

        /// <summary>
        /// Sets the maximum size to which the lookup caches can collectively grow based on the specified ResourceMode
        /// </summary>
        /// <param name="mode">The ResourceMode which will be used to determine the maximum collective cache size</param>
        public static void SetFromPerformanceMode(Mode mode) {
            MinRamThreshold = mode == Mode.High ? (MB)2048 : mode == Mode.Normal ? (MB)3072 : (MB)4096;

        }
        /// <summary>
        /// The maxiumum number of MBs to which the lookup caches can collectively grow.
        /// </summary>
        public static MB MinRamThreshold { get; private set; }

        static Memory() {
            // Default to a medium or "normal" memory usage profile if none is specified.
            SetFromPerformanceMode(ResourceUsageManager.Mode.Normal);
            var checkIntervalTimer = new System.Timers.Timer(10000);
            checkIntervalTimer.Start();
            checkIntervalTimer.Elapsed += (sender, e) => {
                var available = GetAvailableMemory();
                if (available < MinRamThreshold) {
                    MemoryUsageCritical(null, new MemoryThresholdExceededEventArgs {
                        RemainingMemory = available,
                        TriggeringThreshold = MinRamThreshold
                    });
                }
            };
        }
        /// <summary>
        /// Configures a custom memory usage event and sets up its periodic orchestratation and execution.
        /// </summary>
        /// <param name="threshold">The condition, in terms of MegaBytes available to the Operating System, which must be met for the event to fire.</param>
        /// <param name="frequency">The number of miliseconds between checks.</param>
        /// <param name="availableRamDecreased">The function to be invoked when available memory drops below the specified threshold.</param>
        /// <param name="availableRamIncreased">The function to be invoked when available memory is least 128 MegaBytes greater than the specified threshold.</param>
        static void ConfigureRamEvent(MB threshold, uint frequency, MemoryHandler availableRamDecreased, MemoryHandler availableRamIncreased) {
            if (frequency < 1) { throw new ArgumentOutOfRangeException("frequency", "event frequency may not be 0"); }
            var increased = availableRamDecreased ?? delegate { };
            var backingTimer = new System.Timers.Timer(frequency);
            backingTimer.Elapsed += (sender, e) => {
                var available = GetAvailableMemory();
                if (available < threshold) {
                    availableRamDecreased(null,
                        new MemoryThresholdExceededEventArgs {
                            RemainingMemory = available,
                            TriggeringThreshold = threshold
                        });
                }
                else if (available >= threshold + 128) {
                    increased(null, new MemoryThresholdExceededEventArgs { RemainingMemory = available, TriggeringThreshold = threshold });
                }
            };
        }

        /// <summary>
        /// Raised when less than the minimum amount of available ram remains.
        /// </summary>
        public static event MemoryHandler MemoryUsageCritical = delegate { };
        private static MB GetAvailableMemory() {
            return (MB)(uint)new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue();
        }

       
    }

}
