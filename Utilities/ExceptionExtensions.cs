using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines various extension methods for conveniently working with Exception instances.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Logs the Exception to the Output object if the debug flag is set.
        /// </summary>
        /// <param name="exception">The excption to log.</param>
        public static void LogIfDebug(this Exception exception) {
#if DEBUG
            Output.WriteLine(exception.Message);
#endif
        }
    }
}
