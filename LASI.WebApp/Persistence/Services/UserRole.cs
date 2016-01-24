using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;

namespace LASI.WebApp.Models
{
    public class UserRole : IEquatable<UserRole>
    {
        public ObjectId _id { get; set; }

        public string RoleId => _id.ToString();

        public string UserId { get; set; }

        public string RoleName { get; set; }

        public string NormalizedRoleName { get; set; }

        public bool Equals(UserRole other) => NormalizedRoleName?.Equals(other?.NormalizedRoleName) ?? false;

        public override bool Equals(object obj) => Equals(obj as UserRole);

        public override int GetHashCode() => RoleName?.GetHashCode() ?? 0;

        public static bool operator ==(UserRole left, UserRole right) => left != null ? left.Equals(right) : right == null;

        public static bool operator !=(UserRole left, UserRole right) => !(left == right);
    }
}