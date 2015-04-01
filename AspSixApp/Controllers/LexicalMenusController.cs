using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers.Controllers
{
    // TODO: Create injectable transient (or singleton) which holds all IReifiedTextual sources across a sesson and 
    // inject it into this controller and results controller.
    [Route("api/[controller]/{documentId}")]
    public class LexicalMenusController : Controller
    {
        [HttpGet("{lexicalType}/{lexicalId}")]
        public IEnumerable<string> Get(string documentId, string lexicalType, string lexicalId)
        {
            return new string[] { "value1", "value2" };
        }
    }
}
