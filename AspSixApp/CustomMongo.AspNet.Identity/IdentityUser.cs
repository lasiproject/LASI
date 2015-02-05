using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MongoDB.AspNet.Identity
{
    /// <summary>
    /// Class IdentityUser.
    /// </summary>
    public class IdentityUser : IdentityUser<string>
    {

        /// <summary>
        /// Unique key for the user
        /// </summary>
        /// <value>The identifier.</value>
        /// <returns>The unique key for the user</returns>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string Id { get; set; }

        public override ICollection<IdentityUserRole<string>> Roles => roles;
        private readonly ISet<IdentityUserRole<string>> roles = new HashSet<IdentityUserRole<string>>();

        //      /// <summary>
        //      /// Initializes a new instance of the <see cref="IdentityUser"/> class.
        //      /// </summary>
        //public IdentityUser() {
        //          this.Claims = new List<IdentityUserClaim>();
        //          this.Roles = new List<string>();
        //          this.Logins = new List<UserLoginInfo>();
        //      }

        //      /// <summary>
        //      /// Initializes a new instance of the <see cref="IdentityUser"/> class.
        //      /// </summary>
        //      /// <param name="userName">Name of the user.</param>
        //public IdentityUser(string userName) : this() {
        //          this.UserName = userName;
        //      }
    }
    public class IdentityUserRole : IdentityUserRole<string> { }
    /// <summary>
    /// Class IdentityUserClaim.
    /// </summary>
	public class IdentityUserClaim : IdentityUserClaim<string>
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public override int Id { get; set; }
    }
}
