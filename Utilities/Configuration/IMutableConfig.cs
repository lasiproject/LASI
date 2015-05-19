using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Utilities.Configuration
{
    public interface IMutableConfig
    {
        string this[string name] { set; }
        void Persist();
    }
}