using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace AspSixApp.Models
{
    // Add profile data for application users by adding properties to the IndividualUser class
    public class IndividualUser : IdentityUser
    {
        public IEnumerable<Project> Projects { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}