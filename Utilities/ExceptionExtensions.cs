using System;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines various extension methods for conveniently working with Exception instances.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Logs the Exception to the output channel.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        [System.Diagnostics.DebuggerStepThrough]
        public static void Log(this Exception exception)
        {
            Logger.Log($"{exception.Message}\n{exception.StackTrace}");
        }
    }
}
