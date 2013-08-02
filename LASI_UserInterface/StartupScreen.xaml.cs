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
using LASI.ContentSystem;
using System.IO;
using LASI.Utilities;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for StartupScreen.xaml
    /// </summary>
    public partial class StartupScreen : Window
    {
        public StartupScreen() {
            //Setup logfile location.
            var appFileName = Environment.GetCommandLineArgs()[0];
            Output.SetToFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(appFileName), "lasi_log.txt"));

            InitializeComponent();
            WindowManager.StartupScreen = this;
            BindWindowEventHandlers();
            Resources["createButtonContent"] = "Create";
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = (System.Windows.SystemParameters.WorkArea.Width - this.Width) / 2;
            this.Top = (System.Windows.SystemParameters.WorkArea.Height - this.MaxHeight) / 2;

            DocumentManager.Initialize(documentsAdded, xbuttons, browseForDocButton, lastDocPathTextBox);
        }
        void BindWindowEventHandlers() {
            //Allow any part of the window to respond to the Drag move command. this is used here for clarity.
            this.MouseLeftButtonDown += (s, e) => DragMove();
            //If the AutoDebugCleanupOn setting is set to "true", destroy the project file directory when the application is closed.
            if (ConfigurationManager.AppSettings["AutoDebugCleanupOn"] == "true") {
                App.Current.Exit += (sender, e) => {
                    if (FileManager.Initialized)
                        FileManager.DecimateProject();
                };
            }
        }


        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }



        private async void expandCreatePanelButton_Click(object sender, RoutedEventArgs e) {
            expandCreatePanelButton.Click -= expandCreatePanelButton_Click;//remove this event handler

            Resources["createButtonContent"] = "Cancel";
            mainGrid.AllowDrop = true;
            await SetUpDefaultDirectory();

            if (Height == 250) {
                for (var i = 0; i < 270; i += 10) {
                    Height += 10;
                    await Task.Delay(8);
                }
            }
            expandCreatePanelButton.Click += cancelButton_Click;           //add the cancelButton_Click event handler
            Resources["createButtonContent"] = "Cancel";

        }
        private async void cancelButton_Click(object sender, RoutedEventArgs e) {

            expandCreatePanelButton.Click -= cancelButton_Click;            //remove this event handler and 

            Resources["createButtonContent"] = "Create";
            mainGrid.AllowDrop = false;
            if (Height == 520) {
                for (var i = 0; i < 270 && Height > 250; i += 10) {
                    Height -= 10;
                    await Task.Delay(8);
                }
            }
            expandCreatePanelButton.Click += expandCreatePanelButton_Click; //add the expandCreatePanelButton_Click event handler.
            Resources["createButtonContent"] = "Create";
        }
        private async Task SetUpDefaultDirectory() {
            locationTextBox.Text = await Task.Run(() => {
                var location = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData, System.Environment.SpecialFolderOption.Create), "Projects");
                if (!Directory.Exists(location)) {
                    Directory.CreateDirectory(location);
                }

                return location;

            });

            locationTextBox.TextChanged += (sender2, e2) => locationTextBox.ScrollToEnd();
        }



        private void browseForDocButton_Click(object sender, RoutedEventArgs e) {
            var openDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "LASI File Types|*.docx; *.pdf; *.txt",
            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Any()) {
                if (!DocumentManager.FileNamePresent(openDialog.SafeFileName)) {
                    var fileName = openDialog.SafeFileName;
                    var filePath = openDialog.FileName;
                    DocumentManager.AddUserDocument(fileName, filePath);
                } else {
                    MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", openDialog.SafeFileName));
                }
            }

        }




        private async void completeSetupAndContinueButton_Click(object sender, RoutedEventArgs e) {
            if (!Directory.Exists(locationTextBox.Text)) {
                try {
                    Directory.CreateDirectory(locationTextBox.Text);
                } catch (Exception) {
                    MessageBox.Show(this, "The folder you have chosen for your project does not exist or could not be created. Please select an existing directory");
                }
            }
            if (ValidateProjectNameField() && ValidateProjectLocationField() && ValidateProjectDocumentField()) {
                createButton.Click -= completeSetupAndContinueButton_Click;
                Resources["CurrentProjectName"] = ProjectNameTextBox.Text;
                var previewWindow = WindowManager.ProjectPreviewScreen;
                previewWindow.SetTitle(Resources["CurrentProjectName"] + " - L.A.S.I.");
                await InitializeFileManager();
                this.SwapWith(WindowManager.ProjectPreviewScreen);
                WindowManager.ProjectPreviewScreen.LoadDocumentPreviews();
            } else {
                AlertUserAboutInvalidFields();
            }


        }

        private async Task InitializeFileManager() {
            FileManager.Initialize(System.IO.Path.Combine(locationTextBox.Text, ProjectNameTextBox.Text));

            foreach (var file in documentsAdded.Items) {
                FileManager.AddFile((file as ListViewItem).Tag.ToString(), true);
            }
            await FileManager.ConvertAsNeededAsync();

        }

        private void AlertUserAboutInvalidFields() {
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

        private void SelectProjFolderButton_Click(object sender, RoutedEventArgs e) {


            var locationSelectDialog = new System.Windows.Forms.FolderBrowserDialog {

            };

            System.Windows.Forms.DialogResult dirResult = locationSelectDialog.ShowDialog();
            if (dirResult == System.Windows.Forms.DialogResult.OK) {
                locationTextBox.Text = locationSelectDialog.SelectedPath + @"\";
            }

        }


        private bool ValidateProjectNameField() {
            if (String.IsNullOrWhiteSpace(ProjectNameTextBox.Text)
            || String.IsNullOrEmpty(ProjectNameTextBox.Text) && !(
            from char c1 in ProjectNameTextBox.Text
            join c2 in System.IO.Path.GetInvalidFileNameChars()
            on c1 equals c2
            select false).Any()

                ) {
                ProjectNameTextBox.ToolTip = new ToolTip {
                    Visibility = Visibility.Visible,
                    Content = "You must enter a name for your new project"
                };
                return false;
            }
            return true;
        }


        private bool ValidateProjectLocationField() {
            if (String.IsNullOrWhiteSpace(locationTextBox.Text)
            || String.IsNullOrEmpty(locationTextBox.Text) || !Directory.Exists(locationTextBox.Text.Substring(0, locationTextBox.Text.LastIndexOf("\\")))) {

                locationTextBox.ToolTip = new ToolTip {
                    Visibility = Visibility.Visible,
                    Content = "You must enter a valid location for your new project"
                };
                return false;
            }
            return true;

        }

        private bool ValidateProjectDocumentField() {
            if (DocumentManager.IsEmpty) {
                lastDocPathTextBox.ToolTip = new ToolTip {
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





        private void EnteredProjectName_TextChanged(object sender, TextChangedEventArgs e) {
            locationTextBox.Text = locationTextBox.Text.Substring(0, locationTextBox.Text.LastIndexOf(@"\")) + @"\" + ProjectNameTextBox.Text;
        }

        private void Grid_Drop(object sender, DragEventArgs e) {
            var filesInValidFormats = DocumentManager.GetValidFilesInPathList(e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[]);
            if (!filesInValidFormats.Any()) {
                MessageBox.Show(this, string.Format("Only the following file formats are accepted:\n{0}", DocumentManager.AcceptedFormats.Aggregate((sum, current) => sum += current + ", ")));
            } else if (!filesInValidFormats.Any(fn => !DocumentManager.FileNamePresent(fn.Name))) {
                MessageBox.Show(this, string.Format("A document named {0} is already part of the projects.", filesInValidFormats.First()));
            } else {
                foreach (var droppedFile in filesInValidFormats) {
                    if (!DocumentManager.FileIsLocked(droppedFile)) {
                        DocumentManager.AddUserDocument(droppedFile.Name, droppedFile.FullName);
                    } else {
                        MessageBox.Show(this, string.Format("The document {0} is in use by another process, please close any applications which may be using the file and try again.", droppedFile));
                    }
                }
            }
        }





    }
}
