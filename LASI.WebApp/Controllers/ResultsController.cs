using LASI.Core;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.DocumentStructures;
    using Interop;
    using Utilities;
    using Path = System.IO.Path;
    using JobStatusMap = System.Collections.Concurrent.ConcurrentDictionary<int, Models.Results.WorkItemStatus>;
    using NaiveTopResultSelector = Core.Analysis.Heuristics.NaiveTopResultSelector;
    using UpdateEventHandler = System.EventHandler<Core.Configuration.ReportEventArgs>;
    using CustomIdentity;
    using System.Collections.Immutable;
    using Newtonsoft.Json.Linq;
    using Models.User;
    using System.Collections.Concurrent;
    using System;
    using Microsoft.AspNet.Authorization;
    using Models;
    using System.Security.Claims;
    using Microsoft.AspNet.Hosting;

    [Authorize]
    public class ResultsController : Controller
    {
        public ResultsController(IDocumentProvider<UserDocument> documentStore, IWorkItemsService userWorkItemsService, IHostingEnvironment hostingEnvironment)
        {
            Phrase.VerboseOutput = true;
            this.documentStore = documentStore;
            this.userWorkItemsService = userWorkItemsService;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("Results/{documentId}")]
        public async Task<DocumentModel> Get(string documentId)
        {
            var userDocument = documentStore.GetByIds(User.GetUserId(), documentId);
            var model = await this.LoadResultDocument(userDocument);
            return model.First();
        }

        [HttpGet("Results")]
        public async Task<IEnumerable<dynamic>> Get()
        {
            var userDocuments = GetAllUserDocuments();
            var resultModels = await LoadResultDocument(userDocuments);
            return resultModels;
        }

        public async Task<ActionResult> Index()
        {
            TrackedJobs.Clear();
            CurrentOperation = string.Empty;
            PercentComplete = 0;
            return await Results();
        }
        public async Task<ViewResult> Results()
        {
            var resultModels = await Get();

            ViewBag.Charts = resultModels.DistinctBy(m => m.Title).ToDictionary(m => m.Title, m => m.ChartData);
            ViewBag.Title = "Results";
            return await Task.FromResult(View(new DocumentSetModel(resultModels as IEnumerable<Document>)));

        }

        [HttpGet("Results/Single/{documentId}")]
        public async Task<PartialViewResult> Single(string documentId) => PartialView("_Single", await Get(documentId));
        private UserDocument[] GetAllUserDocuments()
        {

            var userId = Context.User.GetUserId();

            var files = documentStore.GetAllForUser(userId).ToArray();
            return files;
        }

        private async Task<IEnumerable<DocumentModel>> LoadResultDocument(params UserDocument[] userDocuments) =>
            from document in await ProcessUserDocuments(userDocuments)
            let topResultPointsPlot = NaiveTopResultSelector.GetTopResultsByEntity(document).Take(ChartItemLimit)
            let chartData = from chartResult in topResultPointsPlot
                            select new object[] { chartResult.First, chartResult.Second }
            select new DocumentModel(document, chartData);
        //new { Content = document.Text, document.Name, userDocuments.First(d => d.Name == document.Name).Id };
        private async Task<IEnumerable<Document>> ProcessUserDocuments(params UserDocument[] userDocuments)
        {
            //processedDocuments = processedDocuments.Clear();
            var workItems = CreateWorkItemsForProcessingOperations(userDocuments).ToList();
            workItems.ForEach(item => userWorkItemsService.UpdateWorkItemForUser(User.GetUserId(), item));
            var results = new ConcurrentBag<Document>();
            await Task.WhenAll(from userDocument in userDocuments
                               join workItem in workItems
                               on userDocument.Id equals workItem.Id
                               select new
                               {
                                   WorkItem = workItem,
                                   Document = userDocument
                               } into documentWithWorkItem
                               select Task.Run(() =>
                               {
                                   var analyzer = new AnalysisOrchestrator(documentWithWorkItem.Document);
                                   var workItem = documentWithWorkItem.WorkItem;
                                   analyzer.ProgressChanged += CreateWorkItemUpdateEventHandler(workItem);
                                   var processedDocument = analyzer.ProcessAsync().Result.First();
                                   UpdateWorkItemToComplete(workItem);
                                   results.Add(processedDocument);
                               }));
            ProcessedDocuments = ProcessedDocuments.Union(results);
            return results;
        }

        #region ProcessUserDocuments Helpers

        private static UpdateEventHandler CreateWorkItemUpdateEventHandler(WorkItem workItem) => (sender, e) =>
        {
            workItem.PercentComplete = Math.Round(Math.Min(workItem.PercentComplete + e.PercentWorkRepresented, 100), 4);
            workItem.StatusMessage = e.Message;
            workItem.State = TaskState.Ongoing;
        };

        private static void UpdateWorkItemToComplete(WorkItem workItem)
        {
            workItem.PercentComplete = 100;
            workItem.StatusMessage = "Analysis Complete.";
            workItem.State = TaskState.Complete;
        }


        /// <summary>
        /// Determines the active user and appends the document processing operations to their active work items.
        /// </summary>
        /// <param name="userDocuments">The documents whose processing will become active work items.</param>
        /// <returns>A <see cref="Task"/> representing the ongoing operation.</returns>
        private IEnumerable<WorkItem> CreateWorkItemsForProcessingOperations(UserDocument[] userDocuments) =>
            from document in userDocuments
            let isCached = ProcessedDocuments.Select(d => d.Name).Any(name => name.EqualsIgnoreCase(document.Name))
            select new WorkItem
            {
                Id = document.Id,
                Name = document.Name,
                State = isCached ? TaskState.Complete : TaskState.Pending,
                StatusMessage = isCached ? "Analysis Complete." : "Pending",
                PercentComplete = isCached ? 100 : 0
            };

        #endregion

        #region Properties

        // These properties should be removed and replaced with a better solution to sharing
        // progress This was a hack to initially test the functionality
        public static string CurrentOperation { get; private set; }

        public static double PercentComplete { get; internal set; }

        public static IImmutableSet<Document> ProcessedDocuments { get; internal set; } = ImmutableHashSet.Create(ComparerFactory.Create<Document>((dx, dy) => dx.Name == dy.Name, d => d.Name.GetHashCode()));

        public static JobStatusMap TrackedJobs { get; } = new JobStatusMap();
        private string ServerPath => System.IO.Directory.GetParent(hostingEnvironment.WebRootPath).FullName;
        private string UserDocumentsDirectory => Path.Combine(ServerPath, "App_Data", "SourceFiles");
        #endregion Properties

        #region Fields
        private const int ChartItemLimit = 5;
        private readonly IDocumentProvider<UserDocument> documentStore;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IWorkItemsService userWorkItemsService;
        #endregion Fields
    }
}
