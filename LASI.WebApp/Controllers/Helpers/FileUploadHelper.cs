﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.Content;
using LASI.Utilities;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace LASI.WebApp.Controllers.Helpers
{
    public static class FileUploadHelper
    {
        public static bool IsAcceptedContentType(this IFormFile formFile) => AcceptedContentTypes.Contains(formFile.ContentType);

        public static string ExtractFileName(this IFormFile formFile) => formFile.ContentDisposition.SplitRemoveEmpty(';')
            .Select(s => s.Trim())
            .First(p => p.StartsWith("filename", StringComparison.OrdinalIgnoreCase))
            .Substring(9).Trim('\"');


        private static readonly ISet<string> AcceptedContentTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "text/plain", // generally corresponds to .txt
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // generally corresponds to .docx
            "application/msword", // generally corresponds to .doc
            "application/pdf" // generally corresponds to .pdf
        };
    }
}