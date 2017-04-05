using LASI.App.Dialogs;
using LASI.Content;
using LASI.Content.Serialization.Xml;
using LASI.Core;
using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LASI.Interop;
using LASI.Content.Serialization;
using LASI.App.Extensions;

namespace LASI.App
{
    using Visualization;
    using Core.Analysis.Heuristics;
    using Interop.ResourceManagement;
    using Utilities;
    using Utilities.Specialized.Enhanced.Universal;
    using FlowDocument = System.Windows.Documents.FlowDocument;
    using DocumentRun = System.Windows.Documents.Run;
    using FlowDocumentPageViewer = FlowDocumentPageViewer;
    using FileInfo = System.IO.FileInfo;
    using LASI.App.Helpers;
    using Extensions;

    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ResultsScreen class.
        /// </summary>
        public ResultsWindow()
        {
            InitializeComponent();
            currentOperationFeedbackCanvas.Visibility = Visibility.Hidden;
            this.Closed += (s, e) => Application.Current.Shutdown();
        }
        #endregion

        #region Methods

        #region View Construction

        /// <summary>
        /// This function associates The buttons which allow the user to modify various aspects of the chart views with their respective functionality.
        /// This is done to allow the functionality to be exposed only after the charts have been 
        /// </summary>
        private void SetupChartViewControls()
        {
            changeToBarChartButton.Click += ChangeToBarChartButton_Click;
            changeToColumnChartButton.Click += ChangeToColumnChartButton_Click;
            changeToPieChartButton.Click += ChangeToPieChartButton_Click;
        }
        /// <summary>
        /// Creates and displays a weighted list of the top NounPhrases for each document.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing the ongoing asynchronous operation.</returns>
        public async Task CreateWeightViewsForAllDocumentsAsync()
        {
            foreach (var document in documents)
            {
                await CreateWeightViewAsync(document);

            }
            SetupChartViewControls();
        }

        private async Task CreateWeightViewAsync(Document document)
        {
            //var pages = document.Paginate(lineLength: 100, linesPerPage: 100);

            var nounPhraseLabels = from sentence in document.Sentences
                                   select sentence.Phrases.OfNounPhrase() into nounPhrases
                                   from np in nounPhrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   group np by new { np.Text, Type = np.GetType() } into byTypeAndText
                                   let first = byTypeAndText.First()
                                   orderby first.Weight descending
                                   select CreateLabelForWeightedView(first);

            var weightedListPanel = new StackPanel();
            var grid = new Grid();
            grid.Children.Add(new ScrollViewer { Content = weightedListPanel });
            foreach (var label in nounPhraseLabels) { weightedListPanel.Children.Add(label); }

            var tab = new TabItem { Header = document.Name, Content = grid };

            weightedByDocumentTabControl.Items.Add(tab);
            weightedByDocumentTabControl.SelectedItem = tab;
            //await Task.Yield();
            await Visualizer.InitChartDisplayAsync(document);
            await Visualizer.DisplayKeyRelationshipsAsync(document);
        }

        private static Label CreateLabelForWeightedView(NounPhrase nounPhrase)
        {
            var gender = nounPhrase.GetGender();
            var label = new Label
            {
                Tag = nounPhrase,
                Content = $"Weight : {nounPhrase.Weight}  \"{nounPhrase.Text}\"",
                Foreground = Brushes.Black,
                Padding = new Thickness(1, 1, 1, 1),
                ContextMenu = new ContextMenu(),
                ToolTip = $"{nounPhrase.GetType().Name}{(gender.IsMaleOrFemale() ? "\nprevialing gender: " + gender : string.Empty)}"
            };
            var menuItem1 = new MenuItem { Header = "View definition" };
            menuItem1.Click += (s, e) => Process.Start(string.Format("http://www.dictionary.reference.com/browse/{0}?s=t", nounPhrase.Text));
            label.ContextMenu.Items.Add(menuItem1);
            var menuItem2 = new MenuItem { Header = "Copy" };
            menuItem2.Click += (s, e) => Clipboard.SetText((label.Tag as ILexical).Text);
            label.ContextMenu.Items.Add(menuItem2);
            return label;
        }

        /// <summary>
        /// Asynchronously builds and displays the reconstructed text views for all documents.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task object representing the ongoing asynchronous operation.</returns>
        public async Task BuildTextViewsForAllDocumentsAsync()
        {  // This is for the lexial relationships tab
            var tasks = documents.Select(document => BuildTextViewOfDocument(document)).ToList();
            while (tasks.Any())
            {
                var finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
            }

        }

        private async Task BuildTextViewOfDocument(Document document)
        {
            Phrase.VerboseOutput = true;
            Word.VerboseOutput = true;
            var panel = new WrapPanel();
            var phrases = document
                .Paginate(80, 20)
                .Where(page => page.Paragraphs.PercentWhere(p => !p.Text.IsNullOrWhiteSpace()) < 50)
                .Take(10)
                .Select(page => page.Sentences)
                .DefaultIfEmpty(document.Sentences)
                .SelectMany(sentence => sentence.Phrases());
            var colorizer = new SyntacticColorMapping();
            var flowDocument = new FlowDocument();

            var documentContents = (from phrase in phrases
                                    select new DocumentRun
                                    {
                                        Text = phrase.Text,
                                        Tag = phrase,
                                        Foreground = colorizer[phrase],
                                        Background = Brushes.White,
                                        ToolTip = phrase.ToString().SplitRemoveEmpty('\n', '\r').Format(Tuple.Create(' ', '\n', ' '))
                                    }).ToList();



            foreach (var run in documentContents)
            {
                var menu = ContextMenuBuilder.ForLexical(run.Tag as ILexical, documentContents);
                if (menu != null)
                {
                    run.ContextMenu = menu;
                }
            }
            var flowDocumentParagraph = new System.Windows.Documents.Paragraph();
            flowDocumentParagraph.Inlines.AddRange(documentContents.SelectMany(run => new[] { RebuildRun(run), run }));
            flowDocument.Blocks.Add(flowDocumentParagraph);
            //flowDocument.GotFocus += (s, e) => MessageBox.Show((s as System.Windows.Documents.Run).Text);

            var tab = new TabItem
            {
                Header = document.Name,
                Content = new FlowDocumentPageViewer
                {
                    Document = flowDocument,
                    OpacityMask = Brushes.White,
                },
                Background = Brushes.White,
                OpacityMask = Brushes.White,
            };
            recomposedDocumentsTabControl.Items.Add(tab);
            recomposedDocumentsTabControl.SelectedItem = tab;
            await Task.Yield();
        }

        private static DocumentRun RebuildRun(DocumentRun run) => new DocumentRun((run.Tag as Phrase).Words.FirstOrDefault() is Symbol ? string.Empty : " ");

        #endregion


        private async Task ProcessNewDocument(string docPath, ProgressBar progressBar, Label progressLabel)
        {
            try
            {
                this.currentOperationProgressBar = progressBar;
                this.currentOperationLabel = progressLabel;
                var chosenFile = await AttemptToAddNewDocument(docPath);
                var docName = chosenFile.NameSansExt;
                var doc = await ProcessNewDocDocument(docName);
                documents.Add(doc);
            }
            catch (FileConversionFailureException e)
            {
                var failureMessage = $".doc file conversion failed\n{e.Message}";
                Logger.Log(failureMessage);
                this.ShowMessage(failureMessage);
            }
        }

        private async Task<Document> ProcessNewDocDocument(string documentName)
        {
            currentOperationLabel.Content = $"Tagging {documentName}...";
            var textfile = FileManager.TxtFiles.Where(f => f.NameSansExt == documentName).First();
            var analizer = new AnalysisOrchestrator(textfile.Lift());
            analizer.ProgressChanged += async (sender, e) =>
            {
                currentOperationLabel.Content = e.Message;
                currentOperationProgressBar.ToolTip = e.Message;
                currentOperationProgressBar.Value += e.PercentWorkRepresented;
                await StepProgress();
            };

            var doc = (await analizer.ProcessAsync()).First();

            await CreateWeightViewAsync(doc);
            await BuildTextViewOfDocument(doc);
            currentOperationLabel.Content = $"{documentName}: Added...";
            currentOperationProgressBar.Value = 100;
            return doc;
        }

        private async Task<InputFile> AttemptToAddNewDocument(string documentPath)
        {
            var chosenFile = FileManager.AddFile(documentPath);
            try
            {// Attempt to convert the newly added file
                await FileManager.ConvertAsNeededAsync();
                return chosenFile;
            }
            catch (FileConversionFailureException)
            {
                FileManager.RemoveFile(chosenFile);// Remove the original file from the project
                throw;
            }
        }

        private async Task StepProgress()
        {
            for (var i = 0; i < 9; ++i)
            {
                currentOperationProgressBar.Value += 1;
                await Task.Delay(1);
            }
        }

        /// <summary>
        /// Tags, loads, parses, binds weights, and visualizes the document indicated by the provided path.
        /// </summary>
        /// <param name="documentPath">The file system path to a document file.</param>
        /// <returns>A System.Threading.Tasks.Task object representing the ongoing work while the document is being processed.</returns>
        private async Task ProcessNewDocument(string documentPath)
        {
            await ProcessNewDocument(documentPath, currentOperationProgressBar, currentOperationLabel);
        }



        #region Named Event Handlers

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void PrintButton_Click_1(object sender, RoutedEventArgs e)
        {
            PrintSelectedChart();
        }

        private void PrintSelectedChart()
        {
            var printDialog = new PrintDialog();
            printDialog.ShowDialog();
            var focusedChart = (FrequencyCharts.SelectedItem as dynamic).Content;
            try
            {
                printDialog.PrintVisual(focusedChart, "Current View");
            }
            catch (NullReferenceException)
            {
                Logger.Log("There is no chart selected by the user, there is nothing to print.");
            }
        }

        private void ChangeToBarChartButton_Click(object sender, RoutedEventArgs e)
        {
            Visualizer.ToBarChartsAsync();
        }
        private void ChangeToColumnChartButton_Click(object sender, RoutedEventArgs e)
        {
            Visualizer.ToColumnChartsAsync();
        }
        private void ChangeToPieChartButton_Click(object sender, RoutedEventArgs e)
        {
            Visualizer.ToPieChartsAsync();
        }
        private void NewProjectMenuItem_Click_1(object sender, RoutedEventArgs e)
        {  //Hacky solution to make every option function. This makes new project restart LASI.
            Application.Current.Exit += (sndr, evt) => System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        private void OpenManualMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SharedFunctionality.OpenManualWithInstalledViewer(this);
        }
        private void openLicensesMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var componentsDisplay = new ComponentInfoDialog
            {

                Owner = this
            };
            componentsDisplay.Reposition(this.Top / 2, this.Left / 2).ShowDialog();
        }
        private void HelpAbout_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(LASI.App.App.Config["ProjectWebsite"]);
        }
        private async void exportButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.WhenAll(from document in documents
                               let outfilePath = System.IO.Path.Combine(
                                   FileManager.ResultsDirectory,
                                   document.Name.SplitRemoveEmpty('.').LastOrDefault() ?? (document.Name + '.' + Properties.Settings.Default.OutputFormat))
                               let serializer = new SimpleXmlSerializer()
                               select Task.Run(() => serializer
                                    .Serialize(document.Phrases, document.Name)
                                    .Save(outfilePath)));
            var exportDialog = new ExportResultsDialog();
            exportDialog.ShowDialog();
        }
        private async void documentJoinButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CrossJoinSelectDialog(this)
            {
                Left = this.Left,
                Top = this.Top,
            };
            if (dialog.ShowDialog() ?? false)
            {
                var joinedRelationshipResults = await new CrossDocumentJoiner().GetCommonResultsAsnyc(dialog.SelectedDocuments);
                metaRelationshipsDataGrid.ItemsSource = joinedRelationshipResults.ToGridRowData();
                metaViewTab.Visibility = Visibility.Visible;
            }
        }
        private async void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = DocumentManager.FileTypeFilter,
                Multiselect = true,
            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Length < 1)
            {
                return;
            }
            for (int i = 0; i < openDialog.SafeFileNames.Length; i++)
            {
                var file = new FileInfo(openDialog.FileNames[i]);
                if (DocumentManager.HasFileWithName(file.Name))
                {
                    this.ShowMessage($"A document named {file} is already part of the project.");
                }
                else if (DocumentManager.AbleToOpen(file))
                {
                    await AddNewDocument(file);
                }
                else
                {
                    this.ShowMessage($"The document {file} is in use by another process, please close any applications which may be using the document and try again.");
                }
            }
        }
        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            await SharedFunctionality.HandleDropAddAsync(this, e, AddNewDocument);
        }
        private void openPreferencesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SharedFunctionality.DisplayPreferencesWindow(this);
        }

        #endregion

        private async Task AddNewDocument(FileInfo file)
        {
            addDocumentMenuItem.IsEnabled = DocumentManager.CanAdd;
            DocumentManager.AddDocument(file.Name, file.FullName);
            currentOperationLabel.Content = $"Converting {file.Name}";
            currentOperationFeedbackCanvas.Visibility = Visibility.Visible;
            currentOperationProgressBar.Value = 0;
            await ProcessNewDocument(file.FullName);
        }

        #endregion

        private void CloseApp_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void AddDocument_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            AddMenuItem_Click(sender, e);
        }

        private void Print_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            PrintSelectedChart();
        }

        private void OpenPreferences_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            openPreferencesMenuItem_Click(sender, e);
        }

        private void OpenManual_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenManualMenuItem_Click_1(sender, e);
        }

        #region Properties and Fields

        private ICollection<Document> documents = new List<Document>();
        /// <summary>
        /// Gets or sets the list of LASI.Algorithm.Document objects in the current project.
        /// </summary>
        public IEnumerable<Document> Documents
        {
            get
            {
                return documents;
            }
            set
            {
                documents = value.ToList();
            }
        }

        #endregion

    }

}
