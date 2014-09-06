using LASI.App.Dialogs;
using LASI.App.LexicalElementInfo;
using LASI.ContentSystem;
using LASI.ContentSystem.Serialization.Xml;
using LASI.Core;
using LASI.Core.Heuristics;
using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LASI.Interop;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ResultsScreen class.
        /// </summary>
        public ResultsWindow() {
            InitializeComponent();
            currentOperationFeedbackCanvas.Visibility = Visibility.Hidden;
            Visualizer.ChangeChartKind(ChartContentType.NounPhrasesOnly);
            this.Closed += (s, e) => Application.Current.Shutdown();
        }
        #endregion

        #region Methods

        #region View Construction

        /// <summary>
        /// This function associates The buttons which allow the user to modify various aspects of the chart views with their respective functionality.
        /// This is done to allow the functionality to be exposed only after the charts have been 
        /// </summary>
        private void SetupChartViewControls() {
            changeToBarChartButton.Click += ChangeToBarChartButton_Click;
            changeToColumnChartButton.Click += ChangeToColumnChartButton_Click;
            changeToPieChartButton.Click += ChangeToPieChartButton_Click;
        }
        /// <summary>
        /// Creates and displays a weighted list of the top NounPhrases for each document.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing the ongoing asynchronous operation.</returns>
        public async Task CreateWeightViewsForAllDocumentsAsync() {
            foreach (var doc in documents) {
                await CreateWeightViewAsync(doc);

            }
            SetupChartViewControls();
        }

        private async Task CreateWeightViewAsync(Document document) {
            var page1 = document.Paginate(100, 100).FirstOrDefault();

            var nounPhraseLabels = from s in page1 != null ? page1.Sentences : document.Paragraphs.SelectMany(p => p.Sentences)
                                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   select s.Phrases.OfNounPhrase() into nounPhrases
                                   from np in nounPhrases
                                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   group np by new { np.Text, Type = np.GetType() }
                                       into npg
                                   let first = npg.First()
                                   orderby first.Weight descending
                                   select CreateLabelForWeightedView(first);

            var weightedListPanel = new StackPanel();
            var grid = new Grid();
            grid.Children.Add(new ScrollViewer { Content = weightedListPanel });
            foreach (var l in nounPhraseLabels) { weightedListPanel.Children.Add(l); }


            var tab = new TabItem { Header = document.Name, Content = grid };

            weightedByDocumentTabControl.Items.Add(tab);
            weightedByDocumentTabControl.SelectedItem = tab;

            await Visualizer.InitChartDisplayAsync(document);
            await Visualizer.DisplayKeyRelationships(document);
        }

        private static Label CreateLabelForWeightedView(NounPhrase np) {
            var gender = np.GetGender();
            var label = new Label
            {
                Tag = np,
                Content = String.Format("Weight : {0}  \"{1}\"", np.Weight, np.Text),
                Foreground = Brushes.Black,
                Padding = new Thickness(1, 1, 1, 1),
                ContextMenu = new ContextMenu(),
                ToolTip = string.Format("{0}{1}",
                np.GetType().Name, gender.IsMaleOrFemale() ? "\nprevialing gender: " + gender : string.Empty)
            };
            var menuItem1 = new MenuItem { Header = "View definition" };
            menuItem1.Click += (s, e) => Process.Start(string.Format("http://www.dictionary.reference.com/browse/{0}?s=t", np.Text));

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
        public async Task BuildTextViewsForAllDocumentsAsync() {  // This is for the lexial relationships tab
            var tasks = documents.Select(d => BuildTextViewOfDocument(d)).ToList();
            while (tasks.Any()) {
                var finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
            }

        }

        private async Task BuildTextViewOfDocument(Document document) {
            Phrase.VerboseOutput = true;
            Word.VerboseOutput = true;
            var panel = new WrapPanel();
            var tab = new TabItem
            {
                Header = document.Name,
                Content = new ScrollViewer
                {
                    Content = panel,
                    Background = Brushes.White,
                    OpacityMask = Brushes.White,
                }
            };
            var elementLabels = new List<Label>();
            var phrases = document.Paginate(80, 20)
                .Take(1)
                .Select(page => page.Sentences)
                .DefaultIfEmpty(document.Sentences)
                .SelectMany(sen => sen.OfPhrase());
            var colorizer = new SyntacticColorMap();
            foreach (var phrase in phrases) {
                var label = new Label
                {
                    Content = phrase.Text + (phrase is SymbolPhrase ? " " : string.Empty),
                    Tag = phrase,
                    Foreground = colorizer[phrase],
                    Background = Brushes.White,
                    OpacityMask = Brushes.White,
                    Padding = new Thickness(1, 1, 1, 1),
                    ContextMenu = ContextMenuFactory.ForLexical(phrase, elementLabels) ?? new ContextMenu(),
                    ToolTip = phrase.ToString()
                        .SplitRemoveEmpty('\n', '\r')
                        .Format(Tuple.Create(' ', ' ', ' '), s => s + '\n')
                };
                elementLabels.Add(label);
            }
            foreach (var l in elementLabels) {
                panel.Children.Add(l);
            }
            recomposedDocumentsTabControl.Items.Add(tab);
            recomposedDocumentsTabControl.SelectedItem = tab;
            await Task.Yield();
        }

        #endregion


        private async Task ProcessNewDocument(string docPath, ProgressBar progressBar, Label progressLabel) {
            try {
                var chosenFile = await AttemptToAddNewDocument(docPath);
                var docName = chosenFile.NameSansExt;
                var doc = await ProcessNewDocDocument(docName);
                documents.Add(doc);
            }
            catch (FileConversionFailureException e) {
                var failureMessage = string.Format(".doc file conversion failed\n{0}", e.Message);
                Output.WriteLine(failureMessage);
                MessageBox.Show(this, failureMessage);
            }
        }

        private async Task<Document> ProcessNewDocDocument(string docName) {
            currentOperationLabel.Content = string.Format("Tagging {0}...", docName);
            var textfile = FileManager.TxtFiles.Where(f => f.NameSansExt == docName).First();
            var analizer = new AnalysisOrchestrator(textfile);
            analizer.ProgressChanged += (sender, e) => {
                currentOperationLabel.Content = e.Message; currentOperationProgressBar.ToolTip = e.Message;
                currentOperationProgressBar.Value += e.PercentWorkRepresented;
            };

            var doc = (await analizer.ProcessAsync()).First();

            await CreateWeightViewAsync(doc);
            await BuildTextViewOfDocument(doc);
            currentOperationLabel.Content = string.Format("{0}: Added...", docName);
            currentOperationProgressBar.Value = 100;
            return doc;
        }

        private async Task<InputFile> AttemptToAddNewDocument(string docPath) {
            var chosenFile = FileManager.AddFile(docPath, true);
            try {// Attempt to convert the newly added file
                await FileManager.ConvertAsNeededAsync();
                return chosenFile;
            }
            catch (FileConversionFailureException) {
                FileManager.RemoveFile(chosenFile);// Remove the original file from the project
                throw;
            }
        }

        private async Task StepProgress(double steps) {
            for (int i = 0;
            i < 9;
            i++) {
                currentOperationProgressBar.Value += 1;
                await Task.Delay(1);
            }
        }

        /// <summary>
        /// Tags, loads, parses, binds weights, and visualizes the document indicated by the provided path.
        /// </summary>
        /// <param name="docPath">The file system path to a document file.</param>
        /// <returns>A System.Threading.Tasks.Task object representing the ongoing work while the document is being processed.</returns>
        private async Task ProcessNewDocument(string docPath) {
            await ProcessNewDocument(docPath, currentOperationProgressBar, currentOperationLabel);
        }



        #region Named Event Handlers

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) {
            this.Close();
            Application.Current.Shutdown();
        }

        private void printButton_Click_1(object sender, RoutedEventArgs e) {
            var printDialog = new PrintDialog();
            printDialog.ShowDialog();
            var focusedChart = (FrequencyCharts.SelectedItem as TabItem).Content as Visual;
            try {
                printDialog.PrintVisual(focusedChart, "Current View");
            }
            catch (NullReferenceException) {
                Output.WriteLine("There is no chart selected by the user, there is nothing to print.");
            }
        }
        private async void ChangeToBarChartButton_Click(object sender, RoutedEventArgs e) {
            await Visualizer.ToBarCharts();
        }
        private async void ChangeToColumnChartButton_Click(object sender, RoutedEventArgs e) {
            await Visualizer.ToColumnCharts();
        }
        private async void ChangeToPieChartButton_Click(object sender, RoutedEventArgs e) {
            await Visualizer.ToPieCharts();
        }
        private void NewProjectMenuItem_Click_1(object sender, RoutedEventArgs e) {  //Hacky solution to make every option function. This makes new project restart LASI.
            App.Current.Exit += (sndr, evt) => System.Windows.Forms.Application.Restart();
            App.Current.Shutdown();
        }

        private void OpenManualMenuItem_Click_1(object sender, RoutedEventArgs e) {
            SharedWindowFunctionality.ProcessOpenManualRequest(this);
        }
        private void openLicensesMenuItem_Click_1(object sender, RoutedEventArgs e) {
            var componentsDisplay = new ComponentInfoDialogWindow
            {
                Left = this.Left,
                Top = this.Top,
                Owner = this
            };
            componentsDisplay.ShowDialog();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            Process.Start("http://lasi-project.org");
        }
        private async void exportButton_Click(object sender, RoutedEventArgs e) {
            foreach (var doc in documents) {
                await Task.Run(() => SimpleLexicalSerializer.Serialize(from S in doc.Sentences
                                                                       from R in S.Phrases
                                                                       select R, doc.Name, DegreeOfOutput.Comprehensive).Save(
                FileManager.ResultsDir + System.IO.Path.DirectorySeparatorChar + new string(
                doc.Name.TakeWhile(c => c != '.').ToArray()) + ".xml"));

            }
            var exportDialog = new ExportResultsDialog();
            exportDialog.ShowDialog();
        }
        private async void documentJoinButton_Click(object sender, RoutedEventArgs e) {
            var dialog = new CrossJoinSelectDialog(this)
            {
                Left = this.Left,
                Top = this.Top,
            };

            if (dialog.ShowDialog() ?? false) {
                var joinedRelationshipResults = await new CrossDocumentJoiner().GetCommonResultsAsnyc(dialog.SelectedDocuments);
                metaRelationshipsDataGrid.ItemsSource = joinedRelationshipResults.ToTextItemSource();
            }
        }
        private async void AddMenuItem_Click(object sender, RoutedEventArgs e) {
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
                } else if (!file.CanOpen()) {
                    await AddNewDocument(file);
                } else {
                    MessageBox.Show(this, string.Format("The document {0} is in use by another process, please close any applications which may be using the document and try again.", file));
                }
            }
        }
        private async void Grid_Drop(object sender, DragEventArgs e) {
            await SharedWindowFunctionality.HandleDropAddAttemptAsync(this, e, AddNewDocument);
        }
        private void openPreferencesMenuItem_Click(object sender, RoutedEventArgs e) {
            SharedWindowFunctionality.OpenPreferencesWindow(this);
        }

        #endregion

        private async Task AddNewDocument(FileInfo file) {
            addDocumentMenuItem.IsEnabled = DocumentManager.CanAdd;
            DocumentManager.AddDocument(file.Name, file.FullName);
            currentOperationLabel.Content = string.Format("Converting {0}...", file.Name);
            currentOperationFeedbackCanvas.Visibility = Visibility.Visible;
            currentOperationProgressBar.Value = 0;
            await ProcessNewDocument(file.FullName);
        }




        #endregion

        private void CloseApp_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            this.Close();
            Application.Current.Shutdown();
        }

        private void AddDocument_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            AddMenuItem_Click(sender, e);
        }

        private void Print_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            printButton_Click_1(sender, e);
        }

        private void OpenPreferences_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            openPreferencesMenuItem_Click(sender, e);
        }

        private void OpenManual_CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            OpenManualMenuItem_Click_1(sender, e);
        }



        #region Properties and Fields

        private ICollection<Document> documents = new List<Document>();
        /// <summary>
        /// Gets or sets the list of LASI.Algorithm.Document objects in the current project.
        /// </summary>
        public IEnumerable<Document> Documents {
            get {
                return documents;
            }
            set {
                documents = value.ToList();
            }
        }

        #endregion

    }

}
