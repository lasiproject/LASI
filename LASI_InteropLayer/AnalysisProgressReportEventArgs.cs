using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop
{
    /// <summary>
    /// Contains event data regarding the current state and progress of analysis.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class AnalysisProgressReportEventArgs : LASI.Core.Interop.Reporting.ReportEventArgs
    {
        internal AnalysisProgressReportEventArgs(string message, double increment) {
            Message = message;
            Increment = increment;
        }
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public override string Message { get; protected set; }
        /// <summary>
        /// Gets a value indicating the amount by which overall progress of analysis has increased since the last Report was created.
        /// </summary>
        public override double Increment { get; protected set; }
    }
}
