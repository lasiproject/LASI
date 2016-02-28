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
            Core.Configuration.Configuration.ConfigureConcurrency(ComputeHighMax.Apply(Environment.ProcessorCount));
        }

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
            [PerformanceProfile.High] = ComputeHighMax,
            [PerformanceProfile.Normal] = ComputeNormalMax,
            [PerformanceProfile.Low] = ComputeLowMax
        };

        private static Func<int, int> ComputeHighMax = (int logicalCores) => logicalCores < 3 ? logicalCores : logicalCores - 1;

        private static Func<int, int> ComputeNormalMax = (int logicalCores) => logicalCores < 3 ? logicalCores : logicalCores - 2;

        private static Func<int, int> ComputeLowMax = (int logicalCores) => logicalCores < 4 ? 1 : logicalCores - 3;


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