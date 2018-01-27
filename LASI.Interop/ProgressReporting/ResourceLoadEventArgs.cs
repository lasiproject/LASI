using LASI.Core.Configuration;

namespace LASI.Interop.ProgressReporting
{
    /// <summary>
    /// Carries event data associated with the loading of a specific LASI resource.
    /// </summary>
    public class ResourceLoadEventArgs : ReportEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ResourceLoadEventArgs class.
        /// </summary>
        /// <param name="message">The phase of analysis currently underway.</param>
        /// <param name="percentComplete">The percent of overall progress completed.</param>
        public ResourceLoadEventArgs(string message, double percentComplete) : base(message, percentComplete) {}
    }
}