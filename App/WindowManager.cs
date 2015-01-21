using System;
using System.Linq;
using System.Windows;

namespace LASI.App
{
    /// <summary>
    /// This class provides static access to each of the 5 windows defined by the lasi user interface
    /// In addition to this, it defines some useful extension methods which stream the manipulation of windows
    /// </summary>
    internal static class WindowManager
    {
        #region Methods
        internal static void Intialize() {
            startupScreen = App.Current.Windows.OfType<StartupWindow>().First();
        }
        #endregion

        #region Fields
        private static StartupWindow startupScreen;
        private static ResultsWindow resultsScreen = new ResultsWindow();
        private static ProjectPreviewWindow projectPreviewScreen = new ProjectPreviewWindow();
        private static InProgressWindow inProgressScreen = new InProgressWindow();
        #endregion

        #region Properties
        public static StartupWindow StartupScreen { get { return startupScreen; } }
        public static InProgressWindow InProgressScreen { get { return inProgressScreen; } }
        public static ResultsWindow ResultsScreen { get { return resultsScreen; } }
        public static ProjectPreviewWindow ProjectPreviewScreen { get { return projectPreviewScreen; } }
        #endregion

        #region Extension Methods

        public static void PositionAt(this Window window, Window other) {
            window.PositionAt(other.Left, other.Top);
        }
        public static void PositionAt(this Window window, double left, double top) {
            window.Left = left;
            window.Top = top;
        }
        public static void SetTitle(this Window window, string title) {
            window.Title = title;
        }
        public static void SwapWith(this Window window, Window other) {
            other.PositionAt(window);
            other.Show();
            window.Hide();
        }

        #endregion
    }
}
