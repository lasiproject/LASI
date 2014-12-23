namespace LASI.Interop.ResourceManagement
{
    using System;
    using System.Collections.Generic;
    using MemoryEventArgs = MemoryThresholdExceededEventArgs;
    using MemoryHandler = System.EventHandler<MemoryThresholdExceededEventArgs>;
    using Mode = UsageManager.Mode;

    /// <summary>
    /// Centralizes management and control of the concurrency level of concurrent operations.
    /// </summary>
    public static class Concurrency
    {
        /// <summary>
        /// Sets the maximum allowed Concurrency level based on the supplied ResourceUsageMode.
        /// </summary>
        /// <param name="mode">
        /// The ResourceUsageMode value from which to determine concurrency settings.
        /// </param>
        public static void SetByPerformanceMode(Mode mode) {
            var logicalCores = Environment.ProcessorCount; // Get the number of logical cores.
            var calculate = concurrencyCalculationMap[mode];
            Max = calculate(logicalCores);
        }

        private static readonly IDictionary<Mode, Func<int, int>> concurrencyCalculationMap = new Dictionary<Mode, Func<int, int>>
        {
            [Mode.High] = cores => cores < 3 ? cores : cores - 1,
            [Mode.Normal] = cores => cores < 3 ? cores : cores - 2,
            [Mode.Low] = cores => cores < 4 ? 1 : cores - 3,
            [0] = cores => ComputeDefaultMax()
        };

        /// <summary>
        /// Gets the default maximum number of logical CPU cores, based on the executing hardware,
        /// the document analysis process is allowed to utilize.
        /// </summary>
        /// <returns>
        /// The default maximum number of logical CPU cores the document analysis process is allowed
        /// to utilize.
        /// </returns>
        private static int ComputeDefaultMax() {
            var logicalCores = Environment.ProcessorCount;
            return logicalCores < 3 ? logicalCores : logicalCores - 1;
        }

        static Concurrency() {
            Max = ComputeDefaultMax();
            // This is critical for LASI.Core to obey the same concurrency constraints as its client
            // assemblies without a circular dependency
            Core.Reporting.Configuation.ConfigureConcurrency(() => Max);
        }
        /// <summary>
        /// Gets the maximum allowed Concurrency level for Parallel operations.
        /// </summary>
        public static int Max { get; private set; }

    }

    /// <summary>
    /// Centralizes management and control of the memory (RAM) consumed by lookup caches.
    /// </summary>
    public static class Memory
    {
        /// <summary>
        /// Sets the maximum size to which the lookup caches can collectively grow based on the
        /// specified ResourceMode
        /// </summary>
        /// <param name="mode">
        /// The ResourceMode which will be used to determine the maximum collective cache size
        /// </param>
        public static void SetByPerformanceMode(Mode mode) {
            MinRamThreshold = mode == Mode.High ? (MB)2048 : mode == Mode.Normal ? (MB)3072 : (MB)4096;
        }

        /// <summary>
        /// The maximum number of MBs to which the lookup caches can collectively grow.
        /// </summary>
        public static MB MinRamThreshold { get; private set; }

        static Memory() {
            // Default to a medium or "normal" memory usage profile if none is specified.
            SetByPerformanceMode(Mode.Normal);
            var checkIntervalTimer = new System.Timers.Timer(10000);
            checkIntervalTimer.Start();
            checkIntervalTimer.Elapsed += (s, e) => {
                var available = GetAvailableMemory();
                if (available < MinRamThreshold) {
                    MemoryCritical(null, new MemoryEventArgs
                    {
                        RemainingMemory = available,
                        TriggeringThreshold = MinRamThreshold
                    });
                }
            };
        }

        /// <summary>
        /// Configures a custom memory usage event and sets up its periodic orchestration and execution.
        /// </summary>
        /// <param name="threshold">
        /// The condition, in terms of MegaBytes available to the Operating System, which must be
        /// met for the event to fire.
        /// </param>
        /// <param name="frequency">
        /// The number of milliseconds between checks.
        /// </param>
        /// <param name="availableRamDecreased">
        /// The function to be invoked when available memory drops below the specified threshold.
        /// </param>
        /// <param name="availableRamIncreased">
        /// The function to be invoked when available memory is least 128 MegaBytes greater than the
        /// specified threshold.
        /// </param>
        private static void ConfigureRamEvent(MB threshold, uint frequency, MemoryHandler availableRamDecreased, MemoryHandler availableRamIncreased) {
            if (frequency < 1) { throw new ArgumentOutOfRangeException("frequency", "event frequency may not be 0"); }
            var increased = availableRamDecreased ?? delegate { };
            var backingTimer = new System.Timers.Timer(frequency);
            backingTimer.Elapsed += (sender, e) => {
                var available = GetAvailableMemory();
                if (available < threshold) {
                    availableRamDecreased(
                        null,
                        new MemoryEventArgs
                        {
                            RemainingMemory = available,
                            TriggeringThreshold = threshold
                        }
                    );
                } else if (available >= threshold + 128) {
                    increased(null, new MemoryEventArgs { RemainingMemory = available, TriggeringThreshold = threshold });
                }
            };
        }

        /// <summary>
        /// Raised when less than the minimum amount of available ram remains.
        /// </summary>
        public static event MemoryHandler MemoryCritical = delegate { };

        private static MB GetAvailableMemory() {
            return (MB)new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue();
        }
    }
}