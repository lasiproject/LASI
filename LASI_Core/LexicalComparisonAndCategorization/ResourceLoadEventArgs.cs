using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{

    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ResourceLoadEventArgs : LASI.Core.Interop.Reporting.ReportEventArgs
    {
        internal ResourceLoadEventArgs() {
            ElapsedTime = 0L;
            Message = string.Empty;
        }

        public ResourceLoadEventArgs(string message, double increment) {
            Message = message;
            Increment = increment;
        }

        public override string Message { get; protected set; }

        public long ElapsedTime { get; internal set; }

        public override double Increment {
            get;
            protected set;
        }

    }
}
