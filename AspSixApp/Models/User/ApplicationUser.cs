using System;
using System.Collections.Generic;
using System.Linq;
using AspSixApp.Models.User;
using Microsoft.AspNet.Identity;
using static System.Linq.Enumerable;
namespace AspSixApp.Models
{
    // Add profile data for application users by adding properties to the IndividualUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<UserProject> Projects { get; set; } = Empty<UserProject>();
        public IEnumerable<UserDocument> Documents { get; set; } = Empty<UserDocument>();
        public IEnumerable<Organization> Organizations { get; set; } = Empty<Organization>();
        public IEnumerable<UserWorkItem> ActiceWorkItems { get; set; } = Empty<UserWorkItem>();
    }
}