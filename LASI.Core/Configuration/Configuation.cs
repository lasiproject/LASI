using System;
using System.Linq;

namespace LASI.Core.Configuration
{
    /// <summary>Provides access to resource and performance configuration options.</summary>
    public static class Configuration
    {
        /// <summary>
        /// Initializes Configuration uses the supplied settings.
        /// </summary>
        /// <param name="settings">The <see cref="Utilities.Configuration.IConfig"/> object holding the settings from which configuration will be initialized.</param>
        public static void Initialize(Utilities.Configuration.IConfig settings)
        {
            Settings = settings;
            Paths.Settings = Settings;
        }

        /// <summary>Sets the maximum degree of concurrency to the result of the specified function.</summary>
        /// <param name="concurrencyLevelFactory">A function to specify the concurrency level.</param>
        public static void ConfigureConcurrency(Func<int> concurrencyLevelFactory)
        {
            MaxConcurrency = concurrencyLevelFactory();
        }
        /// <summary>
        /// Gets or sets the <see cref="Utilities.Configuration.IConfig"/> which specifies configuration settings.
        /// </summary>
        internal static Utilities.Configuration.IConfig Settings { get; private set; }
        /// <summary>
        /// Gets or sets the max concurrency level, number of CPU cores used, by algorithms.
        /// </summary>
        internal static int MaxConcurrency { get; private set; }

        /// <summary>
        /// The maximum allowed degree of parallelism as defined by
        /// <see cref="ParallelEnumerable.WithDegreeOfParallelism{TSource}(ParallelQuery{TSource}, int)"/>.
        /// </summary>
        /// <seealso cref="ParallelEnumerable"/>
        private const int MaxParallelismDefinedByParallelEnumerable = 63;

        /// <summary>
        /// Statically initializes the performance proxy with a concurrency level determined by the
        /// same heuristic as the PLINQ infrastructure. Thus, if never later configured. Parallel
        /// queries will use as many CPUs as possible up to the maximum value defined by <see cref="ParallelEnumerable"/>.
        /// </summary>
        /// <seealso cref="ParallelEnumerable.WithDegreeOfParallelism{TSource}(ParallelQuery{TSource}, int)"/>
        static Configuration()
        {
            MaxConcurrency = Math.Min(Environment.ProcessorCount, MaxParallelismDefinedByParallelEnumerable);
        }
    }
}