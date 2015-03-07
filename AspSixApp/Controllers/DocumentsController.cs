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
        private readonly IInputDocumentStore<UserDocument> documentStore;

        public DocumentsController(IInputDocumentStore<UserDocument> documentStore)
        {
            this.documentStore = documentStore;
        }
        [HttpPost]
        public async Task<HttpResponse> Upload()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                throw new ArgumentException("No files were received");
            }
            if (!files.All(IsValidContentType))
            {
                throw new UnsupportedFileTypeAddedException(
                     $@"One or more of your files was in an incorrect format.
                    The accepted formats are {string.Join(", ", FileManager.AcceptedFileFormats)}"
                 );
            }
            else
            {
                foreach (var file in files)
                {
                    var ownerId = Context.User.Identity.GetUserId();
                    var fileName = GetFileName(file);
                    var textContent = await ProcessFileContents(file);
                    this.documentStore.AddUserInputDocument(ownerId, new UserDocument
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
            await SaveFileAsync(file, fileName);
            var fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var fileTypeHandler = new ExtensionWrapperMap(unsupported => { throw new UnsupportedFileTypeAddedException(fileExtension); });
            var typed = fileTypeHandler[fileExtension](fileName);
            return await typed.GetTextAsync();
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
            var pdfFile = new PdfFile(fileName);
            return await pdfFile.GetTextAsync();
        }

        private async Task<string> ProcessWordDocument(IFormFile file, string fileName)
        {
            await SaveFileAsync(file, fileName);
            var docx = new DocXFile(fileName);
            return await docx.GetTextAsync();
        }

        private async Task<string> HandleLegacyDocument(IFormFile file, string fileName)
        {
            await SaveFileAsync(file, fileName);
            var docFile = new DocFile(fileName);
            return await docFile.GetTextAsync();
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
                "text/plain", // generally corresponds to .txt
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // generally corresponds to .docx
                "application/msword", // generally corresponds to .doc
                "application/pdf" // generally corresponds to .pdf
            }.Contains(arg.ContentType, StringComparer.OrdinalIgnoreCase);
        }
    }
}
