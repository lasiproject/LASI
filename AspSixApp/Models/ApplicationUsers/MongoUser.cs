using System;
using MongoDB.Bson;

namespace AspSixApp.Models.ApplicationUsers
{
    public class MongoUser : WebAppIdentityUser<ObjectId>
    {

        public MongoUser() { }
        public MongoUser(string userName) : base(userName) { }
    }
}