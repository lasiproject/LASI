using System;

namespace LASI.Interop.ContractHelperTypes.Base
{
    abstract class SystemResourceNotifierDualizer : Progress<Core.ResourceLoadEventArgs>
    {
        private readonly string messageAdjunct;
        /// <summary>
        /// Initializes a new instance of the SystemResourceNotifierImplementation class which provides for the basis for the subscription of events corresponding to resource loading.
        /// </summary>
        /// <param name="messageAdjunct">An additional piece of text which will be appended to the Message property of the ResourceLoadEventArgs instance.</param>
        protected SystemResourceNotifierDualizer(string messageAdjunct) { this.messageAdjunct = messageAdjunct; }
    }
}
