using LASI.Content;
using LASI.Core;
using LASI.Interop;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Reactive.Linq;
using System.Reactive;
using LASI.Interop.ResourceManagement;
using System.ComponentModel;

namespace LASI.App
{
    using LASI.App.Helpers;
    using ReportEventArgs = Core.Configuration.ReportEventArgs;
    using static WindowManager;
    /// <summary>
    /// Interaction logic for DialogToProceedToResults.xaml
    /// </summary>
    public partial class InProgressWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the InProgressScreen class.
        /// </summary>
        public InProgressWindow()
        {
            InitializeComponent();
            SetPlatformSpecificStyling();
        }


        private void SetPlatformSpecificStyling()
        {
            var osVersionInfo = System.Environment.Version;
            //Check if current OS is windows NT or later (PlatformID) and then check if Vista or 7 (Major) then check if 7 
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT && osVersionInfo.Major == 6 && osVersionInfo.Minor == 1)
            {
                progressBar.Foreground = System.Windows.Media.Brushes.DarkRed;
            }
        }


        #region Process Control

        /// <summary>
        /// Asynchronously processes all documents in the project in a comprehensive manner.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing the asynchronous processing operation.</returns>
        public async Task ParseDocuments()
        {
            var resourceLoadingNotifier = new ResourceNotifier();
            var analysisOrchestrator = new AnalysisOrchestrator(FileManager.TxtFiles);

            var loadingEvents = ConfigureLoadingEvents(resourceLoadingNotifier);
            var loadedEvents = ConfigureLoadedEventStream(resourceLoadingNotifier);
            var analysisUpdateEvents = ConfigureAnalysisUpdateEvents(analysisOrchestrator);

            Observable.Merge(
                    loadingEvents.Select(pattern => pattern.EventArgs),
                    loadedEvents.Select(pattern => pattern.EventArgs),
                    analysisUpdateEvents.Select(pattern => pattern.EventArgs)
                )
                .Select(pattern => new
                {
                    pattern.Message,
                    Progress = pattern.PercentWorkRepresented
                })
                .SubscribeOn(System.Threading.SynchronizationContext.Current)
                .Subscribe(onNext: async e =>
                {
                    progressLabel.Content = e.Message;
                    progressBar.ToolTip = e.Message;
                    var animateStep = 0.028 * e.Progress;
                    for (var i = 0; i < 33; ++i)
                    {
                        progressBar.Value += animateStep;
                        await Task.Yield();
                    }
                });
            var timer = System.Diagnostics.Stopwatch.StartNew();
            ResultsScreen.Documents = await analysisOrchestrator.ProcessAsync();
            progressBar.Value = 100;
            var completetionMessage = $"Processing Complete. Time: {timer.ElapsedMilliseconds / 1000f} seconds";
            progressLabel.Content = completetionMessage;
            progressBar.ToolTip = completetionMessage;
            proceedtoResultsButton.Visibility = Visibility.Visible;
            NativeMethods.StartFlashing(this);
            await Task.WhenAll(ResultsScreen.CreateWeightViewsForAllDocumentsAsync(), ResultsScreen.BuildTextViewsForAllDocumentsAsync());
            ProcessingCompleted(this, new EventArgs());
        }
        #region Observable Event Adapters
        private IObservable<EventPattern<ReportEventArgs>> ConfigureAnalysisUpdateEvents(AnalysisOrchestrator analyzerNotifier) =>
            Observable.FromEventPattern<ReportEventArgs>(
                addHandler: h => analyzerNotifier.ProgressChanged += h,
                removeHandler: h => analyzerNotifier.ProgressChanged -= h
            );

        private IObservable<EventPattern<ResourceLoadEventArgs>> ConfigureLoadingEvents(ResourceNotifier loadingNotifier) =>
            Observable.FromEventPattern<ResourceLoadEventArgs>(
                addHandler: h => loadingNotifier.ResourceLoading += h,
                removeHandler: h => loadingNotifier.ResourceLoading -= h
            );
        private IObservable<EventPattern<ResourceLoadEventArgs>> ConfigureLoadedEventStream(ResourceNotifier loadedNotifier) =>
            Observable.FromEventPattern<ResourceLoadEventArgs>(
                addHandler: h => loadedNotifier.ResourceLoaded += h,
                removeHandler: h => loadedNotifier.ResourceLoaded -= h
            );

        #endregion

        private void ProceedToResultsView()
        {
            WindowManager.ResultsScreen.SetTitle(WindowManager.StartupScreen.ProjectNameTextBox.Text + " - L.A.S.I.");
            this.SwapWith(WindowManager.ResultsScreen);
        }
        #endregion

        #region Named Event Handlers

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            this.TaskbarItemInfo.ProgressValue = e.NewValue / 100;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }



        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (ArgumentOutOfRangeException x)
            {
                System.Diagnostics.Debug.Write(x.Message);
            }
        }

        private void ProceedtoResultsButton_Click(object sender, RoutedEventArgs e)
        {
            ProceedToResultsView();
        }
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Raised when processing of all documents has been completed.
        /// </summary>
        public event EventHandler ProcessingCompleted = delegate { };

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
            internal static void StartFlashing(Window windowToFlash)
            {
                {
                    var fInfo = new FLASHWINFO
                    {
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
            /// Causes the application icon in the Windows Taskbar to discontinue flashing.
            /// </summary>
            internal static void StopFlashing(Window windowToFlash)
            {
                var fInfo = new FLASHWINFO
                {

                    hwnd = new System.Windows.Interop.WindowInteropHelper(windowToFlash).Handle,
                    dwFlags = 0,
                    uCount = System.UInt32.MaxValue,
                    dwTimeout = 0
                }; fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));
                FlashWindowEx(ref fInfo);
            }
        }


        #endregion

    }
}