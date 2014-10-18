using System;

namespace LASI.Interop.ContractHelperTypes
{
    /// <summary>
    /// Carries event data associated with the loading of a specific resource.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ResourceLoadEventArgs : LASI.Core.Interop.ReportEventArgs
    {
        /// <summary>
        /// The number of milliseconds consumed by the loading task associated with the event.
        /// </summary>
        public long ElapsedMiliseconds { get; internal set; }
    }
}
