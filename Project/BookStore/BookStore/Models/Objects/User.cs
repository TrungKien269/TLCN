using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Order = new HashSet<Order>();
            Rating = new HashSet<Rating>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [StringLength(20)]
        public string Gender { get; set; }
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }

        [InverseProperty("IdNavigation")]
        public Account Account { get; set; }
        [InverseProperty("IdNavigation")]
        public Cart Cart { get; set; }
        [InverseProperty("User")]
        public ICollection<Comment> Comment { get; set; }
        [InverseProperty("User")]
        public ICollection<Order> Order { get; set; }
        [InverseProperty("User")]
        public ICollection<Rating> Rating { get; set; }
    }
}
