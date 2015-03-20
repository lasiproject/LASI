using System.Threading.Tasks;

namespace AspSixApp.Models
{
    public class UserDocument : LASI.Content.IRawTextSource
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string UserId { get; set; }
        public string GetText() => Content;
        public async Task<string> GetTextAsync() => await Task.FromResult(Content);

        public string Name { get; set; }
        /// <summary>
        /// A string representation of the Date and Time when the document was uploaded. The format is the ISO JSON date format.
        /// <c>null</c> is a valid value for this property.
        /// </summary>
        public string DateUploaded { get; set; }
        public string Content { get; set; }
    }
}