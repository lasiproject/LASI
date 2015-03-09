using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;

namespace AspSixApp.Models
{
    public class UserDocument : LASI.Content.IRawTextSource
    {
        public ObjectId _id { get; set; }

        public string OwnerId { get; set; }

        public async Task<string> GetTextAsync() => await Task.FromResult(Content);
        public string Name { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string Content { get; set; }
        public string SourceName => Name;
        public string GetText() => Content;
    }
}