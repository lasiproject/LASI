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

        #region Interaction Logic

        /// <summary>
        /// Opens the file indicated specified by the provided path using the default associated application for the current machine, displaying the default help URI if an error occurs.
        /// </summary>
        /// <param name="filePath">The location of the file to open. This may be an absolute or relative path.</param>
        private void OpenFileInDefaultApp(string filePath) {
            OpenFileInDefaultApp(filePath, "http://lasi-product.org");
        }
        /// <summary>
        /// Opens the file indicated specified by the provided path using the default associated application for the current machine, displaying the provided help URI if an error occurs.
        /// </summary>
        /// <param name="filePath">The location of the file to open. This may be an absolute or relative path.</param>
        /// <param name="helpURI">The help URI to display if an error occurs.</param>
        private void OpenFileInDefaultApp(string filePath, string helpURI) {
            try {
                System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, filePath));
            }
            catch (System.ComponentModel.Win32Exception) {
                MessageBox.Show(this, string.Format("An error occured when trying to open or locate the file {0}. Please visit {1} for assistance.", filePath, helpURI));
            }
        }

        #region Event Handlers

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            OpenFileInDefaultApp((sender as Label).Tag.ToString());
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            OpenFileInDefaultApp(label1.Tag.ToString());
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            OpenFileInDefaultApp(label2.Tag.ToString());
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            OpenFileInDefaultApp(label3.Tag.ToString());
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e) {
            OpenFileInDefaultApp(label4.Tag.ToString());
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e) {
            OpenFileInDefaultApp(label5.Tag.ToString());
        }
        private void okButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        #endregion

        #endregion
    }
}
