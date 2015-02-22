using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using LASI.Utilities;
using System.Threading.Tasks;
using System;

//// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly string uploadDirRelativePath;

        public DocumentsController()
        {
            this.uploadDirRelativePath = @"\App_Data\SourceFiles";
        }
        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            await ReadRawRequest(Request.Body);
            if (!ModelState.IsValid)
            {
                throw new InvalidDataException("Invalid Model State");
            }
            //foreach (var file in files.WithIndex())
            //{
            //    await file.First.SaveAsync(Path.Combine(serverPath, $"testfile{file.Second}.txt"));
            //}
            return await Task.FromResult(View());
        }

        private async Task ReadRawRequest(Stream body)
        {
            using (var reader = new StreamReader(body))
            {
                var rawData = await reader.ReadToEndAsync();
                var lines = rawData.Split('\r', '\n');

                var fileName = lines
                    .Where(line => line.Contains("filename="))
                    .Select(line =>
                    {
                        var startIndex = line.IndexOf("filename=");
                        var endIndex = line.LastIndexOf('"');
                        var name = line.Substring(startIndex + 10, endIndex - (startIndex + 10));
                        return name.Substring(name.LastIndexOfAny(new[] { '\\', '/' }) + 1);
                    })
                .First();
                var documentLines = lines
                    .SkipWhile(line => !line.StartsWith("Content-Type"))
                    .Skip(1)
                    .Reverse()
                    .SkipWhile(line => !line.StartsWith("Content-Disposition:"))
                    .Skip(2)
                    .Reverse();
                var saveAs = Path.Combine(@"C:\Users\Aluan\Documents\GitHub\LASI\AspSixApp\App_Data\SourceFiles", fileName);
                await SaveFileAsync(documentLines, saveAs);

                await Response.WriteAsync(rawData);
            }
        }

        private async Task SaveFileAsync(IEnumerable<string> lines, string saveAs)
        {
            var saveAsFullName = saveAs;
            if (System.IO.File.Exists(saveAs))
            {
                saveAsFullName = saveAs.Substring(0, saveAs.LastIndexOf('.') - 1) + "1" + saveAs.Substring(saveAs.LastIndexOf('.'));
            }
            using (var writer = new StreamWriter(saveAsFullName))
            {
                foreach (var line in lines)
                {
                    await writer.WriteLineAsync(line);
                }
            }
        }
    }
}
