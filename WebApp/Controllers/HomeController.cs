using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LASI.Utilities;
using LASI.ContentSystem;
using LASI.Interop;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;

namespace LASI.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        public ActionResult Index(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View(new Models.User.UserModel());
        }

        [HttpPost]
        public ActionResult Upload() {
            var SERVER_PATH = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            if (!Directory.Exists(SERVER_PATH)) {
                Directory.CreateDirectory(SERVER_PATH);
            }
            for (var i = 0;
            i < Request.Files.Count;
            ++i) {

                var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
                foreach (var remnant in from remnant in new DirectoryInfo(SERVER_PATH).EnumerateFileSystemInfos()
                                        where remnant.Name.Contains(file.FileName.SplitRemoveEmpty('\\').Last())
                                        select remnant) {
                    var dir = remnant as DirectoryInfo;
                    if (dir != null) {
                        dir.Delete(true);
                    } else {
                        remnant.Delete();
                    }
                }
                var path = Path.Combine(SERVER_PATH, file.FileName);

                file.SaveAs(path);
            }
            return RedirectToAction("Results");

        }

        private const short CHART_ITEM_MAX = 5;

        public ActionResult Progress() {
            return View();
        }
        private static HashSet<Core.DocumentStructures.Document> processedDocuments =
            new HashSet<Core.DocumentStructures.Document>(new CustomComparer<Core.DocumentStructures.Document>(
                (dx, dy) => dx.Name == dy.Name, d => d.Name.GetHashCode()));

        public async Task<ActionResult> Results() {
            var documents = await LoadResults();

            ViewData["docs"] = documents;
            ViewData["charts"] = (from document in documents
                                  let naiveTopResults = NaiveResultSelector.GetTopResultsByEntity(document).Take(CHART_ITEM_MAX)
                                  from r in naiveTopResults
                                  orderby r.Value descending
                                  group new object[] { r.Key, r.Value } by document).ToDictionary(g => g.Key, g => JsonConvert.SerializeObject(g.ToArray().Take(CHART_ITEM_MAX)));
            ViewBag.Title = "Results";
            return View();
        }

        private async Task<IEnumerable<Core.DocumentStructures.Document>> LoadResults() {
            var serverPath = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            var extensionMap = new ExtensionWrapperMap(UnsupportedFormatHandling.YieldNull);
            var files = Directory.EnumerateFiles(serverPath)
                .Select(file => {
                    try {
                        return extensionMap[file.SplitRemoveEmpty('.').Last()](file);
                    }
                    catch (ArgumentException) { return null; }
                })
                .Where(file => file != null && !processedDocuments.Any(d => d.Name == file.NameSansExt));
            var analyzer = new AnalysisOrchestrator(files);
            analyzer.ProgressChanged += (s, e) => {
                percentComplete += e.PercentWorkRepresented;
                currentOperation = e.Message;
            };
            var documents = await Task.Run(async () => await analyzer.ProcessAsync());
            percentComplete = 100;
            currentOperation = "Analysis Complete.";
            processedDocuments.UnionWith(documents);
            return processedDocuments;

        }
        private static double percentComplete;


        private static string currentOperation;

        private static IDictionary<string, dynamic> trackedJobs = new Dictionary<string, dynamic>(comparer: StringComparer.OrdinalIgnoreCase);

        //static int timesExecuted = 0;
        [HttpGet]
        public ContentResult GetJobStatus(string jobId = "") {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            if (jobId == "") {
                return Content(JsonConvert.SerializeObject(trackedJobs
                    .Select(j => new
                {
                    j.Value.Message,
                    j.Value.Percent,
                    Id = j.Key
                }).ToArray(),
                      serializerSettings));
            }
            percentComplete %= 101;

            if (percentComplete > 100) {
                percentComplete = 0;
            }
            bool extant = trackedJobs.ContainsKey(jobId);
            int id;
            var update = new JobStatus(int.TryParse(jobId, out id) ? id : -1, currentOperation, percentComplete);
            trackedJobs[jobId] = update;

            return Content(JsonConvert.SerializeObject(update, serializerSettings));
        }

    }
}