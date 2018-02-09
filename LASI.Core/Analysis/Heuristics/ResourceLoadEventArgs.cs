using System;

namespace LASI.Core
{
    /// <summary>
    /// Carries event data associated with the loading of a specific resource.
    /// </summary>
    public class ResourceLoadEventArgs : Configuration.ReportEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ResourceLoadEventArgs.
        /// </summary>
        /// <param name="message">A short textual description of the event.</param>
        /// <param name="increment">The percentage of total work completed.</param>
        /// <param name=" elapsedMilliseconds">The number of milliseconds consumed by the loading task associated with the event.</param>
        public ResourceLoadEventArgs(string message, double increment, long  elapsedMilliseconds)
            : base(message, increment) => ElapsedMiliseconds =  elapsedMilliseconds;
        /// <summary>
        /// The number of milliseconds consumed by the loading task associated with the event.
        /// </summary>
        public long ElapsedMiliseconds { get; }
    }
}
