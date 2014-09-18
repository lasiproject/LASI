using System;

namespace LASI.Core.Interop.Reporting
{
    /// <summary>
    /// Contains numeric and textual data related to an event.
    /// </summary>
    [Serializable]
    public abstract class ReportEventArgs : EventArgs
    {
        protected ReportEventArgs(string message, double percentWorkRepresented) {
            Message = message;
            PercentWorkRepresented = percentWorkRepresented;
        }
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Gets a value indicating the amount of overall progress.
        /// </summary>
        public double PercentWorkRepresented { get; private set; }

    }
}
