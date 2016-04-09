using System;
using System.Runtime.InteropServices;

namespace LASI.Interop
{
    /// <summary>
    /// The event data which is accessible to a function which handles a memory threshold exceeded event.
    /// </summary>
    /// <seealso cref="ResourceManagement.ResourceNotifier">Provides access to memory usage events</seealso> 
    [Serializable]
    [ComVisible(true)]
    public class MemoryThresholdExceededEventArgs : EventArgs
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
