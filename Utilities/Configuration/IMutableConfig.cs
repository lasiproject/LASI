using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// Describes an indexed collection of mutable values where both keys and value are <see cref="string"/>s.
    /// </summary>
    public interface IMutableConfig
    {
        /// <summary>
        /// Sets the value with the given key to the given value.
        /// </summary>
        /// <param name="name">The name of the value to set.</param>
        string this[string name] { set; }
    }
}