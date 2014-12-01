using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop.ContractHelperTypes.Base
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierImplementation
    {
        private const string MESSAGE_ADJUNCT = "Loading";

        public SystemResourceLoadingNotifier()
            : base(MESSAGE_ADJUNCT) {
            Core.Heuristics.Lookup.ResourceLoading += (sender, e) => {
                OnReport(e);
            };
        }
    }

}
