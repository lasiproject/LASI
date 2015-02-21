namespace LASI.Core.InteropBindings
{
    /// <summary>
    /// Provides access to resource and performance configuration options.
    /// </summary>
    public static class Configuation
    {
        public static void Initialize(Utilities.Configuration.IConfig settings)
        {
            Settings = settings;
            Heuristics.Paths.Settings = Settings;
        }
        internal static Utilities.Configuration.IConfig Settings { get; private set; }

        /// <summary>
        /// Statically initializes the performance proxy with a concurrency level determined by the same heuristic as the PLINQ infrastructure.
        /// Thus, if never later configured. Parallel quueries will be unconstrained.
        /// </summary>
        static Configuation()
        {
            MaxConcurrency = System.Math.Min(System.Environment.ProcessorCount, 64);
        }
        /// <summary>
        /// Sets the maximum degree of concurrency to the result of the given function.
        /// </summary>
        /// <param name="concurrencyLevelFactory"></param>
        public static void ConfigureConcurrency(System.Func<int> concurrencyLevelFactory)
        {
            MaxConcurrency = concurrencyLevelFactory();
        }

        internal static int MaxConcurrency { get; private set; }
    }
}
