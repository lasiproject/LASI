using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities.Configuration
{
    // TODO: Mark with [AssemblyNeutral] as defined by the next version (5.0) of AspNet for better interop.
    public interface IConfig
    {
        string this[string name] { get; }
        string this[string name, StringComparison stringComparison] { get; }
    }
}
