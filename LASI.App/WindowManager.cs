using System.Linq;

namespace LASI.App
{
    /// <summary>
    /// This class provides static access to each of the 5 windows defined by the LASI user interface In addition to this, it defines some useful extension methods which stream the manipulation of windows 
    /// </summary>
    internal static class WindowManager
    {
        #region Methods

        internal static void Intialize()
        {
            StartupScreen = App.Windows.OfType<StartupWindow>().First();
        }

        #endregion Methods
        #region Fields
        private static ResultsWindow resultsScreen;
        private static ProjectPreviewWindow projectPreviewScreen;
        private static InProgressWindow inProgressScreen;

        #endregion Fields

        #region Properties

        private static System.Windows.Application App => System.Windows.Application.Current;
        public static StartupWindow StartupScreen { get; private set; }

        public static InProgressWindow InProgressScreen => inProgressScreen ?? (inProgressScreen = new InProgressWindow());
        public static ResultsWindow ResultsScreen => resultsScreen ?? (resultsScreen = new ResultsWindow());
        public static ProjectPreviewWindow ProjectPreviewScreen => projectPreviewScreen ?? (projectPreviewScreen = new ProjectPreviewWindow());

        #endregion Properties
    }
}
