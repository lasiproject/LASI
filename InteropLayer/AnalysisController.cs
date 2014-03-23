using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.ContentSystem;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LASI.Interop
{

    /// <summary>
    /// Governs the complete analysis and processing of one or more text sources.
    /// Provides synchronous and asynchronoun callback based progress reports.
    /// </summary>
    public sealed class AnalysisController : Progress<AnalysisUpdateEventArgs>
    {        /// <summary>
        /// Initializes a new instance of the AnalysisController class.
        /// </summary>
        /// <param name="rawTextSource">An untagged english language written work.</param>
        public AnalysisController(IRawTextSource rawTextSource)
            : this(new[] { rawTextSource }) { }

        /// <summary>
        /// Initializes a new instance of the AnalysisController class.
        /// </summary>
        /// <param name="rawTextSources">A collection of untagged english language written works.</param>
        public AnalysisController(IEnumerable<LASI.ContentSystem.IRawTextSource> rawTextSources)
            : base(e => { }) {
            this.rawTextSources = rawTextSources;
            sourceCount = rawTextSources.Count();
            stepMagnitude = 2d / sourceCount;
        }



        /// <summary>
        /// <para>Gets a Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;</para>
        /// <para>which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as</para>
        /// <body>a sequence of Bound and Weighted LASI.Algorithm.Document instances. Progress update logic is specified via an asynchronous function parameter.</body>
        /// </summary>
        /// <returns>
        /// <para>A Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</para>
        /// </returns>
        /// <example>
        ///Example event registration:
        ///<code>
        /// myProcessController.ProgressChanged += async (sender, e) => MsgBox.Show(e.Message + " " + e.Increment);
        /// </code>
        /// </example>
        public async Task<IEnumerable<Document>> ProcessAsync() {

            var taggedFiles = await TagFilesAsync(rawTextSources);
            var results = await BindAndWeightDocumentsAsync(taggedFiles);

            return results;
        }





        private async Task<ConcurrentBag<ITaggedTextSource>> TagFilesAsync(IEnumerable<LASI.ContentSystem.IRawTextSource> rawTextDocuments) {
            OnReport(new AnalysisUpdateEventArgs("Tagging Documents", 0));
            var tasks = rawTextDocuments.Select(raw => Task.Run(async () => await new Tagger().TaggedFromRawAsync(raw))).ToList();
            var taggedFiles = new ConcurrentBag<LASI.ContentSystem.ITaggedTextSource>();
            while (tasks.Any()) {
                var task = await Task.WhenAny(tasks);
                var tagged = await task;
                tasks.Remove(task);
                taggedFiles.Add(tagged);
                OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Tagged", tagged.SourceName), stepMagnitude + 1.5));
            }
            OnReport(new AnalysisUpdateEventArgs("Tagged Documents", 3));
            return taggedFiles;
        }
        private async Task<IEnumerable<Document>> BindAndWeightDocumentsAsync(ConcurrentBag<ITaggedTextSource> taggedFiles) {
            var tasks = taggedFiles.Select(tagged => ProcessTaggedFileAsync(tagged)).ToList();
            var documents = new ConcurrentBag<Document>();
            while (tasks.Any()) {
                var currentTask = await Task.WhenAny(tasks);
                var processedDocument = await currentTask;
                tasks.Remove(currentTask);
                documents.Add(processedDocument);
            }
            return documents;
        }
        private async Task<Document> ProcessTaggedFileAsync(LASI.ContentSystem.ITaggedTextSource tagged) {
            var fileName = tagged.SourceName;
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Loading...", fileName), 0));
            var document = await new Tagger().DocumentFromTaggedAsync(tagged);
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Loaded", fileName), 4 / sourceCount));
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Analyzing Syntax...", fileName), 0));
            foreach (var bindingTask in document.GetBindingTasks()) {
                OnReport(new AnalysisUpdateEventArgs(bindingTask.InitializationMessage, 0));
                await bindingTask.Task;
                OnReport(new AnalysisUpdateEventArgs(bindingTask.CompletionMessage, bindingTask.PercentWorkRepresented * 0.5 / sourceCount));
            }
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Correlating Relationships...", fileName), 0));
            foreach (var task in document.GetWeightingTasks()) {
                OnReport(new AnalysisUpdateEventArgs(task.InitializationMessage, 1 / sourceCount));
                await task.Task;
                OnReport(new AnalysisUpdateEventArgs(task.CompletionMessage, task.PercentWorkRepresented * 0.5 / sourceCount));
            }

            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Coalescing Results...", fileName), stepMagnitude));
            return document;
        }

        #region Fields

        private double sourceCount;
        private double stepMagnitude;
        private IEnumerable<IRawTextSource> rawTextSources;

        #endregion


    }
}



