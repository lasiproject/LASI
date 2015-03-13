using MongoDB.Bson;

namespace AspSixApp.Models
{
    public class Organization
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}