using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.WebApp.Controllers.Helpers;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using LASI.Content;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Authorization;
using LASI.Utilities;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserDocumentsController : Controller
    {
        public UserDocumentsController(UserManager<ApplicationUser> userManager,
            IDocumentAccessor<UserDocument> documentStore,
            IHostingEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.documentStore = documentStore;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet(Order = 1)]
        public IEnumerable<UserDocument> Get() => documentStore.GetAllForUser(Context.User.GetUserId());

        [HttpGet("{documentId}", Order = 2)]
        public UserDocument Get(string documentId) => documentStore.GetById(Context.User.GetUserId(), documentId);

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var files = Request.Form.Files;
            if (!files.Any())
            {
                return new ObjectResult("Unable to process the request because no files were recieved. Request must contain at least one file.")
                {
                    StatusCode = HttpResponseHelper.UnprocessableEntity
                };
            }
            if (files.Any(file => !file.IsAcceptedContentType())) // If any of the files is invalid, reject them all.
            {
                return new UnsupportedMediaTypeResult();
                //    $@"One or more of your files was in an incorrect format.
                //    The accepted formats are {string.Join(", ", FileManager.AcceptedFileFormats)}"
                //);
            }
            return Json(await ProcessFormFiles(Request.Form.Files));
        }

        [HttpDelete("{documentId}")]
        public async Task Delete(string documentId)
        {
            var userId = User.GetUserId();
            var userDocument = documentStore.GetById(userId, documentId);
            documentStore.RemoveById(userId, documentId);
            await userManager.UpdateAsync(await userManager.FindByIdAsync(userId));
            // TODO: Clean this up and make it follow a better pattern. Right now it is a hack.
            UpdateResultsControllerCache(userDocument);
        }

        private static void UpdateResultsControllerCache(UserDocument userDocument)
        {
            AnalysisController.ProcessedDocuments = AnalysisController.ProcessedDocuments.Except(AnalysisController.ProcessedDocuments.Where(d => d.Name == userDocument.Name));
        }

        private async Task<IEnumerable<FileUploadedResult>> ProcessFormFiles(IFormFileCollection files)
        {
            var userId = Context.User.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            var uploads = from file in files
                          let fileName = new FileInfo(file.ExtractFileName()).Name
                          let textContent = ExtractRawTextAsync(file).Result
                          select new UserDocument
                          {
                              _id = MongoDB.Bson.ObjectId.GenerateNewId(),
                              Name = fileName,
                              Content = textContent,
                              UserId = userId,
                              DateUploaded = (string)(JToken)DateTimeOffset.Now
                          };
            foreach (var upload in uploads)
            {
                var documentId = documentStore.AddForUser(upload.UserId, upload);
                upload._id = MongoDB.Bson.ObjectId.Parse(documentId);
                user.Documents = user.Documents.Append(upload);
                documentStore.AddForUser(userId, upload);
            }

            await userManager.UpdateAsync(user);
            return uploads.Select(document => new FileUploadedResult
            {
                Id = document._id.ToString(),
                FileName = document.Name,
                Progress = 100
            });
        }
        public class FileUploadedResult
        {
            public string Id { get; set; }
            public string FileName { get; set; }
            public int Progress { get; set; }
        }
        private async Task<string> ExtractRawTextAsync(IFormFile formFile)
        {
            var tempDirectory = Path.Combine(Directory.GetParent(hostingEnvironment.WebRootPath).FullName, "App_Data", "Temp");
            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }
            var fileName = formFile.ExtractFileName();
            var fullpath = Path.Combine(tempDirectory, formFile.GetHashCode() + new FileInfo(fileName).Name);
            await formFile.SaveAsAsync(fullpath);
            var extension = fileName.Substring(fileName.LastIndexOf('.'));
            var wrapped = wrapperFactory[extension](fullpath);
            return await wrapped.LoadTextAsync();
        }

        #region Fields

        private static readonly ExtensionWrapperMap wrapperFactory = new ExtensionWrapperMap();
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IDocumentAccessor<UserDocument> documentStore;
        private readonly UserManager<ApplicationUser> userManager;

        #endregion
    }
}
