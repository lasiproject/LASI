using System;

namespace LASI.Interop.ContractHelperTypes.Base
{
    abstract class SystemResourceNotifierImplementation : Progress<Interop.ContractHelperTypes.ResourceLoadEventArgs>
    {
        private string messageAdjunct;
        /// <summary>
        /// Initializes a new instance of the SystemResourceNotifierImplementation class which provides for the basis for the subscription of events corresponding to resource loading.
        /// </summary>
        /// <param name="messageAdjunct">An additional piece of text which will be appended to the Message property of the ResourceLoadEventArgs instance.</param>
        protected SystemResourceNotifierImplementation(string messageAdjunct) { this.messageAdjunct = messageAdjunct; }
        /// <summary>
        /// Translates between two incompatible implementations of EventArgs.
        /// </summary>
        /// <param name="e">The event to translate from.</param>
        /// <returns>A new EventArgs with belonging to the interop assembly.</returns>
        protected ResourceLoadEventArgs TranslateEventArgs(Core.ResourceLoadEventArgs e) {
            return new ResourceLoadEventArgs
            {
                Message = e.Message + " " + messageAdjunct,
                PercentWorkRepresented = 1.5,
                ElapsedMiliseconds = e.ElapsedMiliseconds
            };
        }

    }
}
