using System;

namespace LASI.Interop.ContractHelperTypes
{
    /// <summary>
    /// Carries event data associated with the loading of a specific resource.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ResourceLoadEventArgs
    {

        /// <summary>
        /// The number of miliseconds consumed by the loading task associated with the event.
        /// </summary>
        public long ElapsedMiliseconds { get; internal set; }
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public string Message { get; internal set; }
        /// <summary>
        /// Gets a value indicating the amount by which overall progress of analysis has increased since the last Report was created.
        /// </summary>
        public double PercentWorkRepresented { get; internal set; }

    }
}
