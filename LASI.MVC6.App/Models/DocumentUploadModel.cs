using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace LASI.MVC6.App.Models
{
    public class DocumentUploadModel
    {
        public const string ALLOWED_EXTENSIONS = ".txt, .doc, .docx, .pdf";

        public string Message { get; set; }

        public double Percentage { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = ALLOWED_EXTENSIONS)]
        public string FileName { get; set; }

        [DataType(DataType.Upload)]
        public string UploadTarget { get; set; }

    }
}