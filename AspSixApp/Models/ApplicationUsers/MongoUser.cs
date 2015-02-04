using System;
using MongoDB.Bson;

namespace AspSixApp.Models.ApplicationUsers
{
    public class MongoUser : WebAppIdentityUserBase<ObjectId>
    {
        ObjectId _id {
            get { return base.Id; }
            set { base.Id = value; }
        }
        public MongoUser() : base() { }
        public MongoUser(string userName) : base(userName) { }
    }
}