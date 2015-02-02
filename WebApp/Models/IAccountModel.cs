using MongoDB.Bson;

namespace LASI.WebApp.Models
{
    public interface IAccountModel 
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Organization { get; set; }
        string Password { get; set; }
        ObjectId _id { get; set; }
    }
}