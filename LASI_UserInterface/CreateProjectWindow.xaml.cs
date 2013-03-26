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
            var openDialog = new OpenFileDialog {
                Filter = "LASI Inputs|*.docx; *.doc; *.txt"
            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Count() <= 0) {
                return;
            }
            var docPath = openDialog.FileName;

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

                documentsAdded.Items.Remove(docEntry);
                xbuttons.Children.Remove(button);
                NumberOfDocuments--;
                if (NumberOfDocuments == 0)
                    documentsAdded.Visibility = Visibility.Hidden;


            };


            xbuttons.Children.Add(button);
            documentsAdded.Items.Add(docEntry);
            lastDocPath.Text = string.Empty;
            NumberOfDocuments++;
            if (!documentsAdded.IsVisible)
                documentsAdded.Visibility = Visibility.Visible;

        }


        private void CreateButton_Click(object sender, RoutedEventArgs e) {
            if (ValidateProjectNameField() && ValidateProjectLocationField() && ValidateProjectDocumentField()) {
                LastLoadedProjectName = EnteredProjectName.Text;

                this.SwapWith(WindowManager.LoadedProjectScreen);
                WindowManager.LoadedProjectScreen.SetTitle(LastLoadedProjectName + " - L.A.S.I.");
                WindowManager.LoadedProjectScreen.Show();
                this.Hide();
            }
            else {

                if (ValidateProjectNameField() == false) {
                    ProjNameErrorLabel.Visibility = Visibility.Visible;
                }
                else
                    ProjNameErrorLabel.Visibility = Visibility.Hidden;

                if (ValidateProjectLocationField() == false) {
                    ProjLocationErrorLabel.Visibility = Visibility.Visible;
                }
                else
                    ProjLocationErrorLabel.Visibility = Visibility.Hidden;

                if (ValidateProjectDocumentField() == false) {
                    ProjDocumentErrorLabel.Visibility = Visibility.Visible;
                }
                else
                    ProjDocumentErrorLabel.Visibility = Visibility.Hidden;


                ProjCreateErrorLabel.Content = "All fields must be filled out.";
                ProjCreateErrorLabel.Visibility = Visibility.Visible;

                TextChangedEventHandler resetErrorFunc = null;
                resetErrorFunc = (S, E) => {
                    //
                    ProjCreateErrorLabel.Visibility = Visibility.Hidden;
                    // ProjLocationErrorLabel.Visibility = Visibility.Hidden;
                    //ProjDocumentErrorLabel.Visibility = Visibility.Hidden;
                    // EnteredProjectName.TextChanged -= resetErrorFunc;
                    // projectFolderText.TextChanged -= resetErrorFunc;





                };
                // EnteredProjectName.TextChanged += resetErrorFunc;
                //  projectFolderText.TextChanged += resetErrorFunc;

            }
        }

        private void SelectProjFolderButton_Click(object sender, RoutedEventArgs e) {


            var selectDialog = new OpenFileDialog();
            selectDialog.ShowDialog(this);

            var folderPath = selectDialog.FileName;
            projectFolderText.Text = folderPath;

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


        private bool ValidateProjectLocationField() {
            if (String.IsNullOrWhiteSpace(projectFolderText.Text)
            || String.IsNullOrEmpty(projectFolderText.Text)) {

                projectFolderText.ToolTip = new ToolTip {
                    Visibility = Visibility.Visible,
                    Content = "You must enter a location for your new project"
                };
                return false;
            }
            return true;

        }

        private bool ValidateProjectDocumentField() {
            if (NumberOfDocuments == 0) {
                lastDocPath.ToolTip = new ToolTip {
                    Visibility = Visibility.Visible,
                    Content = "You must have documents for your new project"
                };
                return false;

            }
            return true;

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

        public int NumberOfDocuments {
            get;
            set;
        }
        #endregion




    }
}
