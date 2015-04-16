using AspSixApp.Models;
using LASI.Core;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.DocumentStructures;
    using LASI.Interop;
    using LASI.Utilities;
    using Path = System.IO.Path;
    using JobStatusMap = System.Collections.Concurrent.ConcurrentDictionary<int, Models.Results.JobStatus>;
    using NaiveTopResultSelector = LASI.Core.Analysis.Heuristics.NaiveTopResultSelector;
    using UpdateEventHandler = System.EventHandler<LASI.Core.Configuration.ReportEventArgs>;
    using System.Security.Principal;
    using CustomIdentity;
    using System.Collections.Immutable;
    using Microsoft.AspNet.Identity;
    using Models.User;
    using System.Collections.Concurrent;
    using System;

    [Authorize]
    public class ResultsController : Controller
    {
        public ResultsController(UserManager<ApplicationUser> userManager, IInputDocumentProvider<UserDocument> documentStore)
        {
            Phrase.VerboseOutput = true;
            this.documentStore = documentStore;
            this.userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            TrackedJobs.Clear();
            CurrentOperation = string.Empty;
            PercentComplete = 0;
            return await Results();
        }
        [HttpGet("Results/Single/{documentId}")]
        public async Task<PartialViewResult> Single(string documentId)
        {
            //var docResult =  
            var doc = documentStore.GetUserDocumentById(User.Identity.GetUserId(), documentId);
            var results = await LoadResultDocument(doc);
            return PartialView("_Single", results.First());
        }

        public async Task<ViewResult> Results()
        {
            var userDocuments = GetAllUserDocuments();
            var resultModels = await LoadResultDocument(userDocuments);

            ViewBag.Charts = resultModels.DistinctBy(chart => chart.Title).ToDictionary(chart => chart.Title, chart => chart.ChartData);
            ViewBag.Title = "Results";
            return await Task.FromResult(View(new DocumentSetModel(resultModels)));

        }
        private async Task<IEnumerable<DocumentModel>> LoadResultDocument(params UserDocument[] userDocuments)
        {
            var documents = from document in (await ProcessUserDocuments(userDocuments))
                            let topResults = NaiveTopResultSelector.GetTopResultsByEntity(document).Take(ChartItemLimit)
                            let chartData = from chartResult in topResults
                                            select new object[] { chartResult.First, chartResult.Second }
                            select new { Document = document, Title = document.Title, Rows = Newtonsoft.Json.Linq.JArray.FromObject(chartData) };

            return documents.Select(e => new DocumentModel(e.Document, e.Rows));
        }
        private UserDocument[] GetAllUserDocuments()
        {

            var userId = Context.User.Identity.GetUserId();

            var files = documentStore.GetAllUserDocuments(userId).ToArray();
            return files;
        }

        private async Task<IEnumerable<Document>> ProcessUserDocuments(params UserDocument[] userDocuments)
        {
            var workItems = CreateWorkItemsForProcessingOperations(userDocuments);
            CurrentUser.ActiceWorkItems = CurrentUser.ActiceWorkItems.Concat(workItems);
            var results = new ConcurrentBag<Document>();
            var tasks = (from userDocument in userDocuments
                         join workItem in workItems
                         on userDocument._id.ToString() equals workItem.Id
                         select new
                         {
                             WorkItem = workItem,
                             Document = userDocument
                         })
             .Select(documentWithWorkItem => new Task(() =>
              {
                  var analyzer = new AnalysisOrchestrator(documentWithWorkItem.Document);
                  var workItem = documentWithWorkItem.WorkItem;
                  analyzer.ProgressChanged += CreateWorkItemUpdateEventHandler(workItem);
                  var processedDocument = analyzer.ProcessAsync().Result.First();
                  UpdateWorkItemToComplete(workItem);
                  results.Add(processedDocument);
              })).ToList();
            tasks.ForEach(t => t.Start());
            await Task.WhenAll(tasks);
            processedDocuments = processedDocuments.Union(results);
            return results;
        }
        #region ProcessUserDocuments Helpers

        private static UpdateEventHandler CreateWorkItemUpdateEventHandler(UserWorkItem workItem) => (sender, e) =>
        {
            workItem.PercentComplete += e.PercentWorkRepresented;
            workItem.StatusMessage = e.Message;
            workItem.State = TaskState.Ongoing;
        };

        private static void UpdateWorkItemToComplete(UserWorkItem workItem)
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
        private IEnumerable<UserWorkItem> CreateWorkItemsForProcessingOperations(UserDocument[] userDocuments) =>
            userDocuments.Select(d => new UserWorkItem
            {
                Id = d._id.ToString(),
                Name = d.Name,
                State = TaskState.Pending,
                StatusMessage = "Pending",
                PercentComplete = 0
            });

        #endregion


        // These properties should be removed and replaced with a better solution to sharing
        // progress This was a hack to initially test the functionality
        public static string CurrentOperation { get; private set; }

        public static double PercentComplete { get; internal set; }

        public static JobStatusMap TrackedJobs { get; } = new JobStatusMap();
        private string ServerPath => System.IO.Directory.GetParent(HostingEnvironment.WebRoot).FullName;
        [Activate]
        private Microsoft.AspNet.Hosting.IHostingEnvironment HostingEnvironment { get; set; }
        private string UserDocumentsDirectory => Path.Combine(ServerPath, "App_Data", "SourceFiles");
        ApplicationUser CurrentUser => CurrentUserField.Value;


        private const int ChartItemLimit = 5;

        private static IImmutableSet<Document> processedDocuments =
            ImmutableHashSet.Create(ComparerFactory.Create<Document>((dx, dy) => dx.Title == dy.Title, d => d.Title.GetHashCode()));


        private readonly IInputDocumentProvider<UserDocument> documentStore;
        private readonly UserManager<ApplicationUser> userManager;

        private Lazy<ApplicationUser> CurrentUserField => new Lazy<ApplicationUser>(() => userManager.FindByIdAsync(Context.User.Identity.GetUserId()).Result);


    }
}
