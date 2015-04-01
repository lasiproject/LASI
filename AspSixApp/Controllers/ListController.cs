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
            this.DocumentStore = documentStore;
        }

        public IInputDocumentProvider<UserDocument> DocumentStore { get; }

        // GET: api/values
        [HttpGet]
        public async Task<IJEnumerable<JToken>> Get()
        {
            //throw new Exception("Always Will Failed");
            var infoList = from document in DocumentStore.GetAllUserDocuments(Context.User.Identity.GetUserId())
                           let activeDoc = ActiveUserDocument.FromUserDocument(document)
                           select new JObject
                           {
                               ["id"] = activeDoc._id.ToString(),
                               ["name"] = activeDoc.Name,
                               ["progress"] = activeDoc.Progress
                           };
            return await Task.FromResult(new JArray(infoList));
        }
    }
}
