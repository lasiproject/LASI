using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop.ContractHelperTypes.Base
{
    /// <summary>
    /// Provides for the subscription of events involving the loading of core resources.
    /// </summary>
    class SystemResourceNotifier : SystemResourceNotifierImplementation
    {
        /// <summary>
        /// Initializes a new instance of the SystemResourceNotifier class which provides for the subscription of events corresponding to resource loading events.
        /// </summary>
        public SystemResourceNotifier()
            : base(BASE_MESSAGE_ADJUNCT) {
            Core.Heuristics.Lookup.ResourceLoaded += (sender, e) => OnReport(TranslateEventArgs(e));
        }
        /// <summary>
        /// Raised when a System Core resource begins loading.
        /// </summary>
        public event EventHandler<ResourceLoadEventArgs> ProgressChanging {
            add { loadingProvider.ProgressChanged += value; }
            remove { loadingProvider.ProgressChanged -= value; }
        }
        private SystemResourceLoadingNotifier loadingProvider = new SystemResourceLoadingNotifier();
        private const string BASE_MESSAGE_ADJUNCT = "Loaded";
    }
}
