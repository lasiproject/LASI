using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace LASI.WebApp.Models.User
{
    public class UserModel
    {
        //[Required(AllowEmptyStrings = false)]
        //public string FirstName { get; set; }
        //[Required(AllowEmptyStrings = false)]
        //public string LastName { get; set; }
        //public string Organization { get; set; }
        //[Required(AllowEmptyStrings = false)]
        //[EmailAddress]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        //[Required(AllowEmptyStrings = false)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        public ObjectId _id { get; set; }
        public BsonDouble personID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
