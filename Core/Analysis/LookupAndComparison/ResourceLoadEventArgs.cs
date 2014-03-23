using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Carries event data associated with the loading of a specific resource.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ResourceLoadEventArgs : LASI.Core.Interop.Reporting.ReportEventArgs
    {
        internal ResourceLoadEventArgs() {
            ElapsedMiliseconds = 0L;
            Message = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the ResourceLoadEventArgs.
        /// </summary>
        /// <param name="message">A short textual description of the event.</param>
        /// <param name="increment">The amount of progress, assumed to be on a scale of 1 to 100, the event represents.</param>
        public ResourceLoadEventArgs(string message, double increment)
            : this() {
            Message = message;
            PercentOfWorkRepresented = increment;
        }
        /// <summary>
        /// The number of miliseconds consumed by the loading task associated with the event.
        /// </summary>
        public long ElapsedMiliseconds { get; internal set; }


    }
}
