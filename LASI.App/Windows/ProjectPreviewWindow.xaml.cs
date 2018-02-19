using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LASI.App.Dialogs;
using LASI.App.Extensions;
using LASI.App.Helpers;
using LASI.Content;
using LASI.Content.Exceptions;
using LASI.Content.FileTypes;
using LASI.Interop;
using LASI.Utilities;

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
        public async Task LoadDocumentPreviewsAsync()
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
            var text = await textfile.LoadTextAsync();
            var processedText = string.Join("\t", from displayChunk in text.SplitRemoveEmpty("\r\n\r\n", "\r\n", "\n\",", "<paragraph>", "</paragraph>")
                                                  select displayChunk.Trim());

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
                this.ShowMessage($".doc file conversion failed\n{e.Message}");
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
            WindowManager.InProgressScreen.Reposition(this);
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
                DocumentManager.RemoveByFileName((docSelected as TabItem).Header.ToString());
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
        private async Task DisplayAddNewDocumentDialogAsync()
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = DocumentManager.FileTypeFilter,
                Multiselect = true,

            };
            openDialog.ShowDialog(this);
            if (!openDialog.FileNames.Any())
            {
                return;
            }
            for (var i = 0; i < openDialog.SafeFileNames.Length; i++)
            {
                var file = new FileInfo(openDialog.FileNames[i]);
                if (DocumentManager.HasFileWithName(file.Name))
                {
                    this.ShowMessage($"A document named {file} is already part of the project.");
                }
                else if (DocumentManager.AbleToOpen(file))
                {
                    DocumentManager.AddDocument(file.Name, file.FullName);
                    await DisplayAddNewDocumentDialogImplementation(file.FullName);
                }
                else
                {
                    this.ShowMessage($"The document {file} is in use by another process, please close any applications which may be using the document and try again.");
                }
            }
        }



        private void OpenLicensesMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var componentsDisplay = new ComponentInfoDialog
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

        private async void AddDocument_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await DisplayAddNewDocumentDialogAsync();
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

        private async void AddNewDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            await DisplayAddNewDocumentDialogAsync();
        }
        private void NewProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {  //Hacky solution to make every option function. This makes new project restart LASI.
            Application.Current.Exit += (sndr, evt) => System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        #endregion

        #endregion


        private void FreeWriterMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void FreeWriter_KeyDown(object sender, KeyEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (e.Key == Key.Enter && !textBlock.Text.IsNullOrWhiteSpace())
            {
                var chunks = textBlock.Text.SplitRemoveEmpty('.', '!', '?').ToList();
                var orchestrator = new AnalysisOrchestrator(new RawTextFragment(chunks.Take(chunks.Count - 1), "free snippet"));
                await orchestrator.ProcessAsync();
            }
        }

        public string ApplicationVersion { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion;
    }
}
