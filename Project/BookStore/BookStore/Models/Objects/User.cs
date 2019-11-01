using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Objects
{
    public partial class User
    {
        const string RegExInvalidCharacters = @"[^&<>\""'/]*$";

        public User()
        {
            Comment = new List<Comment>();
            Order = new List<Order>();
            Rating = new List<Rating>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Fullname must not be blank!")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender must not be blank!")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Phone Number must not be blank!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[0-9 ]{10,15}",
            ErrorMessage = "Invalid Characters")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address must not be blank!")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string Address { get; set; }

        public Account Account { get; set; }
        public Cart Cart { get; set; }
        public List<Comment> Comment { get; set; }
        public List<Order> Order { get; set; }
        public List<Rating> Rating { get; set; }
    }
}
