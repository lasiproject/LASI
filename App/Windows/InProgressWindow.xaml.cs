using LASI.Content;
using LASI.Core;
using LASI.Interop;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Reactive.Linq;
using LASI.Interop.ResourceManagement;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for DialogToProceedToResults.xaml
    /// </summary>
    public partial class InProgressWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the InProgressScreen class.
        /// </summary>
        public InProgressWindow() {
            InitializeComponent();
            ConfigureOptions();
        }



        private void ConfigureOptions() {
            SetPlatformSpecificStyling();
        }

        private void SetPlatformSpecificStyling() {
            var osVersionInfo = System.Environment.Version;
            //Check if current OS is windows NT or later (PlatformID) and then check if Vista or 7 (Major) then check if 7 
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT && osVersionInfo.Major == 6 && osVersionInfo.Minor == 1) {
                progressBar.Foreground = System.Windows.Media.Brushes.DarkRed;
            }
        }


        #region Process Control

        /// <summary>
        /// Asynchronously processes all documents in the project in a comprehensive manner.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing the asynchronous processing operation.</returns>
        public async Task ParseDocuments() {

            var resourceLoadingNotifier = new ResourceNotifier();
            Observable.FromEventPattern<ResourceLoadEventArgs>(handler => resourceLoadingNotifier.ResourceLoading += handler, handler => resourceLoadingNotifier.ResourceLoading -= handler)
                  //.Throttle(TimeSpan.FromMilliseconds(1))
                  .Subscribe(e => ProgressUpdated(this, e.EventArgs));
            Observable.FromEventPattern<ResourceLoadEventArgs>(d => resourceLoadingNotifier.ResourceLoaded += d, d => resourceLoadingNotifier.ResourceLoaded -= d)
                            //.Throttle(TimeSpan.FromMilliseconds(1))
                            .Subscribe(e => ProgressUpdated(this, e.EventArgs));

            //resourceLoadingNotifier.ResourceLoading += ProgressUpdated;
            //resourceLoadingNotifier.ResourceLoaded += ProgressUpdated;

            var analysisProvider = new AnalysisOrchestrator(FileManager.TxtFiles);
            //analysisProvider.ProgressChanged += ProgressUpdated;
            Observable.FromEventPattern<Core.Reporting.ReportEventArgs>(h => analysisProvider.ProgressChanged += h, h => analysisProvider.ProgressChanged -= h)
                .Subscribe(e => ProgressUpdated(e.Sender, e.EventArgs));

            var timer = System.Diagnostics.Stopwatch.StartNew();
            WindowManager.ResultsScreen.Documents = await analysisProvider.ProcessAsync();
            progressBar.Value = 100;
            var completetionMessage = string.Format("Complete. Time: {0} miliseconds", timer.ElapsedMilliseconds);
            progressLabel.Content = completetionMessage;
            progressBar.ToolTip = completetionMessage;
            proceedtoResultsButton.Visibility = Visibility.Visible;
            NativeMethods.StartFlashing(this);
            ProcessingCompleted += delegate { NativeMethods.StopFlashing(this); };
            await Task.WhenAll(WindowManager.ResultsScreen.CreateWeightViewsForAllDocumentsAsync(), WindowManager.ResultsScreen.BuildTextViewsForAllDocumentsAsync());
            ProcessingCompleted(this, new EventArgs());
        }


        private void ProceedToResultsView() {
            WindowManager.ResultsScreen.SetTitle(WindowManager.StartupScreen.ProjectNameTextBox.Text + " - L.A.S.I.");
            this.SwapWith(WindowManager.ResultsScreen);
        }
        #endregion

        #region Named Event Handlers

        private async void ProgressUpdated(object sender, Core.Reporting.ReportEventArgs e) {
            progressLabel.Content = e.Message;
            progressBar.ToolTip = e.Message;
            var animateStep = 0.028 * e.PercentWorkRepresented;
            for (int i = 0; i < 33; ++i) {
                progressBar.Value += animateStep;
                await Task.Delay(1);
            }
        }

        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            this.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            this.TaskbarItemInfo.ProgressValue = e.NewValue / 100;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
            Application.Current.Shutdown();
        }



        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            try {
                DragMove();
            } catch (ArgumentOutOfRangeException x) {
                System.Diagnostics.Debug.Write(x.Message);
            }
        }

        private void proceedtoResultsButton_Click(object sender, RoutedEventArgs e) {
            ProceedToResultsView();
        }
        private void minButton_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Taskbar Notification
        static class NativeMethods
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

            private const UInt32 FLASHW_ALL = 3;

            [StructLayout(LayoutKind.Sequential)]
            private struct FLASHWINFO
            {
                public System.UInt32 cbSize;
                public System.IntPtr hwnd;
                public System.UInt32 dwFlags;
                public System.UInt32 uCount;
                public System.UInt32 dwTimeout;

            }
            /// <summary>
            /// Causes the application icon to begin flashing in the Windows Taskbar.
            /// </summary>
            internal static void StartFlashing(Window windowToFlash) {
                {
                    var fInfo = new FLASHWINFO {
                        hwnd = new System.Windows.Interop.WindowInteropHelper(windowToFlash).Handle,
                        dwFlags = FLASHW_ALL,
                        uCount = System.UInt32.MaxValue,
                        dwTimeout = 0
                    };
                    fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));
                    FlashWindowEx(ref fInfo);
                }
                windowToFlash.StateChanged += (s, e) => StopFlashing(windowToFlash);

            }
            /// <summary>
            /// Cuases the application icon in the Windows Taskbar to dicontinue flashing.
            /// </summary>
            internal static void StopFlashing(Window windowToFlash) {
                var fInfo = new FLASHWINFO {

                    hwnd = new System.Windows.Interop.WindowInteropHelper(windowToFlash).Handle,
                    dwFlags = 0,
                    uCount = System.UInt32.MaxValue,
                    dwTimeout = 0
                }; fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));
                FlashWindowEx(ref fInfo);
            }
        }
        #endregion

        /// <summary>
        /// Raised when processing of all documents has been completed.
        /// </summary>
        public event EventHandler ProcessingCompleted = delegate { };

    }
}