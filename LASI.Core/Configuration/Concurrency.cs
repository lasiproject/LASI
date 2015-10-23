namespace LASI.Core.Configuration
{
    /// <summary>
    /// Provides access to concurrency information.
    /// </summary>
    public static class Concurrency
    {
        /// <summary>
        /// Gets the current maximum degree of concurrency.
        /// </summary>
        /// <returns>The current maximum degree of parallelism.</returns>
        public static int Max => Configuration.MaxConcurrency;
    }
}