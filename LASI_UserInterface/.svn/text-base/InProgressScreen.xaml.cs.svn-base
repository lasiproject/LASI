using LASI.InteropLayer;
using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for DialogToProceedToResults.xaml
    /// </summary>
    public partial class InProgressScreen : Window
    {
        public InProgressScreen() {
            InitializeComponent();
            //WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ConfigureOptions();
        }



        private void ConfigureOptions() {
            SetPlatformSpecificStyling();

            minButton.Click += (sender, e) => WindowState = WindowState.Minimized;
        }

        private void SetPlatformSpecificStyling() {
            var osVersionInfo = System.Environment.Version;
            //Check if current OS is windows NT or later (PlatformID) and then check if Vista or 7 (Major) then check if 7 
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT && osVersionInfo.Major == 6 && osVersionInfo.Minor == 1) {
                progressBar.Foreground = System.Windows.Media.Brushes.DarkRed;
            }
        }


        #region Process Control

        public async Task InitializeParsing() {

            var processController = new ProcessController();
            var progressPercentage = Resources["AnalysisProgressPercentage"];
            var analyzedDocuments = await processController.AnalyseAllDocumentsAsync(LASI.ContentSystem.FileManager.TextFiles, async (message, increment) => await UpdateProgressDisplay(message, increment));
            progressBar.Value = 100;
            progressLabel.Content = "Complete";
            WindowManager.ResultsScreen.Documents = analyzedDocuments.ToList();
            proceedtoResultsButton.Visibility = Visibility.Visible;
            StartFlashing();
            if (ProcessingComplete != null)
                ProcessingComplete(this, new EventArgs());

        }



        /// <summary>
        /// Updates progress by setting the progressLabel and progressBar tooltip to the provided status message and incrementing the progressBar by the provided value.
        /// </summary>
        /// <param name="statusMessage">The status update message to display.</param>
        /// <param name="progressIncrement">The amount by which progress is to be incremented.</param>
        /// <returns>A System.Threading.Tasks.Task representing the asynchronous Update Progress operation.</returns>
        private async Task UpdateProgressDisplay(string statusMessage, double progressIncrement) {
            progressLabel.Content = statusMessage;
            progressBar.ToolTip = statusMessage;
            var animateStep = progressIncrement / 100d;
            for (int i = 0; i < 25d; ++i) {
                progressBar.Value += 4 * animateStep;
                await Task.Delay(1);

            }
        }
        private async Task ProceedToResultsView() {
            WindowManager.ResultsScreen.SetTitle(WindowManager.StartupScreen.ProjectNameTextBox.Text + " - L.A.S.I.");
            this.SwapWith(WindowManager.ResultsScreen);

            await WindowManager.ResultsScreen.CreateWeightViewsForAllDocumentsAsync();
            await WindowManager.ResultsScreen.BuildTextViewsForAllDocumentsAsync();

        }
        #endregion



        #region Event Handlers

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
            DragMove();
        }

        #endregion


        #region Taskbar Notification

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);


        public const UInt32 FLASHW_ALL = 3;
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
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
        void StartFlashing() {
            {
                FLASHWINFO fInfo = new FLASHWINFO();
                fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));

                fInfo.hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

                fInfo.dwFlags = FLASHW_ALL;

                fInfo.uCount = System.UInt32.MaxValue;

                fInfo.dwTimeout = 0;

                FlashWindowEx(ref fInfo);
            }
            this.StateChanged += (s, e) => StopFlashing();

        }
        /// <summary>
        /// Cuases the application icon in the Windows Taskbar to dicontinue flashing.
        /// </summary>
        void StopFlashing() {
            FLASHWINFO fInfo = new FLASHWINFO();


            fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));

            fInfo.hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            fInfo.dwFlags = 0;

            fInfo.uCount = System.UInt32.MaxValue;

            fInfo.dwTimeout = 0;

            FlashWindowEx(ref fInfo);
        }

        #endregion

        private async void proceedtoResultsButton_Click(object sender, RoutedEventArgs e) {
            await ProceedToResultsView();
        }
        private void minButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
            //if ((App.Current as App).AppSettingsCollection["ReduceResourceUsaageWhenMinimized"].Value == "true") {
            //    PerformanceManager.SetPerformanceLevel(PerforamanceLevel.Low);
            //}
        }


        private void Window_Activated(object sender, EventArgs e) {
            if (WindowState == System.Windows.WindowState.Minimized) {
                var currentForgroundPerformance = (PerforamanceLevel)Enum.Parse(typeof(PerforamanceLevel), LASI.UserInterface.Properties.Settings.Default.PerformanceLevel);
                PerformanceManager.SetPerformanceLevel(currentForgroundPerformance);
            }
        }

        public event EventHandler ProcessingComplete;

    }
}