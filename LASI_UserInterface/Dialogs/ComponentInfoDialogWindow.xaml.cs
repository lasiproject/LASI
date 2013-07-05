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
            label1.ContextMenu = new System.Windows.Controls.ContextMenu();
            label2.ContextMenu = new System.Windows.Controls.ContextMenu();
            label3.ContextMenu = new System.Windows.Controls.ContextMenu();
            label4.ContextMenu = new System.Windows.Controls.ContextMenu();
            label5.ContextMenu = new System.Windows.Controls.ContextMenu();
            var item = new MenuItem {
                Header = "View License"
            };
            item.Click += (s, e) => System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\b2xtranslatorLicense.txt"));
            label1.ContextMenu.Items.Add(item);
            item = new MenuItem {
                Header = "View License"
            };
            item.Click += (s, e) => System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\iTextSharpLicense.txt"));
            label2.ContextMenu.Items.Add(item);
            item = new MenuItem {
                Header = "View License"
            };
            item.Click += (s, e) => System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\SharpNLPLicense.txt"));
            label3.ContextMenu.Items.Add(item);
            item = new MenuItem {
                Header = "View License"
            };
            item.Click += (s, e) => System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\WPFToolkitLicense.txt"));
            label4.ContextMenu.Items.Add(item);
            item = new MenuItem {
                Header = "View License"
            };
            item.Click += (s, e) => System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\Licenses\WordNetLicense.txt"));
            label5.ContextMenu.Items.Add(item);
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

        private void MenuItem_Click(object sender, RoutedEventArgs e) {

        }
    }
}
