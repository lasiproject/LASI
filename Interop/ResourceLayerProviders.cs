namespace LASI.Interop.ResourceManagement
{
    using System;
    using MemoryEventArgs = MemoryThresholdExceededEventArgs;
    using MemoryHandler = System.EventHandler<MemoryThresholdExceededEventArgs>;
    using Mode = PerformanceProfile;
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

        private static MB GetAvailableMemory() => (MB)new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue();

    }
}