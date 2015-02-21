namespace LASI.Interop.ResourceManagement
{
    using Lexicon = Core.Heuristics.Lexicon;
    using MemoryThresholdEventHandler = System.EventHandler<MemoryThresholdExceededEventArgs>;
    using DateTime = System.DateTime;
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
        public static void SetPerformanceLevel(PerformanceLevel mode)
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
        /// w
        public static ResourceUsageSample GetCurrentUsage()
        {
            return new ResourceUsageSample
            (
                memoryUsage: new System.Diagnostics.PerformanceCounter("Memory", "% Committed Bytes In Use").NextValue(),
                cpuUsage: new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total").NextValue()
            );
        }

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
            MemoryThresholdExceeded += (s, e) =>
            {
                Lexicon.ClearNounCache();
                Lexicon.ClearVerbCache();
                Lexicon.ClearAdjectiveCache();
                Lexicon.ClearAdverbCache();

                // Experimental: Invoke an explicit garbage collection to free up memory. This may
                // be advantageous to performance in this situation, but it remains to be seen. See
                // the second paragraph of http://msdn.microsoft.com/en-us/library/bb384155
                System.GC.Collect();
            };
        }

    }

    /// <summary>
    /// Broadly specifies the various resource usage profiles of the program.
    /// </summary>
    public enum PerformanceLevel
    {
        /// <summary>
        /// High resource usage indicates a liberal allocation and consumption of available
        /// system resources.
        /// </summary>
        High = 0,

        /// <summary>
        /// Normal resource usage indicates a modest allocation and consumption of available
        /// system resources.
        /// </summary>
        Normal,

        /// <summary>
        /// High resource usage indicates a conservative allocation and consumption of available
        /// system resources.
        /// </summary>
        Low,
    }
    /// <summary>
    /// Represents a resource usage sample.
    /// </summary>
    public struct ResourceUsageSample
    {
        /// <summary>
        /// Initializes a new instance of the ResourceSample structure with the specified values.
        /// </summary>
        /// <param name="cpuUsage">
        /// The CPU usage value.
        /// </param>
        /// <param name="memoryUsage">
        /// The memory usage value.
        /// </param>
        /// <param name="timeSnapshotted">
        /// The time when the sample was taken.
        /// </param>
        public ResourceUsageSample(float cpuUsage, float memoryUsage, DateTime? timeSnapshotted = default(DateTime?))
            : this()
        {
            CpuUsage = cpuUsage;
            MemoryUsage = memoryUsage;
            TimeSnapshotted = timeSnapshotted ?? DateTime.Now;
        }

        /// <summary>
        /// Gets the CPU usage % of the machine hosting the application when the sample was taken.
        /// </summary>
        public float CpuUsage { get; }

        /// <summary>
        /// Gets the Memory usage % of the machine hosting the application when the sample was taken.
        /// </summary>
        public float MemoryUsage { get; }

        /// <summary>
        /// Gets the local time of the machine hosting the application when the sample was taken.
        /// </summary>
        public DateTime TimeSnapshotted { get; }
    }
}