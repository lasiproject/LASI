using System.ComponentModel.DataAnnotations;

namespace LASI.WebService.Data.Models
{
    public class UploadDocument : IDeletable
    {
        public int Id { get; internal set; }


        public ApplicationUser User { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string FileName { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = ".txt, .doc, .docx, .pdf")]
        public string Content { get; set; }

        public bool Deleted { get; set; }
    }
}
