using System.Linq;

namespace LASI.App
{
    /// <summary>
    /// This class provides static access to each of the 5 windows defined by the lasi user interface
    /// In addition to this, it defines some useful extension methods which stream the manipulation of windows
    /// </summary>
    internal static class WindowManager
    {
        #region Methods
        internal static void Intialize()
        {
            startupScreen = App.Windows.OfType<StartupWindow>().First();
        }
        #endregion

        #region Fields
        private static StartupWindow startupScreen;
        private static ResultsWindow resultsScreen;
        private static ProjectPreviewWindow projectPreviewScreen;
        private static InProgressWindow inProgressScreen;
        #endregion

        #region Properties
        private static System.Windows.Application App => System.Windows.Application.Current;
        public static StartupWindow StartupScreen => startupScreen;
        public static InProgressWindow InProgressScreen => inProgressScreen ?? (inProgressScreen = new InProgressWindow());
        public static ResultsWindow ResultsScreen => resultsScreen ?? (resultsScreen = new ResultsWindow());
        public static ProjectPreviewWindow ProjectPreviewScreen => projectPreviewScreen ?? (projectPreviewScreen = new ProjectPreviewWindow());
        #endregion
    }
}
