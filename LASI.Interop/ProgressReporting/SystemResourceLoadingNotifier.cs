namespace LASI.Interop.ProgressReporting.Basis
{
    class SystemResourceLoadingNotifier : SystemResourceNotifierDualizer
    {
        public SystemResourceLoadingNotifier() : base(messageAdjunct: "Loading")
        {
            LASI.Core.Heuristics.Lexicon.ResourceLoading += (s, e) => OnReport(e);
        }
    }
}