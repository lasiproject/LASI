using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Interop
{
    /// <summary>
    /// The event data which is accessable to a function which handles a memory threshold exceeded event.
    /// </summary>
    /// <see cref="Performance">Provides access to memory usage events</see> 
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class MemoryThresholdExceededEventArgs
    {
        /// <summary>
        /// Gets the amount of total machine memory available when the event was raised.
        /// This represents the memory available to the operating system, and so takes into account the effects of other running applications and services.
        /// </summary>
        public MB RemainingMemory { get; internal set; }
        /// <summary>
        /// Gets the minimum memory threshold which triggered the event. This will tend to differ slightly from the value of Remaining memory.
        /// </summary>
        public MB TriggeringThreshold { get; internal set; }
    }
}
