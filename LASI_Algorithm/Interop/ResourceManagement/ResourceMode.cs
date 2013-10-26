using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core.Interop
{
    /// <summary>
    /// Broadly specifies the various resource usage profiles of the program.
    /// </summary>
    public enum ResourceMode
    {
        /// <summary>
        /// High resource usage indicates a liberal allocation and consumption of available system resources.
        /// </summary>
        High,
        /// <summary>
        /// Noraml resource usage indicates a modest allocation and consumption of available system resources.
        /// </summary>
        Normal,
        /// <summary>
        /// High resource usage indicates a conservative allocation and consumption of available system resources.
        /// </summary>
        Low,
    }
}
