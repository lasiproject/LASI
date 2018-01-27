using LASI.Interop.ProgressReporting;
using MemoryHandler = System.EventHandler<LASI.Interop.ProgressReporting.MemoryThresholdExceededEventArgs>;

namespace LASI.Interop
{
    /// <summary>
    /// Centralizes management and control of the memory (RAM) consumed by lookup caches.
    /// </summary>
    public static class Memory
    {
        static Memory()
        {
            // Default to a medium or "normal" memory usage profile if none is specified.
            SetByPerformanceMode(PerformanceProfile.Normal);
            var checkIntervalTimer = new System.Timers.Timer(10000);
            checkIntervalTimer.Start();
            checkIntervalTimer.Elapsed += (s, e) =>
            {
                var available = AvailableMemory;
                if (available < MinRamThreshold)
                {
                    MemoryCritical(null, new MemoryThresholdExceededEventArgs
                    {
                        RemainingMemory = available,
                        TriggeringThreshold = MinRamThreshold
                    });
                }
            };
        }

        /// <summary>
        /// Sets the maximum size to which the lookup caches can collectively grow based on the
        /// specified ResourceMode
        /// </summary>
        /// <param name="mode">
        /// The ResourceMode which will be used to determine the maximum collective cache size
        /// </param>
        public static void SetByPerformanceMode(PerformanceProfile mode) =>
            MinRamThreshold = mode == PerformanceProfile.High ?
            (MB)2048 : mode == PerformanceProfile.Normal ? (MB)3072 : (MB)4096;
        static MB AvailableMemory => (MB)new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue();

        /// <summary>
        /// The maximum number of MBs to which the lookup caches can collectively grow.
        /// </summary>
        public static MB MinRamThreshold { get; set; }

        /// <summary>
        /// Raised when less than the minimum amount of available ram remains.
        /// </summary>
        public static event MemoryHandler MemoryCritical = (s, e) => { };
    }
}