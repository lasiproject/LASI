namespace LASI.Interop.ResourceManagement
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Centralizes management and control of the concurrency level of concurrent operations.
    /// </summary>
    public static class Concurrency
    {
        /// <summary>
        /// Sets the maximum allowed Concurrency level based on the supplied ResourceUsageMode.
        /// </summary>
        /// <param name = "profile">
        /// The <see cref="PerformanceProfile"/> value from which to determine concurrency settings.
        /// </param>
        public static void SetByPerformanceMode(PerformanceProfile profile)
        {
            var logicalCores = Environment.ProcessorCount; // Get the number of logical cores.
            var calculate = concurrencyCalculationMap[profile];
            Max = calculate(logicalCores);
        }

        private static readonly IDictionary<PerformanceProfile, Func<int, int>> concurrencyCalculationMap = new Dictionary<PerformanceProfile, Func<int, int>>
        {
            [PerformanceProfile.High] = cores => cores < 3 ? cores : cores - 1,
            [PerformanceProfile.Normal] = cores => cores < 3 ? cores : cores - 2,
            [PerformanceProfile.Low] = cores => cores < 4 ? 1 : cores - 3,
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
        private static int ComputeDefaultMax()
        {
            var logicalCores = Environment.ProcessorCount;
            return logicalCores < 3 ? logicalCores : logicalCores - 1;
        }

        static Concurrency()
        {
            // This is critical for LASI.Core to obey the same concurrency constraints as its client
            // assemblies without a circular dependency
            Core.Configuration.Configuration.ConfigureConcurrency(ComputeDefaultMax);
        }

        /// <summary>
        /// Gets the maximum allowed Concurrency level for Parallel operations.
        /// </summary>
        public static int Max
        {
            get;
            private set;
        }
    }
}