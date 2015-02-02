using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
 using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace LASI.WebR
{
    public class JobController : ApiController
    {
        [HttpGet]
        [Route("api/jobs/{id:int?}")]
        public JToken Get(int? id = null) {
            if (id == null) {
                return JArray.FromObject(trackedJobs.Values, serializer);
            }
            var idValue = id.Value;

            if (Controllers.HomeController.PercentComplete > 99) { Controllers.HomeController.PercentComplete = 100; }
            if (trackedJobs.ContainsKey(idValue)) {
                var update = new JobStatus(Controllers.HomeController.CurrentOperation, Controllers.HomeController.PercentComplete, idValue);
                trackedJobs[idValue] = update;
                return JObject.FromObject(update, serializer);//, serializerSettings);
            } else {
                return JsonConvert.SerializeObject(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ReasonPhrase = $"No active job with Id: {idValue}"
                });
            }
        }

        [HttpPost]
        [Route("api/jobs")]
        public HttpResponseMessage Post([FromBody] JobStatus data) {
            // Ensure a unique id is used
            lock (lockon) {
                data.Id = idProvider++;
                trackedJobs[data.Id] = data;
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.Created, data, mediaTypeFormatter, "application/json");
        }
        [HttpDelete]
        [Route("api/jobs/{id:int}")]
        public void Delete(int id) {
            JobStatus job;
            var succeeded = trackedJobs.TryRemove(id, out job);
            //return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
        }
        private const string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        private static JsonSerializer serializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        System.Net.Http.Formatting.JsonMediaTypeFormatter mediaTypeFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter { SerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() } };
        private static ConcurrentDictionary<int, JobStatus> trackedJobs = Controllers.HomeController.TrackedJobs;
        private static object lockon = new object();
        private static int idProvider = 0;
    }
}