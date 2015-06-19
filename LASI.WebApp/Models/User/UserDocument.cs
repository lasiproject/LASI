using System.Threading.Tasks;

namespace LASI.WebApp.Models
{
    using BsonIgnoreAttribute = MongoDB.Bson.Serialization.Attributes.BsonIgnoreAttribute;
    public class UserDocument : Content.IRawTextSource
    {
        public MongoDB.Bson.ObjectId _id { get; set; }
        [BsonIgnore]
        public string Id => _id.ToString();
        public string UserId { get; set; }
        public string LoadText() => Content;
        public async Task<string> LoadTextAsync() => await Task.FromResult(Content);
        public string Name { get; set; }
        /// <summary>
        /// A string representation of the Date and Time when the document was uploaded. The format is the ISO JSON date format.
        /// <c>null</c> is a valid value for this property.
        /// </summary>
        public string DateUploaded { get; set; }
        public string Content { get; set; }
    }
}