using LASI.Algorithm;
using LASI.FileSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections.ObjectModel;
using LASI.Algorithm.Binding;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.Analysis;

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

        }


        public void BuildAssociationTextView() {
            var TestDocument1 = new TaggedFileParser(FileManager.TaggedFiles.First()).LoadDocument();
            Binder.Bind(TestDocument1);
            LASI.Algorithm.Analysis.Weighter.weight(TestDocument1);
            foreach (var phrase in TestDocument1.Phrases) {
                var phraseLabel = new Label {
                    Content = phrase.Text,
                    Tag = phrase,
                    Foreground = Brushes.Black,
                    Padding = new Thickness(1, 1, 1, 1),
                    ContextMenu = new ContextMenu(),
                    ToolTip = phrase.Type.Name,
                };
                var vP = phrase as VerbPhrase;
                if (vP != null && vP.BoundSubjects.Count() > 0) {
                    var visitSubjectMI = new MenuItem {
                        Header = "view subjects"
                    };
                    visitSubjectMI.Click += (sender, e) => {
                        var objlabels = from r in vP.BoundSubjects
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
                        var objlabels = from r in vP.IndirectObjects
                                        join l in phraseLabels on r equals l.Tag
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
                AssociationPhrasePanal.Children.Add(l);
            }
        }





        public void BuildFullSortedView() {
            foreach (var doc in from doc in FileManager.TaggedFiles.AsParallel()
                                select new TaggedFileParser(doc).LoadDocument()) {
                var wordLabels = new List<Label>();
                Binder.Bind(doc);
                Weighter.weight(doc);
                var words = doc.Words.GetNouns().Concat<Word>(doc.Words.GetAdverbs()).
                     Concat<Word>(doc.Words.GetAdjectives()).Concat<Word>(doc.Words.GetVerbs()).
                     GroupBy(w => new {
                         w.Text,
                         w.Type
                     }).Select(g => g.First());

                foreach (var word in words) {
                    var wordLabel = MakeWordLabel(word);
                    var menuItem1 = new MenuItem {
                        Header = "view definition",
                    };
                    menuItem1.Click += (sender, e) => {
                        Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", word.Text));
                    };
                    wordLabel.ContextMenu.Items.Add(menuItem1);

                    wordLabels.Add(wordLabel);


                }

                ValueList = new List<KeyValuePair<string, int>>();
                ValueList.AddRange(
                    (from w in words
                     orderby w.Weight descending
                     select new KeyValuePair<string, int>(w.Text, (int) w.Weight)).Take(15).ToList());
                ValueList.Reverse();
                lineChart.DataContext = ValueList;




                var s = new ScrollViewer();
                var stackPanel = new StackPanel();
                s.Content = stackPanel;
                var grid = new Grid();
                grid.Children.Add(s);
                var tabItem = new TabItem {
                    Header = doc.FileName,
                    Content = grid
                };
                foreach (var l in from w in wordLabels
                                  orderby (w.Tag as Word).Weight descending
                                  select w) {
                    stackPanel.Children.Add(l);
                }
                TestView.Items.Add(tabItem);

            }

        }
        public List<KeyValuePair<string, int>> ValueList {
            get;
            private set;
        }

        private static Label MakeWordLabel(Word word) {
            var wordLabel = new Label {
                Tag = word,
                Content = String.Format("Weight : {0}  \"{1}\"", word.Weight, word.Text),
                Foreground = Brushes.Black,
                Padding = new Thickness(1, 1, 1, 1),
                ContextMenu = new ContextMenu(),
                ToolTip = word.Type.Name,
            };
            return wordLabel;
        }

        //private void BuildUniqueClickAction(Label wordLabel) {
        //    wordLabel.MouseDown += (sender, e) => {
        //        var intraPhraseLabels = from w in (wordLabel.Tag as Word).ParentPhrase.Words
        //                                join l in wordLabels on w.ID equals (l.Tag as Word).ID
        //                                select l;
        //        foreach (var l in intraPhraseLabels) {
        //            if (l.Background != Brushes.Green && wordLabel.Foreground != Brushes.Red) {
        //                l.Foreground = Brushes.White;
        //                l.Background = Brushes.Green;
        //                wordLabel.Foreground = Brushes.Black;
        //                wordLabel.Background = Brushes.Red;

        //            } else {
        //                l.Background = Brushes.White;
        //                l.Foreground = Brushes.Black;
        //                wordLabel.Foreground = Brushes.Black;
        //                wordLabel.Background = Brushes.White;
        //            }
        //        }

        //    };
        //}

        private List<Label> phraseLabels = new List<Label>();

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {

            this.SwapWith(WindowManager.CreateProjectScreen);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }

        List<string> tokens = @"The typically small numbers of COIs identified by stakeholders with specific problems should each be accompanied by a similarly small number
` effectiveness measures (Sproles, 2002) that stakeholders can use to make binary, 'yes' or 'no,' determinations as to whether or not COI and 
the problems they characterize have been resolved. Recalling their definition as 'emergent properties that induce rank orderings,' 
MOEs are truly the 'standards' of the same definition as well as, perhaps most plainly, the variables of 'goodness' described by Dockery (1986, p. 172).
Their complete independence from solutions proposed to dispel the problems from which they fundamentally derive represents another of MOEs' 
most significant features, one reinforced with the Figure 3 that builds on Figure 2 by displaying only those entities - the problem and 
the COIs characterizing it - to which MOEs may be properly linked.".Split('\r', '\n', '\t', ' ').ToList();



        private void printButton_Click_1(object sender, RoutedEventArgs e) {
            var printDialog = new PrintDialog();
            printDialog.ShowDialog();
            printDialog.PrintVisual(resultsGrid, "Current View");
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }




        public bool AutoExport {
            get;
            set;
        }
    }
}
