using LASI.ContentSystem;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for StartupScreen.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the StartupScreen class.
        /// </summary>
        public StartupWindow() {
            SetupLogging(Environment.GetCommandLineArgs()[0], "lasi_log");
            InitializeComponent();
            ProjectNameTextBox.Text = Properties.Settings.Default.AutoNameProjects ? "MyProject" : "";
            WindowManager.Intialize();
            Resources["createButtonContent"] = "Create";
            this.Left = (System.Windows.SystemParameters.WorkArea.Width - this.Width) / 2;
            this.Top = (System.Windows.SystemParameters.WorkArea.Height - this.MaxHeight) / 2;

            DocumentManager.Initialize(documentsAddedListBox, xbuttons, browseForDocButton, lastDocPathTextBox);
            ProcessCommandLineArgs(System.Environment.GetCommandLineArgs().Skip(1));
        }

        private void SetupLogging(string logFileParentDirectory, string logFileName) {
            if (Properties.Settings.Default.LogProcessMessagesToFile) {
                try {
                    var logDir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LASI");
                    if (!Directory.Exists(logDir)) { Directory.CreateDirectory(logDir); }
                    Output.SetToFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                       "LASI",
                        logFileName + ".txt"));
                }
                catch (IOException) {
                    SetupLogging(logFileParentDirectory, logFileName + (char)(DateTime.Now.Second % 9 + 48));
                }
            } else { Output.SilenceAll(); }
        }
        #endregion

        #region Methods

        #region Intialization Methods

        private void ProcessCommandLineArgs(IEnumerable<string> filePaths) {
            foreach (var f in DocumentManager.GetValidFilesInPathList(filePaths)) {
                DocumentManager.AddDocument(f.Name, f.FullName);
            }
            if (!DocumentManager.IsEmpty) { expandCreatePanelButton_Click(expandCreatePanelButton, new RoutedEventArgs()); }
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private async Task SetUpDefaultDirectory() {
            locationTextBox.Text = await Task.Run(() => {
                var location =
                    System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData,
                    System.Environment.SpecialFolderOption.Create),
                    "LASI",
                    "Projects");
                if (!Directory.Exists(location)) {
                    Directory.CreateDirectory(location);
                }

                return location;

            });
            locationTextBox.ScrollToEnd();
            locationTextBox.TextChanged += (s, e) => locationTextBox.ScrollToEnd();
        }

        private async Task InitializeFileManager() {
            var initPath = System.IO.Path.Combine(locationTextBox.Text, ProjectNameTextBox.Text);
            for (var i = 0;
            i < Int32.MaxValue - 1;
            ++i) {
                if (Directory.Exists(initPath))
                    initPath = initPath + i;
                else
                    break;
            }
            FileManager.Initialize(initPath);
            foreach (var file in documentsAddedListBox.Items) {
                try {
                    FileManager.AddFile((file as ListViewItem).Tag.ToString(), true);
                }
                catch (FileNotFoundException e) { MessageBox.Show(this, e.Message); }
            }
            try {
                await FileManager.ConvertAsNeededAsync();
            }
            catch (FileConversionFailureException e) {
                MessageBox.Show(this, string.Format(".doc file conversion failed\n{0}", e.Message));
            }
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
                for (var i = 0;
                i < 270;
                i += 10) {
                    Height += 10;
                    await Task.Delay(8);
                }
            }
            Height = 550;
            expandCreatePanelButton.Click += cancelButton_Click;           //add the cancelButton_Click event handler
            Resources["createButtonContent"] = "Cancel";

        }
        private async void cancelButton_Click(object sender, RoutedEventArgs e) {

            expandCreatePanelButton.Click -= cancelButton_Click;            //remove this event handler and 

            Resources["createButtonContent"] = "Create";
            mainGrid.AllowDrop = false;
            if (Height == 550) {
                for (var i = 0;
                i < 270 && Height > 250;
                i += 10) {
                    Height -= 10;
                    await Task.Delay(8);
                }
            }
            Height = 250;
            expandCreatePanelButton.Click += expandCreatePanelButton_Click; //add the expandCreatePanelButton_Click event handler.
            Resources["createButtonContent"] = "Create";
        }

        private void browseForDocButton_Click(object sender, RoutedEventArgs e) {
            if (DocumentManager.CanAdd) {
                var openDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "LASI File Types|*.doc; *.docx; *.pdf; *.txt",
                    Multiselect = true
                };
                openDialog.ShowDialog(this);
                if (openDialog.FileNames.Any()) {
                    for (int i = 0;
                    i < openDialog.FileNames.Length;
                    i++) {
                        if (!DocumentManager.FileNamePresent(openDialog.SafeFileNames[i])) {
                            var fileName = openDialog.SafeFileNames[i];
                            var filePath = openDialog.FileNames[i];
                            DocumentManager.AddDocument(fileName, filePath);
                        } else {
                            MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", openDialog.SafeFileName));
                        }
                    }
                }
            }
        }

        private async void completeSetupAndContinueButton_Click(object sender, RoutedEventArgs e) {
            if (!Directory.Exists(locationTextBox.Text)) {
                try {
                    Directory.CreateDirectory(locationTextBox.Text);
                }
                catch (Exception) {
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





        private void Grid_Drop(object sender, DragEventArgs e) {
            SharedWindowFunctionality.HandleDropAddAttempt(this, e, fi => DocumentManager.AddDocument(fi.Name, fi.FullName));
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
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
            if (string.IsNullOrWhiteSpace(ProjectNameTextBox.Text) ||
                string.IsNullOrEmpty(ProjectNameTextBox.Text) &&
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
            if (string.IsNullOrWhiteSpace(locationTextBox.Text)
                || string.IsNullOrEmpty(locationTextBox.Text) ||
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
