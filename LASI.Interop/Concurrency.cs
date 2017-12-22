namespace LASI.Interop
{
    using Utilities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides high level management facilities for the concurrency concurrency level of operations performed by the analysis facilities in LASI.Core.
    /// Adjust these values via the <see cref="SetByPerformanceMode(PerformanceProfile)"/> to dynamically adapt the peak consumption of Logical Cores.
    /// </summary>
    public static class Concurrency
    {
        static Concurrency()
        {
            // This is critical for LASI.Core to obey the same concurrency constraints as its client
            // assemblies without a circular dependency
            Core.Configuration.Configuration.ConfigureConcurrency(concurrencyCalculationMap[PerformanceProfile.High].Apply(Environment.ProcessorCount));
        }

        /// <summary>
        /// Sets the maximum allowed Concurrency level based on the supplied ResourceUsageMode.
        /// </summary>
        /// <param name="profile">
        /// The <see cref="PerformanceProfile"/> value from which to determine concurrency settings.
        /// </param>
        public static void SetByPerformanceMode(PerformanceProfile profile)
        {
            // Get the number of logical cores.
            Max = concurrencyCalculationMap[profile](Environment.ProcessorCount);
        }

        private static readonly IReadOnlyDictionary<PerformanceProfile, Func<int, int>> concurrencyCalculationMap = new Dictionary<PerformanceProfile, Func<int, int>>
        {
            [PerformanceProfile.High] = logicalCores => logicalCores < 3 ? logicalCores : logicalCores - 1,
            [PerformanceProfile.Normal] = logicalCores => logicalCores < 3 ? logicalCores : logicalCores - 2,
            [PerformanceProfile.Low] = logicalCores => logicalCores < 4 ? 1 : logicalCores - 3
        };

        /// <summary>
        /// The maximum allowed Concurrency level for Parallel operations.
        /// </summary>
        public static int Max
        {
            get;
            private set;
        }
    }
}