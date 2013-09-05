using LASI;
using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Lookup;
using LASI.Algorithm.Patternization;
using LASI.ContentSystem;
using LASI.ContentSystem.Serialization.XML;
using LASI.InteropLayer;
using LASI.UserInterface.Dialogs;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using LASI.UserInterface.LexicalElementInfo;
using LASI.Algorithm.Weighting;


namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsScreen : Window
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ResultsScreen class.
        /// </summary>
        public ResultsScreen() {

            InitializeComponent();
            currentOperationFeedbackCanvas.Visibility = Visibility.Hidden;
            Visualizer.ChangeChartKind(ChartKind.NounPhrasesOnly);
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
            var page1 = document.Paginate(100).FirstOrDefault();

            var nounPhraseLabels = from s in page1 != null ? page1.Sentences : document.Paragraphs.SelectMany(p => p.Sentences)
                                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   select s.Phrases.GetNounPhrases() into nounPhrases
                                   from np in nounPhrases
                                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                   group np by new { np.Text, np.Type }
                                       into npg
                                       let first = npg.First()
                                       orderby first.Weight descending
                                       select CreateLabelForWeightedView(first);

            var weightedListPanel = new StackPanel();
            var grid = new Grid();

            foreach (var l in nounPhraseLabels) { weightedListPanel.Children.Add(l); }

            grid.Children.Add(new ScrollViewer { Content = weightedListPanel });
            var tab = new TabItem { Header = document.Name, Content = grid };

            weightedByDocumentTabControl.Items.Add(tab);
            weightedByDocumentTabControl.SelectedItem = tab;

            await Visualizer.InitChartDisplayAsync(document);
            await Visualizer.DisplayKeyRelationships(document);
        }

        private static Label CreateLabelForWeightedView(NounPhrase np) {
            var gender = np.GetGender();
            var label = new Label {
                Tag = np,
                Content = String.Format("Weight : {0}  \"{1}\"", np.Weight, np.Text),
                Foreground = Brushes.Black,
                Padding = new Thickness(1, 1, 1, 1),
                ContextMenu = new ContextMenu(),
                ToolTip = string.Format("{0}{1}",
                np.Type.Name, gender.IsMaleOrFemale() ? "\nprevialing gender: " + gender : "")
            };
            var menuItem1 = new MenuItem { Header = "View definition" };
            menuItem1.Click += (s, e) => Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", np.Text));

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
            var tab = new TabItem {
                Header = document.Name,
                Content = new ScrollViewer {
                    Content = panel,
                    Background = Brushes.White,
                    OpacityMask = Brushes.White,
                }
            };
            var elementLabels = new List<Label>();
            var dataSource = document.Paginate(50).FirstOrDefault();
            foreach (var phrase in (dataSource != null ? dataSource.Sentences : document.Sentences).SelectMany(p => p.Phrases)) {
                var label = new Label {
                    Content = phrase.Text + (phrase is SymbolPhrase ? " " : string.Empty),
                    Tag = phrase,
                    Foreground = phrase.GetBrush(),
                    Background = Brushes.White,
                    OpacityMask = Brushes.White,
                    Padding = new Thickness(1, 1, 1, 1),
                    ContextMenu = new ContextMenu(),
                    ToolTip = phrase.ToString()
                        .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Format(Tuple.Create(' ', ' ', ' '), s => s + '\n')
                };
                label.ContextMenu =
                    phrase.Match()
                    .Yield<ContextMenu>()
                        .Case<IPronoun>(p => ContextMenuFactory.MakePronounContextMenu(elementLabels, p))
                        .Case<IVerbal>(v => ContextMenuFactory.MakeVerbalContextMenu(elementLabels, v))
                    .Result(label.ContextMenu);
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
            catch (NullReferenceException) { // There is no chart selected by the user.
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

        private async Task ProcessNewDocument(string docPath, ProgressBar progressBar, Label progressLabel) {

            var chosenFile = FileManager.AddFile(docPath, true);
            try {
                await FileManager.ConvertAsNeededAsync();
            }
            catch (FileConversionFailureException e) {
                FileManager.RemoveFile(chosenFile);
                MessageBox.Show(this, string.Format(".doc file conversion failed\n{0}", e.Message));
            }

            await StepProgress(5);
            currentOperationLabel.Content = string.Format("Tagging {0}...", chosenFile.NameSansExt);
            var textfile = FileManager.TextFiles.Where(f => f.NameSansExt == chosenFile.NameSansExt).First();

            var doc = await Tagger.DocumentFromRawAsync(textfile);
            await StepProgress(10);
            currentOperationLabel.Content = string.Format("{0}: Analyzing Syntax...", chosenFile.NameSansExt);
            foreach (var task in Binder.GetBindingTasksForDocument(doc)) {
                currentOperationLabel.Content = task.InitializationMessage;
                await task.Task;
                await StepProgress(task.PercentWorkRepresented);
                currentOperationLabel.Content = task.CompletionMessage;
            }

            await StepProgress(3);
            currentOperationLabel.Content = string.Format("{0}: Correlating Relationships...", chosenFile.NameSansExt);
            var tasks = Weighter.GetWeightingProcessingTasks(doc).ToList();
            foreach (var task in tasks) {

                var message = task.InitializationMessage;
                currentOperationLabel.Content = message;
                await task.Task;
                await StepProgress(3);


            }

            currentOperationProgressBar.Value += 5;
            currentOperationLabel.Content = string.Format("{0}: Visualizing...", chosenFile.NameSansExt);
            await CreateWeightViewAsync(doc);
            await BuildTextViewOfDocument(doc);

            currentOperationLabel.Content = string.Format("{0}: Added...", chosenFile.NameSansExt);

            currentOperationProgressBar.Value = 100;

            documents.Add(doc);
        }

        private async Task StepProgress(double steps) {
            for (int i = 0; i < 9; i++) {
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

        private void NewProjectMenuItem_Click_1(object sender, RoutedEventArgs e) {

            //Hacky solution to make every option function. This makes new project restart LASI.
            App.Current.Exit += (sndr, evt) => System.Windows.Forms.Application.Restart();
            App.Current.Shutdown();
        }

        private void OpenManualMenuItem_Click_1(object sender, RoutedEventArgs e) {
            try {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"\Manual.pdf");
            }
            catch (FileNotFoundException) {
                MessageBox.Show(this, "Unable to locate the User Manual, please contact the LASI team for further support.");
            }
            catch (Exception) {
                MessageBox.Show(this, "Sorry, the manual could not be opened. Please ensure you have a pdf viewer installed.");
            }
        }

        private void openLicensesMenuItem_Click_1(object sender, RoutedEventArgs e) {
            var componentsDisplay = new ComponentInfoDialogWindow {
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
                using (
                    var docWriter = new SimpleLexicalSerializer(
                    FileManager.ResultsDir + System.IO.Path.DirectorySeparatorChar + new string(
                    doc.Name.TakeWhile(c => c != '.').ToArray()) + ".xml")) {
                    await docWriter.WriteAsync(from S in doc.Sentences
                                               from R in S.Phrases
                                               select R, doc.Name, DegreeOfOutput.Comprehensive);
                }
            }
            var exportDialog = new ExportResultsDialog();
            exportDialog.ShowDialog();
        }

        private async void documentJoinButton_Click(object sender, RoutedEventArgs e) {
            var dialog = new CrossJoinSelectDialog(this) {
                Left = this.Left,
                Top = this.Top,
            };

            if (!(dialog.ShowDialog()) ?? false)
                return;

            var r = await new CrossDocumentJoiner().JoinDocumentsAsnyc(dialog.SelectDocuments);

            metaRelationshipsDataGrid.ItemsSource = Visualizer.CreateRelationshipData(r);


        }


        private async void AddMenuItem_Click(object sender, RoutedEventArgs e) {

            var openDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "LASI File Types|*.doc; *.docx; *.pdf; *.txt",
            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Any()) {
                if (!DocumentManager.FileNamePresent(openDialog.SafeFileName)) {
                    currentOperationLabel.Content = string.Format("Converting {0}...", openDialog.FileName);
                    currentOperationFeedbackCanvas.Visibility = Visibility.Visible;
                    currentOperationProgressBar.Value = 0;
                    await ProcessNewDocument(openDialog.FileName);
                    //currentOperationFeedbackCanvas.Visibility = Visibility.Hidden;
                } else {
                    MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", openDialog.SafeFileName));
                }
            }

        }

        #endregion


        #endregion

        private void openPreferencesMenuItem_Click(object sender, RoutedEventArgs e) {
            var preferences = new PreferencesWindow();
            preferences.Left = (this.Left - preferences.Left) / 2;
            preferences.Top = (this.Top - preferences.Top) / 2;
            var saved = preferences.ShowDialog();
        }

        #region Properties and Fields

        private List<Document> documents = new List<Document>();
        /// <summary>
        /// Gets or sets the list of LASI.Algorithm.Document objects in the current project.
        /// </summary>
        public List<Document> Documents {
            get {
                return documents;
            }
            set {
                documents = value;
            }
        }

        #endregion

    }

}
