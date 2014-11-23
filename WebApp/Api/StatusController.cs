using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.WebApp.Api
{
    [Route("Api/Status")]
    public class StatusController : ApiController
    {
        private static ConcurrentDictionary<int, JobStatus> trackedJobs = new ConcurrentDictionary<int, JobStatus>();
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        }; public StatusController() {
        }
        //static int timesExecuted = 0;
        [HttpGet]
        public async Task<JObject> Get(int jobId = -1) {
            if (jobId < 0) {
                return await Task.FromResult(JObject.FromObject(TrackedJobs.FirstOrDefault()));
            }
            percentComplete %= 100;

            if (percentComplete > 99) { percentComplete = 0; }
            var update = new JobStatus(jobId, status, percentComplete);
            TrackedJobs[jobId] = update;
            return await Task.FromResult(JObject.FromObject(update));
        }


        // These fields should be removed and replaced with a better solution to sharing progress
        // This was a hack to initially test the functionality
        internal static double percentComplete;
        internal static string status;

        public static ConcurrentDictionary<int, JobStatus> TrackedJobs => trackedJobs;
    }
}
