using System;

namespace LASI.Interop
{
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
        public ResourceUsageSample(float cpuUsage, float memoryUsage, DateTimeOffset? timeSnapshotted = default)
        {
            CpuUsage = cpuUsage;
            MemoryUsage = memoryUsage;
            TimeSnapshotted = timeSnapshotted ?? DateTimeOffset.Now;
        }

        /// <summary>
        /// The CPU usage % of the machine hosting the application when the sample was taken.
        /// </summary>
        public float CpuUsage { get; }

        /// <summary>
        /// The Memory usage % of the machine hosting the application when the sample was taken.
        /// </summary>
        public float MemoryUsage { get; }

        /// <summary>
        /// The local time of the machine hosting the application when the sample was taken.
        /// </summary>
        public DateTimeOffset TimeSnapshotted { get; }
    }
}
