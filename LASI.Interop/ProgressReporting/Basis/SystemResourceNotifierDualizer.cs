using System;
using LASI.Core;

namespace LASI.Interop.ProgressReporting.Basis
{
    abstract class SystemResourceNotifierDualizer : Progress<Core.ResourceLoadEventArgs>
    {
        string messageAdjunct;
        /// <summary>
        /// Initializes a new instance of the SystemResourceNotifierImplementation class which provides for the basis for the subscription of events corresponding to resource loading.
        /// </summary>
        /// <param name="messageAdjunct">An additional piece of text which will be appended to the Message property of the ResourceLoadEventArgs instance.</param>
        protected SystemResourceNotifierDualizer(string messageAdjunct) { this.messageAdjunct = messageAdjunct; }

        protected override void OnReport(Core.ResourceLoadEventArgs value)
        {
            value.Message += messageAdjunct;
            value.Message += messageAdjunct;
            base.OnReport(value);
        }
    }
}
