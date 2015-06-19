using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LASI.App.Helpers;
namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for ComponentInfoDialogWindow.xaml
    /// </summary>
    public partial class ComponentInfoDialogWindow : Window
    {
        private const string ProductHomePage = "http://lasi-product.org";

        /// <summary>
        /// Initializes a new instance of the ComponentInfoDialogWindow class.
        /// </summary>
        public ComponentInfoDialogWindow()
        {
            InitializeComponent();
        }

        #region Interaction Logic

        /// <summary>
        /// Opens the file indicated specified by the provided path using the default associated application for the current machine, displaying the default help URI if an error occurs.
        /// </summary>
        /// <param name="filePath">The location of the file to open. This may be an absolute or relative path.</param>
        private void OpenFileInDefaultApp(string filePath)
        {
            OpenTxtFileInDefaultApp(filePath, ProductHomePage);
        }
        /// <summary>
        /// Opens the file indicated specified by the provided path using the default associated application for the current machine, displaying the provided help URI if an error occurs.
        /// </summary>
        /// <param name="filePath">The location of the file to open. This may be an absolute or relative path.</param>
        /// <param name="helpUri">The help URI to Display if an error occurs.</param>
        private void OpenTxtFileInDefaultApp(string filePath, string helpUri)
        {
            var actualPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, filePath);
            try
            {
                using (var reader = new System.IO.StreamReader(actualPath, encoding: System.Text.Encoding.UTF8))
                {
                    var licenseViewer = new LicenseDisplayDialogWindow(reader.ReadToEnd());
                    var pos = PointToScreen(new Point(this.Left, this.Top));
                    licenseViewer.Reposition(pos.Y * 0.5, pos.X * 0.5).Show();
                    EventHandler closed = delegate { licenseViewer.Close(); };
                    this.Closed += closed;
                    if (this.Owner != null)
                    {
                        this.Owner.Closed += closed;
                    }
                }
            }
            catch (Win32Exception)
            {
                MessageBox.Show(this, $"An error occured when trying to open or locate the file {actualPath}. Please visit {helpUri} for assistance.");
            }
        }

        #region Event Handlers

        private void LicenseLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileInDefaultApp((string)(sender as Label).Tag);
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp((string)(label1.Tag));
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp((string)(label2.Tag));
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp((string)(label3.Tag));
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp((string)(label4.Tag));
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp((string)(label5.Tag));
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            this.CloseIf(e.Key == Key.Escape);
        }
        private void LicenseLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                OpenFileInDefaultApp((string)(sender as Label).Tag);
            }
        }

        #endregion

        #endregion

    }
}
