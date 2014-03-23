using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop.ContractHelperTypes.Base
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierImplementation
    {
        public SystemResourceLoadingNotifier()
            : base("Loading") {
            LASI.Core.Heuristics.Lookup.ResourceLoading += (sender, e) => {
                OnReport(TranslateEventArgs(e));
            };
        }
    }

}
