using LASI.ContentSystem;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for StartupScreen.xaml
    /// </summary>
    public partial class StartupScreen : Window
    {
        #region Constructor
        public StartupScreen() {
            var logFileName = "lasi_log";
            SetupFileLogging(Environment.GetCommandLineArgs()[0], logFileName);
            InitializeComponent();
            ProjectNameTextBox.Text = LASI.UserInterface.Properties.Settings.Default.AutoNameProjects ? "MyProject" : "";
            WindowManager.Intialize();
            SetupAdditionalBehaviors();
            Resources["createButtonContent"] = "Create";
            this.Left = (System.Windows.SystemParameters.WorkArea.Width - this.Width) / 2;
            this.Top = (System.Windows.SystemParameters.WorkArea.Height - this.MaxHeight) / 2;

            DocumentManager.Initialize(documentsAddedListBox, xbuttons, browseForDocButton, lastDocPathTextBox);
            ProcessOpenWithFiles(System.Environment.GetCommandLineArgs().Skip(1));
        }

        private void SetupFileLogging(string logFileParentDirectory, string logFileName) {

            try {
                Output.SetToFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), logFileName + ".txt"));
            } catch (IOException) {
                SetupFileLogging(logFileParentDirectory, logFileName + (char)(DateTime.Now.Second % 9 + 48));
            }
        }
        #endregion

        #region Methods

        #region Intialization Methods

        private void ProcessOpenWithFiles(IEnumerable<string> filePaths) {
            foreach (var f in DocumentManager.GetValidFilesInPathList(filePaths)) {
                DocumentManager.AddDocument(f.Name, f.FullName);
            }
            if (!DocumentManager.IsEmpty) { expandCreatePanelButton_Click(expandCreatePanelButton, new RoutedEventArgs()); }
        }

        void SetupAdditionalBehaviors() {
            //Allow any part of the window to respond to the Drag move command. this is used here for clarity.
            this.MouseLeftButtonDown += (s, e) => DragMove();
            //If the AutoDebugCleanupOn setting is set to "true", destroy the project file directory when the application is closed.
            //if (ConfigurationManager.AppSettings["AutoDebugCleanupOn"] == "true") {
            //    App.Current.Exit += (sender, e) => {
            //        if (FileManager.Initialized)
            //            FileManager.DecimateProject();
            //    };
            //}
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

        private async Task InitializeFileManager() {
            var initPath = System.IO.Path.Combine(locationTextBox.Text, ProjectNameTextBox.Text);
            for (var i = 0; i < Int32.MaxValue - 1; ++i) {
                if (Directory.Exists(initPath)) 
                    initPath = initPath + i;
                else
                    break;
            }
            FileManager.Initialize(initPath);
            foreach (var file in documentsAddedListBox.Items) {
                FileManager.AddFile((file as ListViewItem).Tag.ToString(), true);
            }
            await FileManager.ConvertAsNeededAsync();

        }

        #endregion

        #region Validation Methods

        private void AlertUserAboutInvalidFields() {
            threepaws.Visibility = Visibility.Hidden;

            if (!ValidateProjectNameField() && !ValidateProjectDocumentField()) {
                ShowElements(NothingFilledImage);

            } else {
                HideElements(NothingFilledImage);
            }
            if (!ValidateProjectNameField() && ValidateProjectDocumentField()) {
                ShowElements(ProjNameErrorLabel, ProjNameErrorImage, ProjLocationErrorLabel);
            } else {
                HideElements(ProjLocationErrorLabel, ProjNameErrorLabel, ProjNameErrorImage);
            }

            if (ValidateProjectNameField() && !ValidateProjectDocumentField()) {
                ShowElements(ProjDocumentErrorLabel, NoDocumentsImage);
            } else {
                HideElements(ProjDocumentErrorLabel, NoDocumentsImage);
            }
        }


        private bool ValidateProjectNameField() {
            if (String.IsNullOrWhiteSpace(ProjectNameTextBox.Text) ||
                String.IsNullOrEmpty(ProjectNameTextBox.Text) &&
                !(from char c1 in ProjectNameTextBox.Text
                  join c2 in System.IO.Path.GetInvalidFileNameChars()
                  on c1 equals c2
                  select false).Any()
                ) {
                ProjectNameTextBox.ToolTip = new ToolTip { Content = ErrorEmptyProjectNameMessage };
                return false;
            }
            return true;
        }


        private bool ValidateProjectLocationField() {
            if (String.IsNullOrWhiteSpace(locationTextBox.Text)
                || String.IsNullOrEmpty(locationTextBox.Text) ||
                !Directory.Exists(locationTextBox.Text.Substring(0, locationTextBox.Text.LastIndexOf("\\")))
                ) {
                locationTextBox.ToolTip = new ToolTip { Content = ErrorOnProjectLocationMessage };
                return false;
            }
            return true;
        }

        private bool ValidateProjectDocumentField() {
            if (DocumentManager.IsEmpty) {
                lastDocPathTextBox.ToolTip = new ToolTip { Content = ErrorNoDocumentsAddedMessage };
                return false;
            }
            return true;
        }

        #endregion

        #region Named Event Handlers


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

        private void browseForDocButton_Click(object sender, RoutedEventArgs e) {
            if (DocumentManager.AddingAllowed) {
                var openDialog = new Microsoft.Win32.OpenFileDialog {
                    Filter = "LASI File Types|*.docx; *.pdf; *.txt",
                };
                openDialog.ShowDialog(this);
                if (openDialog.FileNames.Any()) {
                    if (!DocumentManager.FileNamePresent(openDialog.SafeFileName)) {
                        var fileName = openDialog.SafeFileName;
                        var filePath = openDialog.FileName;
                        DocumentManager.AddDocument(fileName, filePath);
                    } else {
                        MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", openDialog.SafeFileName));
                    }
                }
            }
        }

        private async void completeSetupAndContinueButton_Click(object sender, RoutedEventArgs e) {
            if (!Directory.Exists(locationTextBox.Text)) {
                try {
                    Directory.CreateDirectory(locationTextBox.Text);
                } catch (Exception) {
                    MessageBox.Show(this, ErrorOnSelectedProjectDirectoryMessage);
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

        private void SelectProjFolderButton_Click(object sender, RoutedEventArgs e) {
            var locationSelectDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dirResult = locationSelectDialog.ShowDialog();
            if (dirResult == System.Windows.Forms.DialogResult.OK) {
                locationTextBox.Text = locationSelectDialog.SelectedPath + @"\";
            }
        }


        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void EnteredProjectName_TextChanged(object sender, TextChangedEventArgs e) {
            locationTextBox.Text = locationTextBox.Text.Substring(0, locationTextBox.Text.LastIndexOf(@"\")) + @"\" + ProjectNameTextBox.Text;
        }

        private void Grid_Drop(object sender, DragEventArgs e) {
            var filesInValidFormats = DocumentManager.GetValidFilesInPathList(e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[]);
            if (!filesInValidFormats.Any()) {
                MessageBox.Show(this, string.Format("Only the following file formats are accepted:\n{0}", string.Join(",", DocumentManager.AcceptedFormats)));
            } else if (!filesInValidFormats.Any(fn => !DocumentManager.FileNamePresent(fn.Name))) {
                MessageBox.Show(this, string.Format("A document named {0} is already part of the projects.", filesInValidFormats.First()));
            } else {
                foreach (var droppedFile in filesInValidFormats) {
                    if (!DocumentManager.FileIsLocked(droppedFile)) {
                        DocumentManager.AddDocument(droppedFile.Name, droppedFile.FullName);
                    } else {
                        MessageBox.Show(this, string.Format("The document {0} is in use by another process, please close any applications which may be using the file and try again.", droppedFile));
                    }
                }
            }
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Hides all of the provided UIElements.
        /// </summary>
        /// <param name="elements">Zero or more UIElements to hide.</param>
        private void HideElements(params UIElement[] elements) {
            foreach (var e in elements) { e.Visibility = Visibility.Hidden; }
        }
        /// <summary>
        /// Shows all of the provided UIElements.
        /// </summary>
        /// <param name="elements">Zero or more UIElements to show.</param>
        private void ShowElements(params UIElement[] elements) {
            foreach (var e in elements) { e.Visibility = Visibility.Visible; }
        }
        #endregion

        #endregion

        #region Fields

        #region Error Messages
        const string ErrorOnSelectedProjectDirectoryMessage = "The folder you have chosen for your project does not exist or could not be created. Please select an existing directory";
        const string ErrorNoDocumentsAddedMessage = "You must have documents for your new project";
        const string ErrorOnProjectLocationMessage = "You must enter a valid location for your new project";
        const string ErrorEmptyProjectNameMessage = "You must enter a name for your new project";
        #endregion

        #endregion
    }
}
