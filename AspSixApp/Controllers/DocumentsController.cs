using System.IO;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNet.Identity;
using AspSixApp.Models;
using System.Security.Principal;
using LASI.Utilities;
using LASI.Content;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using AspSixApp.CustomIdentity;
using Newtonsoft.Json.Linq;
using AspSixApp.Controllers.Helpers;
using Microsoft.AspNet.Hosting;

namespace AspSixApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IInputDocumentStore<UserDocument> documentStore;

        public DocumentsController(IInputDocumentStore<UserDocument> documentStore,
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnvironment)
        {
            this.documentStore = documentStore;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public async Task<HttpResponse> Upload()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                throw new ArgumentException("No files were received");
            }
            if (!files.All(file => file.ContentTypeIsValid()))
            {
                throw new UnsupportedFileTypeAddedException(
                     $@"One or more of your files was in an incorrect format.
                        The accepted formats are {string.Join(", ", FileManager.AcceptedFileFormats)}"
                 );
            }
            else
            {
                await ProcessFormFiles(files);
            }
            return Response;
        }

        private async Task ProcessFormFiles(IFormFileCollection files)
        {
            var userId = Context.User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            var uploads =
                from file in files
                let fileName = file.ExtractFileName()
                let textContent = new Lazy<string>(() => ExtractRawText(file).Result)
                select new
                {
                    ContentType = $"{file.ContentType}\n{file.ContentDisposition}\n{file.Length}",
                    Document = new UserDocument
                    {
                        Name = fileName,
                        Content = textContent.Value,
                        UserId = userId,
                        DateUploaded = (string)(JToken)DateTime.Now
                    }
                };
            uploads.ForEach(async file =>
            {
                this.documentStore.AddUserDocument(userId, file.Document);
                await Response.WriteAsync(file.ContentType);
            });
            user.Documents = user.Documents.Concat(uploads.Select(upload => upload.Document));

            await this.userManager.UpdateAsync(user);
        }
        private async Task<string> ExtractRawText(IFormFile formFile)
        {
            var name = formFile.ExtractFileName();
            var fullpath = Path.Combine(Directory.GetParent(
                hostingEnvironment.WebRoot).FullName,
                "App_Data",
                "Temp",
                formFile.GetHashCode() + name
            ); await formFile.SaveAsAsync(fullpath);
            var extension = name.Substring(name.LastIndexOf('.'));
            var wrapped = WrapperFactory[extension](fullpath);
            return await wrapped.GetTextAsync();
        }
        private ExtensionWrapperMap WrapperFactory { get; } = new ExtensionWrapperMap();

        private readonly IHostingEnvironment hostingEnvironment;

        private readonly UserManager<ApplicationUser> userManager;
    }
}
