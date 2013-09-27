using LASI.ContentSystem;
using LASI.UserInterface.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for ExportResultsDialog.xaml
    /// </summary>
    public partial class ProjectPreviewScreen : Window
    {
        /// <summary>
        /// Initializes a new instance of the ProjectPreviewScreen class.
        /// </summary>
        public ProjectPreviewScreen()
        {
            InitializeComponent();
            var titleText = Resources["CurrentProjectName"] as string ?? Title;
        }

        #region Methods

        #region Document Preview Construction
        /// <summary>
        /// Loads and displays a text preview tab for each document in the project.
        /// </summary>
        public async void LoadDocumentPreviews()
        {
            foreach (var textfile in FileManager.TextFiles) {
                await LoadTextandTabAsync(textfile);
            }
            DocumentPreview.SelectedIndex = 0;
        }

        private async Task LoadTextandTabAsync(TextFile textfile)
        {
            var processedText = await await textfile.GetTextAsync().ContinueWith(async (t) => {
                var data = await t;
                return data.Split(new[] { "\r\n\r\n", "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Aggregate((sum, s) => sum += "\n\t" + s);
            });
            var item = new TabItem
            {
                Header = textfile.NameSansExt,
                AllowDrop = true,
                Content = new TextBox
                {
                    IsReadOnly = true,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    TextWrapping = TextWrapping.Wrap,
                    Text = processedText,
                    FontSize = 12,

                },
            };
            item.Drop += Grid_Drop;
            DocumentPreview.Items.Add(item);
            DocumentPreview.SelectedItem = item;
        }

        private async Task AddNewDocument(string docPath)
        {
            var chosenFile = FileManager.AddFile(docPath, true);
            try {
                await FileManager.ConvertAsNeededAsync();
            }
            catch (FileConversionFailureException e) {
                MessageBox.Show(this, string.Format(".doc file conversion failed\n{0}", e.Message));
            }
            var textfile = FileManager.TextFiles.Where(f => f.NameSansExt == chosenFile.NameSansExt).First();
            await LoadTextandTabAsync(textfile);
            CheckIfAddingAllowed();
            startProcessingButton.IsEnabled = true;
            StartProcessMenuItem.IsEnabled = true;
        }

        private void CheckIfAddingAllowed()
        {
            var addingEnabled = DocumentManager.AddingAllowed;
            AddNewDocumentButton.IsEnabled = addingEnabled;
            FileMenuAdd.IsEnabled = addingEnabled;
        }

        #endregion

        #region Named Event Handlers

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WindowManager.InProgressScreen.Show();
            await WindowManager.InProgressScreen.ParseDocuments();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
        private void RemoveCurrentDocument_Click(object sender, RoutedEventArgs e)
        {
            var docSelected = DocumentPreview.SelectedItem;
            if (docSelected != null) {
                DocumentPreview.Items.Remove(docSelected);
                DocumentManager.RemoveDocument((docSelected as TabItem).Header.ToString());
                FileManager.RemoveFile((docSelected as TabItem).Header.ToString());
                CheckIfAddingAllowed();

            }
            if (DocumentManager.IsEmpty) {
                startProcessingButton.IsEnabled = false;
                StartProcessMenuItem.IsEnabled = false;
            }

        }
        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            await SharedScreenFunctionality.HandleDropAddAttemptAsync(this, e, async fi => { DocumentManager.AddDocument(fi.Name, fi.FullName); await AddNewDocument(fi.FullName); });
        }
        private async void AddNewDocument_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "LASI File Types|*.doc; *.docx; *.pdf; *.txt",
                Multiselect = true,

            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Count() <= 0) {
                return;
            }
            for (int i = 0; i < openDialog.SafeFileNames.Length; i++) {
                var file = new FileInfo(openDialog.FileNames[i]);
                if (DocumentManager.FileNamePresent(file.Name)) {
                    MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", file));
                } else if (!DocumentManager.FileIsLocked(file)) {
                    DocumentManager.AddDocument(file.Name, file.FullName);
                    await AddNewDocument(file.FullName);
                } else {
                    MessageBox.Show(this, string.Format("The document {0} is in use by another process, please close any applications which may be using the document and try again.", file));
                }

            }
        }
        private void openPreferencesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SharedScreenFunctionality.OpenPreferencesWindow(this);
        }

        #endregion

        #endregion

        #region Help Menu

        private void OpenManualMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SharedScreenFunctionality.ProcessOpenManualRequest(this);
        }

        private void openLicensesMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var componentsDisplay = new ComponentInfoDialogWindow
            {
                Left = this.Left,
                Top = this.Top,
                Owner = this
            };
            componentsDisplay.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SharedScreenFunctionality.ProcessOpenWebsiteRequest(this);
        }

        #endregion




    }
}
