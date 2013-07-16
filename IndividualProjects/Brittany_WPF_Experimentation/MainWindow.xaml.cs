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
using System.Collections.ObjectModel;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Utilities;
using System.IO;


namespace Brittany_WPF_Experimentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (ExpandoGrid.Height == 250) {
                ExpandoGrid.Height = 200;
                Height = 450;
            }
            else {
                Height = 250;
                ExpandoGrid.Height = 250;

            }

        }
    }





}
