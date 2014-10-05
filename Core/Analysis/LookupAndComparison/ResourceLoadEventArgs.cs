using System;

namespace LASI.Core
{
    /// <summary>
    /// Carries event data associated with the loading of a specific resource.
    /// </summary>
    [Serializable]
    public class ResourceLoadEventArgs : Interop.Reporting.ReportEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ResourceLoadEventArgs.
        /// </summary>
        /// <param name="message">A short textual description of the event.</param>
        /// <param name="increment">The percentage of total work completed.</param>
        public ResourceLoadEventArgs(string message, double increment) : base(message, increment) { }
        /// <summary>
        /// The number of miliseconds consumed by the loading task associated with the event.
        /// </summary>
        public long ElapsedMiliseconds { get; internal set; }
    }
}
