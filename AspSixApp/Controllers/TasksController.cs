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
    [Route("api/[controller]")]
    public class TasksController : Controller
    {

        // GET: api/values

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
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
        private async Task<IEnumerable<JObject>> ProcessFormFilesAsync(IFormFileCollection files)
        {
            var userId = Context.User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);
            var uploads = from file in files
                          let fileName = new FileInfo(file.ExtractFileName()).Name
                          let textContent = ExtractRawTextAsync(file).Result
                          select new
                          {
                              ContentType = $"{file.ContentType}\n{file.ContentDisposition}\n{file.Length}",
                              Document = new UserDocument
                              {
                                  Name = fileName,
                                  Content = textContent,
                                  UserId = userId,
                                  DateUploaded = (string)(JToken)DateTime.Now
                              }
                          };
            foreach (var upload in uploads)
            {
                var documentId = DocumentStore.AddUserDocument(userId, upload.Document);
                upload.Document._id = ObjectId.Parse(documentId);
                user.Documents = user.Documents.Append(upload.Document);
            }
            await UserManager.UpdateAsync(user);
            return uploads.Select(upload => new JObject
            {
                ["id"] = upload.Document._id.ToString(),
                ["name"] = upload.Document.Name
            });
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            DocumentStore.Remove(User.Identity.GetUserId(), id);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            user.Documents = user.Documents.Where(d => d._id.ToString() != id);
            DocumentStore.Remove(user.Id, id);
            await UserManager.UpdateAsync(user);
        }
        private async Task<string> ExtractRawTextAsync(IFormFile formFile)
        {
            var name = formFile.ExtractFileName();
            var fullpath = Path.Combine(
                Directory.GetParent(HostingEnvironment.WebRoot).FullName,
                "App_Data",
                "Temp",
                formFile.GetHashCode() + new FileInfo(name).Name
            );
            await formFile.SaveAsAsync(fullpath);
            var extension = name.Substring(name.LastIndexOf('.'));
            var wrapped = WrapperFactory[extension](fullpath);
            return await wrapped.GetTextAsync();
        }
        private ExtensionWrapperMap WrapperFactory { get; } = new ExtensionWrapperMap();

        [Activate]
        private IHostingEnvironment HostingEnvironment { get; set; }
        [Activate]
        private UserManager<ApplicationUser> UserManager { get; set; }
        [Activate]
        private IInputDocumentStore<UserDocument> DocumentStore { get; set; }


    }
}
