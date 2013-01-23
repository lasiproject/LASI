using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class InProgressScreen : Window
    {
        public InProgressScreen()
        {
            InitializeComponent();


        }
        private void BindEventHandlers()
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            WindowManager.ResultsScreen.PositionAt(this.Left, this.Top);
            WindowManager.ResultsScreen.SetTitle(WindowManager.CreateProjectScreen.LastLoadedProjectName + " - L.A.S.I.");
            WindowManager.ResultsScreen.Show();
            this.Hide();
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
