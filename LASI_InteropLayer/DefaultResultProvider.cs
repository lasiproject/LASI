using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ServiceModel;
using LASI.Algorithm;
namespace LASI.GuiInterop
{

    public class DefaultProgressProvider : IProgressUpdateProvider
    {
        public event EventHandler CompilationFinished;

        public event EventHandler ProgressUpdated;
    }

}

