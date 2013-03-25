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
            this.Closing += (s, e) => Application.Current.Shutdown();
        }

    


        #region EventHandlers

        private void browseForDocButton_Click(object sender, RoutedEventArgs e) {
            var openDialog = new OpenFileDialog();
            openDialog.ShowDialog(this);
            var docPath = openDialog.FileName;
            if (String.IsNullOrEmpty(docPath))
                return;
            lastDocPath.Text = docPath;

            var num = "x";
            var button = new Button {
                Content = num.ToString(),
                Height = 20,
                Width = 20
            };

            var docEntry = new ListViewItem {
                Content = docPath
            };

            button.Click += (s, args) => {
                //MessageBox.Show(string.Format("num: {0}: even?: {1}", num, (num % 2 == 0)));
                documentsAdded.Items.Remove(docEntry);
                xbuttons.Children.Remove(button);

            };
            //  docEntry.MouseDoubleClick += (d, args) => documentsAdded.Items.Remove(docEntry);

            xbuttons.Children.Add(button);
            documentsAdded.Items.Add(docEntry);
            lastDocPath.Text = string.Empty;

        }



        private void CreateButton_Click(object sender, RoutedEventArgs e) {
            if (ValidateProjectNameField()) {
                LastLoadedProjectName = EnteredProjectName.Text;
                // this.Content = WindowManager.LoadedProjectScreen.Content;
                this.SwapWith(WindowManager.LoadedProjectScreen);
                WindowManager.LoadedProjectScreen.SetTitle(LastLoadedProjectName + " - L.A.S.I.");
                WindowManager.LoadedProjectScreen.Show();
                this.Hide();
            } else {
                ProjCreateErrorLabel.Content = "Project must have a name";
                ProjCreateErrorLabel.Visibility = Visibility.Visible;
                ProjNameErrorLabel.Visibility = Visibility.Visible;
                
                //Create an event handler as a local variable. The reason for this is so that it can refer to itself later
                TextChangedEventHandler resetErrorFunc = null;
                //Set the value of that event handler to a function which does two things when invoked
                //1. It hides the error labels
                //2. It removes itself from the invocation list of the event it will be added to
                resetErrorFunc = (S, E) => {
                    ProjNameErrorLabel.Visibility = Visibility.Hidden;
                    ProjCreateErrorLabel.Visibility = Visibility.Hidden;
                    EnteredProjectName.TextChanged -= resetErrorFunc; //The function refers to itself by name, allowing it to remove itself.
                };
                //Add the self removing function to the invocation list of EnteredProjectName's TextChanged event
                EnteredProjectName.TextChanged += resetErrorFunc;
            }
        }


        private bool ValidateProjectNameField() {
            if (String.IsNullOrWhiteSpace(EnteredProjectName.Text)
                || String.IsNullOrEmpty(EnteredProjectName.Text)) {
                EnteredProjectName.ToolTip = new ToolTip {
                    Visibility = Visibility.Visible,
                    Content = "You must enter a name for your new project"
                };
                return false;
            }
            return true;
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
