using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop
{
    /// <summary>
    /// A LASI.Core.Interop.Reporting.ReportEventArgs implementation providing updates on an ongoing operation.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ProgressReportEventArgs : LASI.Core.Interop.Reporting.ReportEventArgs
    {
        /// <summary>
        /// Intializes a new instance of the ProgressReportEventArgs class.
        /// </summary>
        /// <param name="message">The message pertaining to an ongoing operation</param>
        /// <param name="increment">The numerical increase in the progress of the operation since the event was last raised.</param>
        public ProgressReportEventArgs(string message, double increment) {
            Message = message;
            Increment = increment;
        }


    }
}
