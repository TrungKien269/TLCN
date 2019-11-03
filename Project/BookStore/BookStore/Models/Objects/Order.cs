using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Objects
{
    public partial class Order
    {
        const string RegExInvalidCharacters = @"[^&<>\""'/]*$";

        public Order()
        {
            OrderDetail = new List<OrderDetail>();
        }

        public string Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Total { get; set; }

        [Required(ErrorMessage = "Fullname must not be blank!")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone Number must not be blank!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[0-9 ]{10,15}",
            ErrorMessage = "Invalid Characters")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address must not be blank!")]
        [RegularExpression(RegExInvalidCharacters, ErrorMessage = "Invalid Characters")]
        public string Address { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
