using System.Collections.Generic;
using System.Linq;

namespace LASI.WebApp.Models
{
    public class Project
    {
        MongoDB.Bson.ObjectId _id { get; set; }
        public IEnumerable<UserDocument> SourceTexts { get; set; } = Enumerable.Empty<UserDocument>();
    }
}