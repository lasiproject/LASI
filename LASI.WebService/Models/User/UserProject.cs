using System.Collections.Generic;
//using MongoDB.Bson;

namespace LASI.WebService.Models
{
    using static System.Linq.Enumerable;

    public class UserProject : IProject
    {
        //[MongoDB.Bson.Serialization.Attributes.BsonConstructor("_id", "documents")]
        public UserProject(System.Guid id, IEnumerable<UserDocument> documents)
        {
            this._id = id;
            Documents = Documents ?? documents;
        }
        public System.Guid _id { get; }
        public IEnumerable<UserDocument> Documents { get; } = Empty<UserDocument>();
    }
}