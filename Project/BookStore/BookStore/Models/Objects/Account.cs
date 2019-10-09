using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Objects
{
    public partial class Account
    {
        const string RegExInvalidCharacters = @"[^&<>\""'/]*$";

        public int Id { get; set; }

        [Required(ErrorMessage = "Username must not be blank!")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password must not be blank!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string Password { get; set; }

        public string Salt { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email must not be blank!")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string Email { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string Cookie { get; set; }

        public User IdNavigation { get; set; }
    }
}
