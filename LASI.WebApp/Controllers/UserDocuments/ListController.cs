using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using LASI.WebApp.CustomIdentity;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers.Controllers
{
    [Route("api/UserDocuments/[controller]")]
    public class ListController : Controller
    {
        public ListController(IDocumentProvider<UserDocument> documentStore)
        {
            this.documentStore = documentStore;
        }
        [HttpGet]
        public IEnumerable<dynamic> Get() => from document in documentStore.GetAllForUser(Context.User.GetUserId())
                                             let activeDoc = ActiveUserDocument.FromUserDocument(document)
                                             let dateUploaded = (DateTime)(JToken)(document.DateUploaded)
                                             orderby dateUploaded descending
                                             select new
                                             {
                                                 Id = activeDoc._id.ToString(),
                                                 Name = activeDoc.Name,
                                                 Progress = activeDoc.Progress
                                             };

        [HttpGet("{limit}")]
        public IEnumerable<dynamic> Get(int limit) => this.Get().Take(limit);

        private readonly IDocumentProvider<UserDocument> documentStore;
    }
}
