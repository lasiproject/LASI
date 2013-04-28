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
using System.Timers;
using System.Configuration;
using System.Threading;
using LASI.FileSystem;
using System.IO;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for StartupScreen.xaml
    /// </summary>
    public partial class StartupScreen : Window
    {
        public StartupScreen() {
            InitializeComponent();
            WindowManager.StartupScreen = this;
            BindWindowEventHandlers();
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = (System.Windows.SystemParameters.WorkArea.Width - this.Width) / 2;
            this.Top = (System.Windows.SystemParameters.WorkArea.Height - this.MaxHeight) / 2;


        }
        void BindWindowEventHandlers() {
            this.MouseLeftButtonDown += (s, e) => DragMove();
            if (ConfigurationManager.AppSettings["AutoDebugCleanupOn"] == "true") {
                App.Current.Exit += (sender, e) => {
                    if (FileSystem.FileManager.Initialized)
                        FileSystem.FileManager.DecimateProject();
                };
            }
        }


        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }



        private async void createProjectButton_Click(object sender, RoutedEventArgs e) {


            await SetUpDirectory();

            if (Height == 250) {
                for (var i = 0; i < 270; i += 10) {
                    Height += 10;
                    await Task.Delay(8);
                }



            }


        }
        private async void CancelButton_Click(object sender, RoutedEventArgs e) {

            if (Height == 520) {
                for (var i = 0; i < 270 && Height > 250; i += 10) {
                    Height -= 10;
                    await Task.Delay(8);
                }
            }
        }
        private async Task SetUpDirectory() {
            ProjectLocation = await Task.Run(() => {
                var location = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LASI_Projects";
                if (!Directory.Exists(location)) {
                    Directory.CreateDirectory(location);
                }


                return location;

            });
            LocationTextBox.Text = ProjectLocation;
            LocationTextBox.TextChanged += (sender2, e2) => LocationTextBox.ScrollToEnd();
        }



        #region EventHandlers

        private void browseForDocButton_Click(object sender, RoutedEventArgs e) {
            var openDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "LASI File Types|*.docx; *.doc; *.txt",
            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Count() <= 0) {
                return;
            }
            var docPath = openDialog.FileName;

            lastDocPath.Text = docPath;
            var docEntry = new ListViewItem {
                Content = docPath
            };

            var num = "x";
            var button = new Button {
                Content = num.ToString(),
                Height = 20,
                Width = 20,
                Style = FindResource("xButton") as Style
            };


            button.Click += (s, args) => {

                documentsAdded.Items.Remove(docEntry);
                xbuttons.Children.Remove(button);
                NumberOfDocuments--;
                if (NumberOfDocuments == 0) {
                    //  documentsAdded.Visibility = Visibility.Hidden;
                    documentsAdded.Opacity = 0.25;
                }

                browseForDocButton.IsEnabled = true;


            };


            xbuttons.Children.Add(button);
            documentsAdded.Items.Add(docEntry);
            lastDocPath.Text = string.Empty;
            NumberOfDocuments++;
            if (NumberOfDocuments > 0) {
                //documentsAdded.Visibility = Visibility.Visible;
                documentsAdded.Opacity = 100;
            }

            if (NumberOfDocuments == 5) {
                browseForDocButton.IsEnabled = false;
            }

        }


        private async void CreateButton_Click(object sender, RoutedEventArgs e) {
            if (ValidateProjectNameField() && ValidateProjectLocationField() && ValidateProjectDocumentField()) {
                Resources["CurrentProjectName"] = ProjectNameTextBox.Text;



                WindowManager.LoadedProjectScreen.SetTitle(Resources["CurrentProjectName"] + " - L.A.S.I.");
                WindowManager.LoadedProjectScreen.Show();

                FileManager.Initialize(ProjectLocation + @"\" + ProjectNameTextBox.Text);

                foreach (var file in documentsAdded.Items) {
                    FileManager.AddFile((file as ListViewItem).Content.ToString(), true);
                }

                await FileManager.ConvertAsNeededAsync();

                WindowManager.LoadedProjectScreen.LoadDocumentPreviews();
                this.SwapWith(WindowManager.LoadedProjectScreen);



            } else {

                threepaws.Visibility = Visibility.Hidden;

                if (ValidateProjectNameField() == false && ValidateProjectDocumentField() == false) {
                    //   ProjCreateErrorLabel.Content = "All fields must be filled out.";
                    // ProjCreateErrorLabel.Visibility = Visibility.Visible;
                    NothingFilledImage.Visibility = Visibility.Visible;

                } else {
                    //ProjCreateErrorLabel.Visibility = Visibility.Hidden;
                    NothingFilledImage.Visibility = Visibility.Hidden;

                }
                if (ValidateProjectNameField() == false && ValidateProjectDocumentField() == true) {
                    ProjNameErrorLabel.Visibility = Visibility.Visible;
                    ProjNameErrorImage.Visibility = Visibility.Visible;
                    ProjLocationErrorLabel.Visibility = Visibility.Visible;
                } else {

                    ProjLocationErrorLabel.Visibility = Visibility.Hidden;
                    ProjNameErrorLabel.Visibility = Visibility.Hidden;
                    ProjNameErrorImage.Visibility = Visibility.Hidden;
                }

                if (ValidateProjectNameField() == true && ValidateProjectDocumentField() == false) {
                    ProjDocumentErrorLabel.Visibility = Visibility.Visible;
                    NoDocumentsImage.Visibility = Visibility.Visible;
                } else {
                    ProjDocumentErrorLabel.Visibility = Visibility.Hidden;
                    NoDocumentsImage.Visibility = Visibility.Hidden;
                }





            }

        }

        private void SelectProjFolderButton_Click(object sender, RoutedEventArgs e) {


            var locationSelectDialog = new System.Windows.Forms.FolderBrowserDialog {
                SelectedPath = ProjectLocation
            };

            System.Windows.Forms.DialogResult dirResult = locationSelectDialog.ShowDialog();
            if (dirResult == System.Windows.Forms.DialogResult.OK) {
                LocationTextBox.Text = locationSelectDialog.SelectedPath + @"\" + ProjectNameTextBox.Text;
            }
            ProjectLocation = locationSelectDialog.SelectedPath;
        }


        private bool ValidateProjectNameField() {
            if (String.IsNullOrWhiteSpace(ProjectNameTextBox.Text)
            || String.IsNullOrEmpty(ProjectNameTextBox.Text)) {
                ProjectNameTextBox.ToolTip = new ToolTip {
                    Visibility = Visibility.Visible,
                    Content = "You must enter a name for your new project"
                };
                return false;
            }
            return true;
        }


        private bool ValidateProjectLocationField() {
            if (String.IsNullOrWhiteSpace(LocationTextBox.Text)
            || String.IsNullOrEmpty(LocationTextBox.Text) || !Directory.Exists(LocationTextBox.Text.Substring(0, LocationTextBox.Text.LastIndexOf("\\")))) {

                LocationTextBox.ToolTip = new ToolTip {
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

        #endregion


        #region Properties

        public string ProjectLocation {
            get;
            private set;
        }
        //private string lastLoadedProjectName = string.Empty;

        //public string LastLoadedProjectName {
        //    get {
        //        return lastLoadedProjectName;
        //    }
        //    private set {
        //        lastLoadedProjectName = value;
        //    }
        //}


        private int NumberOfDocuments {
            get;
            set;
        }




        #endregion

        private void EnteredProjectName_TextChanged(object sender, TextChangedEventArgs e) {
            LocationTextBox.Text = ProjectLocation + @"\" + ProjectNameTextBox.Text;
        }







    }
}
