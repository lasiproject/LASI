using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LASI.WebApp.Models
{
    public class MuliFileUploadModel
    {
        public IEnumerable<FileUploadModel> UploadModels { get; set; }
        public int Count { get { return UploadModels.Count(); } }
    }
}