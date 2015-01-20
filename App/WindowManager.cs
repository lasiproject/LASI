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

    /// <summary>
    /// Class implementing support for "minimize to tray" functionality.
    /// Thanks to David Anson for the excellent examples and source provided in his blog post:
    /// Get out of the way with the tray ["Minimize to tray" sample implementation for WPF]
    /// http://blogs.msdn.com/b/delay/archive/2009/08/31/get-out-of-the-way-with-the-tray-minimize-to-tray-sample-implementation-for-wpf.aspx
    /// </summary>
    public static class TrayIconManager
    {
        /// <summary>
        /// Enables "minimize to tray" behavior for the specified Window.
        /// </summary>
        /// <param name="window">Window to enable the behavior for.</param>
        public static void Enable(Window window) {
            // No need to track this instance; its event handlers will keep it alive
            new TrayIconProvider(window);
        }/// <summary>
         /// Enables "minimize to tray" behavior for the specified Window.
         /// </summary>
         /// <param name="window">Window to enable the behavior for.</param>
        public static void Enable(InProgressWindow window) {
            // No need to track this instance; its event handlers will keep it alive
            new TrayIconProvider(window);
        }

        /// <summary>
        /// Class implementing "minimize to tray" functionality for a Window instance.
        /// </summary>
        class TrayIconProvider : IDisposable
        {

            /// <summary>
            /// Initializes a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            /// <param name="window">Window instance to attach to.</param>
            public TrayIconProvider(Window window) {
                this.window = window;
                window.StateChanged += HandleStateChanged;
            }
            public TrayIconProvider(InProgressWindow window) {
                window.ProcessingCompleted += (s, e) => window.Title = "Analysis Complete";
                window.ProcessingCompleted += HandleStateChanged;
                this.window = window;
                window.StateChanged += HandleStateChanged;
            }

            /// <summary>
            /// Handles the Window's StateChanged event.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleStateChanged(object sender, EventArgs e) {
                if (notifyIcon == null) {
                    // Initialize NotifyIcon instance "on demand"
                    notifyIcon = new System.Windows.Forms.NotifyIcon();
                    notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);
                    notifyIcon.MouseClick += HandleNotifyIconOrBalloonClicked;
                    notifyIcon.BalloonTipClicked += HandleNotifyIconOrBalloonClicked;
                }
                // Update copy of Window Title in case it has changed
                notifyIcon.Text = window.Title;

                // Show/hide Window and NotifyIcon
                var minimized = (window.WindowState == WindowState.Minimized);
                window.ShowInTaskbar = !minimized;
                notifyIcon.Visible = minimized;
                if (minimized) {
                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, null, window.Title, System.Windows.Forms.ToolTipIcon.None);
                }
            }

            /// <summary>
            /// Handles a click on the notify icon or its balloon.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleNotifyIconOrBalloonClicked(object sender, EventArgs e) {
                // Restore the Window
                window.WindowState = WindowState.Normal;
            }

            public void Dispose() {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool fromDispose) {
                if (fromDispose) {
                    notifyIcon.Dispose();
                }
            }

            #region Fields
            private Window window;
            private System.Windows.Forms.NotifyIcon notifyIcon;
            #endregion
        }
    }
}
