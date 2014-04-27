using LASI.Core;
using LASI.Core.Heuristics;
using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using LASI.Utilities;
using LASI.Interop;

namespace LASI.App
{
    /// <summary>
    /// Provides static methods for formatting and displaying results to the user.
    /// </summary>
    public static class Visualizer
    {


        #region Chart Transposing Methods

        /// <summary>
        /// Instructs the Charting manager to recreate all Per-Document charts based on the provided ChartKind. 
        /// The given ChartKind will be used for all further chart operations until it is changed via another call to ChangeChartKind.
        /// </summary>
        /// <param name="chartKind">The ChartKind value determining the what data set is to be displayed.</param>
        public static void ChangeChartKind(ChartKind chartKind) {
            ChartKind = chartKind;
            foreach (var pair in documentsByChart) {

                Document doc = pair.Value;
                Chart chart = pair.Key;

                IEnumerable<KeyValuePair<string, float>> data = null;

                switch (chartKind) {
                    case ChartKind.SubjectVerbObject:
                        data = GetVerbWiseData(doc);
                        break;
                    case ChartKind.NounPhrasesOnly:
                        data = GetNounWiseData(doc);
                        break;
                }
                //data = data.Take(CHART_ITEM_LIMIT);
                chart.Series.Clear();
                chart.Series.Add(new BarSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = data,
                    IsSelectionEnabled = true,

                });
                chart.Title = string.Format("Key Relationships in {0}", doc.Name);
                break;
            }
        }


        /// <summary>
        /// Reconfigures all charts to Subjects Column perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToColumnCharts() {
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(item => item.Content as Chart).Where(c => c != null)) {
                var items = chart.GetItemSource();
                var series = new ColumnSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true
                };
                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }



        /// <summary>
        /// Reconfigures all charts to Subjects Pie perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToPieCharts() {
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(item => item.Content as Chart).Where(c => c != null)) {
                var items = chart.GetItemSource();
                var series = new PieSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                series.IsMouseCaptureWithinChanged += (sender, e) => {
                    series.ToolTip = (series.SelectedItem as DataPoint).DependentValue;
                };

                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }

        /// <summary>
        /// Reconfigures all charts to Subjects Bar perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToBarCharts() {

            await Task.Yield();
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(item => item.Content as Chart).Where(c => c != null)) {
                var items = chart.GetItemSource();
                var series = new BarSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                ResetChartContent(chart, series);
            }
        }
        /// <summary>
        /// Asynchronously initializes, creates and displays the default Chart for the given document. This method should only be called once.
        /// </summary>
        /// <param name="document">The document whose contents are to be charted.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task InitChartDisplayAsync(Document document) {
            var chart = await BuildBarChart(document);
            documentsByChart.Add(chart, document);
            var tab = new TabItem {
                Header = document.Name,
                Content = chart,
                Tag = chart
            };
            WindowManager.ResultsScreen.FrequencyCharts.Items.Add(tab);
            WindowManager.ResultsScreen.FrequencyCharts.SelectedItem = tab;
            await ToBarCharts();
        }


        private static async Task<Chart> BuildBarChart(Document document) {

            var dataPointSource =
                ChartKind == ChartKind.NounPhrasesOnly ?
                await GetNounWiseDataAsync(document) :
                ChartKind == ChartKind.SubjectVerbObject ?
                GetVerbWiseData(document) :
                GetVerbWiseData(document);
            // Materialize item source so that changing chart types is less expensive.s
            var topPoints = dataPointSource.OrderByDescending(point => point.Value).Take(CHART_ITEM_LIMIT).ToList();
            Series series = new BarSeries {
                DependentValuePath = "Value",
                IndependentValuePath = "Key",
                ItemsSource = topPoints,
                IsSelectionEnabled = true,
                Tag = document,

            };

            var chart = new Chart {
                Title = string.Format("Key Subjects in {0}", document.Name),
                Tag = topPoints
            };

            series.MouseMove += (sender, e) => {
                series.ToolTip = (e.Source as DataPoint).IndependentValue;
            };

            chart.Series.Add(series);
            return chart;
        }



        #endregion

        #region General Chart Building Methods
        /// <summary>
        /// Removes all current data points from the chart before adding the provided DataPointSeries to the chart.
        /// </summary>
        /// <param name="chart">The chart whose contents to replace with the given The DataPointSeries.</param>
        /// <param name="series">The DataPointSeries containing the data points which the chart will contain.</param>
        public static void ResetChartContent(Chart chart, DataPointSeries series) {
            chart.Series.Clear();
            chart.Series.Add(series);
        }

        private static IEnumerable<KeyValuePair<string, float>> GetItemSource(this Chart chart) {
            return (chart.Tag as IEnumerable<KeyValuePair<string, float>>).Reverse();//.Take(CHART_ITEM_LIMIT);

        }

        #endregion
        private static IEnumerable<KeyValuePair<string, float>> GetVerbWiseData(Document doc) {
            var data = GetVerbWiseRelationships(doc);
            return from svs in data
                   let SV = new KeyValuePair<string, float>(
                       string.Format("{0} -> {1}\n", svs.Subject.Text, svs.Verbal.Text) +
                       (svs.Direct != null ? " -> " + svs.Direct.Text : "") +
                       (svs.Indirect != null ? " -> " + svs.Indirect.Text : ""),
                       (float)Math.Round(svs.CombinedWeight, 2))
                   group SV by SV into svg
                   select svg.Key;

        }
        private static IEnumerable<SvoRelationship> GetVerbWiseRelationships(Document doc) {
            var consideredVerbals = doc.Phrases.OfVerbPhrase()
                           .WithSubject(s => !(s is IReferencer) || (s as IReferencer).ReferesTo != null)
                           .Distinct((x, y) => x.IsSimilarTo(y))
                           .AsParallel()
                           .WithDegreeOfParallelism(Concurrency.Max);
            var relationships = from vp in consideredVerbals
                                from s in vp.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                let subject = s.Match().Yield<IEntity>().With((IReferencer r) => r.ReferesTo).Result(s)
                                where subject != null
                                from direct in vp.DirectObjects.DefaultIfEmpty()
                                from indirect in vp.IndirectObjects.DefaultIfEmpty()
                                let relationship = new SvoRelationship {
                                    Subject = vp.AggregateSubject,
                                    Verbal = vp,
                                    Direct = vp.AggregateDirectObject,
                                    Indirect = vp.AggregateIndirectObject,
                                    Prepositional = vp.ObjectOfThePreoposition,
                                    CombinedWeight = subject.Weight + vp.Weight + (direct != null ? direct.Weight : 0) + (indirect != null ? indirect.Weight : 0)
                                }
                                where relationship.Subject != null && relationship.Direct != null || relationship.Indirect != null && relationship.Subject.Text != (relationship.Direct ?? relationship.Indirect).Text
                                select relationship;
            return relationships.Distinct().OrderByDescending(r => r.CombinedWeight);

        }
        private static async Task<IEnumerable<KeyValuePair<string, float>>> GetNounWiseDataAsync(Document doc) {
            return await Task.Run(() => GetNounWiseData(doc));
        }

        private static IEnumerable<KeyValuePair<string, float>> GetNounWiseData(Document doc) {
            return from n in doc.Phrases.OfNounPhrase()
                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                   group n by new { n.Text, Weight = (float)Math.Round(n.Weight, 2) } into g
                   select new KeyValuePair<string, float>(g.Key.Text, g.Key.Weight);
        }

        /// <summary>
        /// Asynchronously generates, composeses and displays the key relationships view for the given Document.
        /// </summary>
        /// <param name="document">The document for which to build relationships.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task DisplayKeyRelationships(Document document) {

            var transformedData = await Task.Factory.StartNew(() => {
                return GetVerbWiseRelationships(document).ToTextItemSource();
            });
            var wpfToolKitDataGrid = new Microsoft.Windows.Controls.DataGrid {
                ItemsSource = transformedData,
            };
            var tab = new TabItem {
                Header = document.Name,
                Content = wpfToolKitDataGrid
            };
            WindowManager.ResultsScreen.SVODResultsTabControl.Items.Add(tab);
            WindowManager.ResultsScreen.SVODResultsTabControl.SelectedItem = tab;

        }

        /// <summary>
        /// Gets the ChartKind currently used by the ChartManager.
        /// </summary>
        public static ChartKind ChartKind {
            get;
            private set;
        }
        private static Dictionary<Chart, Document> documentsByChart = new Dictionary<Chart, Document>();


        private const int CHART_ITEM_LIMIT = 14;

        private static string GetTextIfNotNull<TLexical>(TLexical lexical) where TLexical : ILexical {
            return lexical != null ? lexical.Text : string.Empty;
        }

        /// <summary>
        /// Creates and returns a sequence of textual Display elements from the given sequence of RelationshipTuple elements.
        /// The resulting sequence is suitable for direct insertion into a DataGrid.
        /// </summary>
        /// <param name="elementsToConvert">The sequence of Relationship Tuple to tranform into textual Display elements.</param>
        /// <returns>A sequence of textual Display elements from the given sequence of RelationshipTuple elements.</returns>
        internal static IEnumerable<object> ToTextItemSource(this IEnumerable<SvoRelationship> elementsToConvert) {
            return
            from e in elementsToConvert
            orderby e.CombinedWeight
            select new {
                Subject = GetTextIfNotNull(e.Subject),
                Verbal = e.Verbal == null ? string.Empty : GetTextIfNotNull(e.Verbal.PrepositionOnLeft) + GetTextIfNotNull(e.Verbal.Modality) + e.Verbal.Text + string.Join(" ", e.Verbal.AdverbialModifiers.Select(m => m.Text)),
                Direct = e.Direct == null ? string.Empty : GetTextIfNotNull(e.Direct.PrepositionOnLeft) + e.Direct.Text,
                Indirect = e.Indirect == null ? string.Empty : GetTextIfNotNull(e.Indirect.PrepositionOnLeft) + e.Indirect.Text
            };

        }
    }

}