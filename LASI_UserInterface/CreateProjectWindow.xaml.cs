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
                this.SwapWith(WindowManager.LoadedProjectScreen);
                WindowManager.LoadedProjectScreen.SetTitle(LastLoadedProjectName + " - L.A.S.I.");
                WindowManager.LoadedProjectScreen.Show();
                this.Hide();
            } else {
                ProjCreateErrorLabel.Content = "Project must have a name";
                ProjCreateErrorLabel.Visibility = Visibility.Visible;
                ProjNameErrorLabel.Visibility = Visibility.Visible;

                //Function to hide the error labels. This is a named event handler so that we can remove it by name
                TextChangedEventHandler resetErrorFunc = (S, E) => {
                    ProjNameErrorLabel.Visibility = Visibility.Hidden;
                    ProjCreateErrorLabel.Visibility = Visibility.Hidden;
                };
                //Add the eventhandler function above to be called when the text is changed in the name entry box
                EnteredProjectName.TextChanged += resetErrorFunc;
                //Add another function to the name entry box's TextChanged event.
                //This function removes the resetErrorFunc function from the event handler list. 
                //Multiple functions can be bound to the same event, and they get called in the order they are added.
                EnteredProjectName.TextChanged += (S, E) => {
                    EnteredProjectName.TextChanged -= (resetErrorFunc);
                };
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
