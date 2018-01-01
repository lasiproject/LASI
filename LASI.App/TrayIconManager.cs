using System;
using System.Linq;
using System.Windows;
using LASI.Utilities;

namespace LASI.App
{
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
        /// <param name = "window"><see cref="Window"/> to enable the behavior on.</param>
        /// <param name="windows">Additional <see cref="Window"/>s to enable the behavior on.</param>
        public static void Enable(Window window, params Window[] windows)
        {
            // No need to track this instance; its event handlers will keep it alive
            foreach (var w in windows.Prepend(window))
            {
                switch (w)
                {
                    case InProgressWindow inProgress:
                        TrayIconProvider.FromInProgressWindow(inProgress);
                        break;
                    default:
                        TrayIconProvider.FromWindow(w);
                        break;
                }
            }
        }

        /// <summary>
        /// Class implementing "minimize to tray" functionality for a Window instance.
        /// </summary>
        class TrayIconProvider : IDisposable
        {
            /// <summary>
            /// Creates a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            /// <param name = "window">Window instance to attach to.</param>
            public static TrayIconProvider FromWindow(Window window) => new TrayIconProvider(window);

            /// <summary>
            /// Creates a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            /// <param name = "window">Window instance to attach to.</param>
            public static TrayIconProvider FromInProgressWindow(InProgressWindow window) => new TrayIconProvider(window);

            /// <summary>
            /// Initializes a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            /// <param name = "window">Window instance to attach to.</param>
            TrayIconProvider(Window window)
            {
                this.window = window;
                window.StateChanged += HandleStateChanged;
            }

            TrayIconProvider(InProgressWindow window) : this((Window)window)
            {
                window.ProcessingCompleted += (s, e) =>
                {
                    window.Title = "Analysis Complete";
                    HandleStateChanged(s, e);
                };
            }

            /// <summary>
            /// Handles the Window's StateChanged event.
            /// </summary>
            /// <param name = "sender">Event source.</param>
            /// <param name = "e">Event arguments.</param>
            private void HandleStateChanged(object sender, EventArgs e)
            {
                EnsureIcon();

                // Update copy of Window Title in case it has changed
                notifyIcon.Text = window.Title;
                // Show/hide Window and NotifyIcon
                window.ShowInTaskbar = !window.Minimized;
                notifyIcon.Visible = window.Minimized;
                if (notifyIcon.Visible)
                {
                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, null, window.Title, System.Windows.Forms.ToolTipIcon.None);
                }
            }

            private void EnsureIcon()
            {
                if (notifyIcon != null)
                {
                    return;
                }

                // Initialize NotifyIcon instance "on demand"
                notifyIcon = new System.Windows.Forms.NotifyIcon
                {
                    Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location)
                };
                notifyIcon.MouseClick += HandleNotifyIconOrBalloonClicked;
                notifyIcon.BalloonTipClicked += HandleNotifyIconOrBalloonClicked;
            }

            /// <summary>
            /// Handles a click on the notify icon or its balloon.
            /// </summary>
            /// <param name = "sender">Event source.</param>
            /// <param name = "e">Event arguments.</param>
            private void HandleNotifyIconOrBalloonClicked(object sender, EventArgs e) => window.Restore();


            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    notifyIcon.Dispose();
                }
            }

            private WrappedWindow window;
            private System.Windows.Forms.NotifyIcon notifyIcon;

            private struct WrappedWindow
            {
                public static implicit operator WrappedWindow(Window window) => new WrappedWindow
                {
                    Window = window
                };

                public bool Minimized => Window.WindowState == WindowState.Minimized;
                public void Restore() => Window.WindowState = WindowState.Normal;
                public string Title => Window.Title;

                public bool ShowInTaskbar
                {
                    get => Window.ShowInTaskbar;
                    set => Window.ShowInTaskbar = value;
                }

                private Window Window { get; set; }
            }
        }
    }
}
