using System;
using System.Collections.Generic;

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

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Account Account { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Rating> Rating { get; set; }
    }
}
