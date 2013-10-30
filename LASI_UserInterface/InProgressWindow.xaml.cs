using LASI.ContentSystem;
using LASI.InteropLayer;
using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

            var processController = new ProcessController();
            processController.ProgressChanged += async (sender, e) => {
                progressLabel.Content = e.Message;
                progressBar.ToolTip = e.Message;
                var animateStep = 0.028 * e.Increment;
                for (int i = 0; i < 33; ++i) {
                    progressBar.Value += animateStep;
                    await Task.Delay(1);
                }
            };
            var analyzedDocuments = await processController.AnalyseAllDocumentsAsync(FileManager.TextFiles);

            progressBar.Value = 100;
            progressLabel.Content = "Complete";
            progressBar.ToolTip = "Complete";
            WindowManager.ResultsScreen.Documents = analyzedDocuments.ToList();
            proceedtoResultsButton.Visibility = Visibility.Visible;
            NativeMethods.StartFlashing(this);
            if (ProcessingComplete != null) {
                ProcessingComplete(this, new EventArgs());
            }
            await WindowManager.ResultsScreen.CreateWeightViewsForAllDocumentsAsync();
            await WindowManager.ResultsScreen.BuildTextViewsForAllDocumentsAsync();

        }

        private void ProceedToResultsView() {
            WindowManager.ResultsScreen.SetTitle(WindowManager.StartupScreen.ProjectNameTextBox.Text + " - L.A.S.I.");

            this.SwapWith(WindowManager.ResultsScreen);

        }
        #endregion

        #region Named Event Handlers

        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            this.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            this.TaskbarItemInfo.ProgressValue = e.NewValue / 100;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        private void ExitMenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();
            Application.Current.Shutdown();
        }


        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            try {
                DragMove();
            }
            catch (ArgumentOutOfRangeException) {

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
                    FLASHWINFO fInfo = new FLASHWINFO();
                    fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));

                    fInfo.hwnd = new System.Windows.Interop.WindowInteropHelper(windowToFlash).Handle;

                    fInfo.dwFlags = FLASHW_ALL;

                    fInfo.uCount = System.UInt32.MaxValue;

                    fInfo.dwTimeout = 0;

                    FlashWindowEx(ref fInfo);
                }
                windowToFlash.StateChanged += (s, e) => StopFlashing(windowToFlash);

            }
            /// <summary>
            /// Cuases the application icon in the Windows Taskbar to dicontinue flashing.
            /// </summary>
            internal static void StopFlashing(Window windowToFlash) {
                FLASHWINFO fInfo = new FLASHWINFO();


                fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));

                fInfo.hwnd = new System.Windows.Interop.WindowInteropHelper(windowToFlash).Handle;

                fInfo.dwFlags = 0;

                fInfo.uCount = System.UInt32.MaxValue;

                fInfo.dwTimeout = 0;

                FlashWindowEx(ref fInfo);
            }
        }
        #endregion

        /// <summary>
        /// Raised when processing of all documents has been completed.
        /// </summary>
        public event EventHandler ProcessingComplete;

    }
}