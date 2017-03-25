using System;

namespace LASI.Interop.ContractHelperTypes.Base
{
    using Lexicon = Core.Lexicon;
    using ResourceLoadEventArgs = Core.ResourceLoadEventArgs;

    /// <summary>
    /// Provides for the subscription of events involving the loading of core resources.
    /// </summary>
    class SystemResourceNotifier : SystemResourceNotifierDualizer
    {
        /// <summary>
        /// Initializes a new instance of the SystemResourceNotifier class which provides 
        /// for the subscription of events corresponding to resource loading events.
        /// </summary>
        public SystemResourceNotifier() : base(messageAdjunct: "Loaded")
        {
            Lexicon.ResourceLoaded += (sender, e) => OnReport(e);
        }

        /// <summary>
        /// Raised when a System Core resource begins loading.
        /// </summary>
        public event EventHandler<ResourceLoadEventArgs> ProgressChanging
        {
            add { loadingProvider.ProgressChanged += value; }
            remove { loadingProvider.ProgressChanged -= value; }
        }
        private SystemResourceLoadingNotifier loadingProvider = new SystemResourceLoadingNotifier();
    }
}