using System;
using System.Linq;
using LASI.Utilities.Validation;
using LASI.Utilities;

namespace AspSixApp.Models.ApplicationUserRoles
{
    public struct RoleKind : IEquatable<RoleKind>
    {
        private RoleKind(string roleName) {
            Validator.ThrowIfNotOneOf(RoleNames, roleName, nameof(roleName), StringComparer.OrdinalIgnoreCase);
            Name = roleName;
        }
        private string Name { get; }

        private static readonly string[] RoleNames = { "Standard", "Professional", "Administrator" };

        public override string ToString() => Name;
        public bool Equals(RoleKind other) => Name[0].EqualsIgnoreCase(Name[0]);
        public override bool Equals(object obj) => obj is RoleKind && Equals((RoleKind)obj);
        public override int GetHashCode() => Name[0];

        public static bool operator ==(RoleKind left, RoleKind right) => left.Equals(right);
        public static bool operator !=(RoleKind left, RoleKind right) => !(left == right);

    }
}