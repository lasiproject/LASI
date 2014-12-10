using LASI.Interop.ContractHelperTypes.Base;
using System;

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
        public event EventHandler<Core.ResourceLoadEventArgs> ResourceLoaded {
            add { notificationProvider.ProgressChanged += value; }
            remove { notificationProvider.ProgressChanged -= value; }
        }
        /// <summary>
        /// Raised when a System Core resource begins loading.
        /// </summary>
        public event EventHandler<Core.ResourceLoadEventArgs> ResourceLoading {
            add { notificationProvider.ProgressChanging += value; }
            remove { notificationProvider.ProgressChanging -= value; }
        }
    }
}
