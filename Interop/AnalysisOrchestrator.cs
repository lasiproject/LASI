using LASI.Core;
using LASI.Content;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LASI.Content.TaggerEncapsulation;
using LASI.Utilities;

namespace LASI.Interop
{

    /// <summary>
    /// Governs the complete analysis and processing of one or more text sources.
    /// Provides synchronous and asynchronous callback based progress reports.
    /// </summary>
    public sealed class AnalysisOrchestrator : Progress<Core.Reporting.ReportEventArgs>
    {        /// <summary>
             /// Initializes a new instance of the AnalysisController class.
             /// </summary>
             /// <param name="rawTextSource">An untagged English language written work.</param>
        public AnalysisOrchestrator(IRawTextSource first, params IRawTextSource[] rest)
            : this(rest.Prepend(first)) { }

        /// <summary>
        /// Initializes a new instance of the AnalysisController class.
        /// </summary>
        /// <param name="rawTextSources">A collection of untagged English language written works.</param>
        public AnalysisOrchestrator(IEnumerable<IRawTextSource> rawTextSources)
            : base(delegate { }) {
            this.rawTextSources = rawTextSources;
            sourceCount = rawTextSources.Count();
            stepMagnitude = 2d / sourceCount;
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
            return await BindAndWeightAsync(taggedFiles);
        }
        private async Task<IEnumerable<ITaggedTextSource>> TagFilesAsync(IEnumerable<IRawTextSource> rawTextDocuments) {
            Progress("Tagging Documents");
            var tasks = rawTextDocuments.Select(TagRawAsync).ToList();
            var taggedFiles = new ConcurrentBag<ITaggedTextSource>();
            while (tasks.Any()) {
                var task = await Task.WhenAny(tasks);
                var tagged = await task;
                tasks.Remove(task);
                taggedFiles.Add(tagged);
                percentDone += stepMagnitude + 1.5;
                Progress($"{tagged.SourceName}: Tagged");
            }
             percentDone += 3;
            Progress("Tagged Documents");
            return taggedFiles;
        }
        private async Task<IEnumerable<Document>> BindAndWeightAsync(IEnumerable<ITaggedTextSource> taggedFiles) {
            var tasks = taggedFiles.Select(ProcessTaggedAsync).ToArray();
            return await Task.WhenAll(tasks);
        }
        private async Task<Document> ProcessTaggedAsync(ITaggedTextSource tagged) {
            var name = tagged.SourceName;
            Progress($"{name}: Loading...");
            var document = await tagger.DocumentFromTaggedAsync(tagged);
            percentDone += 4 / sourceCount;
            Progress($"{name}: Loaded");
            Progress($"{name}: Analyzing Syntax...");
            foreach (var bindingTask in document.GetBindingTasks()) {
                Progress(bindingTask.InitializationMessage);
                await bindingTask.Task;
                percentDone += bindingTask.PercentCompleted * 0.8 / sourceCount;
                Progress(bindingTask.CompletionMessage);
            }
            Progress($"{name}: Correlating Relationships...");
            foreach (var task in document.GetWeightingTasks()) {
                percentDone += 1 / sourceCount;
                Progress(task.InitializationMessage);
                await task.Task;
                percentDone += task.PercentCompleted * 0.59 / sourceCount;
                Progress(task.CompletionMessage);
            }
            percentDone += stepMagnitude;
            Progress($"{name}: Coalescing Results...");
            return document;
        }
        private async Task<ITaggedTextSource> TagRawAsync(IRawTextSource raw) => await tagger.TaggedFromRawAsync(raw);
        private void Progress(string message) {
            OnReport(new AnalysisUpdateEventArgs(message, percentDone));
        }
        #region Fields

        private double sourceCount;
        private double stepMagnitude;
        private double percentDone;
        private IEnumerable<IRawTextSource> rawTextSources;
        private Tagger tagger = new Tagger();
        #endregion


    }
}



