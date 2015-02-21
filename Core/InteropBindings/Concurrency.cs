using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.InteropBindings
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
        public static int Max => Configuation.MaxConcurrency;
    }
}