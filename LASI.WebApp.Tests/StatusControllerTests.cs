using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LASI.WebApp.Controllers.Tests
{
    [TestClass]
    public class StatusControllerTests
    {
        [TestMethod]
        public void GetJobStatusTest1() {
            GetJobStatusTest1Helper().Wait();
        }

        private static async Task GetJobStatusTest1Helper() {
            var target = new JobController();

            var job = new JobStatus(null, 0);
            var request = new System.Net.Http.HttpRequestMessage
            {
                Method = System.Net.Http.HttpMethod.Post,
                Content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(job), encoding: UTF8Encoding.Default, mediaType: "application/json"),
                //RequestUri = target.ControllerContext.Url.Request.RequestUri
            };
            using (var response = await new System.Net.Http.HttpClient().SendAsync(request)) {
                var json = JsonConvert.DeserializeObject<JobStatus>(await response.Content.ReadAsStringAsync());


                Assert.IsTrue(json.Percent == 0);
                Assert.IsTrue(json.Message == null);
            }
        }
    }
}
