using LASI.App.Helpers;
using LASI.Content;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using LASI.App.Extensions;

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
        public StartupWindow()
        {
            InitializeComponent();
            SetupLogging(Environment.GetCommandLineArgs()[0], "lasilog");
            ProjectNameTextBox.Text = Properties.Settings.Default.AutoNameProjects ? $"MyProject" : string.Empty;
            WindowManager.Intialize();
            Resources["createButtonContent"] = "Create";
            Left = (SystemParameters.WorkArea.Width - Width) / 2;
            Top = (SystemParameters.WorkArea.Height - MaxHeight) / 2;
            DocumentManager.Initialize(documentsAddedListBox, xbuttons, browseForDocButton, lastDocPathTextBox);
            ProcessCommandLineArgs(Environment.GetCommandLineArgs().Skip(1));
        }

        private void SetupLogging(string logFileParentDirectory, string logFileName)
        {
            if (Properties.Settings.Default.LogProcessMessagesToFile)
            {
                try
                {
                    var logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LASI");
                    if (!Directory.Exists(logDir)) { Directory.CreateDirectory(logDir); }
                    Logger.SetToFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                       "LASI", logFileName + ".txt"));
                }
                catch (IOException e)
                {
                    Logger.Log(e.Message);
                    SetupLogging(logFileParentDirectory, logFileName + (char)(DateTimeOffset.Now.Second % 9 + 48));
                }
            }
            else
            {
                Logger.SetToSilent();
            }
        }
        #endregion

        #region Methods

        #region Initialization Methods

        private void ProcessCommandLineArgs(IEnumerable<string> filePaths)
        {
            foreach (var file in DocumentManager.GetValidFilesInPathList(filePaths))
            {
                DocumentManager.AddDocument(file.Name, file.FullName);
            }
            if (!DocumentManager.IsEmpty)
            {
                expandCreatePanelButton_Click(expandCreatePanelButton, new RoutedEventArgs());
            }
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private async Task SetUpDefaultDirectory()
        {
            locationTextBox.Text = await Task.Run(() =>
            {
                var location = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create),
                    "LASI",
                    "Projects"
                );
                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }
                return location;
            });
            locationTextBox.ScrollToEnd();
            locationTextBox.TextChanged += (s, e) => locationTextBox.ScrollToEnd();
        }

        private async Task InitializeFileManager()
        {
            var initPath = Path.Combine(locationTextBox.Text, ProjectNameTextBox.Text);
            var availablePath = initPath;
            for (var i = 0; i < int.MaxValue; ++i)
            {
                if (Directory.Exists(availablePath))
                {
                    availablePath = initPath + i.ToString();
                }
                else { break; }
            }
            FileManager.Initialize(availablePath);
            foreach (var file in documentsAddedListBox.Items)
            {
                try
                {
                    FileManager.AddFile((file as ListViewItem).Tag.ToString());
                }
                catch (FileNotFoundException e)
                {
                    this.ShowMessage(e.Message);
                }
            }
            try
            {
                await FileManager.ConvertAsNeededAsync();
            }
            catch (FileConversionFailureException e)
            {
                this.ShowMessage($".doc file conversion failed\n{e.Message}");
            }
        }

        #endregion

        #region Named Event Handlers

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void expandCreatePanelButton_Click(object sender, RoutedEventArgs e)
        {
            expandCreatePanelButton.Click -= expandCreatePanelButton_Click;//remove this event handler
            Resources["createButtonContent"] = "Cancel";
            mainGrid.AllowDrop = true;
            await SetUpDefaultDirectory();
            if (Height == 250)
            {
                for (var i = 0; i < 270; i += 10)
                {
                    Height += 10;
                    await Task.Delay(8);
                }
            }
            Height = 550;
            expandCreatePanelButton.Click += cancelButton_Click;           //add the cancelButton_Click event handler
            Resources["createButtonContent"] = "Cancel";
        }

        private async void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            expandCreatePanelButton.Click -= cancelButton_Click;            //remove this event handler and 
            Resources["createButtonContent"] = "Create";
            mainGrid.AllowDrop = false;
            if (Height == 550)
            {
                for (var i = 0; i < 270 && Height > 250; i += 10)
                {
                    Height -= 10;
                    await Task.Delay(8);
                }
            }
            Height = 250;
            expandCreatePanelButton.Click += expandCreatePanelButton_Click; //add the expandCreatePanelButton_Click event handler.
            Resources["createButtonContent"] = "Create";
        }

        private void browseForDocButton_Click(object sender, RoutedEventArgs e)
        {
            if (DocumentManager.CanAdd)
            {
                var openDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = DocumentManager.FileTypeFilter,
                    Multiselect = true
                };
                openDialog.ShowDialog(this);
                if (openDialog.FileNames.Any())
                {
                    for (int i = 0; i < openDialog.FileNames.Length; ++i)
                    {
                        if (!DocumentManager.HasFileWithName(openDialog.SafeFileNames[i]))
                        {
                            var fileName = openDialog.SafeFileNames[i];
                            var filePath = openDialog.FileNames[i];
                            DocumentManager.AddDocument(fileName, filePath);
                        }
                        else
                        {
                            this.ShowMessage($"A document named {openDialog.SafeFileName} is already part of the project.");
                        }
                    }
                }
            }
        }

        private async void CompleteSetupAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(locationTextBox.Text))
            {
                try
                {
                    Directory.CreateDirectory(locationTextBox.Text);
                }
                catch (Exception x)
                {
                    x.Log();
                    this.ShowMessage(ErrorMessages.UnusableProjectDirectory);
                }
            }
            if (ValidateProjectName() && ValidateProjectLocationField() && ValidateProjectHasADocument())
            {
                CreateButton.Click -= CompleteSetupAndContinueButton_Click;
                Resources["CurrentProjectName"] = ProjectNameTextBox.Text;
                var previewWindow = WindowManager.ProjectPreviewScreen;
                previewWindow.SetTitle(Resources["CurrentProjectName"] + " - L.A.S.I.");
                await InitializeFileManager();
                JObject lasiFile = new JObject
                {
                    ["name"] = ProjectNameTextBox.Text,
                    ["files"] = JArray.FromObject(from file in FileManager.TxtFiles
                                                  select new JObject
                                                  {
                                                      ["name"] = file.FileName,
                                                      ["path"] = file.FullPath.Replace("\\", "/")
                                                  })
                };
                using (var writer = File.CreateText($"{FileManager.ProjectDirectory}{Path.DirectorySeparatorChar}project.lasi"))
                {
                    writer.WriteLine(lasiFile.ToString());
                }
                this.SwapWith(WindowManager.ProjectPreviewScreen);
                WindowManager.ProjectPreviewScreen.LoadDocumentPreviews();
            }
            else
            {
                DisplayValidationMessage();
            }
        }

        private void SelectProjFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var locationSelectDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult locationDialogResult = locationSelectDialog.ShowDialog();
            if (locationDialogResult == System.Windows.Forms.DialogResult.OK)
            {
                locationTextBox.Text = locationSelectDialog.SelectedPath + Path.DirectorySeparatorChar;
            }
        }
        private void loadProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var locationSelectDialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = $"LASI Project Files|*.lasi",
                Multiselect = false
            };

            System.Windows.Forms.DialogResult locationDialogResult = locationSelectDialog.ShowDialog();
            if (locationDialogResult == System.Windows.Forms.DialogResult.OK && locationSelectDialog.FileName != null)
            {
                var lasiFile = JObject.Parse(File.ReadAllText(locationSelectDialog.FileName));

                Resources["CurrentProjectName"] = ProjectNameTextBox.Text = (string)lasiFile["name"];
                foreach (dynamic file in lasiFile["files"])
                {
                    DocumentManager.AddDocument((string)file.name, (string)file.path);
                }
            }
        }
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            SharedFunctionality.HandleDropAdd(this, e, fileInfo => DocumentManager.AddDocument(fileInfo.Name, fileInfo.FullName));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Validation Methods

        private void DisplayValidationMessage()
        {
            threepaws.Visibility = Visibility.Hidden;
            if (!ValidateProjectName() && !ValidateProjectHasADocument())
            {
                NothingFilledImage.Show();
            }
            else
            {
                NothingFilledImage.Hide();
            }
            if (!ValidateProjectName() && ValidateProjectHasADocument())
            {
                ProjectErrorControls.Show();
            }
            else
            {
                ProjectErrorControls.Hide();
            }
            if (ValidateProjectName() && !ValidateProjectHasADocument())
            {
                ShowElements(ProjDocumentErrorLabel, NoDocumentsImage);
            }
            else
            {
                HideElements(ProjDocumentErrorLabel, NoDocumentsImage);
            }
        }

        private bool ValidateProjectName()
        {
            var errorMessage = string.Empty;
            if (ProjectNameTextBox.Text.IsNullOrWhiteSpace())
            {
                errorMessage = ErrorMessages.ProjectNameEmpty;
            }
            if (ProjectNameTextBox.Text.Intersect(Path.GetInvalidFileNameChars()).Any())
            {
                errorMessage = ErrorMessages.ProjectNameInvalid;
            }
            if (errorMessage.Length == 0)
            {
                return true;
            }
            else
            {
                ProjectNameTextBox.ToolTip = new ToolTip { Content = errorMessage };
                return false;
            }

        }

        private bool ValidateProjectLocationField()
        {
            if (string.IsNullOrWhiteSpace(locationTextBox.Text) ||
                string.IsNullOrEmpty(locationTextBox.Text) ||
                !Directory.Exists(locationTextBox.Text.Substring(0, locationTextBox.Text.LastIndexOf("\\"))))
            {
                locationTextBox.ToolTip = new ToolTip { Content = ErrorMessages.ProjectLocationInvalid };
                return false;
            }
            return true;
        }
        private IEnumerable<UIElement> ProjectErrorControls => new UIElement[] { ProjLocationErrorLabel, ProjNameErrorLabel, ProjNameErrorImage };
        private bool ValidateProjectHasADocument()
        {
            if (DocumentManager.IsEmpty)
            {
                lastDocPathTextBox.ToolTip = new ToolTip { Content = ErrorMessages.NoDocumentsInProject };
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
        private void HideElements(params UIElement[] elements)
        {
            foreach (var e in elements) { e.Visibility = Visibility.Hidden; }
        }
        /// <summary>
        /// Shows all of the provided UIElements.
        /// </summary>
        /// <param name="elements">Zero or more UIElements to show.</param>
        private void ShowElements(params UIElement[] elements)
        {
            foreach (var e in elements) { e.Visibility = Visibility.Visible; }
        }
        #endregion

        #endregion

        #region Fields

        #region Error Messages

        private static class ErrorMessages
        {
            public const string UnusableProjectDirectory = "The folder you have chosen for your project does not exist and could not be created.";
            public const string NoDocumentsInProject = "You must have documents for your new project";
            public const string ProjectLocationInvalid = "You must enter a valid location for your new project";
            public const string ProjectNameEmpty = "You must enter a name for your new project";
            public const string ProjectNameInvalid = "The project name you enterred is not valid. Please choose a new project name";
        }

        #endregion

        #endregion
    }
}
