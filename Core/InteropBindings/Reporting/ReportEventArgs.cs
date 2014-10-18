using System;

namespace LASI.Core.Interop
{
    /// <summary>
    /// Contains numeric and textual data related to an event.
    /// </summary>
    [Serializable]
    public abstract class ReportEventArgs : EventArgs
    {
        public ReportEventArgs() { }
        /// <summary>
        /// Initializes a new instance of the ReportEventArgs class.
        /// </summary>
        /// <param name="message">The phase of analysis currently underway.</param>
        /// <param name="percentWorkRepresented">The percent of overall progress completed.</param>
        protected ReportEventArgs(string message, double percentWorkRepresented) {
            Message = message;
            PercentWorkRepresented = percentWorkRepresented;
        }
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets a value indicating the amount of overall progress.
        /// </summary>
        public double PercentWorkRepresented { get; set; }

    }
}
