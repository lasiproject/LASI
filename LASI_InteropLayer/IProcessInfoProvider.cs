using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.GuiInterop
{
    interface IProgressUpdateProvider
    {
        event EventHandler CompilationFinished;
        event EventHandler ProgressUpdated;
    }
}
