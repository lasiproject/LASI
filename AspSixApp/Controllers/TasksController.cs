using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AspSixApp.Controllers.Helpers;
using AspSixApp.CustomIdentity;
using AspSixApp.Models;
using LASI.Content;
using LASI.Utilities;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        public TasksController(
            UserManager<ApplicationUser> userManager,
            IInputDocumentStore<UserDocument> documentStore,
            IHostingEnvironment hostingEnvironment
        )
        {
            UserManager = userManager;
            DocumentStore = documentStore;
            HostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IEnumerable<UserDocument> Get() => DocumentStore.GetAllUserDocuments(Context.User.Identity.GetUserId());

        [HttpGet("{id}")]
        public UserDocument Get(string id) => DocumentStore.GetUserDocumentById(Context.User.Identity.GetUserId(), id);


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            IEnumerable<IFormFile> files = Request.Form.Files;
            if (!files.Any())
            {
                return HttpBadRequest(new { Message = "Request must contain at least one file." });
            }
            if (files.Any(file => !file.ContentTypeIsValid()))
            {
                return HttpBadRequest($@"One or more of your files was in an incorrect format.
                    The accepted formats are {string.Join(", ", FileManager.AcceptedFileFormats)}");
            }
            return Json(await ProcessFormFilesAsync(Request.Form.Files));
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            DocumentStore.Remove(User.Identity.GetUserId(), id);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            user.Documents = user.Documents.Where(d => d._id.ToString() != id);
            DocumentStore.Remove(user.Id, id);
            await UserManager.UpdateAsync(user);
        }

        private async Task<IEnumerable<JObject>> ProcessFormFilesAsync(IFormFileCollection files)
        {
            var userId = Context.User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);
            var uploads = from file in files
                          let fileName = new FileInfo(file.ExtractFileName()).Name
                          let textContent = ExtractRawTextAsync(file).Result
                          select new UserDocument
                          {
                              Name = fileName,
                              Content = textContent,
                              UserId = userId,
                              DateUploaded = (string)(JToken)DateTime.Now
                          };
            foreach (var document in uploads)
            {
                var documentId = DocumentStore.AddUserDocument(userId, document);
                document._id = ObjectId.Parse(documentId);
                user.Documents = user.Documents.Append(document);
            }
            await UserManager.UpdateAsync(user);
            return uploads.Select(document => new JObject
            {
                ["id"] = document._id.ToString(),
                ["name"] = document.Name
            });
        }

        private async Task<string> ExtractRawTextAsync(IFormFile formFile)
        {
            var tempDirectory = Path.Combine(Directory.GetParent(HostingEnvironment.WebRoot).FullName, "App_Data", "Temp");
            this.EnsureDirectoryExists(tempDirectory);
            var fileName = formFile.ExtractFileName();
            var fullpath = Path.Combine(tempDirectory, formFile.GetHashCode() + new FileInfo(fileName).Name);
            await formFile.SaveAsAsync(fullpath);
            var extension = fileName.Substring(fileName.LastIndexOf('.'));
            var wrapped = WrapperFactory[extension](fullpath);
            return await wrapped.GetTextAsync();
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #region Properties
        private ExtensionWrapperMap WrapperFactory { get; } = new ExtensionWrapperMap();
        private IHostingEnvironment HostingEnvironment { get; }
        private UserManager<ApplicationUser> UserManager { get; }
        private IInputDocumentStore<UserDocument> DocumentStore { get; }
        #endregion
    }
}
