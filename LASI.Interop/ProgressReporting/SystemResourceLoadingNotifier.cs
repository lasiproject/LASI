namespace LASI.Interop.ContractHelperTypes.Base
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierImplementation
    {
        public SystemResourceLoadingNotifier() : base(messageAdjunct: "Loading")
        {
            Core.Lexicon.ResourceLoading += (s, e) => OnReport(e);
        }
    }
}