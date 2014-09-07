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

namespace LASI.WebApp.Models
{
    public class AccountModel
    {
        [Required]
        [NameField]
        [StringLength(maximumLength: 200, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [NameField]
        [StringLength(maximumLength: 200, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 1)]
        public string LastName { get; set; }

        [NameField]
        [MinLength(2, ErrorMessage = "Organization moniker must contain at least two characters")]
        public string Organization { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = LENGTH_INVALID_MESSAGE, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        #region Validation Messages

        private const string LENGTH_INVALID_MESSAGE = "Must be between 1 and 200 characters";

        #endregion


        #region Custom Validation Attributes

        private class NameFieldAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) {
                var text = value as string ?? string.Empty;
                if (text == string.Empty) { return true; }
                if ((var index = text.IndexOfAny(illegalCharacters)) > -1) {
                    ErrorMessage = string.Format("Field may not contain the character {0}", text[index]);
                }
                return false;
            }
            private static readonly char[] illegalCharacters = { ' ', '\t', '\n', '\r', '!', '?', '.', ';', ':' };
        }

        #endregion
    }
}

