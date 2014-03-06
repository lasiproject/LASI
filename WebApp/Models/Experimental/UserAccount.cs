using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Newtonsoft.Json;
using LASI.WebService.Controllers;

namespace LASI.WebService.PersistenceLayer.Users
{
    /// <summary>
    /// This is just a stub class to help test a controller functions which take data as parameters.
    /// </summary>
    public class UserAccount
    {
        internal UserName UserName { get; set; }
        internal string EmailAddress { get; set; }
        internal string Password { get; set; }
    }
    public class UserName
    {
        internal string First { get; set; }
        internal string Last { get; set; }
        internal string FullName { get { return First + " " + Last; } }
    }
}
