using LASI.Core.Heuristics;
using LASI.Interop.ContractHelperTypes;
using LASI.Interop.ContractHelperTypes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace LASI.Interop.ResourceMonitoring
{


    /// <summary>
    /// Provides for the subscription of events involving the loading of core resources.
    /// </summary>
    public class ResourceNotifier
    {
        private SystemResourceNotifier notificationProvider = new SystemResourceNotifier();
        /// <summary>
        /// Raised when a System Core resource is finished loading.
        /// </summary>
        public event EventHandler<ResourceLoadEventArgs> ResourceLoaded = delegate { };

        /// <summary>
        /// Raised when a System Core resource begins loading.
        /// </summary>
        public event EventHandler<ResourceLoadEventArgs> ResourceLoading = delegate { };


    }
}
