using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace LASI.WebApp.Models
{
    public class FileUploadModel
    {
        public string Message { get; set; }
        public double Percentage { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the file extensions which will be allowed by the upload model.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> AcceptedExtensions { get; set; }
        public string UploadTarget { get; set; }
    }
}