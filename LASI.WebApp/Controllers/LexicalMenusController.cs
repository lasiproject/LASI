using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers.Controllers
{
    // TODO: Create injectable transient (or singleton) which holds all IReifiedTextual sources across a sesson and 
    // inject it into this controller and results controller.
    [Route("api/[controller]/{documentId}")]
    public class LexicalMenusController : Controller
    {
        [HttpGet("{lexicalType}/{lexicalId}")]
        IEnumerable<string> Get(string documentId, string lexicalType, string lexicalId) => new string[] { "value1", "value2" };
    }
}
