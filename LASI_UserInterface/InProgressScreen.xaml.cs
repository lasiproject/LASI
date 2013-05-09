using LASI.GuiInterop;
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

            Output.SetToSilent();
            this.IsVisibleChanged += async (s, e) => await InitPawPrintAlternation();
            this.Closing += (s, e) => Application.Current.Shutdown();
            ProgressBar.Value = 0;
            ProgressLabel.Content = ProcessingState.Initializing;


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


        #region Animation

        private async Task InitPawPrintAlternation() {
            var pawPrints = new[] { pawPrintImg1, pawPrintImg3, pawPrintImg2, pawPrintImg4, pawPrintImg5, pawPrintImg6 }.ToList();
            pawPrints.ForEach(pp => pp.Opacity = 0);
            foreach (var pp in pawPrints) {
                FadeImage(pp);
                await Task.Delay(2700);
            }

        }
        private async void FadeImage(Image img) {
            while (img.Opacity > 0.0) {
                img.Opacity -= 0.01;
                await Task.Delay(10);
            }
            await Task.Delay(500);
            while (img.Opacity < 1.0) {
                img.Opacity += 0.01;
                await Task.Delay(10);
            }
            FadeImage(img);

        }

        #endregion




        #region Process Control


        ProcessController processController = new ProcessController();
        public async Task InitializeParsing() {

            var progressPercentage = Resources["AnalysisProgressPercentage"];
            var analyzedDocuments = await processController.LoadAndAnalyseAllDocuments(ProgressBar, ProgressLabel);
            ProgressBar.Value = 100;
            ProgressLabel.Content = "Complete";
            WindowManager.ResultsScreen.Documents = analyzedDocuments.ToList();
            ProceedtoResultsButton.Visibility = Visibility.Visible;
            startflashing();

        }





        #endregion


        private async Task ProceedToResultsView() {
            WindowManager.ResultsScreen.SetTitle(WindowManager.CreateProjectScreen.LastLoadedProjectName + " - L.A.S.I.");


            await WindowManager.ResultsScreen.CreateInteractiveViews();
            WindowManager.ResultsScreen.BuildReconstructedDocumentViews();
            this.SwapWith(WindowManager.ResultsScreen);
        }

        private void ExitMenuItem_Click_3(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();

        }
        private async void ProceedtoResultsButton_Click(object sender, RoutedEventArgs e) {
            await ProceedToResultsView();
        }

        private void minButton_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
            PerformanceManager.PerformanceMode = PerformanceMode.HiddenLongRunning;
        }
        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
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

        void startflashing() {
            {
                FLASHWINFO fInfo = new FLASHWINFO();
                fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));

                fInfo.hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

                fInfo.dwFlags = FLASHW_ALL;

                fInfo.uCount = System.UInt32.MaxValue;

                fInfo.dwTimeout = 0;



                FlashWindowEx(ref fInfo);
            }
            this.GotFocus += (s, e) => {

                stopflashing();

            };
        }
        void stopflashing() {
            FLASHWINFO fInfo = new FLASHWINFO();


            fInfo.cbSize = System.Convert.ToUInt32(Marshal.SizeOf(fInfo));

            fInfo.hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            fInfo.dwFlags = 0;

            fInfo.uCount = System.UInt32.MaxValue;

            fInfo.dwTimeout = 0;



            FlashWindowEx(ref fInfo);
        }

        #endregion
    }
}