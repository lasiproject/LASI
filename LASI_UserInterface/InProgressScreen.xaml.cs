using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LASI.Utilities;
using LASI.InteropLayer;
using LASI.GuiInterop;
using LASI.UserInterface.Dialogs;
using System.IO;
using System.Windows.Navigation;
using System.Configuration;
namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for DialogToProcedeToResults.xaml
    /// </summary>
    public partial class InProgressScreen : Window
    {
        public InProgressScreen() {
            InitializeComponent();
            BindWindowEventHandlers();
            WindowManager.InProgressScreen = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;


            Output.SetToSilent();
            this.IsVisibleChanged += async (s, e) => await InitPawPrintAlternation();
            this.Closing += (s, e) => Application.Current.Shutdown();
            ProgressBar.Value = 0;
            ProgressLabel.Content = ProcessingState.Initializing;


        }

        void BindWindowEventHandlers() {
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


        ProcessController status = new ProcessController();


        public async Task InitializeParsing() {


            var msg = await status.LoadAndAnalyseAllDocuments(ProgressBar, ProgressLabel);
            ProgressBar.Value = 100;
            ProgressBar.ToolTip = "Complete";
            ProgressLabel.Content = "Complete";
            WindowManager.ResultsScreen.Documents = msg.ToList();
            ProcedetoResultsButton.Visibility = Visibility.Visible;


        }





        #endregion


        private async Task ProceedToResultsView() {
            WindowManager.ResultsScreen.SetTitle(WindowManager.CreateProjectScreen.LastLoadedProjectName + " - L.A.S.I.");


            await WindowManager.ResultsScreen.CreateInteractiveViews();
            WindowManager.ResultsScreen.BuildAssociationTextView();
            this.SwapWith(WindowManager.ResultsScreen);
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) {

        }

        private async void ProcedetoResultsButton_Click(object sender, RoutedEventArgs e) {
            await ProceedToResultsView();

        }

        private void minButton_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }
        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}