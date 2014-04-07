using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Interop
{
    /// <summary>
    /// Provides access to resource and performance configuration options.
    /// </summary>
    public static class Configuation
    {
        /// <summary>
        /// Statically initializes the performance proxy with a concurrency level determined by the same heuristic as the PLINQ infrastructure.
        /// Thus, if never later configured. Parallel quueries will be unconstrained.
        /// </summary>
        static Configuation() {
            MaxConcurrency = Math.Min(System.Environment.ProcessorCount, 64);
        }
        /// <summary>
        /// Sets the maximum degree of concurrency to the result of the given function.
        /// </summary>
        /// <param name="concurrencyLevelFactory"></param>
        public static void ConfigureConcurrency(Func<int> concurrencyLevelFactory) {
            MaxConcurrency = concurrencyLevelFactory();
        }

        internal static int MaxConcurrency { get; private set; }
    }
}
namespace LASI.Core
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
        public static int Max { get { return LASI.Core.Interop.Configuation.MaxConcurrency; } }
    }
}
