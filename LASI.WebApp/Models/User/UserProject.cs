using System.Collections.Generic;
using MongoDB.Bson;

namespace LASI.WebApp.Models
{
    using static System.Linq.Enumerable;

    public class UserProject : IProject
    {
        //[MongoDB.Bson.Serialization.Attributes.BsonConstructor("_id", "documents")]
        public UserProject(ObjectId id, IEnumerable<UserDocument> documents)
        {
            this._id = id;
            Documents = Documents ?? documents;
        }
        public ObjectId _id { get; }
        public IEnumerable<UserDocument> Documents { get; } = Empty<UserDocument>();
    }
}