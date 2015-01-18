using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities
{
    public interface IConfig
    {
        string this[string key] { get; }
        string this[string key, string defaultValue] { get; }
        string this[string key, StringComparison stringComparison] { get; }
        string this[string key, StringComparison stringComparison, string defaultValue] { get; }
    }
}
