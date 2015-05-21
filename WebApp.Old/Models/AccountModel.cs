using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Newtonsoft.Json;
using MongoDB.Bson;
using LASI;
using LASI.Utilities;

using System.Linq;

namespace LASI.WebApp.Old.Models
{
    public class AccountModel : IAccountModel
    {
        public ObjectId _id { get; set; }
        [Required]
        [NameField]
        [StringLength(maximumLength: 200, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 1)]
        [Display(Name = "First Name", ShortName = "First")]
        public string FirstName { get; set; }

        [Required]
        [NameField]
        [StringLength(maximumLength: 200, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 1)]
        [Display(Name = "Last Name", ShortName = "Last")]
        public string LastName { get; set; }

        [NameField]
        [MinLength(2, ErrorMessage = "Organization moniker must contain at least two characters")]
        [Display(Name = "Organization", ShortName = "Org")]
        public string Organization { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address", ShortName = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 4)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 4)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        #region Validation Messages

        private const string LENGTH_INVALID_MESSAGE = "Must be between 1 and 200 characters";

        #endregion

        #region Custom Validation Attributes

        private class NameFieldAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) {
                var text = value as string ?? string.Empty;
                if (text.Length < 1) { return false; }
                var index = text.IndexOfAny(illegalCharacters);
                if (index > -1) {
                    ErrorMessage = $"Field may not contain the character {text[index]}";
                    return false;
                }
                return true;
            }
            private static readonly char[] illegalCharacters = { ' ', '\t', '\n', '\r', '!', '?', '.', ';', ':' };
        }

        #endregion
    }
}

