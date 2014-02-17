using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LASI.WebService.Controllers;
namespace LASI.WebService
{
    public class JsonServicesController : ApiController
    {
        // GET api/jsonservices
        public IEnumerable<string> Get() {
            return new[]{
            JsonConvert.SerializeObject(
                    new
                    {
                        HomeController.PercentComplete,
                        StatusMessage = HomeController.StatusMessage ?? ""
                    }
            )};
        }

        // GET api/jsonservices/5
        public string Get(int id) {
            return "value";
        }

        // POST api/jsonservices
        public void Post([FromBody]string value) {
        }

        // PUT api/jsonservices/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/jsonservices/5
        public void Delete(int id) {
        }
    }
}
