using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Configuration;

namespace LASI.Utilities.Configuration.Mutable
{
    public abstract class MutableConfigBase : LoadableConfigBase, IConfig, IMutableConfig
    {

        public abstract string this[string name] { get; set; }

        public abstract void Persist();
    }
}
