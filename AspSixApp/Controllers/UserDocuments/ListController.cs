using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AspSixApp.CustomIdentity;
using AspSixApp.Models;
using AspSixApp.Models.User;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers.Controllers
{
    [Route("api/UserDocuments/[controller]")]
    public class ListController : Controller
    {
        public ListController(IInputDocumentProvider<UserDocument> documentStore)
        {
            this.documentStore = documentStore;
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> Get() => await Task.FromResult(
                from document in documentStore.GetAllUserDocuments(Context.User.Identity.GetUserId())
                let activeDoc = ActiveUserDocument.FromUserDocument(document)
                select new
                {
                    Id = activeDoc._id.ToString(),
                    Name = activeDoc.Name,
                    Progress = activeDoc.Progress
                }
            );

        private readonly IInputDocumentProvider<UserDocument> documentStore;
    }
}
