using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LASI.WebService.Controllers
{
    public class StatusController : Controller
    {
        //
        // GET: /JobStatus/jobId
        public ContentResult GetJobStatus(string jobId) {

            var json = nodeJobs[jobId].ToJson();
            return new ContentResult { Content = json, ContentType = "application/json" };
        }


        /// <summary>
        /// Hypothetical data structure representing jobs on a worker node.
        /// </summary>
        static IDictionary<string, Job> nodeJobs = new Dictionary<string, Job>();




    }

    public class Job
    {

        public Job(string currentOperation, double percentComplete) {
            JobId = "Job" + idGenerator++;
            CurrentOperation = currentOperation;
            PercentComplete = percentComplete;
        }
        public string JobId { get; private set; }
        public string CurrentOperation { get; private set; }
        public double PercentComplete { get { return Math.Round(percentComplete, 1); } private set { percentComplete = value; } }
        private double percentComplete;
        [JsonIgnore]
        private static int idGenerator = 0;

        public string ToJson() { return JsonConvert.SerializeObject(this); }
    }
}