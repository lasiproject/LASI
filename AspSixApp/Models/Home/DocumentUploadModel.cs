using System.ComponentModel.DataAnnotations;

namespace LASI.WebApp.Models
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