using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace LASI.WebService.Models.Login
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
