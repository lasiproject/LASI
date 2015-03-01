using System;

namespace LASI.Interop.ContractHelperTypes
{
    /// <summary>
    /// Carries event data associated with the loading of a specific resource.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ResourceLoadEventArgs : LASI.Core.Configuration.ReportEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ResourceLoadEventArgs class.
        /// </summary>
        /// <param name="message">The phase of analysis currently underway.</param>
        /// <param name="percentComplete">The percent of overall progress completed.</param>
        public ResourceLoadEventArgs(string message, double percentComplete) : base(message, percentComplete) { }
    }
}
