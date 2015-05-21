using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Configuration;

namespace LASI.Utilities.Configuration.Mutable
{
    /// <summary>
    /// Base class for all mutable configuration objects.
    /// </summary>
    public abstract class MutableConfigBase : LoadableConfigBase, IConfig, IMutableConfig
    {
        /// <summary>When overridden in a derived class, gets or sets the value with the specified name.</summary>
        /// <param name="name">The name of the value to retrieve.</param>
        public abstract string this[string name] { get; set; }
    }
}
