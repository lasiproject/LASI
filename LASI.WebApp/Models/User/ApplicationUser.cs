using System;
using System.Collections.Generic;
using System.Linq;
using LASI.WebApp.Models.User;

namespace LASI.WebApp.Models
{
    using System.Security.Claims;
    using System.Security.Principal;
    using LASI.WebApp.Persistence;
    using Microsoft.AspNet.Identity;
    using static Enumerable;
    using ObjectId = MongoDB.Bson.ObjectId;
    using BsonIgnoreAttribute = MongoDB.Bson.Serialization.Attributes.BsonIgnoreAttribute;

    /// <summary>
    /// The Application user represents an individual user of the app.
    /// </summary>
    ///<remarks> 
    /// Add profile data for application users by adding properties to the IndividualUser class 
    /// </remarks>
    public class ApplicationUser
    {
        /// <summary>
        /// Gets or sets the user's <see cref="ObjectId"/>.
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public virtual ObjectId _id { get; set; }
        /// <summary>
        /// Gets a string representation of the <see cref="_id"/> property.
        /// </summary>
        /// <remarks>
        /// This property exists to provide convenient, name normalized access to a string representation of the <see cref="_id"/> property.
        /// </remarks>
        [BsonIgnore]
        public string Id => _id.ToString();
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public virtual string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public virtual string LastName { get; set; }
        /// <summary>
        /// Gets or sets the user's UserName name.
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public virtual string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets a hash of the user's password.
        /// </summary>
        public virtual string PasswordHash { get; set; }
        /// <summary>
        /// Gets or sets whether the user authenticates via two factor authentication.
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }
        /// <summary>
        /// Gets or sets the concurrency stamp of the user.
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }
        /// <summary>
        /// Gets or sets the number of failed login attempts of the user.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }
        /// <summary>
        /// Gets or sets the user's normalized UserName.
        /// </summary>
        /// <seealso cref="UserName"/>
        public virtual string NormalizedUserName { get; set; }
        /// <summary>
        /// Gets or sets whether the user's email has been confirmed.
        /// </summary>
        /// <seealso cref="Email"/>
        public virtual bool EmailConfirmed { get; set; }
        /// <summary>
        /// Gets or sets whether the user's phone number has been confirmed.
        /// </summary>
        /// <seealso cref="PhoneNumber"/>
        public virtual bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// Gets or sets whether lockout is enabled for the user.
        /// </summary>
        /// <seealso cref="LockoutEnd"/>
        public virtual bool LockoutEnabled { get; set; }
        /// <summary>
        /// Gets or sets the date time offset from the current time when the user will be able to attempt to login again.
        /// </summary>
        /// <seealso cref="LockoutEnabled"/>
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        /// <summary>
        /// Gets or sets the user's normalized email address.
        /// </summary>
        /// <seealso cref="Email"/>
        public virtual string NormalizedEmail { get; set; }
        /// <summary>
        /// Gets or sets the user's security stamp.
        /// </summary>
        public virtual string SecurityStamp { get; set; }
        /// <summary>
        /// Gets or sets the user's <see cref="UserProject"/>s.
        /// </summary>
        public virtual IEnumerable<UserProject> Projects { get; set; } = Empty<UserProject>();
        /// <summary>
        /// Gets or sets the user's <see cref="UserDocument"/>s.
        /// </summary>
        public virtual IEnumerable<UserDocument> Documents { get; set; } = Empty<UserDocument>();
        /// <summary>
        /// Gets or sets the <see cref="Organization"/>s to which the user belongs.
        /// </summary>
        public virtual IEnumerable<Organization.ApplicationOrganization> Organizations { get; set; } = Empty<Organization.ApplicationOrganization>();
        /// <summary>
        /// Gets or sets the user's outstanding <see cref="WorkItem"/>s.
        /// </summary>
        public virtual IEnumerable<WorkItem> ActiveWorkItems { get; set; } = Empty<WorkItem>();
        /// <summary>
        /// The collection of <see cref="UserRole"/>s to which the user belongs.
        /// </summary>
        public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();
        /// <summary>
        /// The collection of <see cref="Claim"/>s belonging to the user.
        /// </summary>
        public virtual ICollection<Claim> Claims { get; } = new List<Claim>();
        /// <summary>
        /// The collection of <see cref="UserLoginInfo"/>s belonging to the user.
        /// </summary>
        public virtual ICollection<UserLoginInfo> Logins { get; } = new List<UserLoginInfo>();
    }
}