using System;
using System.Linq;

namespace LASI.Core.Configuration
{
    /// <summary>Provides access to resource and performance configuration options.</summary>
    public static class Configuration
    {
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

        internal static Utilities.Configuration.IConfig Settings { get; private set; }

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