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

namespace LASI.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for ComponentInfoDialogWindow.xaml
    /// </summary>
    public partial class ComponentInfoDialogWindow : Window
    {
        public ComponentInfoDialogWindow() {
            InitializeComponent();
        }

        private void label1_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\b2xtranslatorLicense.txt"));
        }

        private void label2_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\iTextSharpLicense.txt"));
        }

        private void label3_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\SharpNLPLicense.txt"));
        }

        private void label4_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

            System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\WPFToolkitLicense.txt"));
        }
        private void label5_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\WordNetLicense.txt"));
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }
    }
}
