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
using LASI;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private static IDictionary<string, dynamic> statusDictionary = new Dictionary<string, dynamic>(comparer: StringComparer.OrdinalIgnoreCase);
        private readonly string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        public ActionResult Index(string returnUrl) {
            returnUrl = string.Empty;
            ViewBag.ReturnUrl = returnUrl;

            return View(new LASI.WebApp.Models.User.UserModel());
        }
        [HttpPost]
        public async Task<ActionResult> Upload(params object[] args) {
            var SERVER_PATH = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            if (!Directory.Exists(SERVER_PATH)) {
                Directory.CreateDirectory(SERVER_PATH);
            }
            for (var i = 0; i < Request.Files.Count; ++i) {

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
            await Results();

            return RedirectToAction("Results");
        }

        private const short CHART_ITEM_MAX = 5;

        public ActionResult Progress() {
            return GetJobStatus();
        }

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
            var extensionMap = new ExtensionWrapperMap(UnsupportedFileTypeHandling.YieldNull);
            var files = Directory.EnumerateFiles(serverPath)
                .Select(file => {
                    try {
                        return extensionMap[file.SplitRemoveEmpty('.').Last()](file);
                    }
                    catch (ArgumentException) { return null; }
                })
                .Where(file => file != null);

            var analyzer = new AnalysisController(files);
            analyzer.ProgressChanged += (s, e) => {
                percentComplete += e.Increment;
                statusMessage = e.Message;
            };
            return await Task.Run(async () => await analyzer.ProcessAsync());
        }
        private static double percentComplete;


        private static string statusMessage;


        //static int timesExecuted = 0;
        [HttpGet]
        public ContentResult GetJobStatus(string jobId = "", dynamic _ = null) {

            //var t = new System.Threading.Timer(dueTime: 0, state: timesExecuted, period: 1000L, callback: (state) => { percentComplete += 0.5; statusMessage = "executed " + state + " times"; });

            var status = new
            {
                Message = statusMessage ?? "Test",
                Percent = percentComplete.ToString() + "%"
            };
            //statusDictionary[jobId] = status;
            percentComplete += 1D;
            return Content(JsonConvert.SerializeObject(status), "application/json");
        }


    }
}