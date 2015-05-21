using System.Collections.Generic;
namespace LASI.WebApp.Models.Organization
{
    using static System.Linq.Enumerable;
    public class ApplicationOrganization : IProject
    {
        public MongoDB.Bson.ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<UserDocument> Documents => Empty<UserDocument>();
    }
}
