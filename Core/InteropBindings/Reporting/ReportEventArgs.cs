using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Interop.Reporting
{
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public abstract class ReportEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public string Message { get; protected set; }
        /// <summary>
        /// Gets a value indicating the amount by which overall progress of analysis has increased since the last Report was created.
        /// </summary>
        public double Increment { get; protected set; }

    }
}
