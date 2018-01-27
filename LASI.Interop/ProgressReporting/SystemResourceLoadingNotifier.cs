namespace LASI.Interop.ProgressReporting.Basis
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierDualizer
    {
        public SystemResourceLoadingNotifier() : base(messageAdjunct: "Loading")
        {
            Core.Lexicon.ResourceLoading += (s, e) => OnReport(e);
        }
    }
}