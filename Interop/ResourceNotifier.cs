using LASI.Core.Heuristics;
using LASI.Interop.ContractHelperTypes;
using LASI.Interop.ContractHelperTypes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace LASI.Interop.ResourceManagement
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
        public event EventHandler<ResourceLoadEventArgs> ResourceLoaded {
            add { notificationProvider.ProgressChanged += value; }
            remove { notificationProvider.ProgressChanged -= value; }
        }
        /// <summary>
        /// Raised when a System Core resource begins loading.
        /// </summary>
        public event EventHandler<ResourceLoadEventArgs> ResourceLoading {
            add { notificationProvider.ProgressChanging += value; }
            remove { notificationProvider.ProgressChanging -= value; }
        }
    }
}
