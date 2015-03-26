using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LASI.Content;
using LASI.Content.Tagging;
using LASI.Core;

namespace LASI.Interop
{
    /// <summary>
    /// Governs the complete analysis and processing of one or more text sources.
    /// Provides synchronous and asynchronous callback based progress reports.
    /// </summary>
    /// <remarks>
    /// <example>
    ///Example event registration:
    /// <code>
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
    /// </remarks>
    public sealed class AnalysisOrchestrator : Progress<Core.Configuration.ReportEventArgs>
    {        /// <summary>
             /// Initializes a new instance of the AnalysisController class.
             /// </summary>
             /// <param name="rawTextSource">An untagged English language written work.</param>
        public AnalysisOrchestrator(IRawTextSource rawTextSource)
            : this(new[] { rawTextSource })
        { }

        /// <summary>
        /// Initializes a new instance of the AnalysisController class.
        /// </summary>
        /// <param name="rawTextSources">A collection of untagged English language written works.</param>
        public AnalysisOrchestrator(IEnumerable<IRawTextSource> rawTextSources)
            : base(delegate { })
        {
            this.rawTextSources = rawTextSources;
            sourceCount = rawTextSources.Count();
            stepMagnitude = 2d / sourceCount;
        }

        /// <summary>
        /// <para>Gets a Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;</para>
        /// <para>which, when awaited, loads, analyzes, and aggregates all of the provided TextFile instances as individual documents, collecting them as</para>
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.
        /// </summary>
        /// <returns>
        /// <para>A Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;, when awaited, loads and analyzes,
        /// and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</para>
        /// </returns>
        public async Task<IEnumerable<Document>> ProcessAsync()
        {
            var taggedFiles = await TagFilesAsync(rawTextSources);
            Results = await BindAndWeightAsync(taggedFiles);
            return Results;
        }

        private async Task<IEnumerable<ITaggedTextSource>> TagFilesAsync<TRaw>(IEnumerable<TRaw> rawTextDocuments) where TRaw : IRawTextSource
        {
            Progress("Tagging Documents", 0);
            var tasks = rawTextDocuments.Select(raw => TagRawAsync(raw)).ToList();
            var taggedFiles = new ConcurrentBag<ITaggedTextSource>();
            while (tasks.Count > 0)
            {
                var task = await Task.WhenAny(tasks);
                var tagged = await task;
                tasks.Remove(task);
                taggedFiles.Add(tagged);
                Progress($"{tagged.Name}: Tagged", stepMagnitude + 1.5);
            }
            Progress("Tagged Documents", 3);
            return taggedFiles;
        }

        private async Task<IEnumerable<Document>> BindAndWeightAsync(IEnumerable<ITaggedTextSource> taggedFiles)
        {
            var tasks = taggedFiles.Select(ProcessTaggedAsync).ToArray();
            return await Task.WhenAll(tasks);
        }

        private async Task<Document> ProcessTaggedAsync(ITaggedTextSource tagged)
        {
            var name = tagged.Name;
            Progress($"{name}: Loading...", 0);
            var document = await tagger.DocumentFromTaggedAsync(tagged);
            Progress($"{name}: Loaded", 4 / sourceCount);
            Progress($"{name}: Analyzing Syntax...", 0);
            foreach (var bindingTask in document.GetBindingTasks())
            {
                Progress(bindingTask.InitializationMessage, 0);
                await bindingTask.Task;
                Progress(bindingTask.CompletionMessage, bindingTask.PercentCompleted * 0.71 / sourceCount);
            }
            Progress($"{name}: Correlating Relationships...", 0);
            foreach (var task in document.GetWeightingTasks())
            {
                Progress(task.InitializationMessage, 1 / sourceCount);
                await task.Task;
                Progress(task.CompletionMessage, task.PercentCompleted * 0.59 / sourceCount);
            }
            Progress($"{name}: Coalescing Results...", stepMagnitude);
            return document;
        }

        private async Task<ITaggedTextSource> TagRawAsync<TRaw>(TRaw raw) where TRaw : IRawTextSource =>
            await tagger.TaggedFromRawAsync(raw);

        private void Progress(string message, double percentCompleted)
        {
            OnReport(new AnalysisUpdateEventArgs(message, percentCompleted));
        }

        #region Fields

        private double sourceCount;
        private double stepMagnitude;
        private IEnumerable<IRawTextSource> rawTextSources;
        private Tagger tagger = new Tagger();
        public IEnumerable<Document> Results { get; private set; }
        #endregion Fields
    }
}