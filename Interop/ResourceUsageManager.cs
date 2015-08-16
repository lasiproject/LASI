namespace LASI.Interop
{
    using Lexicon = Core.Lexicon;
    using MemoryThresholdEventHandler = System.EventHandler<MemoryThresholdExceededEventArgs>;

    /// <summary>
    /// Controls global performance and resource usage settings.
    /// </summary>
    public static class ResourceUsageManager
    {
        /// <summary>
        /// Sets the overall performance level based on the provided enumeration value, providing
        /// for coarse grained adjustments to the CPU and RAM consumption.
        /// </summary>
        /// <param name="mode">
        /// The PerformanceLevel value indicating the new performance and resource usage settings to adopt.
        /// </param>
        public static void SetPerformanceLevel(PerformanceProfile mode)
        {
            Concurrency.SetByPerformanceMode(mode);
            Memory.SetByPerformanceMode(mode);
        }

        /// <summary>
        /// Returns a ResourceInfo instance containing the current resource usage percentages for
        /// the machine hosting the application.
        /// </summary>
        /// <returns>
        /// A ResourceInfo instance containing the current resource usage percentages for the
        /// machine hosting the application.
        /// </returns>
        public static ResourceUsageSample GetCurrentUsage() => new ResourceUsageSample
        (
            memoryUsage: new System.Diagnostics.PerformanceCounter("Memory", "% Committed Bytes In Use").NextValue(),
            cpuUsage: new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total").NextValue()
        );

        /// <summary>
        /// Raised when less than the minimum amount of available RAM, in MB, remains.
        /// </summary>
        public static event MemoryThresholdEventHandler MemoryThresholdExceeded
        {
            add { Memory.MemoryCritical += value; }
            remove { Memory.MemoryCritical -= value; }
        }

        static ResourceUsageManager()
        {
            BindDefaultHandlers();
        }

        /// <summary>
        /// By default, set the memory critical application response to simply jettison all cached
        /// synonym data.
        /// </summary>
        private static void BindDefaultHandlers()
        {
            MemoryThresholdExceeded += delegate
            {
                Lexicon.ClearAllCachedSynonymData();

                // Experimental: Invoke an explicit garbage collection to free up memory. This may
                // be advantageous to performance in this situation, but it remains to be seen. See
                // the second paragraph of http://msdn.microsoft.com/en-us/library/bb384155
                System.GC.Collect();
            };
        }
    }
}