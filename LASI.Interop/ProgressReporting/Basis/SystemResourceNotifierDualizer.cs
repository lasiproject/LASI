using System;

namespace LASI.Interop.ContractHelperTypes.Base
{
    abstract class SystemResourceNotifierImplementation : Progress<Core.ResourceLoadEventArgs>
    {
        private string messageAdjunct;
        /// <summary>
        /// Initializes a new instance of the SystemResourceNotifierImplementation class which provides for the basis for the subscription of events corresponding to resource loading.
        /// </summary>
        /// <param name="messageAdjunct">An additional piece of text which will be appended to the Message property of the ResourceLoadEventArgs instance.</param>
        protected SystemResourceNotifierImplementation(string messageAdjunct) { this.messageAdjunct = messageAdjunct; }
    }
}
