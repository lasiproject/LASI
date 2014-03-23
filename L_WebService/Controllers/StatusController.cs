using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace L_WebService.Controllers
{
    public  class StatusController : ApiController
    {
        // GET api/apicontrollerbase
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/apicontrollerbase/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/apicontrollerbase
        public void Post([FromBody]string value)
        {
        }

        // PUT api/apicontrollerbase/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/apicontrollerbase/5
        public void Delete(int id)
        {
        }
    }
}
