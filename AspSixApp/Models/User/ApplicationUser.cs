using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace AspSixApp.Models
{
    // Add profile data for application users by adding properties to the IndividualUser class
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<UserProject> Projects { get; set; } = Enumerable.Empty<UserProject>();
        public IEnumerable<UserDocument> Documents { get; set; } = Enumerable.Empty<UserDocument>();
        public IEnumerable<Organization> Organizations { get; set; } = Enumerable.Empty<Organization>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}