﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for ComponentInfoDialogWindow.xaml
    /// </summary>
    public partial class ComponentInfoDialogWindow : Window
    {
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
            OpenFileInDefaultApp(filePath, "http://lasi-product.org");
        }
        /// <summary>
        /// Opens the file indicated specified by the provided path using the default associated application for the current machine, displaying the provided help URI if an error occurs.
        /// </summary>
        /// <param name="filePath">The location of the file to open. This may be an absolute or relative path.</param>
        /// <param name="helpURI">The help URI to Display if an error occurs.</param>
        private void OpenFileInDefaultApp(string filePath, string helpURI)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, filePath));
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show(this, $"An error occured when trying to open or locate the file {filePath}. Please visit {helpURI} for assistance.");
            }
        }

        #region Event Handlers

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileInDefaultApp((sender as Label).Tag.ToString());
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp(label1.Tag.ToString());
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp(label2.Tag.ToString());
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp(label3.Tag.ToString());
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp(label4.Tag.ToString());
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileInDefaultApp(label5.Tag.ToString());
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) { this.Close(); }
        }

        #endregion

        #endregion
    }
}
