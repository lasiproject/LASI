using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using LASI.Core;
using LASI.Core.Heuristics;
using LASI.Core.PatternMatching;
using LASI.Interop;
using LASI.Utilities;

namespace LASI.App
{
    using Interop.ResourceManagement;
    using LASI.Core.Analysis;


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
        public static async void ChangeChartKindAsync(ChartKind chartKind) {
            ChartKind = chartKind;
            foreach (var pair in documentsByChart) {
                var document = pair.Value;
                var chart = pair.Key;
                var data = await CreateChartDataAsync(chartKind, document);
                chart.Series.Clear();
                chart.Series.Add(new BarSeries
                {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = data,
                    IsSelectionEnabled = true,
                });
                chart.Title = string.Format("Key Relationships in {0}", document.Name);
                break;
            }
        }


        private static async Task<IEnumerable<dynamic>> CreateChartDataAsync(ChartKind chartKind, Document document) {
            switch (chartKind) {
            case ChartKind.SubjectVerbObject: return await GetVerbWiseDataAsync(document);
            case ChartKind.NounPhrasesOnly: return await GetNounWiseDataAsync(document);
            default: return await GetNounWiseDataAsync(document);
            }
        }



        /// <summary>
        /// Reconfigures all charts to Subjects Column perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToColumnChartsAsync() {
            foreach (var chart in RetrieveCharts()) {
                var items = chart.GetItemSource();
                var series = new ColumnSeries
                {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true
                };
                ResetChartContent(chart, series);
                await Task.Yield();
            }
        }



        /// <summary>
        /// Reconfigures all charts to Subjects Pie perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToPieChartsAsync() {
            foreach (var chart in RetrieveCharts()) {
                var items = chart.GetItemSource();
                var series = new PieSeries
                {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                series.IsMouseCaptureWithinChanged += (sender, e) => {
                    series.ToolTip = (((series.SelectedItem as DataPoint))).DependentValue;
                };
                ResetChartContent(chart, series);
                await Task.Yield();
            }
        }

        private static IEnumerable<Chart> RetrieveCharts() {
            return WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(tab => tab.Content).OfType<Chart>();
        }


        /// <summary>
        /// Reconfigures all charts to Subjects Bar perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToBarChartsAsync() {
            foreach (var chart in RetrieveCharts()) {
                ResetChartContent(
                    chart,
                    new BarSeries
                    {
                        DependentValuePath = "Value",
                        IndependentValuePath = "Key",
                        ItemsSource = chart.GetItemSource(),
                        IsSelectionEnabled = true,
                    });
                await Task.Yield();
            }
        }
        /// <summary>
        /// Asynchronously initializes, creates and displays the default Chart for the given document. This method should only be called once.
        /// </summary>
        /// <param name="document">The document whose contents are to be charted.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task InitChartDisplayAsync(Document document) {
            var chart = await BuildBarChartAsync(document);
            documentsByChart.Add(chart, document);
            var tab = new TabItem
            {
                Header = document.Name,
                Content = chart,
                Tag = chart
            };
            WindowManager.ResultsScreen.FrequencyCharts.Items.Add(tab);
            WindowManager.ResultsScreen.FrequencyCharts.SelectedItem = tab;
            await ToBarChartsAsync();
        }


        private static async Task<Chart> BuildBarChartAsync(Document document) {

            var dataPointSource =
                ChartKind == ChartKind.NounPhrasesOnly ?
                await GetNounWiseDataAsync(document) :
                ChartKind == ChartKind.SubjectVerbObject ?
                await GetVerbWiseDataAsync(document) :
                await GetVerbWiseDataAsync(document);
            // Materialize item source so that changing chart types is less expensive.s
            var topPoints = dataPointSource
                .OrderByDescending(point => point.Value)
                .Take(CHART_ITEM_LIMIT).ToList();
            Series series = new BarSeries
            {
                DependentValuePath = "Value", // this string is expected by the charting engine
                IndependentValuePath = "Key", // this string is expected by the charting engine
                ItemsSource = topPoints,
                IsSelectionEnabled = true,
                Tag = document,
            };
            series.MouseMove += (sender, e) => {
                series.ToolTip = (e.Source as DataPoint).IndependentValue;
            };
            var chart = new Chart
            {
                Title = string.Format("Key Subjects in {0}", document.Name),
                Tag = topPoints
            };
            //chart.MouseEnter += (sender, e) => chart.ToolTip = (e.Source as DataPoint).DependentValue + " " + (e.Source as DataPoint).IndependentValue;
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

        private static IEnumerable<dynamic> GetItemSource(this Chart chart) => (chart.Tag as IEnumerable<dynamic>).Reverse();//.Take(CHART_ITEM_LIMIT);



        #endregion
        private static async Task<IEnumerable<dynamic>> GetVerbWiseData(Document document) {
            var dataPoints = from relationship in await GetVerbWiseRelationshipsAsync(document)
                             select new {
                                 Key = string.Format("{0} -> {1}\n", relationship.Subject.Text, relationship.Verbal.Text) +
                                     (relationship.Direct != null ? " -> " + relationship.Direct.Text : string.Empty) +
                                     (relationship.Indirect != null ? " -> " + relationship.Indirect.Text : string.Empty),
                                 Value = (float)Math.Round(relationship.Weight, 2)
                             };
            return dataPoints.Distinct();

        }
        private static async Task<IEnumerable<SvoRelationship>> GetVerbWiseRelationshipsAsync(Document document) {
            return await Task.Run(() => {
                var consideredVerbals = document.Phrases.OfVerbPhrase()
                                     .WithSubject(s => !(s is IReferencer) || (s as IReferencer).RefersTo != null)
                                     .Distinct((x, y) => x.IsSimilarTo(y))
                                     .AsParallel()
                                     .WithDegreeOfParallelism(Concurrency.Max);
                var relationships = from verbal in consideredVerbals
                                    from entity in verbal.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                    let subject = entity.Match().Yield<IEntity>()
                                        .When((IReferencer r) => r.RefersTo != null && r.RefersTo.Any())
                                        .Then((IReferencer r) => r.RefersTo)
                                        .Result(entity)
                                    where subject != null
                                    from direct in verbal.DirectObjects.DefaultIfEmpty()
                                    from indirect in verbal.IndirectObjects.DefaultIfEmpty()
                                    where direct != null || indirect != null
                                    where subject.Text != (direct ?? indirect).Text
                                    let relationship = new SvoRelationship(
                                        verbal.AggregateSubject,
                                        verbal,
                                        verbal.AggregateDirectObject,
                                        verbal.AggregateIndirectObject
                                    )
                                    orderby relationship.Weight descending
                                    select relationship;
                return relationships.Distinct();
            });
        }
        private static async Task<IEnumerable<dynamic>> GetNounWiseDataAsync(Document document) => await Task.Run(() => GetNounWiseData(document));

        private static async Task<IEnumerable<dynamic>> GetVerbWiseDataAsync(Document document) => await Task.Run(() => GetVerbWiseData(document));

        private static IEnumerable<dynamic> GetNounWiseData(Document document) {
            return document.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                 .OfNounPhrase()
                 .Meld()
                 .Select(entity => new { Key = entity.Text, Value = (float)Math.Round(entity.Weight, 2) })
                 .Distinct();
        }

        /// <summary>
        /// Asynchronously generates, composes and displays the key relationships view for the given Document.
        /// </summary>
        /// <param name="document">The document for which to build relationships.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task DisplayKeyRelationshipsAsync(Document document) {
            var tab = new TabItem
            {
                Header = document.Name,
                Content = new Microsoft.Windows.Controls.DataGrid
                {
                    ItemsSource = (await GetVerbWiseRelationshipsAsync(document)).ToGridRowData(),
                }
            };
            WindowManager.ResultsScreen.SVODResultsTabControl.Items.Add(tab);
            WindowManager.ResultsScreen.SVODResultsTabControl.SelectedItem = tab;
        }

        /// <summary>
        /// Gets the ChartKind currently used by the ChartManager.
        /// </summary>
        internal static ChartKind ChartKind {
            get;
            private set;
        }
        private static Dictionary<Chart, Document> documentsByChart = new Dictionary<Chart, Document>();



        private static string GetTextIfNotNull(ILexical lexical) => lexical?.Text ?? string.Empty;

        /// <summary>
        /// Transforms the given relationships into textual objects for display on a 4 column grid.
        /// The resulting sequence is suitable for direct insertion into a DataGrid.
        /// </summary>
        /// <param name="relationships">The sequence of Relationship Tuple to transform into textual Display elements.</param>
        /// <returns>A sequence of textual Display suitable for direct insertion into a DataGrid.</returns>
        internal static IEnumerable<dynamic> ToGridRowData(this IEnumerable<SvoRelationship> relationships) {
            return from relationship in relationships
                   orderby relationship.Weight descending
                   select new {
                       Subject = FormatSubjectRenderText(relationship.Subject),
                       Verbal = FormatVerbalDisplay(relationship.Verbal),
                       Direct = FormatObjectText(relationship.Direct),
                       Indirect = FormatObjectText(relationship.Indirect),
                       Weight = relationship.Weight
                   };
        }

        private static string FormatSubjectRenderText(IEntity entity) => entity?.Text ?? string.Empty;


        private static string FormatObjectText(IEntity directObject) {
            return (directObject?.PrepositionOnLeft?.Text ?? string.Empty) + directObject?.Text ?? string.Empty;
        }

        private static string FormatVerbalDisplay(IVerbal verbal) {
            if (verbal != null) {
                var result =
                    GetTextIfNotNull(verbal.PrepositionOnLeft) + " " +
                    GetTextIfNotNull(verbal.Modality) + " " + verbal.Text + " " +
                    string.Join(" ", verbal.AdverbialModifiers.Select(m => m.Text));
                return result.Trim();
            } else {
                return string.Empty;
            }
        }
        private const int CHART_ITEM_LIMIT = 14;
    }

}