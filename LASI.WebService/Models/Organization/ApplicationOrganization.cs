using System.Collections.Generic;
namespace LASI.WebService.Models.Organization
{
    using static System.Linq.Enumerable;
    public class ApplicationOrganization : IProject
    {
        public System.Guid _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<UserDocument> Documents => Empty<UserDocument>();
    }
}
