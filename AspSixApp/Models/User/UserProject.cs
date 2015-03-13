using System.Collections.Generic;
using System.Linq;

namespace AspSixApp.Models
{
    public class UserProject
    {
        MongoDB.Bson.ObjectId _id { get; set; }
        public IEnumerable<UserDocument> SourceTexts { get; set; } = Enumerable.Empty<UserDocument>();
    }
}