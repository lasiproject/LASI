using Microsoft.Win32;
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
using System.IO;
using System.Diagnostics;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectScreen : Window
    {
        public CreateProjectScreen() {
            InitializeComponent();
            LastLoadedProjectName = "";
            CreateButtons();
            this.Closing += (s, e) => Application.Current.Shutdown();
        }

        void CreateButtons() {
            for (var i = 0; i < 10; ++i) {
                var num = i;
                var button = new Button {
                    Content = i.ToString()
                };

                button.Click += (sender, e) => {
                    //MessageBox.Show(string.Format("num: {0}: even?: {1}", num, (num % 2 == 0)));
                    button.MouseDoubleClick += (s, args) => dynamicContent.Children.Remove(button);
              
                };

                dynamicContent.Children.Add(button);
                
 }

        }


        #region EventHandlers

        private void browseForDocButton_Click(object sender, RoutedEventArgs e) {
            var openDialog = new OpenFileDialog();
            openDialog.ShowDialog(this);

            var docPath = openDialog.FileName;
            lastDocPath.Text = docPath;

            var num = "x";
            var button = new Button
            {
                Content = num.ToString()
            };

            var docEntry = new ListViewItem {
                Content = docPath
            };

            button.Click += (s, args) =>
            {
                //MessageBox.Show(string.Format("num: {0}: even?: {1}", num, (num % 2 == 0)));
                documentsAdded.Items.Remove(docEntry);
                xbuttons.Children.Remove(button);

            };
          //  docEntry.MouseDoubleClick += (s, args) => documentsAdded.Items.Remove(docEntry);

            xbuttons.Children.Add(button);
            documentsAdded.Items.Add(docEntry);


        }



        private void CreateButton_Click(object sender, RoutedEventArgs e) {
            LastLoadedProjectName = EnteredProjectName.Text;
            // this.Content = WindowManager.LoadedProjectScreen.Content;
            this.SwapWith(WindowManager.LoadedProjectScreen);
            WindowManager.LoadedProjectScreen.SetTitle(LastLoadedProjectName + " - L.A.S.I.");
            WindowManager.LoadedProjectScreen.Show();
            this.Hide();
        }

        private void SelectProjFolderButton_Click(object sender, RoutedEventArgs e) {
            var selectDialog = new OpenFileDialog();
            selectDialog.ShowDialog(this);

            var folderPath = selectDialog.FileName;
            projectFolderText.Text = folderPath;
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e) {

            this.SwapWith(WindowManager.SplashScreen);
        }
        #endregion


        #region Properties

        public string LastLoadedProjectName {
            get;
            set;
        }

        #endregion
    }
}
