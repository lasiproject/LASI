using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/contextmenu")]
    public class ContextMenuController : ApiController
    {
        public string Get(PhraseModel model) => model.ContextMenuJson;

        // GET api/<controller>/5
        public string Get(int id) => "value";
    }
}