using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Interop
{
    public static class Configuation
    {
        /// <summary>
        /// Statically initializes the performance proxy with a concurrency level determined by the same heuristic as the PLINQ infrastructure.
        /// Thus, if never later configured. Parallel quueries will be unconstrained.
        /// </summary>
        static Configuation() {
            MaxConcurrency = Math.Min(System.Environment.ProcessorCount, 64);
        }

        public static void ConfigureConcurrency(Func<int> concurrencyLevelFactory) {
            MaxConcurrency = concurrencyLevelFactory();
        }

        internal static int MaxConcurrency { get; private set; }
    }
}
namespace LASI.Core
{
    public static class Concurrency
    {
        public static int Max { get { return LASI.Core.Interop.Configuation.MaxConcurrency; } }
    }
}
