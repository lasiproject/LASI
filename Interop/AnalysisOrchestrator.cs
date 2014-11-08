using LASI.Core;

using LASI.ContentSystem;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace LASI.Interop
{

    /// <summary>
    /// Governs the complete analysis and processing of one or more text sources.
    /// Provides synchronous and asynchronous callback based progress reports.
    /// </summary>
    public sealed class AnalysisOrchestrator : Progress<AnalysisUpdateEventArgs>
    {        /// <summary>
             /// Initializes a new instance of the AnalysisController class.
             /// </summary>
             /// <param name="rawTextSource">An untagged English language written work.</param>
        public AnalysisOrchestrator(IRawTextSource rawTextSource)
            : this(new[] { rawTextSource }) { }

        /// <summary>
        /// Initializes a new instance of the AnalysisController class.
        /// </summary>
        /// <param name="rawTextSources">A collection of untagged English language written works.</param>
        public AnalysisOrchestrator(IEnumerable<IRawTextSource> rawTextSources)
            : base(e => { }) {
            this.rawTextSources = rawTextSources;
            sourceCount = rawTextSources.Count();
            stepMagnitude = 2d / sourceCount;
            observable = Enumerable.Empty<AnalysisUpdateEventArgs>().ToObservable();
        }

        /// <summary>
        /// <para>Gets a Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;</para>
        /// <para>which, when awaited, loads, analyzes, and aggregates all of the provided TextFile instances as individual documents, collecting them as</para>
        /// <body>a sequence of Bound and Weighted LASI.Algorithm.Document instances.</body>
        /// </summary>
        /// <returns>
        /// <para>A Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;, when awaited, loads and analyzes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</para>
        /// </returns>
        /// <example>
        ///Example event registration:
        ///<code>
        /// var myDocument = new TxtFile(path);
        /// var myOrchestrator = new AnalysisOrchestrator(myDocument);
        /// myOrchestrator.ProgressChanged += (sender, e) => MsgBox.Show(e.Message + " " + e.Increment);
        /// IEnumerable&lt;Document&gt; results = await myOrchestrator.ProcessAsync();
        /// </code>
        /// </example>
        /// <example>
        /// Attaching an Asynchronous Event Handler
        /// <code>
        /// var myDocument = new TxtFile(path);
        /// var myOrchestrator = new AnalysisOrchestrator(myDocument);
        /// var logFile = File.OpenText(@"\log.txt");
        /// myOrchestrator.ProgressChanged += async (sender, e) => await logFile.WriteLineAsync(e.Message + " " + e.Increment));
        /// IEnumerable&lt;Document&gt; results = await myOrchestrator.ProcessAsync();
        /// </code>
        /// </example>
        public async Task<IEnumerable<Document>> ProcessAsync() {
            var taggedFiles = await TagFilesAsync(rawTextSources);
            return await BindAndWeightDocumentsAsync(taggedFiles);
        }
        private async Task<IEnumerable<ITaggedTextSource>> TagFilesAsync(IEnumerable<IRawTextSource> rawTextDocuments) {
            OnReport(new AnalysisUpdateEventArgs("Tagging Documents", 0));
            var tasks = rawTextDocuments.Select(raw => Task.Run(async () => await new Tagger().TaggedFromRawAsync(raw))).ToList();
            var taggedFiles = new ConcurrentBag<ITaggedTextSource>();
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
        private async Task<IEnumerable<Document>> BindAndWeightDocumentsAsync(IEnumerable<ITaggedTextSource> taggedFiles) {
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
        private async Task<Document> ProcessTaggedFileAsync(ITaggedTextSource tagged) {
            var fileName = tagged.SourceName;
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Loading...", fileName), 0));
            var document = await new Tagger().DocumentFromTaggedAsync(tagged);
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Loaded", fileName), 4 / sourceCount));
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Analyzing Syntax...", fileName), 0));
            foreach (var bindingTask in document.GetBindingTasks()) {
                OnReport(new AnalysisUpdateEventArgs(bindingTask.InitializationMessage, 0));
                await bindingTask.Task;
                OnReport(new AnalysisUpdateEventArgs(bindingTask.CompletionMessage, bindingTask.PercentCompleted * 0.58 / sourceCount));
            }
            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Correlating Relationships...", fileName), 0));
            foreach (var task in document.GetWeightingTasks()) {
                OnReport(new AnalysisUpdateEventArgs(task.InitializationMessage, 1 / sourceCount));
                await task.Task;
                OnReport(new AnalysisUpdateEventArgs(task.CompletionMessage, task.PercentCompleted * 0.59 / sourceCount));
            }

            OnReport(new AnalysisUpdateEventArgs(string.Format("{0}: Coalescing Results...", fileName), stepMagnitude));
            return document;
        }

        IObservable<AnalysisUpdateEventArgs> observable;// new AnalysisUpdateEventArgs("Initializing...", 0.0)));
        //IImmutableList<IObserver<AnalysisUpdateEventArgs>> observers = ImmutableList.Create<IObserver<AnalysisUpdateEventArgs>>();
        #region Fields

        private double sourceCount;
        private double stepMagnitude;
        private IEnumerable<IRawTextSource> rawTextSources;

        #endregion


    }
}



