using Lexicon = LASI.Core.Lexicon;

namespace LASI.Interop.ContractHelperTypes.Base
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierImplementation
    {
        private const string MESSAGE_ADJUNCT = "Loading";
        public SystemResourceLoadingNotifier()
            : base(MESSAGE_ADJUNCT)
        {
            Lexicon.ResourceLoading += (s, e) =>
            {
                OnReport(e);
            };
        }
    }

}
