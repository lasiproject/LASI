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
        public abstract string Message { get; protected set; }
        public abstract double Increment { get; protected set; }

    }
}
