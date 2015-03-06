using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
//using LASI.Utilities;
using System.Threading.Tasks;
using System;
using Microsoft.AspNet.Identity;
using AspSixApp.Models;
using AspSixApp.CustomIdentity.MongoDb;
using System.Security.Principal;
using LASI.Utilities;
using LASI.Content;

//// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly MongoDbInputDocumentStore documentStore;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        //private readonly string uploadDirRelativePath;

        public DocumentsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MongoDbInputDocumentStore documentStore)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.documentStore = documentStore;

        }
        [HttpPost]
        public async Task<HttpResponse> Upload()
        {
            var files = Request.Form.Files;
            if (!files.All(IsValidContentType))
            {
                await Response.WriteAsync($"One or more of your documents was in an incorrect format. The accepted formats are {string.Join(", ", LASI.Content.FileManager.AcceptedFileFormats)}");
            }
            else
            {
                foreach (var file in files)
                {
                    var ownerId = Context.User.Identity.GetUserId();
                    var fileName = GetFileName(file);
                    var textContent = await ProcessFileContents(file);
                    this.documentStore.AddUserInputDocument(new UserDocument
                    {
                        Name = fileName,
                        Content = textContent,
                        OwnerId = ownerId
                    });

                    await Response.WriteAsync($"{file.ContentType}\n{file.ContentDisposition}\n{file.Length}");

                }
            }
            return Response;
        }

        private async Task<string> ProcessFileContents(IFormFile file)
        {
            var fileName = GetFileName(file);
            var fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
            switch (fileExtension.ToLower())
            {
                case "txt":
                return await ProcessPlainText(file);
                case "doc":
                return await HandleLegacyDocument(file, fileName);
                case "docx":
                return await ProcessWordDocument(file, fileName);
                case "pdf":
                return await ProcessPdfDocument(file, fileName);
                default:
                throw new UnsupportedFileTypeAddedException(fileExtension);
            }
        }

        private static async Task<string> ProcessPlainText(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task<string> ProcessPdfDocument(IFormFile file, string fileName)
        {
            await SaveFileAsync(file, fileName);
            var pdf = new PdfFile(fileName);
            var pdfConverter = new PdfToTextConverter(pdf);
            var txt = await pdfConverter.ConvertFileAsync();
            return await txt.GetTextAsync();
        }

        private async Task<string> ProcessWordDocument(IFormFile file, string fileName)
        {
            await SaveFileAsync(file, fileName);
            var docx = new DocXFile(fileName);
            var docxConverter = new DocxToTextConverter(docx);
            var txt = await docxConverter.ConvertFileAsync();
            return await txt.GetTextAsync();
        }

        private async Task<string> HandleLegacyDocument(IFormFile file, string fileName)
        {
            await SaveFileAsync(file, fileName);
            var docConverter = new DocToDocXConverter(new DocFile(fileName));
            var docx = await docConverter.ConvertFileAsync();
            var docxConverter = new DocxToTextConverter(docx);
            var txt = await docxConverter.ConvertFileAsync();
            return await txt.GetTextAsync();
        }

        private async Task SaveFileAsync(IFormFile file, string fileName)
        {
            await file.SaveAsAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LASI", fileName));
        }

        private string GetFileName(IFormFile file)
        {
            var contentDispositonProperties = file.ContentDisposition.SplitRemoveEmpty(';').Select(s => s.Trim());
            return contentDispositonProperties.First(p => p.StartsWith("filename")).Substring(9).Trim('\"');
        }

        private bool IsValidContentType(IFormFile arg)
        {
            return new[]
            {
                "text/plain",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/msword",
                "application/pdf"
            }.Contains(arg.ContentType, StringComparer.OrdinalIgnoreCase);
        }
    }
}
