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
        public AnalysisProgressReportEventArgs(string message, double increment) {
            Message = message;
            Increment = increment;
        }


    }
}
