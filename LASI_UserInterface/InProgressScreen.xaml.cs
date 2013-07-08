using LASI.InteropLayer;
using LASI.Utilities;
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
            BindControlsAndSettings();
            WindowManager.InProgressScreen = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Closing += (s, e) => Application.Current.Shutdown();
            ProgressBar.Value = 0;
            ProgressLabel.Content = "Initializing";


        }

        void BindControlsAndSettings() {
            this.MouseLeftButtonDown += (s, e) => DragMove();
            if (ConfigurationManager.AppSettings["AutoDebugCleanupOn"] == "true") {
                App.Current.Exit += (sender, e) => {
                    if (FileSystem.FileManager.Initialized)
                        FileSystem.FileManager.DecimateProject();
                };
            }
        }


     

        #region Process Control


        ProcessController processController = new ProcessController();
        public async Task InitializeParsing() {

            var progressPercentage = Resources["AnalysisProgressPercentage"];
            var analyzedDocuments = await processController.AnalyseAllDocumentsAsync(LASI.FileSystem.FileManager.TextFiles, async (message, increment) => await UpdateProgressDisplay(message, increment));
            ProgressBar.Value = 100;
            ProgressLabel.Content = "Complete";
            WindowManager.ResultsScreen.Documents = analyzedDocuments.ToList();
            ProceedtoResultsButton.Visibility = Visibility.Visible;
            StartFlashing();

        }




        private async Task UpdateProgressDisplay(string statusMessage, double progressIncrement) {
            ProgressLabel.Content = statusMessage;
            ProgressBar.ToolTip = statusMessage;
            //ProgressBar.ApplyAnimationClock(ProgressBar.ValueProperty, new System.Windows.Media.Animation.DoubleAnimation(ProgressBar.Value + progressIncrement, new Duration(TimeSpan.FromSeconds(1))).CreateClock());

            var animateStep = progressIncrement / 100d;
            for (int i = 0; i < 25d; ++i) {
                ProgressBar.Value += 4 * animateStep;
                await Task.Delay(1);

            }
        }
        #endregion


        private async Task ProceedToResultsView() {
            WindowManager.ResultsScreen.SetTitle(WindowManager.StartupScreen.ProjectNameTextBox.Text + " - L.A.S.I.");
            this.SwapWith(WindowManager.ResultsScreen);

            await WindowManager.ResultsScreen.CreateInteractiveViews();
            await WindowManager.ResultsScreen.BuildReconstructedDocumentViews();

        }

        private void ExitMenuItem_Click_3(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();

        }
        private async void ProceedtoResultsButton_Click(object sender, RoutedEventArgs e) {
            await ProceedToResultsView();
        }

        private void minButton_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
            PerformanceManager.SetPerformanceMode(PerforamanceMode.Low);
        }
        private void closeButton_Click(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();
        }


        #region Taskbar Flashing

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
            this.Activated += (s, e) => StopFlashing();

        }
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

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            this.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            this.TaskbarItemInfo.ProgressValue = e.NewValue / 100;
        }
    }
}