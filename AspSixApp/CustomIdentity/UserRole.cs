using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;

namespace AspSixApp.CustomIdentity
{
    public class UserRole : IdentityUserRole
    {
        public ObjectId _id { get; set; }
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }

        [Activate]
        private static ILookupNormalizer LookupNormalizer { get; set; }
    }
}