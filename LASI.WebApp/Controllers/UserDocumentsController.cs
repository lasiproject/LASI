using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.WebApp.Controllers.Helpers;
using LASI.WebApp.CustomIdentity;
using LASI.WebApp.Models;
using LASI.Content;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Authorization;
using LASI.Utilities;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserDocumentsController : Controller
    {
        public UserDocumentsController(
            UserManager<ApplicationUser> userManager,
            IDocumentProvider<UserDocument> documentStore,
            IHostingEnvironment hostingEnvironment
        )
        {
            UserManager = userManager;
            DocumentStore = documentStore;
            HostingEnvironment = hostingEnvironment;
        }


        [HttpGet(Order = 1)]
        public IEnumerable<UserDocument> Get() => DocumentStore.GetAllForUser(Context.User.GetUserId());

        [HttpGet("{id}", Order = 2)]
        public UserDocument Get(string documentId) => DocumentStore.GetByIds(Context.User.GetUserId(), documentId);

        [HttpPost]
        public IActionResult Post()
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
            return Json(ProcessFormFiles(Request.Form.Files));
        }

        [HttpDelete("{id}")]
        public void Delete(string documentId)
        {
            var userId = User.GetUserId();
            //var user = await this.UserManager.FindByIdAsync(userId);
            var userDocument = DocumentStore.GetByIds(userId, documentId);
            DocumentStore.RemoveByIds(User.GetUserId(), documentId);
            //user.Documents = user.Documents.Where(d => d._id.ToString() != documentId);
            DocumentStore.RemoveByIds(userId, documentId);
            //await UserManager.UpdateAsync(user);
            // TODO: Clean this up and make it follow a better pattern. Right now it is a hack.
            UpdateResultsControllerCache(userDocument);
        }

        private static void UpdateResultsControllerCache(UserDocument userDocument)
        {
            ResultsController.ProcessedDocuments = ResultsController.ProcessedDocuments.Except(ResultsController.ProcessedDocuments.Where(d => d.Name == userDocument.Name));
        }

        private async Task<IEnumerable<dynamic>> ProcessFormFiles(IFormFileCollection files)
        {
            var userId = Context.User.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);
            var uploads = from file in files
                          let fileName = new FileInfo(file.ExtractFileName()).Name
                          let textContent = ExtractRawTextAsync(file).Result
                          select new UserDocument
                          {
                              _id = ObjectId.GenerateNewId(),
                              Name = fileName,
                              Content = textContent,
                              UserId = userId,
                              DateUploaded = (string)(JToken)DateTime.Now
                          };
            foreach (var upload in uploads)
            {
                var documentId = DocumentStore.AddForUser(upload.UserId, upload);
                upload._id = ObjectId.Parse(documentId);
                user.Documents = user.Documents.Append(upload);
                DocumentStore.AddForUser(userId, upload);
            }

            await UserManager.UpdateAsync(user);
            return uploads.Select(document => new
            {
                Id = document._id.ToString(),
                Name = document.Name
            });
        }

        private async Task<string> ExtractRawTextAsync(IFormFile formFile)
        {
            var tempDirectory = Path.Combine(Directory.GetParent(HostingEnvironment.WebRootPath).FullName, "App_Data", "Temp");
            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }
            var fileName = formFile.ExtractFileName();
            var fullpath = Path.Combine(tempDirectory, formFile.GetHashCode() + new FileInfo(fileName).Name);
            await formFile.SaveAsAsync(fullpath);
            var extension = fileName.Substring(fileName.LastIndexOf('.'));
            var wrapped = WrapperFactory[extension](fullpath);
            return await wrapped.GetTextAsync();
        }

        #region Properties
        private ExtensionWrapperMap WrapperFactory { get; } = new ExtensionWrapperMap();
        private IHostingEnvironment HostingEnvironment { get; }
        //private UserManager<ApplicationUser> UserManager { get; }
        private IDocumentProvider<UserDocument> DocumentStore { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        #endregion
    }
}
