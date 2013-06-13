using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.UserInterface.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using LASI.UserInterface.DataVisualzationProviders;
using LASI.InteropLayer;
using LASI.Utilities;


namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsScreen : Window
    {
        public ResultsScreen() {

            InitializeComponent();
            this.Closed += (s, e) => Application.Current.Shutdown();
            ChartingManager.chartKind = ChartKind.NounPhrasesOnly;
        }
        public async Task CreateInteractiveViews() {

            foreach (var doc in documents) {
                await CreateInteractiveDoumentView(doc);

            }

            BindChartViewControls();
        }

        private async Task CreateInteractiveDoumentView(Document doc) {
            var documentElements = (from de in doc.Paragraphs.Except(doc.EnumContainingParagraphs).AsParallel()
                                    select de.Phrases into phraseElements
                                    from phrase in phraseElements.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    select phrase)
                                   .GetNounPhrases()
                                   .GroupBy(w => new {
                                       w.Text,
                                       w.Type
                                   }).Select(g => g.First());
            var nps = documentElements.GetNounPhrases();
            var vps = documentElements.GetVerbPhrases();
            var advps = documentElements.GetAdverbPhrases();
            var elementLabels = new List<Label>();
            foreach (var e in documentElements) {
                var wordLabel = CreateWeightedNounPhraseLabel(e);
                elementLabels.Add(wordLabel);
            }

            var scrollViewer = new ScrollViewer();
            var stackPanel = new StackPanel();
            scrollViewer.Content = stackPanel;
            var grid = new Grid();
            grid.Children.Add(scrollViewer);
            var tabItem = new TabItem {
                Header = doc.FileName,
                Content = grid
            };
            foreach (var l in from w in elementLabels
                              orderby (w.Tag as ILexical).Weight descending
                              select w) {
                stackPanel.Children.Add(l);
            }
            WordCountLists.Items.Add(tabItem);
            ChartingManager.BuildMainChartDisplay(doc);
            await ChartingManager.BuildKeyRelationshipDisplay(doc);
        }

        private static Label CreateWeightedNounPhraseLabel(NounPhrase e) {
            var wordLabel = new Label {
                Tag = e,
                Content = String.Format("Weight : {0}  \"{1}\"", e.Weight, e.Text),
                Foreground = Brushes.Black,
                Padding = new Thickness(1, 1, 1, 1),
                ContextMenu = new ContextMenu(),
                ToolTip = e.Type.Name,
            };
            var menuItem1 = new MenuItem {
                Header = "view definition",
            };
            menuItem1.Click += (sender, ee) => {
                Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", e.Text));
            };
            var menuItem2 = new MenuItem {
                Header = "Copy Text"
            };
            menuItem2.Click += (se, ee) => Clipboard.SetText((wordLabel.Tag as ILexical).Text);
            wordLabel.ContextMenu.Items.Add(menuItem1);
            wordLabel.ContextMenu.Items.Add(menuItem2);
            return wordLabel;
        }





        public async Task BuildReconstructedDocumentViews() {  // This is for the lexial relationships tab
            foreach (var doc in documents) {
                await BuildInteractiveTextViewOfDocument(doc);
            }
        }

        private async Task BuildInteractiveTextViewOfDocument(Document doc) {
            var panel = new WrapPanel();
            var tab = new TabItem {
                Header = doc.FileName,
                Content = new ScrollViewer {
                    Content = panel
                }

            };
            var phraseLabels = new List<Label>();
            foreach (var phrase in doc.Phrases) {
                var phraseLabel = new Label {
                    Content = phrase.Text,
                    Tag = phrase,
                    Foreground = Brushes.Black,
                    Padding = new Thickness(1, 1, 1, 1),
                    ContextMenu = new ContextMenu(),
                    ToolTip = phrase.Type.Name,


                };
                var vP = phrase as VerbPhrase;

                if (vP != null && vP.Subjects.Count() > 0) {
                    var visitSubjectMI = new MenuItem {
                        Header = "view subjects"
                    };
                    visitSubjectMI.Click += (sender, e) => {
                        var objlabels = from r in vP.Subjects
                                        join l in phraseLabels on r equals l.Tag
                                        select l;
                        foreach (var l in objlabels) {
                            l.Foreground = Brushes.Black;
                            l.Background = Brushes.Red;
                        }
                    };
                    phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                }
                if (vP != null && vP.DirectObjects.Count() > 0) {
                    var visitSubjectMI = new MenuItem {
                        Header = "view direct objects"
                    };
                    visitSubjectMI.Click += (sender, e) => {
                        var objlabels = from r in vP.DirectObjects
                                        join l in phraseLabels on r equals l.Tag
                                        select l;
                        foreach (var l in objlabels) {
                            l.Foreground = Brushes.Black;
                            l.Background = Brushes.Red;
                        }
                    };
                    phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                }
                if (vP != null && vP.IndirectObjects.Count() > 0) {
                    var visitSubjectMI = new MenuItem {
                        Header = "view indirect objects"
                    };
                    visitSubjectMI.Click += (sender, e) => {
                        var objlabels = from r in
                                            vP.IndirectObjects
                                        join l in phraseLabels on r equals l.Tag
                                        select l;
                        foreach (var l in objlabels) {
                            l.Foreground = Brushes.Black;
                            l.Background = Brushes.Red;
                        }
                    };
                    phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                }
                if (vP != null && vP.ObjectOfThePreoposition != null) {
                    var visitSubjectMI = new MenuItem {
                        Header = "view prepositional object"
                    };
                    visitSubjectMI.Click += (sender, e) => {
                        var objlabels = from l in phraseLabels
                                        where l.Tag.Equals(vP.ObjectOfThePreoposition)
                                        select l;
                        foreach (var l in objlabels) {
                            l.Foreground = Brushes.Black;
                            l.Background = Brushes.Red;
                        }
                    };
                    phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                }
                phraseLabels.Add(phraseLabel);
            }
            foreach (var l in phraseLabels) {
                panel.Children.Add(l);
            }
            recomposedDocumentsTabControl.Items.Add(tab);
            await Task.Yield();
        }


        private List<Document> documents = new List<Document>();

        public List<Document> Documents {
            get {
                return documents;
            }
            set {
                documents = value;
            }
        }
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) {
            this.Close();

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
            await ChartingManager.ToBarCharts();
        }

        private async void ChangeToColumnChartButton_Click(object sender, RoutedEventArgs e) {
            await ChartingManager.ToColumnCharts();
        }

        private async void ChangeToPieChartButton_Click(object sender, RoutedEventArgs e) {
            await ChartingManager.ToPieCharts();
        }

        /// <summary>
        /// This function associates The buttons which allow the user to modify various aspects of the chart views with their respective functionality.
        /// This is done to allow the functionality to be exposed only after the charts have been 
        /// </summary>
        private void BindChartViewControls() {
            changeToBarChartButton.Click += ChangeToBarChartButton_Click;
            changeToColumnChartButton.Click += ChangeToColumnChartButton_Click;
            changeToPieChartButton.Click += ChangeToPieChartButton_Click;
        }



        private async void exportButton_Click(object sender, RoutedEventArgs e) {
            foreach (var doc in documents) {
                using (
                    var docWriter = new LASI.FileSystem.Serialization.XML.SimpleLexicalSerializer(
                    FileManager.ResultsDir + System.IO.Path.DirectorySeparatorChar + new string(
                    doc.FileName.TakeWhile(c => c != '.').ToArray()) + ".xml")) {
                    await docWriter.WriteAsync(from S in doc.Sentences
                                               from R in S.Phrases
                                               select R, doc.FileName, FileSystem.Serialization.XML.DegreeOfOutput.Comprehensive);
                }
            }
            var exportDialog = new DialogToProcedeToResults();
            exportDialog.ShowDialog();
        }

        private async void documentJoinButton_Click(object sender, RoutedEventArgs e) {
            var documentJoinDialog = new CrossJoinSelectDialog(this);

            bool? dialogResult = documentJoinDialog.ShowDialog();
            if (dialogResult ?? false) {
                var selectedDocument = documentJoinDialog.SelectDocuments;
                CrossDocumentJoiner joiner = new CrossDocumentJoiner(selectedDocument);

                var results = await joiner.JoinDocumentsAsnyc();
                await CreateMetaResultsView(results);
            }
        }

        private async Task CreateMetaResultsView(IEnumerable<CrossDocumentJoiner.NVNN> crossResults) {
            var data = await ChartingManager.CreateRelationshipDataAsync(crossResults);

            metaRelationshipsDataGrid.ItemsSource = data;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {

        }

        private async void AddMenuItem_Click(object sender, RoutedEventArgs e) {

            var openDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "LASI File Types|*.docx; *.pdf; *.txt",
            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Any()) {
                if (!DocumentManager.FileNamePresent(openDialog.SafeFileName)) {
                    currentOperationLabel.Content = string.Format("Converting {0}...", openDialog.FileName);
                    currentOperationFeedbackCanvas.Visibility = Visibility.Visible;
                    currentOperationProgressBar.Value = 0;



                    await ProcessNewDocument(openDialog.FileName);
                    currentOperationFeedbackCanvas.Visibility = Visibility.Hidden;
                }
                else {
                    MessageBox.Show(string.Format("A document named {0} is already part of the project.", openDialog.SafeFileName));
                }
            }



        }
        private async Task ProcessNewDocument(string docPath, ProgressBar progressBar, Label progressLabel) {

            var chosenFile = FileManager.AddFile(docPath, true);

            await FileManager.ConvertAsNeededAsync();
            currentOperationProgressBar.Value += 10;
            currentOperationLabel.Content = string.Format("Tagging {0}...", chosenFile.NameSansExt);
            var textfile = FileManager.TextFiles.Where(f => f.NameSansExt == chosenFile.NameSansExt).First();

            var doc = await TaggerUtil.LoadTextFileAsync(textfile);
            currentOperationProgressBar.Value += 10;
            currentOperationLabel.Content = string.Format("{0}: Analysing Syntax...", chosenFile.NameSansExt);
            await LASI.Algorithm.Binding.Binder.BindAsync(doc);
            currentOperationProgressBar.Value += 15;
            currentOperationLabel.Content = string.Format("{0}: Correlating Relationships...", chosenFile.NameSansExt);
            var tasks = LASI.Algorithm.Weighting.Weighter.GetWeightingTasksForDocument(doc).ToList();
            foreach (var task in tasks) {

                var message = await task;
                currentOperationLabel.Content = message;
                currentOperationProgressBar.Value += 5;

            }

            currentOperationProgressBar.Value += 5;
            currentOperationLabel.Content = string.Format("{0}: Visualizing...", chosenFile.NameSansExt);
            await CreateInteractiveDoumentView(doc);
            await BuildInteractiveTextViewOfDocument(doc);

            currentOperationLabel.Content = string.Format("{0}: Added...", chosenFile.NameSansExt);

            currentOperationProgressBar.Value = 100;

            documents.Add(doc);
        }
        private async Task ProcessNewDocument(string docPath) {
            await ProcessNewDocument(docPath, currentOperationProgressBar, currentOperationLabel);
        }
    }

}
