using System;

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
        /// <typeparam name="TException">The type of the Exception being logged.</typeparam>
        /// <param name="exception">The excption to log.</param>
        public static void LogIfDebug<TException>(this TException exception) where TException : Exception {
#if DEBUG
            Output.WriteLine(exception.Message);
#endif
        }
    }
}
