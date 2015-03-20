using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.Content;
using LASI.Utilities;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace AspSixApp.Controllers.Helpers
{
    public static class FileUploadHelper
    {
        public static bool ContentTypeIsValid(this IFormFile formFile) => AcceptedContentTypes.Contains(formFile.ContentType);




        public static string ExtractFileName(this IFormFile formFile)
        {
            var contentDispositonProperties = formFile.ContentDisposition.SplitRemoveEmpty(';').Select(s => s.Trim());
            return contentDispositonProperties.First(p => p.StartsWith("filename")).Substring(9).Trim('\"');
        }


        private static readonly HashSet<string> AcceptedContentTypes = new[]{
                "text/plain", // generally corresponds to .txt
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // generally corresponds to .docx
                "application/msword", // generally corresponds to .doc
                "application/pdf" // generally corresponds to .pdf
        }.ToHashSet(StringComparer.OrdinalIgnoreCase);

        public static IHostingEnvironment HostingEnvironment { get; set; }
    }

}