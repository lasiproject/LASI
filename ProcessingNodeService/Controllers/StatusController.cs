using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using LASI;
using LASI.Core;
using LASI.Interop;

namespace ProcessingNodeService.Controllers
{
    public class StatusController : ApiController
    {

        private ResourceSample resourceSample;
        private readonly TimeSpan RESAMPLE_INTERVAL = TimeSpan.FromMilliseconds(5000);

        public StatusController() {
            resourceSample = ResourceUsageManager.GetCurrentUsage();
        }
        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage Head() {
            if (DateTime.Now - resourceSample.TimeWhenTaken > RESAMPLE_INTERVAL) {
                resourceSample = ResourceUsageManager.GetCurrentUsage();
            }
            var result = new HttpResponseMessage(HttpStatusCode.OK) {
                Content = new StringContent(JsonConvert.SerializeObject(resourceSample, Formatting.Indented, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.None
                }))
            };
            return result;
        }


        // GET api/<controller>/5
        public HttpResponseMessage Get(int id) {
            throw new NotImplementedException();
            //var status = GetStatusOfAllJobs().FirstOrDefault(s => s.JobId == id);
            //if (status != null) {
            //    return new HttpResponseMessage(HttpStatusCode.OK) {
            //        Content = new StringContent(JsonConvert.SerializeObject(status, Formatting.Indented, new JsonSerializerSettings {

            //        }))
            //    };
            //}
            //else { return null; }
        }

        // POST api/<controller>
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        public void Delete(int id) {
        }
    }
}

