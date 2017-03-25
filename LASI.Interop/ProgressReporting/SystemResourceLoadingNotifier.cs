namespace LASI.Interop.ContractHelperTypes.Base
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierDualizer
    {
        public SystemResourceLoadingNotifier() : base(messageAdjunct: "Loading")
        {
            Core.Lexicon.ResourceLoading += (s, e) => OnReport(e);
        }
    }
}