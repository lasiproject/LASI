namespace LASI.Interop
{
    /// <summary>
    /// A LASI.Core.Reporting.Reporting.ReportEventArgs implementation providing updates on an ongoing operation.
    /// </summary>
    public class AnalysisUpdateEventArgs : Core.Configuration.ReportEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ProgressReportEventArgs class.
        /// </summary>
        /// <param name="message">The message pertaining to an ongoing operation</param>
        /// <param name="percentOfWorkRepresented">The numerical increase in the progress of the operation since the event was last raised.</param>
        public AnalysisUpdateEventArgs(string message, double percentOfWorkRepresented) : base(message, percentOfWorkRepresented) { }

    }
}
