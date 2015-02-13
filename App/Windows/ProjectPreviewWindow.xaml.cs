using LASI.App.Dialogs;
using LASI.Content;
using LASI.Utilities;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for ExportResultsDialog.xaml
    /// </summary>
    public partial class ProjectPreviewWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the ProjectPreviewScreen class.
        /// </summary>
        public ProjectPreviewWindow()
        {
            InitializeComponent();
            var titleText = Resources["CurrentProjectName"] as string ?? Title;
            SetupEvents();
        }

        private void SetupEvents()
        {
            DocumentManager.NumberOfDocumentsChanged += (sender, e) => numDocsLabel.Content = e.NewValue;
        }

        #region Methods

        #region Document Preview Construction
        /// <summary>
        /// Loads and displays a text preview tab for each document in the project.
        /// </summary>
        public async void LoadDocumentPreviews()
        {
            foreach (var textfile in FileManager.TxtFiles)
            {
                await LoadTextandTabAsync(textfile);
            }
            DocumentPreview.SelectedIndex = 0;
        }
        /// <summary>
        /// Asynchronously loads the text of the given file into a new preview tab, displaying it with some rudimentary formatting.
        /// </summary>
        /// <param name="textfile">The Text File from which to load text.</param>
        /// <returns>A System.Threading.Tasks.Task representing the ongoing asynchronous operation.</returns>
        private async Task LoadTextandTabAsync(TxtFile textfile)
        {
            var text = await textfile.GetTextAsync();



            var processedText = string.Join("\t", text.SplitRemoveEmpty("\r\n\r\n", "\r\n", "\n\",", "<paragraph>", "</paragraph>").Select(value => value.Trim()));


            var block = new System.Windows.Documents.Section(
                 new System.Windows.Documents.Paragraph(
                     new System.Windows.Documents.Run { Text = processedText })
                 {
                     TextAlignment = TextAlignment.Left
                 })
            {
                TextAlignment = TextAlignment.Left,
                BreakColumnBefore = false
            };
            var item = new TabItem
            {
                Header = textfile.NameSansExt,
                AllowDrop = true,
                Content = new FlowDocumentPageViewer
                {
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Document = new System.Windows.Documents.FlowDocument(
                        new System.Windows.Documents.Paragraph(
                            new System.Windows.Documents.Run(
                                processedText
                            )
                        )
                    )
                }
            };
            VisualTextRenderingMode = System.Windows.Media.TextRenderingMode.ClearType;
            item.Drop += Grid_Drop;
            DocumentPreview.Items.Add(item);
            DocumentPreview.SelectedItem = item;
        }
        /// <summary>
        /// Asynchronously adds a new document by the specified file path.
        /// </summary>
        /// <param name="docPath">The file path specifying where to find the document.</param>
        /// <returns>A System.Threading.Tasks.Task representing the ongoing asynchronous operation.</returns>
        private async Task DisplayAddNewDocumentDialogImplementation(string docPath)
        {
            var chosenFile = FileManager.AddFile(docPath);
            try
            {
                await FileManager.ConvertAsNeededAsync();
            }
            catch (FileConversionFailureException e)
            {
                MessageBox.Show(this, $".doc file conversion failed\n{e.Message}");
            }
            var textfile = FileManager.TxtFiles.Where(f => f.NameSansExt == chosenFile.NameSansExt).First();
            await LoadTextandTabAsync(textfile);
            CheckIfAddingAllowed();
            startProcessingButton.IsEnabled = true;
            StartProcessMenuItem.IsEnabled = true;
        }

        private void CheckIfAddingAllowed()
        {
            var addingEnabled = DocumentManager.CanAdd;
            AddNewDocumentButton.IsEnabled = addingEnabled;
            FileMenuAdd.IsEnabled = addingEnabled;
        }

        #endregion

        #region Event Handlers

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.InProgressScreen.PositionAt(this);
            this.Hide();
            WindowManager.InProgressScreen.Show();
            await WindowManager.InProgressScreen.ParseDocuments();
        }

        private void RemoveCurrentDocument_Click(object sender, RoutedEventArgs e)
        {
            var docSelected = DocumentPreview.SelectedItem;
            if (docSelected != null)
            {
                DocumentPreview.Items.Remove(docSelected);
                DocumentManager.RemoveDocument((docSelected as TabItem).Header.ToString());
                FileManager.RemoveFile((docSelected as TabItem).Header.ToString());
                CheckIfAddingAllowed();

            }
            if (DocumentManager.IsEmpty)
            {
                startProcessingButton.IsEnabled = false;
                StartProcessMenuItem.IsEnabled = false;
            }

        }
        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            await SharedFunctionality.HandleDropAddAsync(
                this,
                e,
                async fileInfo =>
                {
                    DocumentManager.AddDocument(fileInfo.Name, fileInfo.FullName);
                    await DisplayAddNewDocumentDialogImplementation(fileInfo.FullName);
                });
        }
        private async void DisplayAddNewDocumentDialog()
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = DocumentManager.FILE_FILTER,
                Multiselect = true,

            };
            openDialog.ShowDialog(this);
            if (!openDialog.FileNames.Any())
            {
                return;
            }
            for (int i = 0; i < openDialog.SafeFileNames.Length; i++)
            {
                var file = new FileInfo(openDialog.FileNames[i]);
                if (DocumentManager.HasFileWithName(file.Name))
                {
                    MessageBox.Show(this, $"A document named {file} is already part of the project.");
                }
                else if (!file.UnableToOpen())
                {
                    DocumentManager.AddDocument(file.Name, file.FullName);
                    await DisplayAddNewDocumentDialogImplementation(file.FullName);
                }
                else
                {
                    MessageBox.Show(this, $"The document {file} is in use by another process, please close any applications which may be using the document and try again.");
                }

            }
        }



        private void OpenLicensesMenuItem_Click_1(object sender, RoutedEventArgs e)
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
            SharedFunctionality.LaunchLASIWebsite(this);
        }

        private void AddDocument_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            DisplayAddNewDocumentDialog();
        }

        private void CloseApp_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void OpenPreferences_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SharedFunctionality.DisplayPreferencesWindow(this);
        }

        private void OpenManual_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SharedFunctionality.OpenManualWithInstalledViewer(this);
        }

        private void AddNewDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayAddNewDocumentDialog();
        }

        #endregion

        #endregion

    }
}
