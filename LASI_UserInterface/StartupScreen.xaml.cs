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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Configuration;
using System.Threading;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for StartupScreen.xaml
    /// </summary>
    public partial class StartupScreen : Window
    {
        public StartupScreen() {
            InitializeComponent();
            WindowManager.StartupScreen = this;
            BindWindowEventHandlers();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;


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


        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }



        private void createProjectButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            WindowManager.CreateProjectScreen.PositionAt(this.Left, Math.Abs((this.Height - WindowManager.CreateProjectScreen.Height) / 2));
            WindowManager.CreateProjectScreen.Show();

        }


    }
}
