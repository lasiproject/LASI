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
using System.Threading;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen() {
            InitializeComponent();
            WindowManager.SplashScreen = this;
            BindWindowEventHandlers();
        }
        void BindWindowEventHandlers() {
            this.MouseLeftButtonDown += (s, e) => DragMove();
        }


        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }



        private void createProjectButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            WindowManager.CreateProjectScreen.PositionAt(this.Left, this.Top);
            WindowManager.CreateProjectScreen.Show();

        }


    }
}
