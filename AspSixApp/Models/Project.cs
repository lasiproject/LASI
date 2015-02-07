using System;
using System.Collections.Generic;

namespace AspSixApp.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public IEnumerable<UserDocument> SourceTexts { get; set; }
    }
}