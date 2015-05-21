using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;

namespace LASI.WebApp.CustomIdentity
{
    public class UserRole : IEquatable<UserRole>
    {
        public ObjectId _id { get; set; }

        public string RoleId => _id.ToString();

        public string UserId { get; set; }

        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }

        public bool Equals(UserRole other) => RoleName?.Equals(other?.RoleName) ?? false;
        public override bool Equals(object obj) => Equals(obj as UserRole);
        public override int GetHashCode() => RoleName?.GetHashCode() ?? 0;
    }
}