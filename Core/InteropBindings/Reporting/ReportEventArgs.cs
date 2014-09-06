using System;

namespace LASI.Core.Interop.Reporting
{
    /// <summary>
    /// Contains numeric and textual data related to an event.
    /// </summary>
    [Serializable]
    public abstract class ReportEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public string Message { get; protected set; }
        /// <summary>
        /// Gets a value indicating the amount of overall progress.
        /// </summary>
        public double PercentWorkRepresented { get; protected set; }

    }
}
