using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LASI.UserInterface
{
    /// <summary>
    /// This class provides static access to each of the 5 windows defined by the lasi user interface
    /// In addition to this, it defines some useful extension methods which stream the manipulation of windows
    /// </summary>
    internal static class WindowManager
    {
        #region Methods
        internal static void Intialize() {
            startupScreen = App.Current.Windows.OfType<StartupScreen>().First();
        }
        #endregion

        #region Fields
        private static StartupScreen startupScreen;
        private static ResultsScreen resultsScreen = new ResultsScreen();
        private static ProjectPreviewScreen projectPreviewScreen = new ProjectPreviewScreen();
        private static InProgressScreen inProgressScreen = new InProgressScreen();
        #endregion

        #region Properties
        public static StartupScreen StartupScreen { get { return startupScreen; } }
        public static InProgressScreen InProgressScreen { get { return inProgressScreen; } }
        public static ResultsScreen ResultsScreen { get { return resultsScreen; } }
        public static ProjectPreviewScreen ProjectPreviewScreen { get { return projectPreviewScreen; } }
        #endregion

        #region Extension Methods

        public static void PositionOver(this Window window, Window other) {
            other.Left = window.Left;
            other.Top = window.Top;
        }
        public static void PositionAt(this Window window, double left, double top) {
            window.Left = left;
            window.Top = top;
        }
        public static void SetTitle(this Window window, string title) {
            window.Title = title;
        }
        public static void SwapWith(this Window window, Window other) {
            other.PositionOver(window);
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
    public static class SystemTrayManager
    {
        /// <summary>
        /// Enables "minimize to tray" behavior for the specified Window.
        /// </summary>
        /// <param name="window">Window to enable the behavior for.</param>
        public static void Enable(Window window) {
            // No need to track this instance; its event handlers will keep it alive
            new TrayMinimizationProvider(window);
        }/// <summary>
        /// Enables "minimize to tray" behavior for the specified Window.
        /// </summary>
        /// <param name="window">Window to enable the behavior for.</param>
        public static void Enable(InProgressScreen window) {
            // No need to track this instance; its event handlers will keep it alive
            new TrayMinimizationProvider(window);
        }

        /// <summary>
        /// Class implementing "minimize to tray" functionality for a Window instance.
        /// </summary>
        class TrayMinimizationProvider
        {
            private Window _window;
            private System.Windows.Forms.NotifyIcon _notifyIcon;

            /// <summary>
            /// Initializes a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            /// <param name="window">Window instance to attach to.</param>
            internal TrayMinimizationProvider(Window window) {
                //System.Diagnostics.Debug.Assert(window != null, "window parameter is null.");
                _window = window;
                _window.StateChanged += HandleStateChanged;
            }
            internal TrayMinimizationProvider(InProgressScreen window) {
                window.ProcessingComplete += (s, e) => window.Title = "Analysis Complete";
                window.ProcessingComplete += HandleStateChanged;
                //System.Diagnostics.Debug.Assert(window != null, "window parameter is null.");
                _window = window;
                _window.StateChanged += HandleStateChanged;
            }

            /// <summary>
            /// Handles the Window's StateChanged event.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleStateChanged(object sender, EventArgs e) {
                if (_notifyIcon == null) {
                    // Initialize NotifyIcon instance "on demand"
                    _notifyIcon = new System.Windows.Forms.NotifyIcon();
                    _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);
                    _notifyIcon.MouseClick += HandleNotifyIconOrBalloonClicked;
                    _notifyIcon.BalloonTipClicked += HandleNotifyIconOrBalloonClicked;
                }
                // Update copy of Window Title in case it has changed
                _notifyIcon.Text = _window.Title;

                // Show/hide Window and NotifyIcon
                var minimized = (_window.WindowState == WindowState.Minimized);
                _window.ShowInTaskbar = !minimized;
                _notifyIcon.Visible = minimized;
                if (minimized) {
                    _notifyIcon.Visible = true;
                    _notifyIcon.ShowBalloonTip(1000, null, _window.Title, System.Windows.Forms.ToolTipIcon.None);
                }
            }

            /// <summary>
            /// Handles a click on the notify icon or its balloon.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleNotifyIconOrBalloonClicked(object sender, EventArgs e) {
                // Restore the Window
                _window.WindowState = WindowState.Normal;
            }
        }
    }
}
