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
        public static void Log(this Exception exception)
        {
            Output.WriteLine($"{exception.Message}\n{exception.StackTrace}");
        }
        /// <summary>
        /// Logs the Exception to the output channel if the debug flag is set.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        public static void LogIfDebug(this Exception exception)
        {
#if DEBUG
            exception.Log();
#endif
        }
    }
}
