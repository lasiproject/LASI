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

    }
}