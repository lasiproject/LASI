using LASI.Algorithm;
using LASI.Algorithm.Analysis;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.Utilities;
using LASI.GuiInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace LASI.InteropLayer
{


    public class ProcessController
    {
        public StringBuilder InfoProvider {
            get;
            private set;
        }

        private ProgressBar ProgressBar = null;
        private Label ProgressLabel = null;
        private int documentStepRatio;
        public async Task<IEnumerable<Document>> LoadAndAnalyseAllDocuments(ProgressBar progressBar, Label progressLabel) {
            ProgressBar = progressBar;
            ProgressLabel = progressLabel;
            progressBar.ToolTip = ProcessingState.LoadingResources.ToString();
            progressLabel.Content = ProcessingState.LoadingResources.ToString();
            await Thesaurus.LoadAllAsync();
            progressBar.Value = 5;

            progressBar.ToolTip = ProcessingState.ConvertingFiles.ToString();
            progressLabel.Content = ProcessingState.ConvertingFiles.ToString();
            await FileManager.ConvertAsNeededAsync();
            progressBar.Value += 5;
            UpdateProgressDisplay("Transforming Data");
            documentStepRatio = 13 / FileManager.TextFiles.Count();
            var docs = new ConcurrentBag<LASI.Algorithm.DocumentConstructs.Document>();
            foreach (var textFile in FileManager.TextFiles) { docs.Add(await (BindParseWeight(await TagDocumentFile(textFile)))); }

            return docs;
        }

        private async Task<Document> TagDocumentFile(FileSystem.FileTypes.TextFile textFile) {
            var statusMessage = string.Format("{0}: Embedding Syntactic Annotations", textFile.NameSansExt);
            UpdateProgressDisplay(statusMessage);
            ProgressBar.Value += documentStepRatio;
            var taggedFile = await new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, textFile.FullPath).ProcessFileAsync();
            statusMessage = string.Format("{0}: Transforming Data", taggedFile.NameSansExt);
            UpdateProgressDisplay(statusMessage);
            ProgressBar.Value += documentStepRatio;
            return await new TaggedFileParser(taggedFile).LoadDocumentAsync();
        }

        private async Task<Document> BindParseWeight(Document doc) {
            var statusMessage = string.Format("{0}: Analyzing Structures", doc.FileName);
            UpdateProgressDisplay(statusMessage);
            await Binder.BindAsync(doc);
            statusMessage = string.Format("{0}: Weighting Significances", doc.FileName);
            UpdateProgressDisplay(statusMessage);
            await Weighter.WeightAsync(doc);
            UpdateProgressDisplay(statusMessage);
            return doc;
        }

        private void UpdateProgressDisplay(string statusMessage) {
            ProgressLabel.Content = statusMessage;
            ProgressBar.ToolTip = statusMessage;
            ProgressBar.Value += documentStepRatio;
        }

        private async Task WeightAllDocs(IEnumerable<LASI.Algorithm.DocumentConstructs.Document> docs) {

            await Task.WhenAll((docs.ToList().Select(doc => {

                return Task.Factory.StartNew(async () => {
                    await Weighter.WeightAsync(doc);
                    var statusMessage = string.Format("Document Scoped Weighting Completed for {0}", doc.FileName);
                    ProgressLabel.Content = statusMessage;
                    ProgressBar.ToolTip = statusMessage;
                    ProgressBar.Value += documentStepRatio;
                }, System.Threading.CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            })).ToArray());

        }

        private async Task BindLexicals(IEnumerable<LASI.Algorithm.DocumentConstructs.Document> docs) {

            await Task.WhenAll((docs.ToList().Select(doc => {

                return Task.Factory.StartNew(async () => {
                    await Binder.BindAsync(doc);
                    var statusMessage = string.Format("Bound Lexicals in {0}", doc.FileName);
                    ProgressLabel.Content = statusMessage;
                    ProgressBar.ToolTip = statusMessage;
                    ProgressBar.Value += documentStepRatio;
                }, System.Threading.CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            })).ToArray());





        }
        private async Task<IEnumerable<Document>> InstantiateDocuments() {
            var result = new ConcurrentBag<LASI.Algorithm.DocumentConstructs.Document>();
            foreach (var doc in FileManager.TaggedFiles) {
                result.Add(await new TaggedFileParser(doc).LoadDocumentAsync());
                await Task.Yield();
            }
            return result;
        }

    }



}


#region deprecated
//internal class AsyncWorkItem
//{

//    public Task WorkToDo {
//        get;
//        set;
//    }
//    public AsyncWorkItem() {

//    }
//    public async Task BeginTask() {
//        await WorkToDo;
//    }

//    public ProcessingState CompletionMessage {
//        get;
//        set;
//    }
//    ProcessingState[] statuses = new[] { 
//            ProcessingState.ConvertingFiles,
//            ProcessingState.TransformingTextualRepresentations,
//            ProcessingState.AggregatingPhrases, 
//ProcessingState.ComputingMetrics, 
//ProcessingState.CrossReferencing, 
//ProcessingState.Completing };

//    public ProcessingState[] Statuses {
//        get {
//            return statuses;
//        }
//        protected set {
//            statuses = value;
//        }
//    }
//}

#endregion